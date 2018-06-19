using System.Drawing;

namespace Starry2D.Engine
{
    class FPSGameObj : BaseGameObject
    {
        public int FPS { get; set; }
        private Font font=new Font("微软雅黑",12);

        public override void Draw(Graphics g)
        {
            g.DrawString($"FPS: {FPS}", font, Brushes.Red, Engine.Width - 100, 0);
        }
        protected override void Update()
        {
            
        }
    }
}
