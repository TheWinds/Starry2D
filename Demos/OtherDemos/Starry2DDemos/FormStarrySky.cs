using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Starry2D.Engine;
using Starry2DDemos.GameObjects.StarrySky;
using System.Threading;

namespace Starry2DDemos
{
    public partial class FormStarrySky : Form
    {
        public FormStarrySky()
        {
            InitializeComponent();
        }
        GameEngine engine;
        private void FormStarrySky_Load(object sender, EventArgs e)
        {
            engine = new GameEngine(CreateGraphics(), Width, Height);
            engine.Background = Color.Transparent;
            engine.AddGameObject(new Sky());
            for (int i = 0; i < 100; i++)
            {
                engine.AddGameObject(new Star());
            }
            engine.Debug = true;
            timer1.Interval = 20;
            timer1.Start();
            engine.Run();
        }

        private void FormStarrySky_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            engine.AddGameObject(new Star());
        }

        private void FormStarrySky_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            engine.Stop();
        }
    }
}
