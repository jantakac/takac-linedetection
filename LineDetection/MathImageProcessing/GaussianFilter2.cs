namespace LineDetection.MathImageProcessing
{
    /// <summary>
    /// Applies a Gaussian filter to an image.
    /// </summary>
    public static class GaussianFilter2
    {
        private static float[] kernel = new float[1];
        private static float sigma = 1.0f;
        private static int radius = 2;

        public static float Sigma
        {
            set
            {
                if (value < 1.0 || value > 10)
                    throw new ArgumentException("Gaussian filter - wrong sigma");

                sigma = value;

                CreateGaussianKernel();
            }
        }

        public static int Radius
        {
            set
            {
                if (value < 3 || value > 20)
                    throw new ArgumentException("Gaussian filter - wrong radius");

                radius = value;

                CreateGaussianKernel();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        static GaussianFilter2()
        {
            CreateGaussianKernel();
        }

        public static void Apply(ref YUVImage? image)
        {
            ArgumentNullException.ThrowIfNull(image);

            int width = image.Width;
            int height = image.Height;

            int size = image.Width * image.Height;

            // transform input to float array
            var src = image.Bytes.Select(x => (float)x).ToArray();

            // Horizontal pass
            byte[] horizontalBlur = Convolve1D(image.Bytes, width, height, kernel, isHorizontal: true);

            // Vertical pass
            image = new YUVImage(Convolve1D(horizontalBlur, width, height, kernel, isHorizontal: false), width, height);
        }

        /// <summary>
        /// CreateGaussianKernel
        /// </summary>
        private static void CreateGaussianKernel()
        {
            int size = radius * 2 + 1;
            kernel = new float[size];
            float sum = 0;

            for (int i = -radius; i <= radius; i++)
            {
                float value = (float)(Math.Exp(-0.5 * (i * i) / (sigma * sigma)) / (Math.Sqrt(2 * Math.PI) * sigma));
                kernel[i + radius] = value;
                sum += value;
            }

            // Normalize the kernel to ensure the sum of all weights is 1
            for (int i = 0; i < size; i++)
            {
                kernel[i] /= sum;
            }
        }

        /// <summary>
        /// Convolve1D
        /// </summary>
        private static byte[] Convolve1D(byte[] image, int width, int height, float[] kernel, bool isHorizontal)
        {
            int radius = kernel.Length / 2;
            byte[] result = new byte[image.Length];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float sum = 0;

                    for (int k = -radius; k <= radius; k++)
                    {
                        int index;
                        if (isHorizontal)
                        {
                            // Horizontal pass
                            int nx = x + k;
                            if (nx >= 0 && nx < width) // Check bounds
                                index = y * width + nx;
                            else
                                continue;
                        }
                        else
                        {
                            // Vertical pass
                            int ny = y + k;
                            if (ny >= 0 && ny < height) // Check bounds
                                index = ny * width + x;
                            else
                                continue;
                        }

                        sum += image[index] * kernel[k + radius];
                    }

                    result[y * width + x] = (byte)Math.Clamp(sum, 0, 255); // Clamp to byte range
                }
            }

            return result;
        }
    }
}
