using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starry2D.Engine;

namespace Starry2DDemos.GameObjects.CollisionBall
{
    public class WallBlock : BaseGameObject
    {
        Brush brush = Brushes.Black;
        public override void Start()
        {
            CheckCollision = true;

        }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(brush, X, Y, Width, Height);
        }
        
        protected override void OnCollision(GameObject obj)
        {
            var rand = new Random(GameUtils.GetRandomSeed());
            brush = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
        }
        protected override void Update()
        {
        }
    }
}
