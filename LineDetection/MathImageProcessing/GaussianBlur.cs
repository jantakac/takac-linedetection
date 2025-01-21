namespace LineDetection.MathImageProcessing
{
    /// <summary>
    /// Applies a Gaussian filter to an image.
    /// </summary>
    public static class GaussianFilter
    {
        private static float[] kernel = new float[1];

        public static double Sigma
        {
            set
            {
                if (value < 1.0 || value > 5.0)
                    throw new ArgumentException("Gaussian filter - wrong sigma");

                kernel = CreateGaussianKernel(value);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        static GaussianFilter()
        {
            // default kernel size and value
            kernel[0] = 1.0f;
        }

        /// <summary>
        /// Generates a 1D Gaussian kernel of size (2*radius + 1).
        /// radius = (int)Math.Ceiling(3 * sigma) by default.
        /// </summary>
        private static float[] CreateGaussianKernel(double sigma)
        {
            var radius = (int)Math.Ceiling(3 * sigma);
            var size = 2 * radius + 1;

            var kernel = new float[size];
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

            return kernel;
        }

        /// <summary>
        /// Applies a Gaussian filter to a 1D byte array, returning a new 1D byte array.
        /// </summary>
        /// <param name="input">Input byte array (grayscale image).</param>
        /// <param name="width">Width of the image.</param>
        /// <param name="height">Height of the image.</param>
        public static byte[] Apply(byte[] input, int width, int height)
        {
            int size = width * height;

            // transform input to float array
            var src = input.Select(x => (float)x).ToArray();

            // create temporary arrays, used as buffers for convolution
            var tmp = new float[size];
            var dst = new float[size];

            // convolve horizontally and vertically
            HorizontalConvolution(src, tmp, width, height);
            VerticalConvolution(tmp, dst, width, height);

            // transform float array back to byte array, clipping values to [0, 255]
            byte[] output = new byte[size];
            for (int i = 0; i < size; i++)
            {
                var val = dst[i];
                if (val < 0f)
                    val = 0f;
                if (val > 255f)
                    val = 255f;
                output[i] = (byte)MathF.Round(val);
            }

            return output;
        }

        /// <summary>
        /// Horizontal convolution
        /// </summary>
        private static void HorizontalConvolution(float[] src, float[] dst, int width, int height)
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
        private static void VerticalConvolution(float[] src, float[] dst, int width, int height)
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
