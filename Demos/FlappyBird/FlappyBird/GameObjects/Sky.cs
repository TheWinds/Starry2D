using Starry2D.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlappyBird.GameObjects
{
    public class Sky : BaseGameObject
    {
        Bitmap backgroundSky = Properties.Resources.background_day;
        
        public override void Draw(Graphics g)
        {
            for (int i = 0; i < Engine.Width / backgroundSky.Width+1; i++)
            {
                g.DrawImage(backgroundSky, i* backgroundSky.Width + X, 0);
            }
        }
        protected override void Update()
        {
            //if (X<=-backgroundSky.Width) X = 0;
            //X -= 1;
        }
    }
}
