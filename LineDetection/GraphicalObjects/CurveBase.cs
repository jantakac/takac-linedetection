using LineDetection.Tools;
using MathNet.Numerics.LinearAlgebra;

namespace LineDetection.GraphicalObjects
{
    public abstract class CurveBase
    {
        #region Properties

        protected readonly List<Vector<double>> controlPoints;
        protected List<double> segmentLengths;
        protected int curvePrecision = 70;
        protected double length = 0;
        protected List<Vector<double>>? curvePoints;
        protected CoordTransformations ct;

        /// <summary>
        /// Curve length
        /// </summary>
        public double Length 
        { 
            get 
            { 
                return length; 
            } 
        }

        /// <summary>
        /// Curve precision
        /// </summary>
        public int RenderPrecision
        {
            get
            {
                return curvePrecision;
            }
            set
            {
                if (value < 5 || value > 100)
                    curvePrecision = 70;
                else
                    curvePrecision = value;

                RecalculateCurve();
            }
        }

        /// <summary>
        /// Curve control points
        /// </summary>
        public List<Vector<double>> ControlPoints
        {
            get
            {
                var newList = new List<Vector<double>>(controlPoints.Count);

                foreach (var point in controlPoints)
                    newList.Add(point.Clone());

                return newList;
            }
        }

        /// <summary>
        /// ControlPointIsSelected
        /// </summary>
        public bool ControlPointIsSelected(int controlPointIndex)
        {
            if (SelectedControlPointIndices == null || SelectedControlPointIndices.Length == 0)
                return false;

            foreach (var index in SelectedControlPointIndices)
            {
                if (index == controlPointIndex)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// SelectedControlPointIndex
        /// </summary>
        public int? SelectedControlPointIndex
        {
            get
            {
                if (SelectedControlPointIndices != null && SelectedControlPointIndices.Length > 0)
                    return SelectedControlPointIndices[0];

                return null;
            }
            set
            {
                if (value == null)
                    SelectedControlPointIndices = null;
                else
                    SelectedControlPointIndices = [value.Value];
            }
        }

        /// <summary>
        /// SelectedControlPointIndices
        /// </summary>
        public int[]? SelectedControlPointIndices { get; set; }

        /// <summary>
        /// SelectedControlPoint
        /// </summary>
        public Vector<double>? SelectedControlPoint
        {
            get
            {
                if (SelectedControlPointIndex == null)
                    return null;

                return controlPoints[SelectedControlPointIndex.Value];
            }
        }

        /// <summary>
        /// Matrix
        /// </summary>
        public Vector<double> this[int index]
        {
            get => controlPoints[index];
            set => controlPoints[index] = value;
        }

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public CurveBase(List<Vector<double>> parControlPoints, CoordTransformations parCoordTrans)
        {
            if (parControlPoints.Count < 4)
                throw new ArgumentException("Control points error");

            controlPoints = parControlPoints;
            segmentLengths = [];
            curvePoints = [];
            SelectedControlPointIndices = [];
            ct = parCoordTrans;

            RecalculateCurve();
        }

        #endregion

        /// <summary>
        /// Is point hit by U, V coordinates
        /// </summary>
        private bool IsHitByUV(Vector<double> controlPoint, Point p)
        {
            Vector<double> xyMatrix = ct.FromUVtoXYVectorDouble(p);
            PointF xyPoint = new((float)xyMatrix[0] - 2, (float)xyMatrix[1] - 2);
            RectangleF r = new(xyPoint, new Size(4, 4));
            PointF point = new((float)controlPoint[0], (float)controlPoint[1]);
            return r.Contains(point);
        }
        /// <summary>
        /// Prepocitanie krivky podla potreby
        /// </summary>
        protected abstract void RecalculateCurve();

        public abstract Vector<double> GetPointAndAngleOnCurve(float time, out float angle);

        #region Public methods

        /// <summary>
        /// Add
        /// </summary>
        public void Add(Vector<double> point)
        {
            controlPoints.Add(point);

            RecalculateCurve();
        }

        /// <summary>
        /// Insert
        /// </summary>
        public void Insert(int index, Vector<double> point)
        {
            controlPoints.Insert(index, point);

            RecalculateCurve();
        }

        /// <summary>
        /// Remove
        /// </summary>
        public Vector<double> Remove(int index)
        {
            Vector<double> removedPoint = controlPoints[index];
            controlPoints.RemoveAt(index);

            RecalculateCurve();

            return removedPoint;
        }

        /// <summary>
        /// Get vertex ID by U, V coordinates
        /// </summary>
        public int? GetVertexIDByUV(Point p)
        {
            for (int i = controlPoints.Count - 1; i >= 0; i--)
            {
                if (IsHitByUV(controlPoints[i], p))
                    return i;
            }
            return null;
        }

        /// <summary>
        /// Move
        /// </summary>
        public void Move(Vector<double> p, int? vID)
        {
            if (vID == null)
                return;

            controlPoints[vID.Value] = p;

            RecalculateCurve();
        }

        #endregion
    }
}

