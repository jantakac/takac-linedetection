namespace LineDetection.MathImageProcessing
{
    public static class SobelEdgeDetection
    {
        /// <summary>
        /// Process
        /// </summary>
        /// <param name="image">Original image</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static int[] Process(YUVImage? image, int numberOfPoints)
        {
            ArgumentNullException.ThrowIfNull(image);

            if (numberOfPoints < 4 || numberOfPoints > image.Height)
                throw new ArgumentException("Wrong value of argument number Of Points - Sobel");

            List<int> result = [];
            int indexLeft = 0;

            float precision = (float)image.Height / numberOfPoints;

            // apply tresholding
            for (float yy = 0; yy < image.Height; yy += precision)
            {
                int y = (int)yy * image.Width;

                int edgeCount = 0;

                for (int x = 4; x < image.Width - 4; x++)
                {
                    // sobel kernel for line
                    int sum = image.Bytes[x - 3 + y] * -1 + 
                              image.Bytes[x - 2 + y] * -1 + 
                              image.Bytes[x - 1 + y] * -1 +
                              image.Bytes[x + y] + 
                              image.Bytes[x + 1 + y] + 
                              image.Bytes[x + 2 + y];

                    if (sum <= -765 || sum >= 765)
                    {
                        edgeCount++;

                        if (edgeCount == 1)
                        {
                            indexLeft = x;
                        }
                        if (edgeCount == 2)
                        {
                            int indexRight = x;

                            result.Add((indexRight + indexLeft) >> 1);
                            result.Add((int)yy);
                            continue;
                        }
                    }
                }
            }

            return [.. result];
        }
    }
}
