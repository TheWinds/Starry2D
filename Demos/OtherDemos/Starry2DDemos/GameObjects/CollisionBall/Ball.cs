using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starry2D.Engine;

namespace Starry2DDemos.GameObjects.CollisionBall
{
    public class Ball : BaseGameObject
    {
        public int vx=8;
        public int vy=4;
        public override void Start()
        {
            Width = 20;
            Height = 20;
            CheckCollision = true;
        }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Red, X, Y, Width, Height);
        }
        
        protected override void Update()
        {
            X += vx;
            Y += vy;
        }

        protected override void OnCollision(GameObject obj)
        {
            if (Math.Abs(X - obj.X) <= Width+obj.Height || X <= 20)
            {
                vx = -vx;

            }
            if (Math.Abs(Y - Engine.Height) <= Height+obj.Width || Y <= 20)
            {
                vy = -vy;
            }
        }
    }
}
