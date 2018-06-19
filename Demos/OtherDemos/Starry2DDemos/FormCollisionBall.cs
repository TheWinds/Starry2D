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
using Starry2DDemos.GameObjects.CollisionBall;

namespace Starry2DDemos
{
    public partial class FormCollisionBall : Form
    {
        public FormCollisionBall()
        {
            InitializeComponent();
        }
        GameEngine engine;
        private bool isIntersect(float x1,float w1,float x2,float w2)
        {
            var minx = Math.Min(x1, x2);
            var maxw = Math.Max(x1 + w1, x2 + w2);
            return (Math.Abs(maxw-minx))<=w1+w2;
        }
        private void FormCollisionBall_Load(object sender, EventArgs e)
        {
            engine = new GameEngine(CreateGraphics(), (int)(Width/1.25), (int)(Height / 1.25));
            engine.Background = Color.Transparent;
            for (int i = 0; i < 10; i++)
            {
                var rand = new Random(GameUtils.GetRandomSeed());
                var ball = new Ball();
                ball.X = rand.Next(30,engine.Width-30);
                ball.Y = rand.Next(30,engine.Height-30);
                ball.vx = rand.Next(3,10);
                ball.vy = rand.Next(3,10);
                engine.AddGameObject(ball);
            }
            

            engine.Debug = true;
            engine.CollisionDetectionFunc = (from, to) =>
            {
                if (from is WallBlock && to is WallBlock)
                {
                    return false;
                }
                
                var a = isIntersect(from.X,from.Width, to.X, to.Width);
                var b = isIntersect(from.Y,from.Height, to.Y, to.Height);
                if ( a && b )
                {
                   return true;
                }
                return false;
            };
            var block = new WallBlock();
            block.X = 0;
            block.Y = 0;
            block.Width = engine.Width;
            block.Height = 20;
            engine.AddGameObject(block);
            block = new WallBlock();
            block.X = 0;
            block.Y = engine.Height-20;
            block.Width = engine.Width;
            block.Height = 20;
            engine.AddGameObject(block);
            block = new WallBlock();
            block.X = engine.Width-20;
            block.Y = 20;
            block.Width = 20;
            block.Height = engine.Height - 40;
            engine.AddGameObject(block);
            block = new WallBlock();
            block.X = 0;
            block.Y = 20;
            block.Width = 20;
            block.Height = engine.Height-40;
            engine.AddGameObject(block);
            engine.Run();
        }

        private void FormCollisionBall_FormClosing(object sender, FormClosingEventArgs e)
        {
            engine.Stop();
        }
    }
}
