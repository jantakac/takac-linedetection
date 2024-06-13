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
            checkBoxHistogram = new CheckBox();
            numericUpDownWidth = new NumericUpDown();
            numericUpDownHeight = new NumericUpDown();
            numericUpDownRadius = new NumericUpDown();
            label4 = new Label();
            checkBoxOtsuTreshold = new CheckBox();
            checkBoxGaussianBlur = new CheckBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            comboBox1 = new ComboBox();
            panelDrawing = new Panel();
            doubleBufferedPanel = new Tools.DoubleBufferedPanel();
            menuStrip1.SuspendLayout();
            panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRadius).BeginInit();
            panelDrawing.SuspendLayout();
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
            exitToolStripMenuItem.Size = new Size(93, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // panelLeft
            // 
            panelLeft.BorderStyle = BorderStyle.FixedSingle;
            panelLeft.Controls.Add(checkBoxHistogram);
            panelLeft.Controls.Add(numericUpDownWidth);
            panelLeft.Controls.Add(numericUpDownHeight);
            panelLeft.Controls.Add(numericUpDownRadius);
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
            // checkBoxHistogram
            // 
            checkBoxHistogram.AutoSize = true;
            checkBoxHistogram.Location = new Point(5, 180);
            checkBoxHistogram.Name = "checkBoxHistogram";
            checkBoxHistogram.Size = new Size(117, 19);
            checkBoxHistogram.TabIndex = 8;
            checkBoxHistogram.Text = "Show histograms";
            checkBoxHistogram.UseVisualStyleBackColor = true;
            checkBoxHistogram.CheckedChanged += checkBoxHistogram_CheckedChanged;
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
            numericUpDownWidth.ValueChanged += numericUpDownWidth_ValueChanged;
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
            numericUpDownHeight.ValueChanged += numericUpDownHeight_ValueChanged;
            // 
            // numericUpDownRadius
            // 
            numericUpDownRadius.Location = new Point(188, 112);
            numericUpDownRadius.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericUpDownRadius.Minimum = new decimal(new int[] { 4, 0, 0, 0 });
            numericUpDownRadius.Name = "numericUpDownRadius";
            numericUpDownRadius.Size = new Size(80, 23);
            numericUpDownRadius.TabIndex = 7;
            numericUpDownRadius.Value = new decimal(new int[] { 15, 0, 0, 0 });
            numericUpDownRadius.ValueChanged += numericUpDownRadius_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(140, 114);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 6;
            label4.Text = "Radius";
            // 
            // checkBoxOtsuTreshold
            // 
            checkBoxOtsuTreshold.AutoSize = true;
            checkBoxOtsuTreshold.Location = new Point(5, 146);
            checkBoxOtsuTreshold.Name = "checkBoxOtsuTreshold";
            checkBoxOtsuTreshold.Size = new Size(131, 19);
            checkBoxOtsuTreshold.TabIndex = 5;
            checkBoxOtsuTreshold.Text = "Apply Otsu treshold";
            checkBoxOtsuTreshold.UseVisualStyleBackColor = true;
            checkBoxOtsuTreshold.CheckedChanged += checkBoxOtsuTreshold_CheckedChanged;
            // 
            // checkBoxGaussianBlur
            // 
            checkBoxGaussianBlur.AutoSize = true;
            checkBoxGaussianBlur.Location = new Point(5, 113);
            checkBoxGaussianBlur.Name = "checkBoxGaussianBlur";
            checkBoxGaussianBlur.Size = new Size(131, 19);
            checkBoxGaussianBlur.TabIndex = 5;
            checkBoxGaussianBlur.Text = "Apply Gaussian blur";
            checkBoxGaussianBlur.UseVisualStyleBackColor = true;
            checkBoxGaussianBlur.CheckedChanged += checkBoxGaussianBlur_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 84);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 3;
            label3.Text = "Height (px)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 55);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
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
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // panelDrawing
            // 
            panelDrawing.Controls.Add(doubleBufferedPanel);
            panelDrawing.Dock = DockStyle.Fill;
            panelDrawing.Location = new Point(275, 24);
            panelDrawing.Name = "panelDrawing";
            panelDrawing.Size = new Size(1169, 960);
            panelDrawing.TabIndex = 2;
            // 
            // doubleBufferedPanel
            // 
            doubleBufferedPanel.BackColor = Color.White;
            doubleBufferedPanel.BorderStyle = BorderStyle.FixedSingle;
            doubleBufferedPanel.Dock = DockStyle.Fill;
            doubleBufferedPanel.Location = new Point(0, 0);
            doubleBufferedPanel.Name = "doubleBufferedPanel";
            doubleBufferedPanel.Size = new Size(1169, 960);
            doubleBufferedPanel.TabIndex = 0;
            doubleBufferedPanel.Paint += doubleBufferedPanel_Paint;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1444, 984);
            Controls.Add(panelDrawing);
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
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRadius).EndInit();
            panelDrawing.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Panel panelLeft;
        private Panel panelDrawing;
        private Tools.DoubleBufferedPanel doubleBufferedPanel;
        private ComboBox comboBox1;
        private Label label1;
        private Label label3;
        private Label label2;
        private TextBox textBoxHeight;
        private TextBox textBoxWidth;
        private NumericUpDown numericUpDownRadius;
        private Label label4;
        private CheckBox checkBoxGaussianBlur;
        private NumericUpDown numericUpDownWidth;
        private NumericUpDown numericUpDownHeight;
        private CheckBox checkBoxOtsuTreshold;
        private CheckBox checkBoxHistogram;
    }
}
