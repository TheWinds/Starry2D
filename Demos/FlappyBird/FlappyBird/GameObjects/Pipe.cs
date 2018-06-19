using Starry2D.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace FlappyBird.GameObjects
{
    public class Pipe : BaseGameObject
    {
        // 是否为反转状态
        public bool Reverse { get; set; }
        // 可用高度
        int availableHeight;
        const int groundHeight = 112;
        // 钻石
        private Diamonds diamonds;
        public override void Draw(Graphics g)
        {   //图片资源
            var pipeImg = Properties.Resources.pipe_green;
            var pipeReverseImg = Properties.Resources.pipe_green_reverse;
            // 绘制反转的水管
            if (Reverse)
            {
                var rectFSrc = new RectangleF(0, pipeReverseImg.Height - Height, pipeReverseImg.Width, Height);
                var rectFDest = new RectangleF(X, 0, pipeReverseImg.Width, Height);
                g.DrawImage(pipeReverseImg, rectFDest, rectFSrc, GraphicsUnit.Pixel);
            }
            // 绘制正常的水管
            else
            {
                var rectFSrc = new RectangleF(0, 0, pipeImg.Width, Height);
                var rectFDest = new RectangleF(X, Engine.Height - Height - 112, pipeImg.Width, Height);
                g.DrawImage(pipeImg, rectFDest, rectFSrc, GraphicsUnit.Pixel);
            }
        }

        public override void Start()
        {
            // 开启碰撞
            CheckCollision = true;
            // 可用高度
            availableHeight = Engine.Height - groundHeight - 100;
            // 设置宽高
            Height = new Random(GameUtils.GetRandomSeed()).Next(availableHeight / 4, availableHeight / 2);
            Width = Properties.Resources.pipe_green.Width;
            // 设置初始坐标
            X = Engine.Width - Properties.Resources.pipe_green.Width - 10;
            if (!Reverse) Y = Engine.Height - Height - groundHeight;
        }
        bool crossed;
        protected override void Update()
        {
            // 向左移动
            X -= 3;
            // 如果通过了三分之一的位置，则说明小鸟通过
            // 发送小鸟通过的消息
            // 必须为 Reverse 的作用使为了保证消息只发送一次
            //（因为上下有两个Y坐标相同的柱子）
            if (X < Engine.Width / 3 && !crossed && Reverse)
            {
                crossed = true;
                var msg = new GameMsg();
                msg.MsgType = GameMsgType.CrossPipe;
                SendMessage(msg);
            }
            // 超出边界则移除
            if (X < -Width) Deleted = true;
        }
    }
}
