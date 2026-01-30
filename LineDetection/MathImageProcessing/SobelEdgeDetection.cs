using System.Numerics;
using System.Runtime.CompilerServices;
namespace LineDetection.MathImageProcessing;

/// <summary>
/// Provides high-performance, SIMD-optimized methods for detecting vertical line edges 
/// in an image using a horizontal gradient (Sobel-like) filter.
/// </summary>
public static class SobelEdgeDetection
{
    /// <summary>
    /// The maximum possible gradient sum for a 6-pixel kernel (3*255 - 0 = 765).
    /// Used as the trigger value for a "perfect" edge transition.
    /// </summary>
    private const int Threshold = 765;

    /// <summary>
    /// The number of elements a Vector&lt;short&gt; can hold (usually 8 on SSE or 16 on AVX2).
    /// </summary>
    private static readonly int VectorCount = Vector<short>.Count;

    /// <summary>
    /// A pre-calculated vector used for efficient "greater than" comparisons across a whole batch of pixels.
    /// </summary>
    private static readonly Vector<short> VectorThreshold = new Vector<short>(Threshold - 1);

    /// <summary>
    /// Scans the image at specific vertical intervals to find pairs of vertical edges.
    /// </summary>
    /// <param name="image">The source YUV image data containing the pixel bytes.</param>
    /// <param name="numberOfPoints">
    /// Determines the vertical scanning resolution. The image is scanned at intervals of 
    /// <c>Height / numberOfPoints</c>. Higher values mean more precise but slower scanning.
    /// </param>
    /// <returns>
    /// A flat array of integers representing the center X coordinates and Y coordinates 
    /// of detected line segments in the format <c>[centerX1, y1, centerX2, y2, ...]</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the image is null.</exception>
    /// <exception cref="ArgumentException">Thrown if numberOfPoints is invalid.</exception>
    public static int[] Process(YUVImage? image, int numberOfPoints)
    {
        ArgumentNullException.ThrowIfNull(image);

        if (numberOfPoints < 4 || numberOfPoints > image.Height)
            throw new ArgumentException("Wrong value of argument numberOfPoints");

        List<int> result = [];
        float precision = (float)image.Height / numberOfPoints;

        unsafe
        {
            // Pin the memory array in place so the Garbage Collector doesn't move it 
            // while we use raw pointers, also skips bound checking.
            fixed (byte* pImage = image.Bytes)
            {
                for (float yy = 0; yy < image.Height; yy += precision)
                {
                    int rowOffset = (int)yy * image.Width;
                    ProcessRow(pImage, rowOffset, image.Width, (int)yy, result);
                }
            }
        }

        return [.. result];
    }

    /// <summary>
    /// Processes a single horizontal row of pixels.
    /// It employs a hybrid approach: scanning rapidly with SIMD vectors first, 
    /// and falling back to scalar processing for detection logic or remaining pixels.
    /// </summary>
    /// <param name="pImage">Pointer to the start of the image data.</param>
    /// <param name="rowOffset">The index offset where the current row begins.</param>
    /// <param name="width">The width of the image.</param>
    /// <param name="yCoord">The current Y coordinate (row number).</param>
    /// <param name="result">The list to store detected coordinates.</param>
    private static unsafe void ProcessRow(byte* pImage, int rowOffset, int width, int yCoord, List<int> result)
    {
        // Allocate a tiny buffer on the stack to temporarily hold byte->short conversions.
        // This is extremely fast and avoids heap allocation overhead.
        short* conversionBuffer = stackalloc short[VectorCount];

        int x = 3;
        int maxX = width - 3;
        int edgeCount = 0;
        int indexLeft = 0;

        for (; x <= maxX - VectorCount; x += VectorCount)
        {
            // Calculate sums for the entire chunk at once.
            Vector<short> vSum = ComputeSobelVector(pImage, rowOffset, x, conversionBuffer);

            // Optimization: If NO pixels in this batch exceed the threshold, skip the complex logic completely.
            if (!VectorContainsEdge(vSum))
                continue;

            // If we found a potential edge, analyze the vector element-by-element to find exactly where.
            if (TryFindEdgeInVector(vSum, x, yCoord, ref edgeCount, ref indexLeft, result))
                return; // The pair was found, stop processing this row.
        }

        // Process any remaining pixels at the end of the row that didn't fit into a full vector chunk.
        for (; x < maxX; x++)
        {
            short sum = ComputeSobelScalar(pImage, rowOffset, x);

            if (UpdateEdgeState(sum, x, yCoord, ref edgeCount, ref indexLeft, result))
                return; // The pair was found, stop processing this row.
        }
    }

