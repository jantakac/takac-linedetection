using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LineDetection.MathImageProcessing
{
    /// <summary>
    /// Provides methods for resizing images using a Gaussian filter.
    /// Uses SIMD (Vectorization) and Unsafe memory access for performance.
    /// </summary>
    public static class ResizeGauss
    {
        private static float[,] kernel = new float[1, 1];
        private static float sigma = 3.0f;
        private static int radius = 3;

        /// <summary>
        /// Gets or sets the Sigma value for the Gaussian distribution.
        /// Changing this value triggers a kernel regeneration.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when value is not between 0.1 and 10.0.</exception>
        public static float Sigma
        {
            get => sigma;
            set
            {
                if (Math.Abs(value - sigma) < float.Epsilon)
                    return;

                if (value < 0.1f || value > 10.0f)
                    throw new ArgumentException("Gaussian filter - wrong sigma");

                sigma = value;
                CreateGaussianKernel();
            }
        }

        /// <summary>
        /// Gets or sets the Radius of the kernel.
        /// Changing this value triggers a kernel regeneration.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when value is not between 3 and 20.</exception>
        public static int Radius
        {
            get => radius;
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
        /// Static constructor to initialize the default Gaussian kernel.
        /// </summary>
        static ResizeGauss()
        {
            CreateGaussianKernel();
        }

        /// <summary>
        /// Generates the 2D Gaussian kernel based on the current Radius and Sigma.
        /// Calculates the weights, creates the 2D matrix, and normalizes it.
        /// </summary>
        public static void CreateGaussianKernel()
        {
            int size = radius * 2 + 1;
            float[] weights = Calculate1DGaussianWeights(size);

            kernel = new float[size, size];

            // Create 2D kernel via outer product of 1D weights
            Generate2DKernelFromWeights(weights, size);

            // Normalize so sum of all elements equals 1.0
            NormalizeKernel();
        }

        /// <summary>
        /// Calculates the 1D Gaussian weights based on the current Sigma.
        /// </summary>
        /// <param name="size">The full size of the kernel (radius * 2 + 1).</param>
        /// <returns>An array of float weights.</returns>
        private static float[] Calculate1DGaussianWeights(int size)
        {
            float[] weights = new float[size];
            float mean = size / 2.0f;
            float twoSigmaSq = 2 * sigma * sigma;
            float varianceFactor = 1.0f / twoSigmaSq;

            for (int i = 0; i < size; i++)
            {
                float v = i - mean;
                weights[i] = (float)Math.Exp(-(v * v) * varianceFactor);
            }

            return weights;
        }

        /// <summary>
        /// Populates the static 2D kernel array using SIMD instructions where possible.
        /// </summary>
        /// <param name="weights">The 1D weights array.</param>
        /// <param name="size">The dimension size of the kernel.</param>
        private static void Generate2DKernelFromWeights(float[] weights, int size)
        {
            Span<float> kernelSpan = MemoryMarshal.CreateSpan(ref kernel[0, 0], kernel.Length);
            int vectorCount = Vector<float>.Count;

            for (int x = 0; x < size; x++)
            {
                float weightX = weights[x];
                int y = 0;

                // SIMD loop
                for (; y <= size - vectorCount; y += vectorCount)
                {
                    Vector<float> vecY = new Vector<float>(weights, y);
                    Vector<float> vecResult = vecY * weightX;
                    vecResult.CopyTo(kernelSpan.Slice((x * size) + y));
                }

                // Scalar loop for remaining elements
                for (; y < size; y++)
                {
                    kernel[x, y] = weightX * weights[y];
                }
            }
        }

        /// <summary>
        /// Normalizes the static kernel so that the sum of all elements equals 1.0.
        /// Uses SIMD to calculate the sum and apply the division.
        /// </summary>
        private static void NormalizeKernel()
        {
            Span<float> kernelSpan = MemoryMarshal.CreateSpan(ref kernel[0, 0], kernel.Length);
            int vectorCount = Vector<float>.Count;
            int k = 0;

            // Calculate Sum
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

            // Apply Inverse Sum
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
        /// Resizes a YUVImage using the pre-calculated Gaussian filter.
        /// </summary>
        /// <param name="image">The source YUV image.</param>
        /// <param name="newWidth">The desired width.</param>
        /// <param name="newHeight">The desired height.</param>
        /// <returns>A new YUVImage containing the resized data.</returns>
        public static YUVImage ResizeWithGaussianFilter(YUVImage image, int newWidth, int newHeight)
        {
            ArgumentNullException.ThrowIfNull(image);

            byte[] resizedBuffer = new byte[newWidth * newHeight];
            float[] kernelFlat = FlattenKernel(out int kernelSize);

            unsafe
            {
                fixed (byte* pSrc = image.Bytes)
                fixed (byte* pDst = resizedBuffer)
                fixed (float* pKernel = kernelFlat)
                {
                    ProcessImageBuffer(pSrc, pDst, pKernel, image.Width, image.Height, newWidth, newHeight, kernelSize);
                }
            }

            return new YUVImage(resizedBuffer, newWidth, newHeight);
        }

        /// <summary>
        /// Flattens the 2D kernel into a 1D array for faster memory access during convolution.
        /// </summary>
        /// <param name="kernelSize">Outputs the dimension size of the kernel.</param>
        /// <returns>A flattened float array of the kernel.</returns>
        private static float[] FlattenKernel(out int kernelSize)
        {
            kernelSize = kernel.GetLength(0);
            float[] flat = new float[kernelSize * kernelSize];

            for (int y = 0; y < kernelSize; y++)
            {
                for (int x = 0; x < kernelSize; x++)
                {
                    flat[y * kernelSize + x] = kernel[y, x];
                }
            }

            return flat;
        }

        /// <summary>
        /// Iterates over the destination dimensions and calculates pixel values.
        /// </summary>
        /// <param name="pSrc">Pointer to source image bytes.</param>
        /// <param name="pDst">Pointer to destination buffer.</param>
        /// <param name="pKernel">Pointer to flattened kernel.</param>
        /// <param name="srcW">Source width.</param>
        /// <param name="srcH">Source height.</param>
        /// <param name="dstW">Destination width.</param>
        /// <param name="dstH">Destination height.</param>
        /// <param name="kernelSize">Size of the kernel dimension.</param>
        private static unsafe void ProcessImageBuffer(byte* pSrc, byte* pDst, float* pKernel,
            int srcW, int srcH, int dstW, int dstH, int kernelSize)
        {
            float scaleX = (float)srcW / dstW;
            float scaleY = (float)srcH / dstH;
            int halfKernel = kernelSize / 2;
            int vectorCount = Vector<float>.Count;

            // Stack allocation for SIMD vector loading
            float* tempPixelBuffer = stackalloc float[vectorCount];

            for (int newY = 0; newY < dstH; newY++)
            {
                double srcY = newY * scaleY;
                int srcYInt = (int)Math.Floor(srcY);
                int destRowOffset = newY * dstW;

                for (int newX = 0; newX < dstW; newX++)
                {
                    double srcX = newX * scaleX;
                    int srcXInt = (int)Math.Floor(srcX);

                    // Check if the kernel fits entirely within the image boundaries
                    bool isSafe = (srcXInt - halfKernel >= 0) && (srcXInt + halfKernel < srcW) &&
                                  (srcYInt - halfKernel >= 0) && (srcYInt + halfKernel < srcH);

                    float resultPixel;

                    if (isSafe)
                    {
                        resultPixel = ConvolveSafe(pSrc, pKernel, srcW, srcYInt, srcXInt,
                            kernelSize, halfKernel, vectorCount, tempPixelBuffer);
                    }
                    else
                    {
                        resultPixel = ConvolveEdge(pSrc, pKernel, srcW, srcH, srcYInt, srcXInt,
                            kernelSize, halfKernel);
                    }

                    pDst[destRowOffset + newX] = ClampToByte(resultPixel);
                }
            }
        }

        /// <summary>
        /// Performs convolution using SIMD instructions.
        /// Assumes bounds checking has already been passed (no boundary checks inside).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe float ConvolveSafe(byte* pSrc, float* pKernel, int srcWidth,
            int srcYInt, int srcXInt, int kernelSize, int halfKernel, int vectorCount, float* tempPixelBuffer)
        {
            float filteredValue = 0.0f;
            float weightSum = 0.0f;

            for (int ky = 0; ky < kernelSize; ky++)
            {
                int currentSrcY = srcYInt + ky - halfKernel;
                int rowStart = currentSrcY * srcWidth + (srcXInt - halfKernel);

                int kx = 0;
                int kIndex = ky * kernelSize;

                // SIMD Loop for Kernel Row
                for (; kx <= kernelSize - vectorCount; kx += vectorCount)
                {
                    Vector<float> vWeights = Unsafe.Read<Vector<float>>(pKernel + kIndex + kx);

                    // Manually load bytes into float buffer for vectorization
                    for (int j = 0; j < vectorCount; j++)
                    {
                        tempPixelBuffer[j] = pSrc[rowStart + kx + j];
                    }

                    Vector<float> vPixels = Unsafe.Read<Vector<float>>(tempPixelBuffer);

                    filteredValue += Vector.Dot(vWeights, vPixels);
                    weightSum += Vector.Sum(vWeights);
                }

                // Scalar cleanup
                for (; kx < kernelSize; kx++)
                {
                    float w = pKernel[kIndex + kx];
                    filteredValue += w * pSrc[rowStart + kx];
                    weightSum += w;
                }
            }

            return weightSum > 0 ? filteredValue / weightSum : filteredValue;
        }

        /// <summary>
        /// Performs convolution with boundary checking (Clamp).
        /// Used for pixels near the edges of the source image.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe float ConvolveEdge(byte* pSrc, float* pKernel, int srcWidth, int srcHeight,
            int srcYInt, int srcXInt, int kernelSize, int halfKernel)
        {
            float filteredValue = 0.0f;
            float weightSum = 0.0f;

            for (int ky = -halfKernel; ky <= halfKernel; ky++)
            {
                for (int kx = -halfKernel; kx <= halfKernel; kx++)
                {
                    int sampleX = Math.Clamp(srcXInt + kx, 0, srcWidth - 1);
                    int sampleY = Math.Clamp(srcYInt + ky, 0, srcHeight - 1);
                    int sampleIndex = sampleY * srcWidth + sampleX;

                    float weight = pKernel[(ky + halfKernel) * kernelSize + (kx + halfKernel)];
                    filteredValue += weight * pSrc[sampleIndex];
                    weightSum += weight;
                }
            }

            return weightSum > 0 ? filteredValue / weightSum : filteredValue;
        }

        /// <summary>
        /// Clamps a float value to a byte (0-255).
        /// </summary>
        /// <param name="val">The input float value.</param>
        /// <returns>The clamped byte.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte ClampToByte(float val)
        {
            if (val > 255) return 255;
            if (val < 0) return 0;
            return (byte)val;
        }
    }
}