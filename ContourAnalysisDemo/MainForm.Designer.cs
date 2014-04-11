namespace ContourAnalysisDemo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ibMain = new Emgu.CV.UI.ImageBox();
            this.tmUpdateState = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbContoursCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbRecognized = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btLoadImage = new System.Windows.Forms.Button();
            this.cbCaptureFromCam = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ibMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ssMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ibMain
            // 
            this.ibMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ibMain.Location = new System.Drawing.Point(176, 6);
            this.ibMain.Name = "ibMain";
            this.ibMain.Size = new System.Drawing.Size(596, 496);
            this.ibMain.TabIndex = 2;
            this.ibMain.TabStop = false;
            this.ibMain.Paint += new System.Windows.Forms.PaintEventHandler(this.ibMain_Paint);
            // 
            // tmUpdateState
            // 
            this.tmUpdateState.Enabled = true;
            this.tmUpdateState.Interval = 1000;
            this.tmUpdateState.Tick += new System.EventHandler(this.tmUpdateState_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(13, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(144, 435);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbFPS
            // 
            this.lbFPS.AutoSize = false;
            this.lbFPS.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lbFPS.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lbFPS.Name = "lbFPS";
            this.lbFPS.Size = new System.Drawing.Size(52, 17);
            this.lbFPS.Text = "0 fps";
            // 
            // lbContoursCount
            // 
            this.lbContoursCount.AutoSize = false;
            this.lbContoursCount.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lbContoursCount.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lbContoursCount.Name = "lbContoursCount";
            this.lbContoursCount.Size = new System.Drawing.Size(120, 17);
            this.lbContoursCount.Text = "Total Contours: ";
            this.lbContoursCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRecognized
            // 
            this.lbRecognized.AutoSize = false;
            this.lbRecognized.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lbRecognized.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lbRecognized.Name = "lbRecognized";
            this.lbRecognized.Size = new System.Drawing.Size(150, 17);
            this.lbRecognized.Text = "Recognized Contours: ";
            this.lbRecognized.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbFPS,
            this.lbContoursCount,
            this.lbRecognized});
            this.ssMain.Location = new System.Drawing.Point(0, 502);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(1350, 22);
            this.ssMain.TabIndex = 4;
            this.ssMain.Text = "statusStrip1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(778, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(572, 496);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // btLoadImage
            // 
            this.btLoadImage.Location = new System.Drawing.Point(13, 13);
            this.btLoadImage.Name = "btLoadImage";
            this.btLoadImage.Size = new System.Drawing.Size(143, 48);
            this.btLoadImage.TabIndex = 8;
            this.btLoadImage.Text = "Открыть изображение";
            this.btLoadImage.UseVisualStyleBackColor = true;
            // 
            // cbCaptureFromCam
            // 
            this.cbCaptureFromCam.AutoSize = true;
            this.cbCaptureFromCam.Checked = true;
            this.cbCaptureFromCam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCaptureFromCam.Location = new System.Drawing.Point(29, 80);
            this.cbCaptureFromCam.Name = "cbCaptureFromCam";
            this.cbCaptureFromCam.Size = new System.Drawing.Size(80, 17);
            this.cbCaptureFromCam.TabIndex = 9;
            this.cbCaptureFromCam.Text = "checkBox1";
            this.cbCaptureFromCam.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 524);
            this.Controls.Add(this.cbCaptureFromCam);
            this.Controls.Add(this.btLoadImage);
            this.Controls.Add(this.ibMain);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.ssMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contour Analysis";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ibMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox ibMain;
        private System.Windows.Forms.Timer tmUpdateState;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel lbFPS;
        private System.Windows.Forms.ToolStripStatusLabel lbContoursCount;
        private System.Windows.Forms.ToolStripStatusLabel lbRecognized;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btLoadImage;
        private System.Windows.Forms.CheckBox cbCaptureFromCam;
    }
}