    /// <summary>
    /// Calculates the horizontal gradient for a batch of pixels simultaneously using SIMD instructions.
    /// <para>
    /// Formula: Sum(Right 3 neighbors) - Sum(Left 3 neighbors)
    /// </para>
    /// </summary>
    /// <param name="pImage">Pointer to image data.</param>
    /// <param name="rowOffset">Offset to the start of the current row.</param>
    /// <param name="x">The current column index.</param>
    /// <param name="buffer">Temporary stack buffer for type conversion.</param>
    /// <returns>A vector containing the calculated gradients for <see cref="VectorCount"/> pixels.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe Vector<short> ComputeSobelVector(byte* pImage, int rowOffset, int x, short* buffer)
    {
        Vector<short> vSum = Vector<short>.Zero;

        // Subtract Left 3 pixels (x-3, x-2, x-1)
        vSum -= LoadChunk(pImage, rowOffset + x - 3, buffer);
        vSum -= LoadChunk(pImage, rowOffset + x - 2, buffer);
        vSum -= LoadChunk(pImage, rowOffset + x - 1, buffer);

        // Add Right 3 pixels (x, x+1, x+2)
        vSum += LoadChunk(pImage, rowOffset + x, buffer);
        vSum += LoadChunk(pImage, rowOffset + x + 1, buffer);
        vSum += LoadChunk(pImage, rowOffset + x + 2, buffer);

        return vSum;
    }

    /// <summary>
    /// Performs a high-speed check to see if *any* value in the vector exceeds the detection threshold.
    /// This allows the main loop to skip 90%+ of the image data without running branching logic.
    /// </summary>
    /// <param name="vSum">The vector of gradient sums.</param>
    /// <returns>True if at least one value is an edge candidate; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool VectorContainsEdge(Vector<short> vSum)
    {
        Vector<short> vAbs = Vector.Abs(vSum);
        // Returns a vector where all bits are set (-1) if true, or 0 if false.
        // We compare against Zero to see if any element matched.
        return Vector.GreaterThan(vAbs, VectorThreshold) != Vector<short>.Zero;
    }

    /// <summary>
    /// Iterates through the calculated vector elements manually to update the edge detection state machine.
    /// This is only called when <see cref="VectorContainsEdge"/> returns true.
    /// </summary>
    /// <param name="vSum">The vector of gradient sums.</param>
    /// <param name="baseX">The absolute X coordinate of the first pixel in this vector.</param>
    /// <param name="yCoord">The current row Y coordinate.</param>
    /// <param name="edgeCount">Reference to the current number of edges found (0, 1, or 2).</param>
    /// <param name="indexLeft">Reference to the X coordinate of the first detected edge.</param>
    /// <param name="result">The results list to populate.</param>
    /// <returns>True if the edge pair is complete and row processing should stop.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryFindEdgeInVector(Vector<short> vSum, int baseX, int yCoord, ref int edgeCount, ref int indexLeft, List<int> result)
    {
        for (int k = 0; k < VectorCount; k++)
        {
            // Access the specific pre-calculated value from the vector
            if (UpdateEdgeState(vSum[k], baseX + k, yCoord, ref edgeCount, ref indexLeft, result))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Updates the core state machine for edge detection.
    /// Tracks if we have found the left edge (Edge 1) or the right edge (Edge 2).
    /// </summary>
    /// <param name="sum">The calculated gradient value for the current pixel.</param>
    /// <param name="currentX">The X coordinate of the current pixel.</param>
    /// <param name="yCoord">The Y coordinate of the current row.</param>
    /// <param name="edgeCount">Reference to the number of edges found so far.</param>
    /// <param name="indexLeft">Reference to the stored location of the first edge.</param>
    /// <param name="result">The results list.</param>
    /// <returns>True if the second edge was found and processing is complete.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool UpdateEdgeState(short sum, int currentX, int yCoord, ref int edgeCount, ref int indexLeft, List<int> result)
    {
        // Check if the gradient meets the strict threshold (Pure Black <-> Pure White)
        if (sum <= -Threshold || sum >= Threshold)
        {
            edgeCount++;
            if (edgeCount == 1)
            {
                indexLeft = currentX;
            }
            else if (edgeCount == 2)
            {
                // Calculate the center point between the two edges
                result.Add((currentX + indexLeft) >> 1);
                result.Add(yCoord);
                return true; // Stop processing this row
            }
        }
        return false;
    }

    /// <summary>
    /// Computes the horizontal gradient for a single pixel using standard scalar arithmetic.
    /// Used for the last few pixels of a row that do not fit into a SIMD vector.
    /// </summary>
    /// <param name="pImage">Pointer to image data.</param>
    /// <param name="rowOffset">Offset to the start of the row.</param>
    /// <param name="x">The column index.</param>
    /// <returns>The calculated gradient sum.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe short ComputeSobelScalar(byte* pImage, int rowOffset, int x)
    {
        // Calculate: (Right side) - (Left side)
        int sum = 0;
        sum -= pImage[rowOffset + x - 3];
        sum -= pImage[rowOffset + x - 2];
        sum -= pImage[rowOffset + x - 1];
        sum += pImage[rowOffset + x];
        sum += pImage[rowOffset + x + 1];
        sum += pImage[rowOffset + x + 2];
        return (short)sum;
    }

    /// <summary>
    /// Helper method to load a sequence of bytes from memory and convert them 
    /// into a Vector&lt;short&gt;.
    /// </summary>
    /// <param name="basePtr">Pointer to the start of the byte array.</param>
    /// <param name="index">The index to start reading from.</param>
    /// <param name="buffer">A pointer to stack memory used for safe casting.</param>
    /// <returns>A vector of shorts representing the loaded bytes.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe Vector<short> LoadChunk(byte* basePtr, int index, short* buffer)
    {
        for (int i = 0; i < VectorCount; i++)
        {
            buffer[i] = basePtr[index + i];
        }
        return *(Vector<short>*)buffer;
    }
}