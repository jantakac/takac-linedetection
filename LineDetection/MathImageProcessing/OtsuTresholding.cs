namespace LineDetection.MathImageProcessing
{
    public static class OtsuTresholding
    {
        public static GrayscaleByteImage Process(GrayscaleByteImage parImage)
        {
            ArgumentNullException.ThrowIfNull(parImage);

            parImage.UpdateHistogramData();

            // Calculate global mean
            int[] histogram = parImage.Histogram;

            // Step 2: Total number of pixels
            int totalPixels = parImage.Width * parImage.Height;

            // Step 3: Calculate the sum of all pixel intensities
            // higher waight is for higher pixel intensities
            double sumAll = 0;
            for (int i = 0; i < 256; i++)
            {
                sumAll += i * histogram[i];
            }

            // Step 4: Iterate to find the optimal threshold
            double sumBackground = 0; // Background class sum
            int weightBackground = 0; // Background class weight
            int weightForeground = 0; // Foreground class weight

            double maxVariance = 0;
            int optimalThreshold = 0;

            // sigma^2 = Wb * Wf * (miB - miF)^2
            // Wb - weight background
            // Wf - weight forground
            // finally we choose the greatest value of sigma^2
            // and separate the pixels to two bins - background and foreground

            for (int t = 0; t < 256; t++)
            {
                // Update background weight and sum
                // count all intensities, set the boundry to t
                // less or equal to t is for this iteration considered as background
                // more than t is considered as foreground
                // if weight background is 0, no need to compute the between class variance
                // and it can continue to the next step
                weightBackground += histogram[t];
                if (weightBackground == 0)
                    continue;

                // Update foreground weight
                weightForeground = totalPixels - weightBackground;
                if (weightForeground == 0)
                    break;

                // Update background sum
                sumBackground += t * histogram[t];

                // Calculate means
                double meanBackground = sumBackground / weightBackground;
                double meanForeground = (sumAll - sumBackground) / weightForeground;

                // Calculate between-class variance
                double varianceBetween = weightBackground * weightForeground * Math.Pow(meanBackground - meanForeground, 2);

                // Update maximum variance and threshold
                if (varianceBetween > maxVariance)
                {
                    maxVariance = varianceBetween;
                    optimalThreshold = t;
                }
            }

            GrayscaleByteImage resultImage = new(parImage.Width, parImage.Height);

            // apply tresholding
            for (int j = 0; j < parImage.Height; j++)
            {
                for (int i = 0; i < parImage.Width; i++)
                {
                    int index = i + j * parImage.Width;

                    // set left and right border as white 3px for later sobel edge detection
                    if (i < 3 || i > parImage.Width - 4)
                    {
                        resultImage.Data[index] = 255; 
                    }
                    else
                    {
                        resultImage.Data[index] = (byte)((parImage.Data[index] > optimalThreshold) ? 255 : 0);
                    }
                }
            }

            return resultImage;
        }
    }
}
