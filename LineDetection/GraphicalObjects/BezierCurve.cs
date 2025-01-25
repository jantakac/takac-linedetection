using LineDetection.Tools;
using MathNet.Numerics.LinearAlgebra;

namespace LineDetection.GraphicalObjects
{
    /// <summary>
    /// Konstruktor
    /// </summary>
    public sealed class BezierCurve(List<Vector<float>> parControlPoints) : CurveBase(parControlPoints)
    {
        /// <summary>
        /// Prepocitanie krivky podla potreby
        /// </summary>
        protected override void RecalculateCurve()
        {
            // get curve points
            curvePoints = DeCasteljau.GetCurvePoints(controlPoints, curvePrecision);

            if (curvePoints == null)
                return;

            // calculate curve length
            segmentLengths = [];

            for (int i = 0; i < curvePoints.Count - 1; i++)
                segmentLengths.Add(CoordTrans.CalculateDistanceBetweenPoints(curvePoints[i], curvePoints[i + 1]));

            length = segmentLengths.Sum();
        }

        /// <summary>
        /// GetPointAndAngleOnCurve
        /// </summary>
		public override Vector<float> GetPointAndAngleOnCurve(float time, out float angle)
        {
            return DeCasteljau.GetDeCasteljauPoint(controlPoints, time, out angle);
        }

        /// <summary>
        /// Draw
        /// </summary>
        public void Draw(Graphics g)
        {
            if (curvePoints == null || controlPoints == null || controlPoints.Count < 4)
                return;

            // Draw lines between control points for better visibility
            for (int i = 0; i < curvePoints.Count - 1; i++)
            {
                PointF point1 = CoordTrans.GetUVF(curvePoints[i]);
                PointF point2 = CoordTrans.GetUVF(curvePoints[i + 1]);

                g.DrawLine(Pens.Pink, point1, point2);
                DrawingPrimitives.DrawAntialiasedDot(g, point1, Color.LimeGreen);
            }

            // Draw lines between control points for better visibility
            float[] dashValues = [10, 6];

            using (Pen pen = new(Color.LightGray))
            {
                for (int i = 0; i < controlPoints.Count - 1; i++)
                {
                    PointF point1 = CoordTrans.GetUVF(controlPoints[i]);
                    PointF point2 = CoordTrans.GetUVF(controlPoints[i + 1]);

                    pen.DashPattern = dashValues;
                    g.DrawLine(pen, point1, point2);
                }
            }

            // Draw control point anchors
            using var font = new Font("Arial", 10);
            // Draw first control point
            Point controlPoint = CoordTrans.GetUV(controlPoints[0]);
            Point point = new(controlPoint.X - 5, controlPoint.Y - 5);
            Rectangle rect = new(point, new Size(10, 10));
            g.FillRectangle(ControlPointIsSelected(0) ? Brushes.LimeGreen : Brushes.OrangeRed, rect);
            g.DrawRectangle(Pens.Black, rect);
            g.DrawString("V start", font, Brushes.Orange, new Point(rect.X + 10, rect.Y - 10));

            // Draw intermediate control points
            for (int i = 1; i < controlPoints.Count - 1; i++)
            {
                controlPoint = CoordTrans.GetUV(controlPoints[i]);
                point = new Point(controlPoint.X - 5, controlPoint.Y - 5);
                rect = new Rectangle(point, new Size(10, 10));
                g.FillRectangle(ControlPointIsSelected(i) ? Brushes.LimeGreen : Brushes.DarkOrange, rect);
                g.DrawRectangle(Pens.Black, rect);
                g.DrawString("C" + i.ToString(), font, Brushes.Orange, new Point(rect.X + 10, rect.Y + 10));
            }

            // Draw last control point
            controlPoint = CoordTrans.GetUV(controlPoints[controlPoints.Count - 1]);
            point = new Point(controlPoint.X - 5, controlPoint.Y - 5);
            rect = new Rectangle(point, new Size(10, 10));
            g.FillRectangle(ControlPointIsSelected(controlPoints.Count - 1) ? Brushes.LimeGreen : Brushes.OrangeRed, rect);
            g.DrawRectangle(Pens.Black, rect);
            g.DrawString("V end", font, Brushes.Orange, new Point(rect.X + 10, rect.Y + 10));
        }
    }
}

