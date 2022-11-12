using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snowFALL
{
    public partial class Form1 : Form
    {

        private readonly IList<Snow> snowfall;
        private readonly Timer timer;
        private readonly Bitmap snowImg;
        private readonly Bitmap backFon;
        private readonly int speed = 5;
        public Form1()
        {
            InitializeComponent();
            snowfall = new List<Snow>();
            backFon = (Bitmap)Properties.Resources.back;
            snowImg = (Bitmap)Properties.Resources.snow;
            CreateSnow();
            timer = new Timer
            {
                Interval = 1
            };
            timer.Tick += Timer_Tick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            DrawScen();
            timer.Start();
        }
        private void CreateSnow()
        {
            var rnd = new Random();
            for (var i = 0; i < 200; i++)
            {
                snowfall.Add(new Snow
                {
                    X = rnd.Next(Screen.PrimaryScreen.WorkingArea.Width),
                    Y = -rnd.Next(Screen.PrimaryScreen.WorkingArea.Height),
                    Size = rnd.Next(1, 30)
                });
            }
        }
        private void DrawScen()
        {
            var scene = new Bitmap(backFon,
                 ClientRectangle.Width,
                 ClientRectangle.Height);
            for (var i = 0; i < snowfall.Count; i++)
            {

                if (snowfall[i].Y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    snowfall[i].Y = -snowfall[i].Size;
                }
                else
                {
                    snowfall[i].Y += speed + snowfall[i].Size;
                }
            }
            for (var i = 0; i < snowfall.Count; i++)
            {
                if (snowfall[i].Y > 0)
                {
                    var gr = Graphics.FromImage(scene);
                    gr.DrawImage(snowImg,
                        new Rectangle(
                        snowfall[i].X,
                        snowfall[i].Y,
                        snowfall[i].Size,
                        snowfall[i].Size));
                }
            }
            var a = CreateGraphics();
            a.DrawImage(scene, 0, 0);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawScen();
        }
    }
}
