namespace LineDetection.MathImageProcessing
{
    /// <summary>
    /// Applies a Gaussian filter to an image.
    /// </summary>
    public static class GaussianFilter3
    {
        private static float[] kernel = new float[1];
        private static float sigma = 1.0f;

        public static float Sigma
        {
            set
            {
                if (value == sigma)
                    return;

                if (value < 0.1f || value > 20.0f)
                    throw new ArgumentException("Gaussian filter - wrong sigma");

                sigma = value;

                CreateGaussianKernel();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        static GaussianFilter3()
        {
            CreateGaussianKernel();
        }

        /// <summary>
        /// Applies a Gaussian filter to a 1D byte array, returning a new 1D byte array.
        /// </summary>
        public static void Apply(YUVImage? image)
        {
            ArgumentNullException.ThrowIfNull(image);

            int size = image.Width * image.Height;

            // transform input to float array
            var src = image.Bytes.Select(x => (float)x).ToArray();

            // create temporary arrays, used as buffers for convolution
            var tmp = new float[size];
            var dst = new float[size];

            // convolve horizontally and vertically
            HorizontalConvolution(src, tmp, image.Width, image.Height);
            VerticalConvolution(tmp, dst, image.Width, image.Height);

            for (int i = 0; i < size; i++)
            {
                var val = dst[i];
                if (val < 0f)
                    val = 0f;
                if (val > 255f)
                    val = 255f;
                image.Bytes[i] = (byte)MathF.Round(val);
            }
        }

        /// <summary>
        /// Generates a 1D Gaussian kernel of size (2*radius + 1).
        /// radius = (int)Math.Ceiling(3 * sigma) by default.
        /// </summary>
        private static void CreateGaussianKernel()
        {
            var radius = (int)Math.Ceiling(3 * sigma);
            var size = 2 * radius + 1;

            kernel = new float[size];
            var sum = 0.0;
            var twoSigmaSq = 2.0 * sigma * sigma;

            for (int i = 0; i < size; i++)
            {
                var x = i - radius;
                var val = Math.Exp(-(x * x) / twoSigmaSq);
                kernel[i] = (float)val;
                sum += val;
            }

            for (int i = 0; i < size; i++)
            {
                kernel[i] /= (float)sum;
            }
        }

        /// <summary>
        /// Horizontal convolution
        /// </summary>
        public static void HorizontalConvolution(float[] src, float[] dst, int width, int height)
        {
            var radius = kernel.Length / 2;

            Parallel.For(0, height, row =>
            {
                var rowOffset = row * width;

                for (int col = 0; col < width; col++)
                {
                    double sum = 0.0;

                    for (int k = -radius; k <= radius; k++)
                    {
                        var c = col + k;
                        if (c < 0)
                            c = 0;
                        if (c >= width)
                            c = width - 1;

                        var pixel = src[rowOffset + c];
                        var weight = kernel[k + radius];
                        sum += pixel * weight;
                    }

                    dst[rowOffset + col] = (float)sum;
                }
            });
        }

        /// <summary>
        /// Vertical convolution
        /// </summary>
        public static void VerticalConvolution(float[] src, float[] dst, int width, int height)
        {
            var radius = kernel.Length / 2;

            Parallel.For(0, width, col =>
            {
                for (int row = 0; row < height; row++)
                {
                    double sum = 0.0;

                    for (int k = -radius; k <= radius; k++)
                    {
                        int r = row + k;
                        if (r < 0)
                            r = 0;
                        if (r >= height)
                            r = height - 1;

                        var pixel = src[r * width + col];
                        var weight = kernel[k + radius];
                        sum += pixel * weight;
                    }

                    dst[row * width + col] = (float)sum;
                }
            });
        }
    }
}
