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
            menuStrip1.SuspendLayout();
            panelLeft.SuspendLayout();
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
            menuStrip1.Size = new Size(1444, 24);
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
            // numericUpDownResizeWidth
            // 
            numericUpDownResizeWidth.Location = new Point(188, 111);
            numericUpDownResizeWidth.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numericUpDownResizeWidth.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownResizeWidth.Name = "numericUpDownResizeWidth";
            numericUpDownResizeWidth.Size = new Size(80, 23);
            numericUpDownResizeWidth.TabIndex = 16;
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
            numericUpDownResizeHeight.Value = new decimal(new int[] { 128, 0, 0, 0 });
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
            checkBoxResizeImage.Location = new Point(5, 201);
            checkBoxResizeImage.Name = "checkBoxResizeImage";
            checkBoxResizeImage.Size = new Size(94, 19);
            checkBoxResizeImage.TabIndex = 13;
            checkBoxResizeImage.Text = "Resize image";
            checkBoxResizeImage.UseVisualStyleBackColor = true;
            checkBoxResizeImage.CheckedChanged += checkBoxResizeImage_CheckedChanged;
            // 
            // textBoxSigma
            // 
            textBoxSigma.Location = new Point(186, 228);
            textBoxSigma.Name = "textBoxSigma";
            textBoxSigma.Size = new Size(82, 23);
            textBoxSigma.TabIndex = 12;
            textBoxSigma.Text = "1.5";
            textBoxSigma.TextAlign = HorizontalAlignment.Right;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(3, 401);
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
            label5.Location = new Point(5, 172);
            label5.Name = "label5";
            label5.Size = new Size(126, 15);
            label5.TabIndex = 10;
            label5.Text = "Process every n-th line";
            // 
            // checkBoxSobelEdge
            // 
            checkBoxSobelEdge.AutoSize = true;
            checkBoxSobelEdge.Location = new Point(5, 299);
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
            checkBoxFitBezier.Location = new Point(5, 333);
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
            checkBoxHistogram.Location = new Point(5, 367);
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
            numericUpDownWidth.Value = new decimal(new int[] { 512, 0, 0, 0 });
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
            numericUpDownHeight.Value = new decimal(new int[] { 512, 0, 0, 0 });
            numericUpDownHeight.ValueChanged += NumericUpDownHeight_ValueChanged;
            // 
            // numericUpDownStep
            // 
            numericUpDownStep.Location = new Point(188, 170);
            numericUpDownStep.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownStep.Name = "numericUpDownStep";
            numericUpDownStep.Size = new Size(80, 23);
            numericUpDownStep.TabIndex = 7;
            numericUpDownStep.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownStep.ValueChanged += NumericUpDownRadius_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(140, 231);
            label4.Name = "label4";
            label4.Size = new Size(40, 15);
            label4.TabIndex = 6;
            label4.Text = "Sigma";
            // 
            // checkBoxOtsuTreshold
            // 
            checkBoxOtsuTreshold.AutoSize = true;
            checkBoxOtsuTreshold.Location = new Point(5, 266);
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
            checkBoxGaussianBlur.Location = new Point(5, 233);
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
            panelDrawing.Size = new Size(1169, 835);
            panelDrawing.TabIndex = 0;
            // 
            // doubleBufferedPanelDrawing
            // 
            doubleBufferedPanelDrawing.Dock = DockStyle.Fill;
            doubleBufferedPanelDrawing.Location = new Point(0, 0);
            doubleBufferedPanelDrawing.Name = "doubleBufferedPanelDrawing";
            doubleBufferedPanelDrawing.Size = new Size(1169, 835);
            doubleBufferedPanelDrawing.TabIndex = 0;
            doubleBufferedPanelDrawing.Paint += DoubleBufferedPanelDrawing_Paint;
            // 
            // panelBottomText
            // 
            panelBottomText.Controls.Add(textBoxMessages);
            panelBottomText.Dock = DockStyle.Bottom;
            panelBottomText.Location = new Point(275, 859);
            panelBottomText.Name = "panelBottomText";
            panelBottomText.Size = new Size(1169, 125);
            panelBottomText.TabIndex = 0;
            // 
            // textBoxMessages
            // 
            textBoxMessages.BorderStyle = BorderStyle.FixedSingle;
            textBoxMessages.Dock = DockStyle.Fill;
            textBoxMessages.Location = new Point(0, 0);
            textBoxMessages.Multiline = true;
            textBoxMessages.Name = "textBoxMessages";
            textBoxMessages.ScrollBars = ScrollBars.Horizontal;
            textBoxMessages.Size = new Size(1169, 125);
            textBoxMessages.TabIndex = 0;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1444, 984);
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
        private TextBox textBoxMessages;
        private Button buttonRefresh;
        private CheckBox checkBoxFitBezier;
        private TextBox textBoxSigma;
        private Tools.DoubleBufferedPanel doubleBufferedPanelDrawing;
        private CheckBox checkBoxResizeImage;
        private NumericUpDown numericUpDownResizeWidth;
        private NumericUpDown numericUpDownResizeHeight;
        private Label label6;
        private Label label7;
    }
}
