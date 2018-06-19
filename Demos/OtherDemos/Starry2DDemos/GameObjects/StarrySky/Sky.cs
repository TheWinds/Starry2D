using System.Drawing;
using Starry2D.Engine;

namespace Starry2DDemos.GameObjects.StarrySky
{
    public class Sky : BaseGameObject
    {
        private Font font = new Font("微软雅黑", 30);
        public override void Start()
        {
        }
        public override void Draw(Graphics g)
        {
            GDIUtils.DrawImage(g, Properties.Resources.bgsky, Engine.Width, Engine.Height);
            g.DrawString("Starry2D粒子效果", font, Brushes.White, Engine.Width/2-200, Engine.Height/2-50);
        }

        protected override void Update()
        {
        }
    }
}
