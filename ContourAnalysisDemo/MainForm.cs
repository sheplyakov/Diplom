using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using ContourAnalysisNS;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ContourAnalysisDemo
{
    public partial class MainForm : Form
    {
        private Capture _capture;
        Image<Bgr, Byte> frame;
        ImageProcessor processor;
        Dictionary<string, Image> AugmentedRealityImages = new Dictionary<string, Image>();

        bool captureFromCam = true;
        int frameCount = 0;
        int oldFrameCount = 0;
        bool showAngle;
        int camWidth = 640;
        int camHeight = 480;
        string templateFile;

        public MainForm()
        {
            InitializeComponent();
            //create image preocessor
            processor = new ImageProcessor();
            //load default templates
            templateFile = AppDomain.CurrentDomain.BaseDirectory + "\\Tahoma.bin";
            LoadTemplates(templateFile);
            //start capture from cam
            StartCapture();
            //apply settings
            ApplySettings();
            //
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void LoadTemplates(string fileName)
        {
            try
            {
                using(FileStream fs = new FileStream(fileName, FileMode.Open))
                    processor.templates = (Templates)new BinaryFormatter().Deserialize(fs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveTemplates(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                    new BinaryFormatter().Serialize(fs, processor.templates);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StartCapture()
        {
            try
            {
                _capture = new Capture();
                ApplyCamSettings();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ApplyCamSettings()
        {
            try
            {
                _capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, camWidth);
                _capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, camHeight);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            ProcessFrame();
        }

        private void ProcessFrame()
        {
            try
            {
                if (captureFromCam)
                    frame = _capture.QueryFrame();
                frameCount++;
                //
                processor.ProcessImage(frame);
                //
                    ibMain.Image = frame;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tmUpdateState_Tick(object sender, EventArgs e)
        {
            lbFPS.Text = (frameCount - oldFrameCount) + " fps";
            oldFrameCount = frameCount;
            if (processor.contours!=null)
                lbContoursCount.Text = "Contours: "+processor.contours.Count;
            if (processor.foundTemplates != null)
                lbRecognized.Text = "Recognized contours: " + processor.foundTemplates.Count;
        }

        private void ibMain_Paint(object sender, PaintEventArgs e)
        {
            if (frame == null) return;

            Font font = new Font(Font.FontFamily, 24);//16

            e.Graphics.DrawString(lbFPS.Text, new Font(Font.FontFamily, 16), Brushes.Yellow, new PointF(1, 1));

            Brush bgBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
            Brush foreBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
            Pen borderPen = new Pen(Color.FromArgb(150, 0, 255, 0));
            //
            foreach (var contour in processor.contours)
                if(contour.Total>1)
                e.Graphics.DrawLines(Pens.Red, contour.ToArray());
            //
            lock (processor.foundTemplates)
            foreach (FoundTemplateDesc found in processor.foundTemplates)
            {
                if (found.template.name.EndsWith(".png") || found.template.name.EndsWith(".jpg"))
                {
                    DrawAugmentedReality(found, e.Graphics);
                    continue;
                }

                Rectangle foundRect = found.sample.contour.SourceBoundingRect;
                Point p1 = new Point((foundRect.Left + foundRect.Right)/2, foundRect.Top);
                string text = found.template.name;
                if (showAngle)
                    text += string.Format("\r\nangle={0:000}°\r\nscale={1:0.0}", 180 * found.angle / Math.PI, found.scale);
                e.Graphics.DrawRectangle(borderPen, foundRect);
                e.Graphics.DrawString(text, font, bgBrush, new PointF(p1.X + 1 - font.Height/3, p1.Y + 1 - font.Height));
                e.Graphics.DrawString(text, font, foreBrush, new PointF(p1.X - font.Height/3, p1.Y - font.Height));
            }
        }

        private void DrawAugmentedReality(FoundTemplateDesc found, Graphics gr)
        {
            string fileName = Path.GetDirectoryName(templateFile) + "\\" + found.template.name;
            if (!AugmentedRealityImages.ContainsKey(fileName))
            {
                if (!File.Exists(fileName)) return;
                AugmentedRealityImages[fileName] = Image.FromFile(fileName);
            }
            Image img = AugmentedRealityImages[fileName];
            Point p = found.sample.contour.SourceBoundingRect.Center();
            var state = gr.Save();
            gr.TranslateTransform(p.X, p.Y);
            gr.RotateTransform((float)(180f * found.angle / Math.PI));
            gr.ScaleTransform((float)(found.scale), (float)(found.scale));
            gr.DrawImage(img, new Point(-img.Width/2, -img.Height/2));
            gr.Restore(state);
        }

        private void cbAutoContrast_CheckedChanged(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void ApplySettings()
        {
            try
            {
                processor.equalizeHist = true;
                showAngle = false;
                captureFromCam = cbCaptureFromCam.Checked;
                btLoadImage.Enabled = !captureFromCam;
               // cbCamResolution.Enabled = captureFromCam;
                processor.finder.maxRotateAngle = true ? Math.PI : Math.PI / 4;
                processor.minContourArea = 10;
                processor.minContourLength = 50;
                processor.finder.maxACFDescriptorDeviation = 20;
                processor.finder.minACF = 20;
                processor.finder.minICF = 20;
                processor.blur = false;
                processor.noiseFilter = true;
                processor.cannyThreshold = 10;
                processor.adaptiveThresholdBlockSize = 10;
                processor.adaptiveThresholdParameter = true?1.5:0.5;
                //cam resolution
                        ApplyCamSettings();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image|*.bmp;*.png;*.jpg;*.jpeg";
            if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                try
                {
                    frame = new Image<Bgr, byte>((Bitmap)Bitmap.FromFile(ofd.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void btCreateTemplate_Click(object sender, EventArgs e)
        {
            if(frame!=null)
                new ShowContoursForm(processor.templates, processor.samples, frame).ShowDialog();
        }

        private void btNewTemplates_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to create new template database?", "Create new template database", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                processor.templates.Clear();
        }

        private void btOpenTemplates_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Templates(*.bin)|*.bin";
            if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                templateFile = ofd.FileName;
                LoadTemplates(templateFile);
            }
        }

        private void btSaveTemplates_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Templates(*.bin)|*.bin";
            if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                templateFile = sfd.FileName;
                SaveTemplates(templateFile);
            }
        }

        private void btTemplateEditor_Click(object sender, EventArgs e)
        {
            new TemplateEditor(processor.templates).Show();
        }

        private void btAutoGenerate_Click(object sender, EventArgs e)
        {
            new AutoGenerateForm(processor).ShowDialog();
        }

        private void cbShowContours_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            trafficLight = new TrafficLight(pictureBox1);
        }

        private TrafficLight trafficLight;

        System.Diagnostics.Stopwatch s;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (trafficLight.State == Light.Red && s == null)
            {
                s = new System.Diagnostics.Stopwatch();
                s.Start();
            }


            if (s != null)
            {
                if (s.ElapsedMilliseconds > trafficLight.RedLongest / 2)
                {
                    //  this.BackColor = Color.Red;
                    s = null;
                    pictureBox2.Image = MakeGray(ibMain.Image.Bitmap);

                }
            }
        }


        private Bitmap MakeGray(Image img)//перевод в черное-белое изображение
        {
            // Задаём формат Пикселя
            PixelFormat pxf = PixelFormat.Format24bppRgb;

            Bitmap bmp = new Bitmap(img);
            // Получаем данные картинки
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            //Блокируем набор данных изображения в памяти
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

            // Получаем адрес первой линии
            IntPtr ptr = bmpData.Scan0;

            // Задаём массив из Byte и помещаем в него набор данных
            // int numBytes = bmp.Width * bmp.Height * 3; 
            //На 3 умножаем - поскольку RGB цвет кодируется 3-мя байтами
            //Либо используем вместо Width - Stride
            int numBytes = bmpData.Stride * bmp.Height;
            int widthBytes = bmpData.Stride;
            byte[] rgbValues = new byte[numBytes];

            // Копируем значения в массив
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            // Перебираем пикселы по 3 байта на каждый и меняем значения
            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {

                int value = rgbValues[counter] + rgbValues[counter + 1] + rgbValues[counter + 2];
                byte color_b = 0;

                color_b = Convert.ToByte(value / 3);
                rgbValues[counter] = color_b;
                rgbValues[counter + 1] = color_b;
                rgbValues[counter + 2] = color_b;

            }
            // Копируем набор данных обратно в изображение
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            // Разблокируем набор данных изображения в памяти
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if(processor.contours != null)
            foreach (var contour in processor.contours)
                if (contour.Total > 1)
                    e.Graphics.DrawLines(Pens.Red, contour.ToArray());

        }

        private void cbCaptureFromCam_CheckedChanged(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void btLoadImage_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image|*.bmp;*.png;*.jpg;*.jpeg";
            if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                try
                {
                    frame = new Image<Bgr, byte>((Bitmap)Bitmap.FromFile(ofd.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }




    }
}
