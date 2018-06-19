using Starry2D.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlappyBird.GameObjects
{
    public class Diamonds : BaseGameObject
    {
        public bool Picked { get; set; }
        public float TargetX { get; set; }
        public float TargetY { get; set; }
        private float k;
        public override void Start()
        {
            Width = 40;
            Height = 40;
            TargetX = Engine.Width / 2;
            TargetY = 50;
            CheckCollision = true;
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(Properties.Resources.diamonds, X, Y, Width, Height);
        }

        protected override void OnCollision(GameObject obj)
        {
            Picked = true;
            k = (Y - TargetY) / (X - TargetX);
            if (k < 0) k = 2;

        }
        protected override void Update()
        {
            if ((Math.Abs(X - TargetX) < 5) && (Math.Abs(Y - TargetY) < 5))
            {
                RemoveSelf();
            }

            if (Picked)
            {
                X -= 4;
                Y = calcY(X);
                if (Width > 5)
                {
                    Width--;
                    Height--;
                }
            }
        }
        private float calcY(float x)
        {
            return x * k - TargetY;
        }
    }
}
