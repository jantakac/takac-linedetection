using LineDetection.GraphicalObjects;
using LineDetection.MathImageProcessing;
using LineDetection.Tools;
using MathNet.Numerics.LinearAlgebra;
using System.Data;
using System.Diagnostics;

namespace LineDetection
{
    public partial class FormMain : Form
    {
        private readonly Stopwatch stopwatch = new();

        private int imageWidth;
        private int imageHeight;
        private int resizeWidth;
        private int resizeHeight;
        private float sigma;
        private int radius;
        private int step;
        private int[]? curvePoints;

        private YUVImage? baseImage;
        private YUVImage? processedImage;
        private CoordTransformations? coordTransformations;
        private BezierCurve? bezierCurve;

        public FormMain()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |  // Do not erase the background, reduce flicker
                     ControlStyles.OptimizedDoubleBuffer | // Double buffering
                     ControlStyles.UserPaint,              // Use a custom redraw event to reduce flicker
                     true);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DoubleBufferedPanelDrawing_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (baseImage != null)
            {
                using Bitmap bitmap = baseImage.ToBitmap();
                g.DrawImage(bitmap, new Point(0, 0));

                //if (checkBoxHistogram.Checked)
                //{
                //    using Bitmap? histogramBitpam = DrawHistogram(baseImage);

                //    if (histogramBitpam != null)
                //        g.DrawImage(histogramBitpam, new Point(0, imageHeight));
                //}
            }

