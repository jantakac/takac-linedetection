using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDetection.MathImageProcessing
{
    public static class ImageResize
    {
        public static byte[,] ResizeNearestNeighbor(byte[,] input, int newWidth, int newHeight)
        {
            int oldWidth = input.GetLength(0);
            int oldHeight = input.GetLength(1);

            byte[,] output = new byte[newWidth, newHeight];
            double xRatio = (double)oldWidth / newWidth;
            double yRatio = (double)oldHeight / newHeight;

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    int nearestX = (int)(x * xRatio);
                    int nearestY = (int)(y * yRatio);
                    output[x, y] = input[nearestX, nearestY];
                }
            }

            return output;
        }

        public static byte[,] ResizeBilinear(byte[,] input, int newWidth, int newHeight)
        {
            int oldWidth = input.GetLength(0);
            int oldHeight = input.GetLength(1);

            byte[,] output = new byte[newWidth, newHeight];
            double xRatio = (double)oldWidth / newWidth;
            double yRatio = (double)oldHeight / newHeight;

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    double gx = x * xRatio;
                    double gy = y * yRatio;

                    int x1 = (int)gx;
                    int y1 = (int)gy;
                    int x2 = Math.Min(x1 + 1, oldWidth - 1);
                    int y2 = Math.Min(y1 + 1, oldHeight - 1);

                    double dx = gx - x1;
                    double dy = gy - y1;

                    // Interpolate
                    double top = (1 - dx) * input[x1, y1] + dx * input[x2, y1];
                    double bottom = (1 - dx) * input[x1, y2] + dx * input[x2, y2];
                    double value = (1 - dy) * top + dy * bottom;

                    output[x, y] = (byte)Math.Round(value);
                }
            }

            return output;
        }
    }
}
