using LineDetection.MathImageProcessing;

namespace LineDetection.DrawingClasses
{
    public static class GaussianBlurGrayscale
    {
        private static int[]? originalImage; 
        private static int width; 
        private static int height;

        private static readonly ParallelOptions pOptions = new() { MaxDegreeOfParallelism = 16 };

        /// <summary>
        /// Process grayscale byte array - image
        /// </summary>
        public static GrayscaleByteImage Process(GrayscaleByteImage parData, int parRadius)
        {
            ArgumentNullException.ThrowIfNull(parData);

            originalImage = new int[parData.Width * parData.Height];

            for (int i = 0; i < parData.Width * parData.Height; i++)
            {
                originalImage[i] = parData.Data[i];
            }

            width = parData.Width;
            height = parData.Height;

            int[] newImage = new int[width * height];

            GrayscaleByteImage newImageReturn = new(width, height);

            GaussBlur_4(originalImage, newImage, parRadius);

            Parallel.For(0, width * height, pOptions, i =>
            {
                if (newImage[i] > 255)
                    newImageReturn.Data[i] = 255;
                else if (newImage[i] < 0)
                    newImageReturn.Data[i] = 0;
                else 
                    newImageReturn.Data[i] = (byte)newImage[i];
            });

            return newImageReturn;
        }

        /// <summary>
        /// GaussBlur_4
        /// </summary>
        private static void GaussBlur_4(int[] source, int[] dest, int r)
        {
            int[] bxs = BoxesForGauss(r, 3);
            BoxBlur_4(source, dest, width, height, (bxs[0] - 1) / 2);
            BoxBlur_4(dest, source, width, height, (bxs[1] - 1) / 2);
            BoxBlur_4(source, dest, width, height, (bxs[2] - 1) / 2);
        }

        /// <summary>
        /// BoxesForGauss
        /// </summary>
        private static int[] BoxesForGauss(int sigma, int n)
        {
            var wIdeal = Math.Sqrt((12 * sigma * sigma / n) + 1);
            var wl = (int)Math.Floor(wIdeal);
            
            if (wl % 2 == 0) 
                wl--;
            
            var wu = wl + 2;

            var mIdeal = (double)(12 * sigma * sigma - n * wl * wl - 4 * n * wl - 3 * n) / (-4 * wl - 4);
            var m = Math.Round(mIdeal);

            var sizes = new List<int>();

            for (var i = 0; i < n; i++)
            {
                sizes.Add(i < m ? wl : wu);
            }
        
            return [.. sizes];
        }

        /// <summary>
        /// BoxBlur_4
        /// </summary>
        private static void BoxBlur_4(int[] source, int[] dest, int w, int h, int r)
        {
            for (int i = 0; i < w * h; i++)
            {
                dest[i] = source[i];
            }

            BoxBlurH_4(dest, source, w, h, r);
            BoxBlurT_4(source, dest, w, h, r);
        }

        /// <summary>
        /// BoxBlurH_4
        /// </summary>
        private static void BoxBlurH_4(int[] source, int[] dest, int w, int h, int r)
        {
            double iar = (double)1 / (r + r + 1);

            Parallel.For(0, h, pOptions, i =>
            {
                int ti = i * w;
                int li = ti;
                int ri = ti + r;
                int fv = source[ti];
                int lv = source[ti + w - 1];
                int val = (r + 1) * fv;
                
                for (var j = 0; j < r; j++)
                {
                    val += source[ti + j];
                }

                for (var j = 0; j <= r; j++)
                {
                    val += source[ri++] - fv;
                    dest[ti++] = (int)Math.Round(val * iar);
                }
                
                for (var j = r + 1; j < w - r; j++)
                {
                    val += source[ri++] - dest[li++];
                    dest[ti++] = (int)Math.Round(val * iar);
                }
                
                for (var j = w - r; j < w; j++)
                {
                    val += lv - source[li++];
                    dest[ti++] = (int)Math.Round(val * iar);
                }
            });
        }

        /// <summary>
        /// BoxBlurT_4
        /// </summary>
        private static void BoxBlurT_4(int[] source, int[] dest, int w, int h, int r)
        {
            double iar = (double)1 / (r + r + 1);

            Parallel.For(0, w, pOptions, i =>
            {
                int ti = i;
                int li = ti;
                int ri = ti + r * w;
                int fv = source[ti];
                int lv = source[ti + w * (h - 1)];
                int val = (r + 1) * fv;
                
                for (int j = 0; j < r; j++)
                {
                    val += source[ti + j * w];
                }

                for (int j = 0; j <= r; j++)
                {
                    val += source[ri] - fv;
                    dest[ti] = (int)Math.Round(val * iar);
                    ri += w;
                    ti += w;
                }
                
                for (int j = r + 1; j < h - r; j++)
                {
                    val += source[ri] - source[li];
                    dest[ti] = (int)Math.Round(val * iar);
                    li += w;
                    ri += w;
                    ti += w;
                }
                
                for (int j = h - r; j < h; j++)
                {
                    val += lv - source[li];
                    dest[ti] = (int)Math.Round(val * iar);
                    li += w;
                    ti += w;
                }
            });
        }
    }
}