            if (processedImage != null)
            {
                using Bitmap bitmap = processedImage.ToBitmap();
                using Graphics gb = Graphics.FromImage(bitmap);

                if (curvePoints != null)
                {
                    for (int i = 0; i < curvePoints.Length; i += 2)
                    {
                        Rectangle r = new(new Point(curvePoints[i] - 2, curvePoints[i + 1] - 2), new Size(4, 4));
                        gb.FillRectangle(Brushes.Yellow, r);
                    }
                }

                // draw fitted bezier curve
                bezierCurve?.Draw(gb);

                g.DrawImage(bitmap, new Point(imageWidth + 10, 0));

                //if (checkBoxHistogram.Checked)
                //{
                //    using Bitmap? histogramBitpam = DrawHistogram(processedImage);

                //    if (histogramBitpam != null)
                //        g.DrawImage(histogramBitpam, new Point(imageWidth + 50, imageHeight));
                //}
            }
        }


        /// <summary>
        /// DrawHistogram
        /// </summary>
        //private static Bitmap? DrawHistogram(GrayscaleByteImage parImage)
        //{
        //    if (parImage == null)
        //        return null;

        //    Bitmap bitmap = new(512, 320);

        //    parImage.UpdateHistogramData();

        //    // histogram processedImage
        //    for (int x = 0; x < parImage.NormalisedHistogram.Length; x++)
        //    {
        //        int ymax = (int)Math.Round(parImage.NormalisedHistogram[x] * 300.0);

        //        for (int y = 0; y < ymax; y++)
        //        {
        //            bitmap.SetPixel(2 * x, y + 14, Color.Green);
        //            bitmap.SetPixel(2 * x + 1, y + 14, Color.Green);
        //        }
        //    }

        //    // cumulative histogram
        //    for (int x = 0; x < parImage.CumulativeNormalisedHistogram.Length; x++)
        //    {
        //        int y = (int)Math.Round(parImage.CumulativeNormalisedHistogram[x] * 300.0);

        //        bitmap.SetPixel(2 * x, y + 14, Color.Red);
        //        bitmap.SetPixel(2 * x + 1, y + 14, Color.Red);
        //    }

        //    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

        //    using Graphics gb = Graphics.FromImage(bitmap);
        //    gb.DrawLine(Pens.Gray, new Point(0, bitmap.Height - 15), new Point(bitmap.Width, bitmap.Height - 15));
        //    gb.DrawLine(Pens.Gray, new Point(0, bitmap.Height - 16), new Point(bitmap.Width, bitmap.Height - 16));

        //    using Font f = new("Arial Narrow", 8);

        //    for (int x = 0; x < bitmap.Width; x += 20)
        //    {
        //        gb.DrawLine(Pens.Gray, new Point(x, bitmap.Height - 14), new Point(x, bitmap.Height - 6));
        //        gb.DrawString((x / 2).ToString(), f, Brushes.Gray, new PointF(x, bitmap.Height - 14));
        //    }

        //    return bitmap;
        //}


        /// <summary>
        /// FormMain_Load
        /// </summary>
        private void FormMain_Load(object sender, EventArgs e)
        {
            stopwatch.Start();

            //string path = @"C:\\Users\\micha\\Desktop\\GrayscaleImages";
            string path = @"C:\\Users\\Michal Lekýr\\Desktop\\GrayscaleImages";
            DataTable table = new();
            table.Columns.Add("File Name");
            table.Columns.Add("File Path");

            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                FileInfo fileInfo = new(file);
                table.Rows.Add(fileInfo.Name, fileInfo.FullName);
            }

            comboBox1.DataSource = table;
            comboBox1.DisplayMember = "File Name";
            comboBox1.ValueMember = "File Path";

            string? selectedString = comboBox1.SelectedValue as string;

            if (string.IsNullOrEmpty(selectedString))
                return;

            ReloadAndDisplay();
        }

        /// <summary>
        /// LoadImageFromFile
        /// </summary>
        private void ReloadAndDisplay()
        {
            baseImage = null;
            processedImage = null;
            curvePoints = null;
            bezierCurve = null;

            Stopwatch stopwatch = new();
            TimeSpan elapsedTime;

            coordTransformations = new((int)numericUpDownWidth.Value, 0, 0, (int)numericUpDownHeight.Value, -500.0f, 0.0f, 500.0f, 1000.0f);

            imageWidth = (int)numericUpDownWidth.Value;
            imageHeight = (int)numericUpDownHeight.Value;

            resizeWidth = (int)numericUpDownResizeWidth.Value;
            resizeHeight = (int)numericUpDownResizeHeight.Value;

            string? selectedString = comboBox1.SelectedValue as string;

            if (string.IsNullOrEmpty(selectedString))
                return;

            int bytesToRead = imageWidth * imageHeight;
            byte[] imageBytes = new byte[bytesToRead];

            try
            {
                stopwatch.Restart();

                // Read specific bytes
                using FileStream fs = new(selectedString, FileMode.Open, FileAccess.Read);

                // Set the position to the desired offset
                fs.Seek(0, SeekOrigin.Begin);

                // Read the desired number of bytes
                int bytesRead = fs.Read(imageBytes, 0, bytesToRead);

                // If fewer bytes are read, adjust the buffer size
                if (bytesRead < bytesToRead)
                    Array.Resize(ref imageBytes, bytesRead);

                stopwatch.Stop();
                elapsedTime = stopwatch.Elapsed;
                textBoxMessages.AppendText($">>> Image load - Elapsed Time (ms): {elapsedTime.TotalMilliseconds}" + "\r\n");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error loading file !", MessageBoxButtons.OK);
                return;
            }

            if (!double.TryParse(textBoxSigma.Text.Replace('.', ','), out double sigmaDouble))
                throw new ArgumentOutOfRangeException(nameof(textBoxSigma));

            sigma = (float)sigmaDouble;
            radius = (int)numericUpDownRadius.Value;

            step = (int)numericUpDownStep.Value;

            baseImage = new YUVImage(imageBytes, imageWidth, imageHeight);

            if (checkBoxResizeImage.Checked)
            {
                stopwatch.Restart();
                processedImage = baseImage.ResizeGrayscaleImageBilinear(resizeWidth, resizeHeight);
                stopwatch.Stop();
                elapsedTime = stopwatch.Elapsed;
                textBoxMessages.AppendText($">>> Image resize - Elapsed Time (ms): {elapsedTime.TotalMilliseconds}" + "\r\n");
            }
            else
            {
                stopwatch.Restart();
                processedImage = (YUVImage)baseImage.Clone();
                stopwatch.Stop();
                elapsedTime = stopwatch.Elapsed;
                textBoxMessages.AppendText($">>> Image clone - Elapsed Time (ms): {elapsedTime.TotalMilliseconds}" + "\r\n");
            }

            if (checkBoxGaussianBlur1.Checked)
            {
                stopwatch.Restart();
                if (radioButtonMethod1.Checked)
                {
                    GaussianBlurGrayscale.Sigma = sigma;
                    GaussianBlurGrayscale.Radius = radius;
                    GaussianBlurGrayscale.Apply(processedImage);
                }
                else if (radioButtonMethod2.Checked)
                {
                    GaussianFilter2.Sigma = sigma;
                    GaussianFilter2.Radius = radius;
                    GaussianFilter2.Apply(ref processedImage);
                }
                else if (radioButtonMethod3.Checked)
                {
                    GaussianFilter3.Sigma = sigma;
                    GaussianFilter3.Apply(processedImage);
                }

                stopwatch.Stop();
                elapsedTime = stopwatch.Elapsed;
                textBoxMessages.AppendText($">>> Gaussian Blur Grayscale - Elapsed Time (ms): {elapsedTime.TotalMilliseconds}" + "\r\n");
            }

            if (checkBoxOtsuTreshold.Checked)
            {
                stopwatch.Restart();
                OtsuTreshold.Apply(processedImage);
                stopwatch.Stop();
                elapsedTime = stopwatch.Elapsed;

                textBoxMessages.AppendText($">>> Histogram + Otsu treshold - Elapsed Time (ms): {elapsedTime.TotalMilliseconds}" + "\r\n");
            }

            if (checkBoxSobelEdge.Checked)
            {
                stopwatch.Restart();
                curvePoints = SobelEdgeDetection.Process(processedImage, step);
                stopwatch.Stop();
                elapsedTime = stopwatch.Elapsed;
                textBoxMessages.AppendText($">>> Sobel edge detector - Elapsed Time (ms): {elapsedTime.TotalMilliseconds}" + "\r\n");
            }

            if (checkBoxFitBezier.Checked && curvePoints != null && curvePoints.Length > 3)
            {
                List<Vector<double>> floatPoints = [];

                for (int i = 0; i < curvePoints.Length; i += 2)
                {
                    floatPoints.Add(coordTransformations.FromUVtoXYVectorDouble(new Point(curvePoints[i], curvePoints[i + 1])));
                }

                floatPoints.Reverse();

                stopwatch.Start();

                Vector<double>[] controlPoints = BezierCurveFitting.FitCubicBezier(floatPoints.ToArray());

                if (controlPoints != null)
                    bezierCurve = new([.. controlPoints], coordTransformations);

                stopwatch.Stop();
                TimeSpan et = stopwatch.Elapsed;
                textBoxMessages.AppendText($">>> Bezier curve fit - Elapsed Time (ms): {et.TotalMilliseconds}" + "\r\n");
            }

            textBoxMessages.AppendText("\r\n");

            // repaint drawing
            doubleBufferedPanelDrawing.Invalidate();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void CheckBoxOtsuTreshold_CheckedChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void CheckBoxGaussianBlur_CheckedChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void CheckBoxHistogram_CheckedChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void CheckBoxFitBezier_CheckedChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void NumericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void NumericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void NumericUpDownRadius_ValueChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void CheckBoxSobelEdge_CheckedChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void CheckBoxResizeImage_CheckedChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void TextBoxSigma_TextChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void NumericUpDownRadius_ValueChanged_1(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        private void RadioButtonMethod1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonMethod1.Checked)
                ReloadAndDisplay();
        }

        private void RadioButtonMethod2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMethod2.Checked)
                ReloadAndDisplay();
        }

        private void RadioButtonMethod3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMethod3.Checked)
                ReloadAndDisplay();
        }
    }
}
