namespace LineDetection.MathImageProcessing
{
    public class GrayscaleByteImage
    {
        private readonly byte[] data;
        private readonly int width;
        private readonly int height;

        private readonly int[] histogram = new int[256];
        private readonly double[] normalizedHistogram = new double[256];

        private readonly int histogramMinimum;
        private readonly int histogramMaximum;

        private readonly int[] cumulativeHistogram = new int[256];
        private readonly double[] cumulativeNormalizedHistogram = new double[256];

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public byte[] Data { get { return data; } }

        public int HistogramMinimum { get { return histogramMinimum; } }
        public int HistogramMaximum { get { return histogramMaximum; } }

        public int[] Histogram { get { return histogram; } }

        public double[] NormalisedHistogram { get { return normalizedHistogram; } }

        public int[] CumulativeHistogram { get { return cumulativeHistogram; } }

        public double[] CumulativeNormalisedHistogram { get { return cumulativeNormalizedHistogram; } }

        /// <summary>
        /// GrayscaleByteImage constructor 1
        /// </summary>
        public GrayscaleByteImage(byte[] parData, int parWidth, int parHeight)
        {
            ArgumentNullException.ThrowIfNull(parData);

            if (parWidth < 16 || parHeight < 16 || parWidth > 3000 || parHeight > 3000)
                throw new ArgumentException("Grayscale byte image - parameter out of range");

            data = parData;
            width = parWidth;
            height = parHeight;
        }

        /// <summary>
        /// GrayscaleByteImage constructor 2
        /// </summary>
        public GrayscaleByteImage(int parWidth, int parHeight)
        {
            if (parWidth < 16 || parHeight < 16 || parWidth > 3000 || parHeight > 3000)
                throw new ArgumentException("Grayscale byte image - parameter out of range");

            width = parWidth;
            height = parHeight;

            data = new byte[width * height];
        }

        /// <summary>
        /// UpdateHistogramData
        /// </summary>
        public void UpdateHistogramData()
        {
            int histogramMinimum = int.MaxValue;
            int histogramMaximum = int.MinValue;

            for (int i = 0; i < width * height; i++)
            {
                histogram[data[i]]++;
            }

            // find min and max
            for (int i = 0; i < 256; i++)
            {
                if (histogram[i] > histogramMaximum)
                    histogramMaximum = histogram[i];

                if (histogram[i] < histogramMinimum)
                    histogramMinimum = histogram[i];
            }

            // normalize histogram
            for (int i = 0; i < 256; i++)
            {
                normalizedHistogram[i] = (double)histogram[i] / histogramMaximum;
            }

            cumulativeHistogram[0] = histogram[0];
            // cumulative histogram
            for (int i = 1; i < 256; i++)
            {
                cumulativeHistogram[i] = histogram[i] + cumulativeHistogram[i-1];
            }

            // normalise cumulative histogram
            for (int i = 0; i < 256; i++)
            {
                cumulativeNormalizedHistogram[i] = (double)cumulativeHistogram[i] / cumulativeHistogram[255];
            }
        }

        /// <summary>
        /// To bitmap
        /// </summary>
        public Bitmap ToBitmap()
        {
            Bitmap resultBitmap = new(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte color = data[x + width * y];
                    resultBitmap.SetPixel(x, y, Color.FromArgb(color, color, color));
                }
            }

            return resultBitmap;
        }
    }
}
