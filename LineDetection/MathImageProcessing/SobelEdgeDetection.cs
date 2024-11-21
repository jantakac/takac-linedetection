namespace LineDetection.MathImageProcessing
{
    public static class SobelEdgeDetection
    {
        public static int[] Process(GrayscaleByteImage parImage, int parStep)
        {
            ArgumentNullException.ThrowIfNull(parImage);

            List<int> result = [];

            int indexLeft = 0;

            // apply tresholding
            for (int y = 0; y < parImage.Height; y += parStep)
            {
                int edgeCount = 0;

                for (int x = 4; x < parImage.Width - 4; x++)
                {
                    // sobel kernel for line
                    int sum = parImage.Data[x - 3,y] * -1 + parImage.Data[x - 2,y] * -1 + parImage.Data[x - 1,y] * -1 +
                        parImage.Data[x,y] + parImage.Data[x + 1,y] + parImage.Data[x + 2,y];

                    if (sum <= -510 || sum >= 510)
                    {
                        edgeCount++;

                        if (edgeCount == 1)
                        {
                            indexLeft = x; 
                        }
                        if (edgeCount == 2)
                        {
                            int indexRight = x;

                            result.Add((int)Math.Round(0.5 * (indexRight + indexLeft)));
                            result.Add(y);
                            continue; 
                        }
                    }
                }
            }

            return [.. result];
        }
    }
}
