using MathNet.Numerics.LinearAlgebra;

namespace LineDetection.Tools
{
    public class CoordTransformations(int parUmax, int parVmax, int parUmin, int parVmin,
                                float parXmin, float parYmin, float parXmax, float parYmax)
    {
        private readonly int Umax = parUmax;
        private readonly int Vmax = parVmax;
        private readonly int Umin = parUmin;
        private readonly int Vmin = parVmin;
                
        private readonly float Xmin = parXmin;
        private readonly float Ymin = parYmin;
        private readonly float Xmax = parXmax;
        private readonly float Ymax = parYmax;

        /// <summary>
        /// FromUVtoXY
        /// </summary>
        public PointF FromUVtoXY(Point p)
        {
            return new PointF((p.X - Umin) / (float)(Umax - Umin) * (Xmax - Xmin) + Xmin,
                              (p.Y - Vmin) / (float)(Vmax - Vmin) * (Ymax - Ymin) + Ymin);
        }

        /// <summary>
        /// FromXYtoUV
        /// </summary>
        public Point FromXYtoUV(PointF p)
        {
            return new Point((int)((p.X - Xmin) / (Xmax - Xmin) * (Umax - Umin) + Umin),
                             (int)((p.Y - Ymin) / (Ymax - Ymin) * (Vmax - Vmin) + Vmin));
        }

        /// <summary>
        /// FromXYtoUVF
        /// </summary>
        public PointF FromXYtoUVF(PointF p)
        {
            return new PointF((p.X - Xmin) / (Xmax - Xmin) * (Umax - Umin) + Umin,
                             (p.Y - Ymin) / (Ymax - Ymin) * (Vmax - Vmin) + Vmin);
        }

        /// <summary>
        /// FromUVtoXYVector
        /// </summary>
        public Vector<double> FromUVtoXYVectorDouble(Point p)
        {
            Vector<double> v = Vector<double>.Build.Dense(3);

            v[0] = (p.X - Umin) / (double)(Umax - Umin) * (Xmax - Xmin) + Xmin;
            v[1] = (p.Y - Vmin) / (double)(Vmax - Vmin) * (Ymax - Ymin) + Ymin;
            v[2] = 0.0;

            return v;
        }

        /// <summary>
        /// GetUVF
        /// </summary>
        public PointF GetUVF(Vector<double> v)
        {
            if (v == null || v.Count != 3)
                throw new ApplicationException($"Wrong vertex data input!");


            return new PointF(((float)v[0] - Xmin) / (Xmax - Xmin) * (Umax - Umin) + Umin,
                              ((float)v[1] - Ymin) / (Ymax - Ymin) * (Vmax - Vmin) + Vmin);
        }

        /// <summary>
        /// GetUV
        /// </summary>
        public Point GetUV(Vector<double> v)
        {
            PointF p = GetUVF(v);
            return new Point((int)Math.Round(p.X), (int)Math.Round(p.Y));
        }

        /// <summary>
        /// Calculates distance between two points
        /// </summary>
        public double CalculateDistanceBetweenPoints(Vector<double> point1, Vector<double> point2)
        {
            double deltaX = Math.Abs(point1[0] - point2[0]);
            double deltaY = Math.Abs(point1[1] - point2[1]);

            return Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
        }
    }
}