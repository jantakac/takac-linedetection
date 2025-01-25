namespace LineDetection
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            panelLeft = new Panel();
            checkBoxResizeWithGauss = new CheckBox();
            groupBoxGaussianBlur = new GroupBox();
            radioButtonMethod3 = new RadioButton();
            radioButtonMethod2 = new RadioButton();
            radioButtonMethod1 = new RadioButton();
            numericUpDownRadius = new NumericUpDown();
            label8 = new Label();
            numericUpDownResizeWidth = new NumericUpDown();
            numericUpDownResizeHeight = new NumericUpDown();
            label6 = new Label();
            label7 = new Label();
            checkBoxResizeImage = new CheckBox();
            textBoxSigma = new TextBox();
            buttonRefresh = new Button();
            label5 = new Label();
            checkBoxSobelEdge = new CheckBox();
            checkBoxFitBezier = new CheckBox();
            checkBoxHistogram = new CheckBox();
            numericUpDownWidth = new NumericUpDown();
            numericUpDownHeight = new NumericUpDown();
            numericUpDownStep = new NumericUpDown();
            label4 = new Label();
            checkBoxOtsuTreshold = new CheckBox();
            checkBoxGaussianBlur = new CheckBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            comboBox1 = new ComboBox();
            panelDrawing = new Panel();
            doubleBufferedPanelDrawing = new Tools.DoubleBufferedPanel();
            panelBottomText = new Panel();
            textBoxMessages = new TextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            checkBoxDebugValues = new CheckBox();
            menuStrip1.SuspendLayout();
            panelLeft.SuspendLayout();
            groupBoxGaussianBlur.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRadius).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownResizeWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownResizeHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStep).BeginInit();
            panelDrawing.SuspendLayout();
            panelBottomText.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1572, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(92, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // panelLeft
            // 
            panelLeft.BorderStyle = BorderStyle.FixedSingle;
            panelLeft.Controls.Add(checkBoxDebugValues);
            panelLeft.Controls.Add(checkBoxResizeWithGauss);
            panelLeft.Controls.Add(groupBoxGaussianBlur);
            panelLeft.Controls.Add(numericUpDownRadius);
            panelLeft.Controls.Add(label8);
            panelLeft.Controls.Add(numericUpDownResizeWidth);
            panelLeft.Controls.Add(numericUpDownResizeHeight);
            panelLeft.Controls.Add(label6);
            panelLeft.Controls.Add(label7);
            panelLeft.Controls.Add(checkBoxResizeImage);
            panelLeft.Controls.Add(textBoxSigma);
            panelLeft.Controls.Add(buttonRefresh);
            panelLeft.Controls.Add(label5);
            panelLeft.Controls.Add(checkBoxSobelEdge);
            panelLeft.Controls.Add(checkBoxFitBezier);
            panelLeft.Controls.Add(checkBoxHistogram);
            panelLeft.Controls.Add(numericUpDownWidth);
            panelLeft.Controls.Add(numericUpDownHeight);
            panelLeft.Controls.Add(numericUpDownStep);
            panelLeft.Controls.Add(label4);
            panelLeft.Controls.Add(checkBoxOtsuTreshold);
            panelLeft.Controls.Add(checkBoxGaussianBlur);
            panelLeft.Controls.Add(label3);
            panelLeft.Controls.Add(label2);
            panelLeft.Controls.Add(label1);
            panelLeft.Controls.Add(comboBox1);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 24);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(275, 960);
            panelLeft.TabIndex = 1;
            // 
            // checkBoxResizeWithGauss
            // 
            checkBoxResizeWithGauss.AutoSize = true;
            checkBoxResizeWithGauss.Location = new Point(105, 201);
            checkBoxResizeWithGauss.Name = "checkBoxResizeWithGauss";
            checkBoxResizeWithGauss.Size = new Size(68, 19);
            checkBoxResizeWithGauss.TabIndex = 22;
            checkBoxResizeWithGauss.Text = "+ Gauss";
            checkBoxResizeWithGauss.UseVisualStyleBackColor = true;
            checkBoxResizeWithGauss.CheckedChanged += CheckBoxResizeWithGauss_CheckedChanged;
            // 
            // groupBoxGaussianBlur
            // 
            groupBoxGaussianBlur.Controls.Add(radioButtonMethod3);
            groupBoxGaussianBlur.Controls.Add(radioButtonMethod2);
            groupBoxGaussianBlur.Controls.Add(radioButtonMethod1);
            groupBoxGaussianBlur.Location = new Point(24, 256);
            groupBoxGaussianBlur.Name = "groupBoxGaussianBlur";
            groupBoxGaussianBlur.Size = new Size(94, 98);
            groupBoxGaussianBlur.TabIndex = 21;
            groupBoxGaussianBlur.TabStop = false;
            groupBoxGaussianBlur.Text = "Filter method";
            // 
            // radioButtonMethod3
            // 
            radioButtonMethod3.AutoSize = true;
            radioButtonMethod3.Checked = true;
            radioButtonMethod3.Location = new Point(5, 72);
            radioButtonMethod3.Name = "radioButtonMethod3";
            radioButtonMethod3.Size = new Size(76, 19);
            radioButtonMethod3.TabIndex = 2;
            radioButtonMethod3.TabStop = true;
            radioButtonMethod3.Text = "Method 3";
            radioButtonMethod3.UseVisualStyleBackColor = true;
            radioButtonMethod3.CheckedChanged += RadioButtonMethod3_CheckedChanged;
            // 
            // radioButtonMethod2
            // 
            radioButtonMethod2.AutoSize = true;
            radioButtonMethod2.Location = new Point(5, 47);
            radioButtonMethod2.Name = "radioButtonMethod2";
            radioButtonMethod2.Size = new Size(76, 19);
            radioButtonMethod2.TabIndex = 1;
            radioButtonMethod2.Text = "Method 2";
            radioButtonMethod2.UseVisualStyleBackColor = true;
            radioButtonMethod2.CheckedChanged += RadioButtonMethod2_CheckedChanged;
            // 
            // radioButtonMethod1
            // 
            radioButtonMethod1.AutoSize = true;
            radioButtonMethod1.Location = new Point(5, 22);
            radioButtonMethod1.Name = "radioButtonMethod1";
            radioButtonMethod1.Size = new Size(76, 19);
            radioButtonMethod1.TabIndex = 0;
            radioButtonMethod1.Text = "Method 1";
            radioButtonMethod1.UseVisualStyleBackColor = true;
            radioButtonMethod1.CheckedChanged += RadioButtonMethod1_CheckedChanged;
            // 
            // numericUpDownRadius
            // 
            numericUpDownRadius.Location = new Point(188, 255);
            numericUpDownRadius.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDownRadius.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            numericUpDownRadius.Name = "numericUpDownRadius";
            numericUpDownRadius.Size = new Size(80, 23);
            numericUpDownRadius.TabIndex = 20;
            numericUpDownRadius.TextAlign = HorizontalAlignment.Right;
            numericUpDownRadius.Value = new decimal(new int[] { 3, 0, 0, 0 });
            numericUpDownRadius.ValueChanged += NumericUpDownRadius_ValueChanged_1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.Location = new Point(146, 258);
            label8.Name = "label8";
            label8.Size = new Size(39, 15);
            label8.TabIndex = 18;
            label8.Text = "radius";
            // 
            // numericUpDownResizeWidth
            // 
            numericUpDownResizeWidth.Location = new Point(188, 111);
            numericUpDownResizeWidth.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numericUpDownResizeWidth.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownResizeWidth.Name = "numericUpDownResizeWidth";
            numericUpDownResizeWidth.Size = new Size(80, 23);
            numericUpDownResizeWidth.TabIndex = 16;
            numericUpDownResizeWidth.TextAlign = HorizontalAlignment.Right;
            numericUpDownResizeWidth.Value = new decimal(new int[] { 128, 0, 0, 0 });
            // 
            // numericUpDownResizeHeight
            // 
            numericUpDownResizeHeight.Location = new Point(188, 140);
            numericUpDownResizeHeight.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numericUpDownResizeHeight.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownResizeHeight.Name = "numericUpDownResizeHeight";
            numericUpDownResizeHeight.Size = new Size(80, 23);
            numericUpDownResizeHeight.TabIndex = 17;
            numericUpDownResizeHeight.TextAlign = HorizontalAlignment.Right;
            numericUpDownResizeHeight.Value = new decimal(new int[] { 96, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(5, 142);
            label6.Name = "label6";
            label6.Size = new Size(99, 15);
            label6.TabIndex = 15;
            label6.Text = "Resize height (px)";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(5, 113);
            label7.Name = "label7";
            label7.Size = new Size(95, 15);
            label7.TabIndex = 14;
            label7.Text = "Resize width (px)";
            // 
            // checkBoxResizeImage
            // 
            checkBoxResizeImage.AutoSize = true;
            checkBoxResizeImage.Checked = true;
            checkBoxResizeImage.CheckState = CheckState.Checked;
            checkBoxResizeImage.Location = new Point(5, 201);
            checkBoxResizeImage.Name = "checkBoxResizeImage";
            checkBoxResizeImage.Size = new Size(94, 19);
            checkBoxResizeImage.TabIndex = 13;
            checkBoxResizeImage.Text = "Resize image";
            checkBoxResizeImage.UseVisualStyleBackColor = true;
            checkBoxResizeImage.CheckedChanged += CheckBoxResizeImage_CheckedChanged;
            // 
            // textBoxSigma
            // 
            textBoxSigma.Location = new Point(186, 226);
            textBoxSigma.Name = "textBoxSigma";
            textBoxSigma.Size = new Size(82, 23);
            textBoxSigma.TabIndex = 12;
            textBoxSigma.Text = "3";
            textBoxSigma.TextAlign = HorizontalAlignment.Right;
            textBoxSigma.TextChanged += TextBoxSigma_TextChanged;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(4, 476);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(75, 23);
            buttonRefresh.TabIndex = 11;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += ButtonRefresh_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(5, 171);
            label5.Name = "label5";
            label5.Size = new Size(101, 15);
            label5.TabIndex = 10;
            label5.Text = "Number of points";
            // 
            // checkBoxSobelEdge
            // 
            checkBoxSobelEdge.AutoSize = true;
            checkBoxSobelEdge.Checked = true;
            checkBoxSobelEdge.CheckState = CheckState.Checked;
            checkBoxSobelEdge.Location = new Point(5, 392);
            checkBoxSobelEdge.Name = "checkBoxSobelEdge";
            checkBoxSobelEdge.Size = new Size(118, 19);
            checkBoxSobelEdge.TabIndex = 9;
            checkBoxSobelEdge.Text = "Apply Sobel edge";
            checkBoxSobelEdge.UseVisualStyleBackColor = true;
            checkBoxSobelEdge.CheckedChanged += CheckBoxSobelEdge_CheckedChanged;
            // 
            // checkBoxFitBezier
            // 
            checkBoxFitBezier.AutoSize = true;
            checkBoxFitBezier.Checked = true;
            checkBoxFitBezier.CheckState = CheckState.Checked;
            checkBoxFitBezier.Location = new Point(5, 420);
            checkBoxFitBezier.Name = "checkBoxFitBezier";
            checkBoxFitBezier.Size = new Size(137, 19);
            checkBoxFitBezier.TabIndex = 8;
            checkBoxFitBezier.Text = "Fit Bezier cubic curve";
            checkBoxFitBezier.UseVisualStyleBackColor = true;
            checkBoxFitBezier.CheckedChanged += CheckBoxFitBezier_CheckedChanged;
            // 
            // checkBoxHistogram
            // 
            checkBoxHistogram.AutoSize = true;
            checkBoxHistogram.Checked = true;
            checkBoxHistogram.CheckState = CheckState.Checked;
            checkBoxHistogram.Location = new Point(5, 448);
            checkBoxHistogram.Name = "checkBoxHistogram";
            checkBoxHistogram.Size = new Size(117, 19);
            checkBoxHistogram.TabIndex = 8;
            checkBoxHistogram.Text = "Show histograms";
            checkBoxHistogram.UseVisualStyleBackColor = true;
            checkBoxHistogram.CheckedChanged += CheckBoxHistogram_CheckedChanged;
            // 
            // numericUpDownWidth
            // 
            numericUpDownWidth.Location = new Point(188, 53);
            numericUpDownWidth.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
            numericUpDownWidth.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
            numericUpDownWidth.Name = "numericUpDownWidth";
            numericUpDownWidth.Size = new Size(80, 23);
            numericUpDownWidth.TabIndex = 7;
            numericUpDownWidth.TextAlign = HorizontalAlignment.Right;
            numericUpDownWidth.Value = new decimal(new int[] { 640, 0, 0, 0 });
            numericUpDownWidth.ValueChanged += NumericUpDownWidth_ValueChanged;
            // 
            // numericUpDownHeight
            // 
            numericUpDownHeight.Location = new Point(188, 82);
            numericUpDownHeight.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
            numericUpDownHeight.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
            numericUpDownHeight.Name = "numericUpDownHeight";
            numericUpDownHeight.Size = new Size(80, 23);
            numericUpDownHeight.TabIndex = 7;
            numericUpDownHeight.TextAlign = HorizontalAlignment.Right;
            numericUpDownHeight.Value = new decimal(new int[] { 480, 0, 0, 0 });
            numericUpDownHeight.ValueChanged += NumericUpDownHeight_ValueChanged;
            // 
            // numericUpDownStep
            // 
            numericUpDownStep.Location = new Point(188, 169);
            numericUpDownStep.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownStep.Name = "numericUpDownStep";
            numericUpDownStep.Size = new Size(80, 23);
            numericUpDownStep.TabIndex = 7;
            numericUpDownStep.TextAlign = HorizontalAlignment.Right;
            numericUpDownStep.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownStep.ValueChanged += NumericUpDownRadius_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(169, 227);
            label4.Name = "label4";
            label4.Size = new Size(19, 21);
            label4.TabIndex = 6;
            label4.Text = "σ";
            // 
            // checkBoxOtsuTreshold
            // 
            checkBoxOtsuTreshold.AutoSize = true;
            checkBoxOtsuTreshold.Checked = true;
            checkBoxOtsuTreshold.CheckState = CheckState.Checked;
            checkBoxOtsuTreshold.Location = new Point(5, 364);
            checkBoxOtsuTreshold.Name = "checkBoxOtsuTreshold";
            checkBoxOtsuTreshold.Size = new Size(131, 19);
            checkBoxOtsuTreshold.TabIndex = 5;
            checkBoxOtsuTreshold.Text = "Apply Otsu treshold";
            checkBoxOtsuTreshold.UseVisualStyleBackColor = true;
            checkBoxOtsuTreshold.CheckedChanged += CheckBoxOtsuTreshold_CheckedChanged;
            // 
            // checkBoxGaussianBlur
            // 
            checkBoxGaussianBlur.AutoSize = true;
            checkBoxGaussianBlur.Checked = true;
            checkBoxGaussianBlur.CheckState = CheckState.Checked;
            checkBoxGaussianBlur.Location = new Point(5, 229);
            checkBoxGaussianBlur.Name = "checkBoxGaussianBlur";
            checkBoxGaussianBlur.Size = new Size(131, 19);
            checkBoxGaussianBlur.TabIndex = 5;
            checkBoxGaussianBlur.Text = "Apply Gaussian blur";
            checkBoxGaussianBlur.UseVisualStyleBackColor = true;
            checkBoxGaussianBlur.CheckedChanged += CheckBoxGaussianBlur_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 84);
            label3.Name = "label3";
            label3.Size = new Size(66, 15);
            label3.TabIndex = 3;
            label3.Text = "Height (px)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 55);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 2;
            label2.Text = "Width (px)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 5);
            label1.Name = "label1";
            label1.Size = new Size(200, 15);
            label1.TabIndex = 1;
            label1.Text = "Choose grayscale image (only NV12)";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(3, 23);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(265, 23);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // panelDrawing
            // 
            panelDrawing.Controls.Add(doubleBufferedPanelDrawing);
            panelDrawing.Dock = DockStyle.Fill;
            panelDrawing.Location = new Point(275, 24);
            panelDrawing.Name = "panelDrawing";
            panelDrawing.Size = new Size(1297, 835);
            panelDrawing.TabIndex = 0;
            // 
            // doubleBufferedPanelDrawing
            // 
            doubleBufferedPanelDrawing.Dock = DockStyle.Fill;
            doubleBufferedPanelDrawing.Location = new Point(0, 0);
            doubleBufferedPanelDrawing.Name = "doubleBufferedPanelDrawing";
            doubleBufferedPanelDrawing.Size = new Size(1297, 835);
            doubleBufferedPanelDrawing.TabIndex = 0;
            doubleBufferedPanelDrawing.Paint += DoubleBufferedPanelDrawing_Paint;
            // 
            // panelBottomText
            // 
            panelBottomText.Controls.Add(textBoxMessages);
            panelBottomText.Dock = DockStyle.Bottom;
            panelBottomText.Location = new Point(275, 859);
            panelBottomText.Name = "panelBottomText";
            panelBottomText.Size = new Size(1297, 125);
            panelBottomText.TabIndex = 0;
            // 
            // textBoxMessages
            // 
            textBoxMessages.BorderStyle = BorderStyle.FixedSingle;
            textBoxMessages.Dock = DockStyle.Fill;
            textBoxMessages.Location = new Point(0, 0);
            textBoxMessages.Multiline = true;
            textBoxMessages.Name = "textBoxMessages";
            textBoxMessages.ScrollBars = ScrollBars.Vertical;
            textBoxMessages.Size = new Size(1297, 125);
            textBoxMessages.TabIndex = 0;
            // 
            // checkBoxDebugValues
            // 
            checkBoxDebugValues.AutoSize = true;
            checkBoxDebugValues.Checked = true;
            checkBoxDebugValues.CheckState = CheckState.Checked;
            checkBoxDebugValues.Location = new Point(128, 448);
            checkBoxDebugValues.Name = "checkBoxDebugValues";
            checkBoxDebugValues.Size = new Size(97, 19);
            checkBoxDebugValues.TabIndex = 23;
            checkBoxDebugValues.Text = "Debug values";
            checkBoxDebugValues.UseVisualStyleBackColor = true;
            checkBoxDebugValues.CheckedChanged += CheckBoxDebugValues_CheckedChanged;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1572, 984);
            Controls.Add(panelDrawing);
            Controls.Add(panelBottomText);
            Controls.Add(panelLeft);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Line detection";
            Load += FormMain_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            groupBoxGaussianBlur.ResumeLayout(false);
            groupBoxGaussianBlur.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRadius).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownResizeWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownResizeHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStep).EndInit();
            panelDrawing.ResumeLayout(false);
            panelBottomText.ResumeLayout(false);
            panelBottomText.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Panel panelLeft;
        private Panel panelDrawing;
        private ComboBox comboBox1;
        private Label label1;
        private Label label3;
        private Label label2;
        private Label label4;
        private CheckBox checkBoxGaussianBlur;
        private NumericUpDown numericUpDownWidth;
        private NumericUpDown numericUpDownHeight;
        private CheckBox checkBoxOtsuTreshold;
        private CheckBox checkBoxHistogram;
        private CheckBox checkBoxSobelEdge;
        private Label label5;
        private NumericUpDown numericUpDownStep;
        private Panel panelBottomText;
        private Button buttonRefresh;
        private CheckBox checkBoxFitBezier;
        private TextBox textBoxSigma;
        private Tools.DoubleBufferedPanel doubleBufferedPanelDrawing;
        private CheckBox checkBoxResizeImage;
        private NumericUpDown numericUpDownResizeWidth;
        private NumericUpDown numericUpDownResizeHeight;
        private Label label6;
        private Label label7;
        private Label label8;
        private NumericUpDown numericUpDownRadius;
        private GroupBox groupBoxGaussianBlur;
        private RadioButton radioButtonMethod3;
        private RadioButton radioButtonMethod2;
        private RadioButton radioButtonMethod1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private CheckBox checkBoxResizeWithGauss;
        public TextBox textBoxMessages;
        private CheckBox checkBoxDebugValues;
    }
}
