using MathNet.Numerics.LinearAlgebra;
using System.Diagnostics;   // NuGet package for numerical linear algebra

namespace LineDetection.MathImageProcessing
{
    public class BezierCurveFitting
    {
        public void DoIt()
        {
            Random rnd = new Random();

            // Example points to fit
            List<Vector<double>> points =
            [
                Vector<double>.Build.DenseOfArray([9.15280 + rnd.NextDouble(), 39.1655]),
                Vector<double>.Build.DenseOfArray([11.6807, 41.5216]),
                Vector<double>.Build.DenseOfArray([14.0600, 43.6950]),
                Vector<double>.Build.DenseOfArray([16.2959, 45.6899]),
                Vector<double>.Build.DenseOfArray([18.3933, 47.5108]),
                Vector<double>.Build.DenseOfArray([20.3574, 49.1622]),
                Vector<double>.Build.DenseOfArray([22.1931, 50.6484]),
                Vector<double>.Build.DenseOfArray([23.9054, 51.9740]),
                Vector<double>.Build.DenseOfArray([25.4995, 53.1432]),
                Vector<double>.Build.DenseOfArray([26.9803, 54.1607]),
                Vector<double>.Build.DenseOfArray([28.3529, 55.0308]),
                Vector<double>.Build.DenseOfArray([29.6223, 55.7579]),
                Vector<double>.Build.DenseOfArray([30.7937, 56.3465]),
                Vector<double>.Build.DenseOfArray([31.8719, 56.8010]),
                Vector<double>.Build.DenseOfArray([32.8621, 57.1258]),
                Vector<double>.Build.DenseOfArray([33.7692, 57.3254]),
                Vector<double>.Build.DenseOfArray([34.5984, 57.4043]),
                Vector<double>.Build.DenseOfArray([35.3547, 57.3667]),
                Vector<double>.Build.DenseOfArray([36.0430, 57.2173]),
                Vector<double>.Build.DenseOfArray([36.6685, 56.9603]),
                Vector<double>.Build.DenseOfArray([37.2362, 56.6003]),
                Vector<double>.Build.DenseOfArray([37.7512, 56.1417]),
                Vector<double>.Build.DenseOfArray([38.2183, 55.5888]),
                Vector<double>.Build.DenseOfArray([38.6428, 54.9462]),
                Vector<double>.Build.DenseOfArray([39.0296, 54.2183]),
                Vector<double>.Build.DenseOfArray([39.3838, 53.4095]),
                Vector<double>.Build.DenseOfArray([39.7105, 52.5242]),
                Vector<double>.Build.DenseOfArray([40.0145, 51.5669]),
                Vector<double>.Build.DenseOfArray([40.3011, 50.5421]),
                Vector<double>.Build.DenseOfArray([40.5752, 49.4540]),
                Vector<double>.Build.DenseOfArray([40.8419, 48.3072]),
                Vector<double>.Build.DenseOfArray([41.1062, 47.1062]),
                Vector<double>.Build.DenseOfArray([41.3731, 45.8552]),
                Vector<double>.Build.DenseOfArray([41.6478, 44.5589]),
                Vector<double>.Build.DenseOfArray([41.9351, 43.2215]),
                Vector<double>.Build.DenseOfArray([42.2403, 41.8476]),
                Vector<double>.Build.DenseOfArray([42.5682, 40.4416]),
                Vector<double>.Build.DenseOfArray([42.9240, 39.0078]),
                Vector<double>.Build.DenseOfArray([43.3127, 37.5508]),
                Vector<double>.Build.DenseOfArray([43.7393, 36.0750]),
                Vector<double>.Build.DenseOfArray([44.2089, 34.5848]),
                Vector<double>.Build.DenseOfArray([44.7265, 33.0846]),
                Vector<double>.Build.DenseOfArray([45.2971, 31.5789]),
                Vector<double>.Build.DenseOfArray([45.9258, 30.0721]),
                Vector<double>.Build.DenseOfArray([46.6176, 28.5687]),
                Vector<double>.Build.DenseOfArray([47.3776, 27.0730]),
                Vector<double>.Build.DenseOfArray([48.2108, 25.5895]),
                Vector<double>.Build.DenseOfArray([49.1222, 24.1226]),
                Vector<double>.Build.DenseOfArray([50.1169, 22.6768]),
                Vector<double>.Build.DenseOfArray([51.2000, 21.2566]),
                Vector<double>.Build.DenseOfArray([52.3763, 19.8662]),
                Vector<double>.Build.DenseOfArray([53.6511, 18.5103]),
                Vector<double>.Build.DenseOfArray([55.0293, 17.1931]),
                Vector<double>.Build.DenseOfArray([56.5160, 15.9192]),
                Vector<double>.Build.DenseOfArray([58.1162, 14.6929]),
                Vector<double>.Build.DenseOfArray([59.8349, 13.5188]),
                Vector<double>.Build.DenseOfArray([61.6772, 12.4012]),
                Vector<double>.Build.DenseOfArray([63.6482, 11.3445]),
                Vector<double>.Build.DenseOfArray([65.7528, 10.3533]),
                Vector<double>.Build.DenseOfArray([67.9962, 9.43190]),
                Vector<double>.Build.DenseOfArray([70.3832, 8.58470]),
                Vector<double>.Build.DenseOfArray([72.9191, 7.81630]),
                Vector<double>.Build.DenseOfArray([75.6088, 7.13100]),
                Vector<double>.Build.DenseOfArray([78.4574, 6.53330]),
                Vector<double>.Build.DenseOfArray([81.4698, 6.02760]),
                Vector<double>.Build.DenseOfArray([84.6512, 5.61830]),
                Vector<double>.Build.DenseOfArray([88.0066, 5.30980]),
                Vector<double>.Build.DenseOfArray([91.5410, 5.10670]),
                Vector<double>.Build.DenseOfArray([95.2595, 5.01340]),
                Vector<double>.Build.DenseOfArray([99.1670, 5.03410])
            ];

            // Fit the points to a cubic Bezier curve
            var bezierControlPoints = FitCubicBezier(points);

            // Output the control points
            //for (int i = 0; i < bezierControlPoints.Length; i++)
            //{
            //    Debug.WriteLine($"Control Point {i}: {bezierControlPoints[i]}");
            //}
        }

        public static Vector<double>[] FitCubicBezier(List<Vector<double>> points)
        {
            int n = points.Count;

            if (n < 4)
            {
                throw new ArgumentException("At least 4 points are required to fit a cubic Bezier curve.");
            }

            // Fixed control points
            Vector<double> P0 = points[0];
            Vector<double> P3 = points[n - 1];

            // Parameter t values for each point (chord length parameterization)
            double[] t = new double[n];

            t[0] = 0;

            for (int i = 1; i < n; i++)
            {
                t[i] = t[i - 1] + (points[i] - points[i - 1]).L2Norm();
            }
            for (int i = 1; i < n; i++)
            {
                t[i] /= t[n - 1];
            }

            // Create matrix A and vector B for least squares solution
            var A = Matrix<double>.Build.Dense(n, 2);
            var Bx = Vector<double>.Build.Dense(n);
            var By = Vector<double>.Build.Dense(n);

            for (int i = 0; i < n; i++)
            {
                double u = 1 - t[i];
                double tt = t[i] * t[i];
                double uu = u * u;
                double ttt = tt * t[i];
                double uuu = uu * u;

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
                Vector<double>.Build.DenseOfArray([P1P2X[0], P1P2Y[0]]),
                Vector<double>.Build.DenseOfArray([P1P2X[1], P1P2Y[1]]),
                P3,
            ];
        }
    }
}
