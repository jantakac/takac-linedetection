using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LineDetection.MathImageProcessing
{
    public static class ResizeGauss
    {
        private static float[,] kernel = new float[1, 1];
        private static float sigma = 3.0f;
        private static int radius = 3;

        public static float Sigma
        {
            set
            {
                if (value == sigma)
                    return;

                if (value < 0.1f || value > 10.0f)
                    throw new ArgumentException("Gaussian filter - wrong sigma");

                sigma = value;

                CreateGaussianKernel();
            }
        }

        public static int Radius
        {
            set
            {
                if (value == radius)
                    return;

                if (value < 3 || value > 20)
                    throw new ArgumentException("Gaussian filter - wrong radius");

                radius = value;

                CreateGaussianKernel();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        static ResizeGauss()
        {
            CreateGaussianKernel();
        }

        /// <summary>
        /// CreateGaussianKernel
        /// </summary>
        public static void CreateGaussianKernel()
        {
            int size = radius * 2 + 1;
            kernel = new float[size, size];
            float mean = size / 2.0f;

            float[] weights = new float[size];

            float twoSigmaSq = 2 * sigma * sigma;
            float varianceFactor = 1.0f / twoSigmaSq;

            for (int i = 0; i < size; i++)
            {
                float v = i - mean;
                weights[i] = (float)Math.Exp(-(v * v) * varianceFactor);
            }

            Span<float> kernelSpan = MemoryMarshal.CreateSpan(ref kernel[0, 0], kernel.Length);

            int vectorCount = Vector<float>.Count;

            for (int x = 0; x < size; x++)
            {
                float weightX = weights[x];
                int y = 0;

                for (; y <= size - vectorCount; y += vectorCount)
                {
                    Vector<float> vecY = new Vector<float>(weights, y);

                    Vector<float> vecResult = vecY * weightX;

                    vecResult.CopyTo(kernelSpan.Slice((x * size) + y));
                }

                for (; y < size; y++)
                {
                    kernel[x, y] = weightX * weights[y];
                }
            }

            int k = 0;
            Vector<float> vSum = Vector<float>.Zero;

            for (; k <= kernelSpan.Length - vectorCount; k += vectorCount)
            {
                vSum += new Vector<float>(kernelSpan.Slice(k));
            }
            float sum = Vector.Sum(vSum);

            for (; k < kernelSpan.Length; k++)
            {
                sum += kernelSpan[k];
            }

            float invSum = 1.0f / sum;
            Vector<float> vInvSum = new Vector<float>(invSum);

            k = 0;
            for (; k <= kernelSpan.Length - vectorCount; k += vectorCount)
            {
                Vector<float> v = new Vector<float>(kernelSpan.Slice(k));
                v *= vInvSum;
                v.CopyTo(kernelSpan.Slice(k));
            }

            for (; k < kernelSpan.Length; k++)
            {
                kernelSpan[k] *= invSum;
            }
        }

        /// <summary>
        /// ResizeWithGaussianFilter
        /// </summary>
        public static YUVImage ResizeWithGaussianFilter(YUVImage image, int newWidth, int newHeight)
        {
            ArgumentNullException.ThrowIfNull(image);

            int originalWidth = image.Width;
            int originalHeight = image.Height;
            int kernelSize = kernel.GetLength(0);
            int halfKernel = kernelSize / 2;

            byte[] resizedBuffer = new byte[newWidth * newHeight];

            // Flatten kernel for faster memory access
            float[] kernelFlat = new float[kernelSize * kernelSize];
            for (int y = 0; y < kernelSize; y++)
                for (int x = 0; x < kernelSize; x++)
                    kernelFlat[y * kernelSize + x] = kernel[y, x];

            float scaleX = (float)originalWidth / newWidth;
            float scaleY = (float)originalHeight / newHeight;

            unsafe
            {
                fixed (byte* pSrc = image.Bytes)
                fixed (byte* pDst = resizedBuffer)
                fixed (float* pKernel = kernelFlat)
                {
                    int vectorCount = Vector<float>.Count;

                    float* tempPixelBuffer = stackalloc float[vectorCount];

                    for (int newY = 0; newY < newHeight; newY++)
                    {
                        double srcY = newY * scaleY;
                        int srcYInt = (int)Math.Floor(srcY);
                        int destRowOffset = newY * newWidth;

                        for (int newX = 0; newX < newWidth; newX++)
                        {
                            double srcX = newX * scaleX;
                            int srcXInt = (int)Math.Floor(srcX);

                            float filteredValue = 0.0f;
                            float weightSum = 0.0f;

                            bool isSafe = (srcXInt - halfKernel >= 0) && (srcXInt + halfKernel < originalWidth) &&
                                          (srcYInt - halfKernel >= 0) && (srcYInt + halfKernel < originalHeight);

                            if (isSafe)
                            {
                                for (int ky = 0; ky < kernelSize; ky++)
                                {
                                    int currentSrcY = srcYInt + ky - halfKernel;
                                    int rowStart = currentSrcY * originalWidth + (srcXInt - halfKernel);

                                    int kx = 0;
                                    int kIndex = ky * kernelSize;

                                    for (; kx <= kernelSize - vectorCount; kx += vectorCount)
                                    {
                                        Vector<float> vWeights = Unsafe.Read<Vector<float>>(pKernel + kIndex + kx);

                                        for (int j = 0; j < vectorCount; j++)
                                        {
                                            tempPixelBuffer[j] = pSrc[rowStart + kx + j];
                                        }

                                        Vector<float> vPixels = Unsafe.Read<Vector<float>>(tempPixelBuffer);

                                        filteredValue += Vector.Dot(vWeights, vPixels);
                                        weightSum += Vector.Sum(vWeights);
                                    }

                                    for (; kx < kernelSize; kx++)
                                    {
                                        float w = pKernel[kIndex + kx];
                                        filteredValue += w * pSrc[rowStart + kx];
                                        weightSum += w;
                                    }
                                }
                            }
                            else
                            {
                                for (int ky = -halfKernel; ky <= halfKernel; ky++)
                                {
                                    for (int kx = -halfKernel; kx <= halfKernel; kx++)
                                    {
                                        int sampleX = Math.Clamp(srcXInt + kx, 0, originalWidth - 1);
                                        int sampleY = Math.Clamp(srcYInt + ky, 0, originalHeight - 1);
                                        int sampleIndex = sampleY * originalWidth + sampleX;

                                        float weight = pKernel[(ky + halfKernel) * kernelSize + (kx + halfKernel)];
                                        filteredValue += weight * pSrc[sampleIndex];
                                        weightSum += weight;
                                    }
                                }
                            }

                            if (weightSum > 0) filteredValue /= weightSum;
                            pDst[destRowOffset + newX] = (byte)(filteredValue > 255 ? 255 : (filteredValue < 0 ? 0 : filteredValue));
                        }
                    }
                }
            }

            return new YUVImage(resizedBuffer, newWidth, newHeight);
        }
    }
}
