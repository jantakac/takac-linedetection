namespace LineDetection.MathImageProcessing
{
    public static class ResizeGauss
    {
        private static float[,] kernel = new float[1, 1];
        private static float sigma = 1.0f;
        private static int radius = 2;

        public static float Sigma
        {
            set
            {
                if (value < 0.1f || value > 10.0f)
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
        static ResizeGauss()
        {
            CreateGaussianKernel();
        }

        /// <summary>
        /// CreateGaussianKernel
        /// </summary>
        public static void CreateGaussianKernel()
        {
            int size = radius * 2 + 1;
            kernel = new float[size, size];
            float mean = size / 2;
            float sum = 0.0f;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    kernel[x, y] = (float)(Math.Exp(-0.5 * (Math.Pow((x - mean) / sigma, 2.0) + Math.Pow((y - mean) / sigma, 2.0))) / (2 * Math.PI * sigma * sigma));
                    sum += kernel[x, y];
                }
            }

            // Normalize the kernel
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    kernel[x, y] /= sum;
                }
            }
        }

        /// <summary>
        /// ResizeWithGaussianFilter
        /// </summary>
        public static YUVImage ResizeWithGaussianFilter(YUVImage image, int newWidth, int newHeight)
        {
            ArgumentNullException.ThrowIfNull(image);

            int originalWidth = image.Width;
            int originalHeight = image.Height;

            int kernelSize = kernel.GetLength(0);
            int halfKernel = kernelSize / 2;

            byte[] resizedImage = new byte[newWidth * newHeight];

            float scaleX = (float)originalWidth / newWidth;
            float scaleY = (float)originalHeight / newHeight;

            for (int newY = 0; newY < newHeight; newY++)
            {
                for (int newX = 0; newX < newWidth; newX++)
                {
                    double srcX = newX * scaleX;
                    double srcY = newY * scaleY;

                    int srcXInt = (int)Math.Floor(srcX);
                    int srcYInt = (int)Math.Floor(srcY);

                    double filteredValue = 0.0;
                    double weightSum = 0.0;

                    for (int ky = -halfKernel; ky <= halfKernel; ky++)
                    {
                        for (int kx = -halfKernel; kx <= halfKernel; kx++)
                        {
                            int sampleX = Math.Clamp(srcXInt + kx, 0, originalWidth - 1);
                            int sampleY = Math.Clamp(srcYInt + ky, 0, originalHeight - 1);

                            int sampleIndex = sampleY * originalWidth + sampleX;
                            double weight = kernel[ky + halfKernel, kx + halfKernel];

                            filteredValue += weight * image.Bytes[sampleIndex];
                            weightSum += weight;
                        }
                    }

                    int newIndex = newY * newWidth + newX;
                    resizedImage[newIndex] = (byte)Math.Clamp(filteredValue / weightSum, 0, 255);
                }
            }

            return new YUVImage(resizedImage, newWidth, newHeight);
        }
    }
}
