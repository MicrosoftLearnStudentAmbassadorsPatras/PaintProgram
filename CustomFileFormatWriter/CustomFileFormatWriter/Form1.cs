using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace CustomFileFormatWriter
{
    public partial class Form1 : Form
    {
        int height = 5;
        int width = 10;
        int heightStd = 25;
        int widthStd = 25;
        pixel[,] myImage;

        int activeR = 255;
        int activeG = 0;
        int activeB = 0;

        public Form1()
        {
            myImage = new pixel[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    myImage[i, j] = new pixel();
                }
            }

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    myBrush = new System.Drawing.SolidBrush(myImage[i,j].getColor());

                    formGraphics.FillRectangle(myBrush, new Rectangle(j * widthStd, i * heightStd, widthStd, heightStd));
                }
            }

            formGraphics.FillRectangle(new System.Drawing.SolidBrush(Color.FromArgb(255, activeR, activeG, activeB)), new Rectangle(14 + 75 + 3, 238, widthStd, heightStd));

            myBrush.Dispose();
            formGraphics.Dispose();
            base.OnPaint(e);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Rectangle activeRect = RectangleToScreen(this.ClientRectangle); //get the active rect

            int mouseX = (Control.MousePosition.X - activeRect.Left)/widthStd;
            int mouseY = (Control.MousePosition.Y - activeRect.Top)/heightStd;


            if (mouseX < width && mouseY < height)
            {
                myImage[mouseY, mouseX].R = activeR;
                myImage[mouseY, mouseX].G = activeG;
                myImage[mouseY, mouseX].B = activeB;
                myImage[mouseY, mouseX].updateColor();
            }

            this.Text = mouseX.ToString() +
                ", " + mouseY.ToString() + " || " +
                    Control.MousePosition.X.ToString() + ", "
                    + Control.MousePosition.Y.ToString();

            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            activeR = 255;
            activeG = 0;
            activeB = 0;
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            activeR = 0;
            activeG = 255;
            activeB = 0;
            this.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            activeR = 0;
            activeG = 0;
            activeB = 255;
            this.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\CustomImageFormat.txt"))
            {
                file.WriteLine(height.ToString());
                file.WriteLine(width.ToString());
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        file.Write(myImage[i,j].R.ToString());
                        file.Write(":");
                        file.Write(myImage[i, j].G.ToString());
                        file.Write(":");
                        file.Write(myImage[i, j].B.ToString());
                        file.Write("\n");
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            activeR += 10;
            if (activeR > 255) { activeR = 255; }
            this.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            activeR -= 10;
            if (activeR < 0) { activeR = 0; }
            this.Refresh();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            activeG += 10;
            if (activeG > 255) { activeG = 255; }
            this.Refresh();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            activeG -= 10;
            if (activeG < 0) { activeG = 0; }
            this.Refresh();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            activeB += 10;
            if (activeB > 255) { activeB = 255; }
            this.Refresh();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            activeB -= 10;
            if (activeB < 0) { activeB = 0; }
            this.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            activeR = 255;
            activeG = 255;
            activeB = 255;
            this.Refresh();
        }
    }

    class pixel
    {
        public pixel()
        {
            R = 255;
            G = 255;
            B = 255;

            ARGB = new Color();
            updateColor();
        }

        public int R;
        public int G;
        public int B;

        
            //SystemSounds.Beep.Play();


        Color ARGB;

        public void updateColor()
        {
            ARGB = Color.FromArgb(255, R, G, B);
        }

        public Color getColor()
        {
            return ARGB;
        }
    }
}
