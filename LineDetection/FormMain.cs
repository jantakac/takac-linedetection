using LineDetection.DrawingClasses;
using LineDetection.MathImageProcessing;
using System.Data;
using System.Diagnostics;

namespace LineDetection
{
    public partial class FormMain : Form
    {
        private readonly Stopwatch stopwatch = new();

        private int imageWidth;
        private int imageHeight;
        private int radius;
        private int[]? curvePoints;

        private GrayscaleByteImage? baseImage;
        private GrayscaleByteImage? processedImage;

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

        private void DoubleBufferedPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (baseImage != null)
            {
                using Bitmap bitmap = baseImage.ToBitmap();
                g.DrawImage(bitmap, new Point(0, 0));
            }

            if (processedImage != null)
            {
                using Bitmap bitmap = processedImage.ToBitmap();
                using Graphics gb = Graphics.FromImage(bitmap);

                if (curvePoints != null)
                {
                    for (int i = 0; i < curvePoints.Length; i += 2)
                    {
                        Rectangle r = new(new Point(curvePoints[i], curvePoints[i + 1]), new Size(3, 3));
                        gb.FillRectangle(Brushes.Yellow, r);
                    }
                }

                g.DrawImage(bitmap, new Point(imageWidth + 50, 0));
            }

            if (checkBoxHistogram.Checked)
            {
                if (baseImage != null)
                {
                    double[] histogramBase = baseImage.Histogram;
                    using Bitmap bitmap = new(2 * histogramBase.Length, 320);

                    // histogram processedImage
                    for (int x = 0; x < histogramBase.Length; x++)
                    {
                        for (int y = 0; y < (int)(histogramBase[x] * 300.0); y++)
                        {
                            bitmap.SetPixel(2 * x, y + 14, Color.OrangeRed);
                            bitmap.SetPixel(2 * x + 1, y + 14, Color.OrangeRed);
                        }
                    }

                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

                    using Graphics gb = Graphics.FromImage(bitmap);
                    gb.DrawLine(Pens.Gray, new Point(0, bitmap.Height - 15), new Point(bitmap.Width, bitmap.Height - 15));
                    gb.DrawLine(Pens.Gray, new Point(0, bitmap.Height - 16), new Point(bitmap.Width, bitmap.Height - 16));

                    g.DrawImage(bitmap, new Point(0, imageHeight));
                }

                if (processedImage != null)
                {
                    double[] histogramProcessed = processedImage.Histogram;
                    using Bitmap bitmap = new(2 * histogramProcessed.Length, 320);

                    // histogram processedImage
                    for (int x = 0; x < histogramProcessed.Length; x++)
                    {
                        for (int y = 0; y < (int)(histogramProcessed[x] * 300.0); y++)
                        {
                            bitmap.SetPixel(2 * x, y + 14, Color.Green);
                            bitmap.SetPixel(2 * x + 1, y + 14, Color.Green);
                        }
                    }

                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

                    using Graphics gb = Graphics.FromImage(bitmap);
                    gb.DrawLine(Pens.Gray, new Point(0, bitmap.Height - 15), new Point(bitmap.Width, bitmap.Height - 15));
                    gb.DrawLine(Pens.Gray, new Point(0, bitmap.Height - 16), new Point(bitmap.Width, bitmap.Height - 16));

                    g.DrawImage(bitmap, new Point(imageWidth + 50, imageHeight));
                }
            }
        }

        /// <summary>
        /// comboBox1_SelectedIndexChanged
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadAndDisplay();
        }

        /// <summary>
        /// FormMain_Load
        /// </summary>
        private void FormMain_Load(object sender, EventArgs e)
        {
            stopwatch.Start();

            string path = @"C:\\Users\\HP\\Desktop\\GrayscaleImages";
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

            string? selectedString = comboBox1.SelectedValue as string;

            if (string.IsNullOrEmpty(selectedString))
                return;

            byte[] imageBytes;

            try
            {
                imageBytes = File.ReadAllBytes(selectedString);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error loading file !", MessageBoxButtons.OK);
                return;
            }

            imageWidth = (int)numericUpDownWidth.Value;
            imageHeight = (int)numericUpDownHeight.Value;
            radius = (int)numericUpDownRadius.Value;

            baseImage = new GrayscaleByteImage(imageWidth, imageHeight);

            for (int y = 0; y < imageHeight; y++)
            {
                for (int x = 0; x < imageWidth; x++)
                {
                    baseImage.Data[x + imageWidth * y] = imageBytes[x + imageWidth * y];
                }
            }

            if (checkBoxGaussianBlur.Checked)
            {
                processedImage = GaussianBlurGrayscale.Process(baseImage, radius);
            }

            if (checkBoxOtsuTreshold.Checked)
            {
                processedImage = OtsuTresholding.Process(processedImage != null ? processedImage : baseImage);
            }

            if (checkBoxSobelEdge.Checked)
            {
                curvePoints = SobelEdgeDetection.Process(processedImage != null ? processedImage : baseImage);
            }

            var ts1 = Stopwatch.GetElapsedTime(0);


            BezierCurveFitting bcf = new();
            bcf.DoIt();

            var ts2 = Stopwatch.GetElapsedTime(0);

            Debug.WriteLine((ts2 - ts1).TotalMilliseconds);

            doubleBufferedPanel.Invalidate();
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
    }
}
