namespace LineDetection.MathImageProcessing
{
    public static class GaussianBlur
    {
        private static double sigma = 1.5;
        private static readonly double[] kernel = [];

        public static double Sigma 
        { 
            get 
            {
                return sigma; 
            }
            set
            {
                sigma = value;
                CreateGaussianKernel();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        static GaussianBlur()
        {
            CreateGaussianKernel();
        }

        /// <summary>
        /// Apply Gaussian blur on grayscale 2D byte array of intensities
        /// </summary>
        /// <param name="input"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static byte[,] ApplyFilter(byte[,] input, int width, int height)
        {
            int kernelRadius = kernel.Length / 2;

            // Step 2: Temporary arrays for intermediate results
            byte[,] temp = new byte[width, height];
            byte[,] output = new byte[width, height];

            // Step 3: Horizontal pass
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double sum = 0;
                    double weightSum = 0;

                    for (int k = -kernelRadius; k <= kernelRadius; k++)
                    {
                        int neighborX = Math.Clamp(x + k, 0, width - 1);
                        sum += input[neighborX, y] * kernel[k + kernelRadius];
                        weightSum += kernel[k + kernelRadius];
                    }

                    temp[x, y] = (byte)Math.Round(sum / weightSum);
                }
            }

            // Step 4: Vertical pass
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double sum = 0;
                    double weightSum = 0;

                    for (int k = -kernelRadius; k <= kernelRadius; k++)
                    {
                        int neighborY = Math.Clamp(y + k, 0, height - 1);
                        sum += temp[x, neighborY] * kernel[k + kernelRadius];
                        weightSum += kernel[k + kernelRadius];
                    }

                    output[x, y] = (byte)Math.Round(sum / weightSum);
                }
            }

            return output;
        }

        /// <summary>
        /// CreateGaussianKernel
        /// </summary>
        private static void CreateGaussianKernel()
        {
            // typically 3*sigma for sufficient kernel size
            int radius = (int)Math.Ceiling(3 * sigma); 
            int size = 2 * radius + 1;

            double sum = 0;
            double twoSigmaSquare = 2 * sigma * sigma;

            for (int i = 0; i < size; i++)
            {
                int x = i - radius;
                kernel[i] = Math.Exp(-x * x / twoSigmaSquare);
                sum += kernel[i];
            }

            // Normalize kernel
            for (int i = 0; i < size; i++)
            {
                kernel[i] /= sum;
            }
        }
    }
}
