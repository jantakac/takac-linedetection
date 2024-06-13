namespace LineDetection.MathImageProcessing
{
    public static class OtsuTresholding
    {
        public static GrayscaleByteImage Process(GrayscaleByteImage parImage)
        {
            ArgumentNullException.ThrowIfNull(parImage);

            // Calculate global mean
            double[] histogram = parImage.Histogram;

            double mg = 0;

            for (int i = 0; i < 256; i++)
            {
                mg += i * histogram[i];
            }

            //Get max between-class variance
            double bcv = 0;
            int threshold = 0;
            
            for (int i = 0; i < 256; i++)
            {
                double cs = 0;
                double m = 0;

                for (int j = 0; j < i; j++)
                {
                    cs += histogram[j];
                    m += j * histogram[j];
                }

                if (cs == 0)
                {
                    continue;
                }

                double old_bcv = bcv;
                bcv = Math.Max(bcv, Math.Pow(mg * cs - m, 2) / (cs * (1 - cs)));

                if (bcv > old_bcv)
                {
                    threshold = i;
                }
            }

            GrayscaleByteImage resultImage = new(parImage.Width, parImage.Height);

            // apply tresholding
            for (int i = 0; i < parImage.Width * parImage.Height; i++)
            {
                resultImage.Data[i] = (byte)((parImage.Data[i] > threshold) ? 255 : 0);
            }

            return resultImage;
        }
    }
}
