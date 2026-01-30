using System;

namespace LineDetection.MathImageProcessing
{
    /// <summary>
    /// Contains methods for applying global and adaptive Otsu thresholding.
    /// </summary>
    public static class OtsuTreshold
    {
        public static int BackgroundMargin { get; set; } = 40;

        /// <summary>
        /// Applies Adaptive Otsu thresholding by dividing the image into vertical zones.
        /// Better for uneven lighting (e.g., shadows on the road).
        /// </summary>
        /// <param name="image">Input image.</param>
        /// <param name="zonesCount">Number of horizontal strips (vertical zones). Default is 8.</param>
        public static void ApplyAdaptive(YUVImage? image, int zonesCount = 8)
        {
            ArgumentNullException.ThrowIfNull(image);

            int width = image.Width;
            int height = image.Height;

            int zoneHeight = (int)Math.Ceiling((double)height / zonesCount);

            for (int i = 0; i < zonesCount; i++)
            {
                int startY = i * zoneHeight;
                int endY = Math.Min(startY + zoneHeight, height);

                if (startY >= height) break;

                ProcessZone(image, startY, endY, width);
            }
        }

        private static void ProcessZone(YUVImage image, int startY, int endY, int width)
        {
            int[] zoneHistogram = new int[256];
            int pixelCount = 0;

            for (int y = startY; y < endY; y++)
            {
                int rowOffset = y * width;
                for (int x = 0; x < width; x++)
                {
                    if (x >= 4 && x <= width - 5)
                    {
                        byte intensity = image.Bytes[rowOffset + x];
                        zoneHistogram[intensity]++;
                        pixelCount++;
                    }
                }
            }

            if (pixelCount == 0) return;

            byte threshold = GetOtsuThresholdFromHistogram(zoneHistogram, pixelCount);

            int leftMax = 0;
            int rightMax = 0;
            int leftMaxIndex = 0; // Default to 0 instead of 128 to be safe
            int rightMaxIndex = 255;

            for (int k = 0; k < 256; k++)
            {
                if (k < threshold)
                {
                    if (zoneHistogram[k] > leftMax)
                    {
                        leftMax = zoneHistogram[k];
                        leftMaxIndex = k;
                    }
                }
                else if (k > threshold)
                {
                    if (zoneHistogram[k] > rightMax)
                    {
                        rightMax = zoneHistogram[k];
                        rightMaxIndex = k;
                    }
                }
            }

            int deltaMode = rightMaxIndex - leftMaxIndex;

            bool isLowContrast = deltaMode < BackgroundMargin;

            for (int y = startY; y < endY; y++)
            {
                int rowOffset = y * width;
                for (int x = 0; x < width; x++)
                {
                    int index = rowOffset + x;

                    // Apply safety margin at borders
                    if (x < 4 || x > width - 1 - 4)
                    {
                        image.Bytes[index] = 255;
                    }
                    else
                    {
                        if (isLowContrast)
                        {
                            image.Bytes[index] = 255;
                        }
                        else
                        {
                            image.Bytes[index] = image.Bytes[index] < threshold ? (byte)0 : (byte)255;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates Otsu threshold based on a pre-calculated histogram.
        /// </summary>
        public static byte GetOtsuThresholdFromHistogram(int[] histogram, int totalPixels)
        {
            float[] intensityProbabilities = new float[256];
            for (int i = 0; i < 256; i++)
            {
                intensityProbabilities[i] = (float)histogram[i] / totalPixels;
            }

            float totalIntensitySum = 0f;
            for (int i = 0; i < 256; i++)
            {
                totalIntensitySum += i * intensityProbabilities[i];
            }

            float bgWeight = 0f;
            float bgIntensity = 0f;
            float maxVariance = -1f;
            byte bestThreshold = 0;

            for (int i = 0; i < 256; i++)
            {
                bgWeight += intensityProbabilities[i];
                bgIntensity += i * intensityProbabilities[i];

                float fgWeight = 1f - bgWeight;

                if (bgWeight <= 0f || fgWeight <= 0f)
                    continue;

                float bgMean = bgIntensity / bgWeight;
                float fgMean = (totalIntensitySum - bgIntensity) / fgWeight;

                float meanDiff = bgMean - fgMean;
                float variance = bgWeight * fgWeight * meanDiff * meanDiff;

                if (variance > maxVariance)
                {
                    maxVariance = variance;
                    bestThreshold = (byte)i;
                }
            }

            return bestThreshold;
        }

        // Original method for compatibility
        public static void Apply(YUVImage? image)
        {
            ApplyAdaptive(image, 8);
        }

        // Original helper kept for compatibility, wrapping the new logic
        public static byte GetOtsuThreshold(YUVImage parImage)
        {
            ArgumentNullException.ThrowIfNull(parImage);
            int pixelCount = parImage.Width * parImage.Height;

            // Calculate histogram globally
            int[] histogram = new int[256];
            for (int i = 0; i < pixelCount; i++)
            {
                histogram[parImage.Bytes[i]]++;
            }

            return GetOtsuThresholdFromHistogram(histogram, pixelCount);
        }
    }
}