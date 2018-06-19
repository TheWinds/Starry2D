using Starry2D.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird.GameObjects
{
    public class Ground : BaseGameObject
    {
        Bitmap backgroundFloor = Properties.Resources._base;
        public override void Draw(Graphics g)
        {
            for (int i = 0; i < Engine.Width / backgroundFloor.Width + 1; i++)
            {
                g.DrawImage(backgroundFloor, i * backgroundFloor.Width + X, Engine.Height - backgroundFloor.Height);
            }
        }

        public override void Start()
        {
            ZIndex = 2;
        }
        protected override void Update()
        {
            X -= 1;
            if (X <= -backgroundFloor.Width) X = 0;
        }
    }
}
