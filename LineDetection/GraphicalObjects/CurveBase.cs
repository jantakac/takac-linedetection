using System.Collections.Generic;
using System.Drawing;

namespace Editor2D.Drawing2DMath
{
    public abstract class CurveBase
    {
        #region Properties

        protected readonly List<Matrix> controlPoints;
        protected List<float> segmentLengths;
        protected int curvePrecision = 70;
        protected float length = 0;
        protected List<Matrix> curvePoints;

        /// <summary>
        /// Curve length
        /// </summary>
        public float Length { get { return length; } private set { } }

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
        public List<Matrix> ControlPoints
        {
            get
            {
                var newList = new List<Matrix>(controlPoints.Count);

                foreach (Matrix point in controlPoints)
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
            internal set
            {
                if (value == null)
                    SelectedControlPointIndices = null;
                else
                    SelectedControlPointIndices = new int[] { value.Value };
            }
        }

        /// <summary>
        /// SelectedControlPointIndices
        /// </summary>
        public int[] SelectedControlPointIndices { get; set; } = null;

        /// <summary>
        /// SelectedControlPoint
        /// </summary>
        public Matrix SelectedControlPoint
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
        public Matrix this[int index]
        {
            get => controlPoints[index];
            set => controlPoints[index] = value;
        }

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public CurveBase()
        {
            controlPoints = new List<Matrix>();
        }

        #endregion

        /// <summary>
        /// Is point hit by U, V coordinates
        /// </summary>
        private bool IsHitByUV(Matrix controlPoint, Point p)
        {
            Matrix xyMatrix = Math2DTools.GetXY(p);
            PointF xyPoint = new PointF((float)xyMatrix[0, 0] - 2, (float)xyMatrix[1, 0] - 2);
            RectangleF r = new RectangleF(xyPoint, new Size(4, 4));
            PointF point = new PointF((float)controlPoint[0, 0], (float)controlPoint[1, 0]);
            return r.Contains(point);
        }
        /// <summary>
        /// Prepocitanie krivky podla potreby
        /// </summary>
        protected abstract void RecalculateCurve();
        public abstract Matrix GetPointAndAngleOnCurve(float time, out float angle);

        #region Public methods

        /// <summary>
        /// Add
        /// </summary>
        public void Add(Matrix point)
        {
            controlPoints.Add(point);

            RecalculateCurve();
        }

        /// <summary>
        /// Insert
        /// </summary>
        public void Insert(int index, Matrix point)
        {
            controlPoints.Insert(index, point);

            RecalculateCurve();
        }

        /// <summary>
        /// Remove
        /// </summary>
        public Matrix Remove(int index)
        {
            Matrix removedPoint = controlPoints[index];
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
        public void Move(Matrix p, int? vID)
        {
            if (vID == null)
                return;

            controlPoints[vID.Value] = p;

            RecalculateCurve();
        }

        #endregion
    }
}

