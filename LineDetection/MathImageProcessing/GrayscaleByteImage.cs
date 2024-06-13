namespace LineDetection.MathImageProcessing
{
    public class GrayscaleByteImage
    {
        private readonly byte[] data;
        private readonly int width;
        private readonly int height;

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public byte[] Data { get { return data; } }

        public double[] Histogram 
        {
            get 
            {
                //Get histogram values
                double[] histogram = new double[256];
                //double minimum = double.MaxValue;
                //double maximum = double.MinValue;

                for (int i = 0; i < width * height; i ++)
                {
                    histogram[data[i]]++;
                }

                //// find min and max
                //for (int i = 0; i < 256; i++)
                //{
                //    if (histogram[i] > maximum)
                //        maximum = histogram[i];

                //    if (histogram[i] < minimum)
                //        minimum = histogram[i];
                //}

                ////Normalize histogram
                //for (int i = 0; i < 256; i++)
                //{
                //    histogram[i] = (histogram[i] - minimum) / (maximum - minimum);
                //}

                return histogram.Select(x => (x / ((double)width * height))).ToArray();
            }
        }

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
