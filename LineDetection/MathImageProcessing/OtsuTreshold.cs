namespace LineDetection.MathImageProcessing
{
    /// <summary>
    /// Contains a method for applying Otsu threshold to a 1D byte array.
    /// </summary>
    public static class OtsuTreshold
    {
        public static int BackgroundMargin { get; set; } = 40;

        /// <summary>
        /// Applies Otsu threshold to a 1D byte array, returning a new 1D byte array with values 0 or 255.
        /// </summary>
        public static void Apply(YUVImage? image)
        {
            ArgumentNullException.ThrowIfNull(image);

            var threshold = GetOtsuThreshold(image);

            int leftMax = 0;
            int rightMax = 0;
            int leftMaxIndex = 128;
            int rightMaxIndex = 128;

            // if the image contains only background, set all bytes to 255
            for (int i = 0; i < 256; i++)
            {
                if (i < threshold)
                {
                    if (image.Histogram[i] > leftMax)
                    {
                        leftMax = image.Histogram[i];
                        leftMaxIndex = i;
                    }
                }
                else if (i > threshold)
                {
                    if (image.Histogram[i] > rightMax)
                    {
                        rightMax = image.Histogram[i];
                        rightMaxIndex = i;
                    }
                }
            }

            int deltaMode = rightMaxIndex - leftMaxIndex;
            if (deltaMode < BackgroundMargin)
            {
                Array.Fill(image.Bytes, (byte)255);
                return;
            }

            int width = image.Width;
            int height = image.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // safe margin at the left and right borders of the image
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
        public static byte GetOtsuThreshold(YUVImage parImage)
        {
            ArgumentNullException.ThrowIfNull(parImage);

            var pixelCount = parImage.Width * parImage.Height;

            var intensityHistogram = new int[256];
            for (int i = 0; i < pixelCount; i++)
            {
                byte intensity = parImage.Bytes[i];
                intensityHistogram[intensity]++;
            }

            var intensityProbabilities = new float[256];
            for (int i = 0; i < 256; i++)
            {
                intensityProbabilities[i] = (float)intensityHistogram[i] / pixelCount;
            }

            var totalIntensitySum = 0f;
            for (int i = 0; i < 256; i++)
            {
                totalIntensitySum += i * intensityProbabilities[i];
            }

            var bgWeight = 0f;
            var bgIntensity = 0f;
            var maxVariance = -1f;
            byte bestThreshold = 0;

            for (int i = 0; i < 256; i++)
            {
                bgWeight += intensityProbabilities[i];
                bgIntensity += i * intensityProbabilities[i];

                var fgWeight = 1f - bgWeight;

                if (bgWeight <= 0f || fgWeight <= 0f)
                    continue;

                var bgMean = bgIntensity / bgWeight;
                var fgMean = (totalIntensitySum - bgIntensity) / fgWeight;

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
    }
}