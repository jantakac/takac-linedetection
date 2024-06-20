using System;
using System.Drawing;

namespace LineDetection.GraphicalObjects
{
    public static class DrawingPrimitives
    {
        /// <summary>
        /// Draw1x1Dot
        /// </summary>
        public static void Draw1x1Dot(Graphics g, Point location, Color c)
        {
            using (var brush = new SolidBrush(c))
            {
                g.FillRectangle(brush, location.X, location.Y, 1, 1);
            }
        }

        /// <summary>
        /// Draw2x2Dot
        /// </summary>
        public static void Draw2x2Dot(Graphics g, PointF location, Color c)
        {
            using (var brush = new SolidBrush(c))
            {
                g.FillRectangle(brush, location.X, location.Y, 1, 1);
                g.FillRectangle(brush, location.X, location.Y + 1, 1, 1);
                g.FillRectangle(brush, location.X + 1, location.Y, 1, 1);
                g.FillRectangle(brush, location.X + 1, location.Y + 1, 1, 1);
            }
        }

        /// <summary>
        /// DrawAntialiasedDot
        /// </summary>
        public static void DrawAntialiasedDot(Graphics g, PointF location, Color c)
        {
            float xLeft = location.X - 1.0f;
            float xRight = location.X + 1.0f;
            float yTop = location.Y - 1.0f;
            float yBottom = location.Y + 1.0f;

            float xLeftOverhang = (float)Math.Ceiling(xLeft) - xLeft;
            float xRightOverhang = xRight - (float)Math.Floor(xRight);
            float yTopOverhang = (float)Math.Ceiling(yTop) - yTop;
            float yBottomOverhang = yBottom - (float)Math.Floor(yBottom);

            int x = (int)location.X;
            int y = (int)location.Y;

            // Antialiased pixel takes up 3 x 3 grid, with the middle pixel fully opaque. Other pixels are made partially transparent.
            // Upper row
            Draw1x1Dot(g, new Point(x - 1, y - 1), BuildTransparentColor(c, xLeftOverhang * yTopOverhang));
            Draw1x1Dot(g, new Point(x, y - 1), BuildTransparentColor(c, yTopOverhang));
            Draw1x1Dot(g, new Point(x + 1, y - 1), BuildTransparentColor(c, xRightOverhang * yTopOverhang));

            // Middle row
            Draw1x1Dot(g, new Point(x - 1, y), BuildTransparentColor(c, xLeftOverhang));
            Draw1x1Dot(g, new Point(x, y), c);
            Draw1x1Dot(g, new Point(x + 1, y), BuildTransparentColor(c, xRightOverhang));

            // Lower row
            Draw1x1Dot(g, new Point(x - 1, y + 1), BuildTransparentColor(c, xLeftOverhang * yBottomOverhang));
            Draw1x1Dot(g, new Point(x, y + 1), BuildTransparentColor(c, yBottomOverhang));
            Draw1x1Dot(g, new Point(x + 1, y + 1), BuildTransparentColor(c, xRightOverhang * yBottomOverhang));
        }

        /// <summary>
        /// BuildTransparentColor
        /// </summary>
        private static Color BuildTransparentColor(Color c, float opacity)
        {
            var result = Color.FromArgb((int)(c.A * opacity), c.R, c.G, c.B);
            return result;
        }
    }
}

