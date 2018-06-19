using Starry2D.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlappyBird.GameObjects
{

    public class UIPicture : BaseGameObject
    {
        private Bitmap bitmap;
        public bool CenterVertical { get; set; }
        public bool CenterHorizontal { get; set; }
        public UIPicture(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }
        public override void Start()
        {
            Delay = 5;
            Width = bitmap.Width;
            Height = bitmap.Height;
            if (CenterHorizontal)
            {
                X = (Engine.Width - bitmap.Width) / 2;
            }
            if (CenterVertical)
            {
                Y = (Engine.Height - bitmap.Height) / 2;
            }
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(bitmap, X, Y);
        }
        protected override void Update()
        {
        }
    }
}
