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

            for (int i = 0; i < image.Bytes.Length; i++)
                image.Bytes[i] = image.Bytes[i] < threshold ? (byte)0 : (byte)255;
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
    }
}