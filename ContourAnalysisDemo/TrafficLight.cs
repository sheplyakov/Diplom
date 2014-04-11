using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContourAnalysisDemo
{
    public class TrafficLight
    {
        private Light stateLight;
        private PictureBox image;
        private Timer timer;
        private int tick;
        private int blinklenth;
        private int blinktick;

        public Light State
        {
            get { return stateLight; }
            private set { stateLight = value; }

        }

        private int redLongest;
        private int yellowLongest;
        private int greenLongest;
        private int blinkLongest;


        public TrafficLight(
            PictureBox image,
            int redLng = 3000,
            int yellowlngst = 1000,
            int greenlngst = 3000,
            int blinklngst = 2000)
        {
            stateLight = Light.Red;

            this.image = image;

            RedLongest = redLng;
            YellowLongest = yellowlngst;
            GreenLongest = greenlngst;
            BlinkLongest = blinklngst;

            blinklenth = 1000;

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += timer_Tick;
            tick = 0;
            timer.Start();

        }

        public int RedLongest
        {
            get { return redLongest; }
            set { redLongest = value; }
        }

        public int YellowLongest
        {
            get { return yellowLongest; }
            set { yellowLongest = value; }
        }

        public int GreenLongest
        {
            get { return greenLongest; }
            set { greenLongest = value; }
        }

        public int BlinkLongest
        {
            get { return blinkLongest; }
            set { blinkLongest = value; }
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            DrawImage();

            tick += timer.Interval;

            if (tick == redLongest)
            {
                stateLight = Light.Yellow;
            }
            else if (tick == redLongest + yellowLongest)
            {
                stateLight = Light.Green;
            }
            else if (tick == redLongest + yellowLongest + greenLongest)
            {
                stateLight = Light.BlinkGreen;
                blinktick = 0;
            }
            else if (tick == redLongest + yellowLongest + greenLongest + blinkLongest)
            {
                stateLight = Light.Red;
                tick = 0;
            }

        }

        private void DrawImage()
        {
            Bitmap b = new Bitmap(110, 200);
            image.Image = b;
            Graphics g = Graphics.FromImage(image.Image);

            Pen border = new Pen(Color.Black, 3);

            g.DrawEllipse(border, new Rectangle(25, 0, 60, 60));
            g.DrawEllipse(border, new Rectangle(25, 65, 60, 60));
            g.DrawEllipse(border, new Rectangle(25, 130, 60, 60));

            Brush
                red = new SolidBrush(Color.Red),
                yellow = new SolidBrush(Color.Yellow),
                green = new SolidBrush(Color.GreenYellow);

            switch (stateLight)
            {
                case Light.Red:
                    red = new SolidBrush(Color.Red);
                    yellow = new SolidBrush(Color.SaddleBrown);
                    green = new SolidBrush(Color.DarkOliveGreen);
                    break;
                case Light.Yellow:
                    red = new SolidBrush(Color.Red);
                    yellow = new SolidBrush(Color.Yellow);
                    green = new SolidBrush(Color.DarkGreen);
                    break;
                case Light.BlinkGreen:

                    red = new SolidBrush(Color.Brown);
                    yellow = new SolidBrush(Color.SaddleBrown);
                    if (blinktick < blinklenth)
                        green = new SolidBrush(Color.DarkOliveGreen);
                    else if (blinktick > (blinklenth + blinklenth))
                    {
                        blinktick = 0;
                        green = new SolidBrush(Color.DarkOliveGreen);
                    }
                    else
                    {
                        green = new SolidBrush(Color.GreenYellow);
                    }
                    blinktick += timer.Interval;
                    break;
                case Light.Green:
                    red = new SolidBrush(Color.Brown);
                    yellow = new SolidBrush(Color.SaddleBrown);
                    green = new SolidBrush(Color.GreenYellow);
                    break;

            }

            g.FillEllipse(red, new Rectangle(25, 0, 60, 60));
            g.FillEllipse(yellow, new Rectangle(25, 65, 60, 60));
            g.FillEllipse(green, new Rectangle(25, 130, 60, 60));
        }

    }

    public enum Light
    {
        Red,
        Yellow,
        Green,
        BlinkGreen
    }
}

