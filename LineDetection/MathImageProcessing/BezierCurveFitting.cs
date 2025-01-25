using MathNet.Numerics.LinearAlgebra; 

namespace LineDetection.MathImageProcessing
{
    public static class BezierCurveFitting
    {
        public static Vector<float>[] FitCubicBezier(Vector<float>[] points)
        {
            int n = points.Length;

            if (n < 4)
            {
                throw new ArgumentException("At least 4 points are required to fit a cubic Bezier curve.");
            }

            // Fixed control points
            Vector<float> P0 = points[0];
            Vector<float> P3 = points[n - 1];

            // Parameter t values for each point (chord length parameterization)
            float[] t = new float[n];

            t[0] = 0;

            for (int i = 1; i < n; i++)
            {
                t[i] = t[i - 1] + (float)(points[i] - points[i - 1]).L2Norm();
            }
            for (int i = 1; i < n; i++)
            {
                t[i] /= t[n - 1];
            }

            // Create matrix A and vector B for least squares solution
            var A = Matrix<float>.Build.Dense(n, 2);
            var Bx = Vector<float>.Build.Dense(n);
            var By = Vector<float>.Build.Dense(n);

            for (int i = 0; i < n; i++)
            {
                float u = 1 - t[i];
                float tt = t[i] * t[i];
                float uu = u * u;
                float ttt = tt * t[i];
                float uuu = uu * u;

                // Note: we are solving for P1 and P2, so we use the fixed P0 and P3
                A[i, 0] = 3 * uu * t[i]; // Coefficient for P1
                A[i, 1] = 3 * u * tt;    // Coefficient for P2

                Bx[i] = points[i][0] - (uuu * P0[0] + ttt * P3[0]);
                By[i] = points[i][1] - (uuu * P0[1] + ttt * P3[1]);
            }

            // Solve the least squares problem
            var P1P2X = A.Solve(Bx);
            var P1P2Y = A.Solve(By);

            return
            [
                P0,
                Vector<float>.Build.DenseOfArray([P1P2X[0], P1P2Y[0], 0]),
                Vector<float>.Build.DenseOfArray([P1P2X[1], P1P2Y[1], 0]),
                P3,
            ];
        }
    }
}
