using System.Drawing.Imaging;

namespace LineDetection.MathImageProcessing
{
    public sealed class YUVImage : ICloneable
    {
        public static readonly int MIN_SIZE = 16;
        public static readonly int MAX_SIZE = 640;

        private readonly int width;
        private readonly int height;
        private readonly byte[] bytes;
        private uint[] histogram = new uint[1];
        private float[] intensityPropabilities = new float[1];
        private float intensitySum;

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public byte[] Bytes => bytes;

        // !!! - warning - before asking these two values, first histogram has to be updated by asking it's value
        public float[] IntensityPropabilities { get { return intensityPropabilities; } }
        public float IntensitySum { get { return intensitySum; } }

        public uint[] Histogram
        {
            get
            {
                UpdateHistogram();

                return histogram;
            }
        }

        public uint[] HistogramCDF
        {
            get
            {
                histogram = Histogram;
                uint[] brightnessCDF = new uint[256];

                for (int i = 0; i < 256; ++i)
                {
                    if (i == 0)
                        brightnessCDF[i] = histogram[i];
                    else
                        brightnessCDF[i] = brightnessCDF[i - 1] + histogram[i];
                }

                return brightnessCDF;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parImageBytes">Grayscale image bytes</param>
        /// <param name="parWidth">Image width</param>
        /// <param name="parHeight">Image height</param>
        /// <param name="parFastLoad">Use unsafe load</param>
        public YUVImage(byte[] parImageBytes, int parWidth, int parHeight)
        {
            if (parWidth < MIN_SIZE || parHeight < MIN_SIZE || parWidth > MAX_SIZE || parHeight > MAX_SIZE)
                throw new ArgumentException("Invalid image size");

            if (parImageBytes is null || parImageBytes.Length < 256 || parImageBytes.Length > 307200)
                throw new ArgumentException("Invalid image buffer");

            width = parWidth;
            height = parHeight;
            bytes = parImageBytes;

            UpdateHistogram();
        }

        /// <summary>
        /// Update histogram
        /// </summary>
        private void UpdateHistogram()
        {
            var pixelCount = bytes.Length;

            histogram = new uint[256];

            for (int i = 0; i < pixelCount; i++)
            {
                byte intensity = bytes[i];
                histogram[intensity]++;
            }

            intensityPropabilities = new float[256];
            intensitySum = 0f;

            for (int i = 0; i < 256; i++)
            {
                intensityPropabilities[i] = (float)histogram[i] / pixelCount;
                intensitySum += i * intensityPropabilities[i];
            }
        }

        /// <summary>
        /// Return a resized version of the image
        /// </summary>
        /// <param name="newWidth">New width</param>
        /// <param name="newHeight">New height</param>
        /// <returns></returns>
        public YUVImage ResizeGrayscaleImageBilinear(int newWidth, int newHeight)
        {
            if (newWidth < 8 || newHeight < 8)
                throw new ArgumentException("Invalid new image size");

            byte[] resized = new byte[newWidth * newHeight];

            // Ratios of old and new dimensions
            float xRatio = (float)width / newWidth;
            float yRatio = (float)height / newHeight;

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    // Compute positions in the source image
                    float srcX = x * xRatio;
                    float srcY = y * yRatio;

                    int x0 = (int)srcX;
                    int y0 = (int)srcY;

                    int x1 = Math.Min(x0 + 1, width - 1);
                    int y1 = Math.Min(y0 + 1, height - 1);

                    // Fractional parts for interpolation
                    float xLerp = srcX - x0;
                    float yLerp = srcY - y0;

                    // Get the four surrounding pixel values
                    byte topLeft = bytes[y0 * width + x0];
                    byte topRight = bytes[y0 * width + x1];
                    byte bottomLeft = bytes[y1 * width + x0];
                    byte bottomRight = bytes[y1 * width + x1];

                    // Bilinear interpolation
                    float top = topLeft + xLerp * (topRight - topLeft);
                    float bottom = bottomLeft + xLerp * (bottomRight - bottomLeft);
                    float pixelValue = top + yLerp * (bottom - top);

                    // Assign the interpolated value to the resized image
                    resized[y * newWidth + x] = (byte)Math.Round(pixelValue);
                }
            }

            return new YUVImage(resized, newWidth, newHeight);
        }

        /// <summary>
        /// Converts YUV image to bitmap
        /// </summary>
        /// <returns>Bitmap</returns>
        public Bitmap ToBitmap()
        {
            Bitmap bitmap = new(width, height);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmap.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int stride = bitmapData.Stride;

            unsafe
            {
                byte* pointer = (byte*)bitmapData.Scan0;

                for (int rowIndex = 0; rowIndex < height; ++rowIndex)
                {
                    for (int columnIndex = 0; columnIndex < width; ++columnIndex)
                    {
                        int offset = rowIndex * stride + columnIndex * bytesPerPixel;
                        byte* pixel = pointer + offset;

                        byte color = bytes[width * rowIndex + columnIndex];

                        pixel[0] = color;
                        pixel[1] = color;
                        pixel[2] = color;
                        pixel[3] = 255;
                    }
                }
            }

            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }

        /// <summary>
        /// Clone of the object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new YUVImage((byte[])bytes.Clone(), width, height);
        }
    }
}
