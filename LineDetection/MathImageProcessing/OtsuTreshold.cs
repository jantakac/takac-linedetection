namespace LineDetection.MathImageProcessing
{
    /// <summary>
    /// Contains a method for applying Otsu threshold to a 1D byte array.
    /// </summary>
    public static class OtsuTreshold
    {
        /// <summary>
        /// Applies Otsu threshold to a 1D byte array, returning a new 1D byte array with values 0 or 255.
        /// </summary>
        public static void Apply(YUVImage? image)
        {
            ArgumentNullException.ThrowIfNull(image);

            var threshold = GetOtsuThreshold(image);

            bool containsFG = ContainsForeground(image.Histogram, threshold, image.Width * image.Height);

            int width = image.Width;
            int height = image.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // safe margin at the border of the image
                    if (x < 4 || x > width - 1 - 4)
                        image.Bytes[x + y * width] = 255;
                    else
                        image.Bytes[x + y * width] = image.Bytes[x + y * width] < threshold ? (byte)0 : (byte)255;
                }
            }
        }

        /// <summary>
        /// Get otsu threshold
        /// </summary>
        /// <param name="grayscaleData"></param>
        /// <returns></returns>
        private static byte GetOtsuThreshold(YUVImage input)
        {
            var bgWeight = 0f;
            var bgIntensity = 0f;
            var maxVariance = -1f;
            byte bestThreshold = 0;

            for (int i = 0; i < 256; i++)
            {
                bgWeight += input.IntensityPropabilities[i];
                bgIntensity += i * input.IntensityPropabilities[i];

                var fgWeight = 1f - bgWeight;

                if (bgWeight <= 0f || fgWeight <= 0f)
                    continue;

                var bgMean = bgIntensity / bgWeight;
                var fgMean = (input.IntensitySum - bgIntensity) / fgWeight;

                var meanDiff = bgMean - fgMean;
                var variance = bgWeight * fgWeight * meanDiff * meanDiff;

                if (variance > maxVariance)
                {
                    maxVariance = variance;
                    bestThreshold = (byte)i;
                }
            }

            return bestThreshold;
        }

        /// <summary>
        /// nalyze the histogram to check for foreground objects
        /// </summary>
        public static bool ContainsForeground(int[] histogram, int otsuThreshold, int totalPixels)
        {
            // Count pixels below and above the threshold
            int belowThreshold = histogram.Take(otsuThreshold).Sum();
            int aboveThreshold = histogram.Skip(otsuThreshold).Sum();

            // Calculate ratios
            float belowRatio = (float)belowThreshold / totalPixels;
            float aboveRatio = (float)aboveThreshold / totalPixels;

            // Heuristic: If one side dominates heavily, it's likely just background
            if (belowRatio > 0.95 || aboveRatio > 0.95)
            {
                return false; // Mostly background
            }

            return true; // Likely foreground objects exist
        }
    }
}