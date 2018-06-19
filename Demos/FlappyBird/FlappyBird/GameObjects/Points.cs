using Starry2D.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlappyBird.GameObjects
{
    /// <summary>
    /// 分数
    /// </summary>
    public class Points : BaseGameObject
    {
        public int Point { get; set; }
        public override void Draw(Graphics g)
        {
            var strPoint = Point.ToString();
            var bitNum = strPoint.Length;
            var numPicWidth = Properties.Resources._0.Width;
            // 分数水平居中
            X = Engine.Width / 2 - (numPicWidth * bitNum) / 2;
            Y = 40;
            for (int i = bitNum-1; i >=0 ; i--)
            {
                var numPic = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + strPoint[i]);
                g.DrawImage(numPic, X + i * numPic.Width, Y);
            }
        }

        public override void Start()
        {
            ZIndex = 10;
        }
        protected override void Update()
        {
            
        }
    }
}
