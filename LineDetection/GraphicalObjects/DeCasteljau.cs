using MathNet.Numerics.LinearAlgebra;

namespace LineDetection.GraphicalObjects
{
    public static class DeCasteljau
    {
        /// <summary>
        /// GetCurvePoints
        /// </summary>
        public static List<Vector<double>>? GetCurvePoints(BezierCurve? curve, int pointCount)
        {
            if (curve == null)
                throw new ArgumentNullException(nameof(curve));

            return GetCurvePoints(curve.ControlPoints, pointCount);
        }

        /// <summary>
        /// GetCurvePoints
        /// </summary>
        public static List<Vector<double>>? GetCurvePoints(List<Vector<double>> bezierCurvePoints, int pointCount)
        {
            if (pointCount < 2)
                throw new ApplicationException($"Invalid parameter: you must request at least 2 points to be returned from the curve!");

            if (bezierCurvePoints == null || bezierCurvePoints.Count < 2)
                return null;

            var result = new Vector<double>[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                float time;

                if (i == 0)
                    time = 0;
                else
                    time = i / (float)(pointCount - 1);

                Vector<double> point = GetDeCasteljauPoint(bezierCurvePoints, time, out _);

                result[i] = point;
            }

            return new List<Vector<double>>(result);
        }

        /// <summary>
        /// GetDeCasteljauPoint
        /// </summary>
        public static Vector<double> GetDeCasteljauPoint(List<Vector<double>> points, double time, out double angle)
        {
            angle = 0;
            var currentPoints = points;

            // Until you end up with a list with a single point...
            while (currentPoints.Count > 1)
            {
                // ... run DeCasteljau's calculation
                if (currentPoints.Count == 2)
                {
                    var point1 = currentPoints[0];
                    var point2 = currentPoints[1];

                    var diff = point2 - point1;

                    angle = (float)Math.Atan2(diff[1], diff[0]);
                }

                // Calculate non-recursively a new list of points. This list will contain 1 less point than the previous list
                var newPoints = new List<Vector<double>>(currentPoints.Count - 1);

                for (int i = 0; i < currentPoints.Count - 1; i++)
                {
                    // Load two points
                    var point1 = currentPoints[i];
                    var point2 = currentPoints[i + 1];

                    // Calculate a point in between, aligned by the time.
                    // If time == 0, result is point1
                    // If time == 1, result is point2
                    var resultPoint = point1 * (1 - time) + point2 * time;

                    newPoints.Add(resultPoint);
                }

                // Don't forget to update the point list with the newly calculated values
                currentPoints = newPoints;
            }

            return currentPoints[0];
        }
    }
}

