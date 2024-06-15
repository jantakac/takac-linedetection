namespace LineDetection.MathImageProcessing
{
    public static class SobelEdgeDetection
    {
        public static int[] Process(GrayscaleByteImage parImage)
        {
            ArgumentNullException.ThrowIfNull(parImage);

            List<int> result = [];

            int indexLeft = 0; 
            int indexRight = 0;
            int edgeCount = 0;

            // apply tresholding
            for (int j = 0; j < parImage.Height; j += 10)
            {
                edgeCount = 0; 

                for (int i = 4; i < parImage.Width - 4; i++)
                {
                    int index = i + j * parImage.Width;

                    // sobel kernel for line
                    int sum = parImage.Data[index - 3] * -1 + parImage.Data[index - 2] * -1 + parImage.Data[index - 1] * -1 +
                        parImage.Data[index] + parImage.Data[index + 1] + parImage.Data[index - 2];

                    if (sum <= 510 || sum >= 510)
                    {
                        edgeCount++;

                        if (edgeCount == 1)
                        {
                            indexLeft = i; 
                        }
                        if (edgeCount == 2)
                        {
                            indexRight = i;

                            result.Add((int)Math.Round(0.5 * (indexRight + indexLeft)));
                            continue; 
                        }
                    }
                }
            }

            return [.. result];
        }
    }
}
