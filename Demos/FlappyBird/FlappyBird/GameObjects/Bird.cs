using System.Drawing;
using Starry2D.Engine;

namespace FlappyBird.GameObjects
{
    public class Bird : BaseGameObject
    {
        public override void Draw(Graphics g)
        {
            // 绘制下降状态
            if (v>0)
            {
                g.DrawImage(Properties.Resources.redbird_downflap, X, Y);

            }
            else if (v<0)
            {
                // 绘制上升状态
                g.DrawImage(Properties.Resources.redbird_upflap, X, Y);
            }
            else
            {
                // 绘制中间状态
                g.DrawImage(Properties.Resources.redbird_midflap, X, Y);
            }
        }

        public override void Start()
        {
            // 开启碰撞检测
            CheckCollision = true;
            // 默认位置
            X = Engine.Width / 3;
            Y = Engine.Height / 2;
            // 默认宽高
            Width = Properties.Resources.redbird_midflap.Width;
            Height = Properties.Resources.redbird_midflap.Height;
        }
        // 速度
        private float v;
        // 加速度向下为正数

        // 加速度
        private float a;
        // 重力加速度
        const float grivaty = 1.2f;
        // 反重力加速度
        const float upGrivaty = -8f;
        // 最大上升速度
        const int maxUpV = -8;
        // 最大下降速度
        const int maxDownV = 16;
        protected override void Update()
        {
            if (v < maxUpV) v = maxUpV;
            if (v > maxDownV) v = maxDownV;
            v += a ;
            if (v<-grivaty) a = grivaty;
            Y += v;
        }
        // 上升
        public void Up()
        {
            a = upGrivaty;
        }

        // 碰撞检测
        protected override void OnCollision(GameObject obj)
        {
            var msg = new GameMsg();
            // 撞到了管道，发送死亡消息
            if (obj is Pipe)
            {
                msg.MsgType = GameMsgType.Died;
            }

            //if (obj is Diamonds)
            //{
            //    msg.MsgType = GameMsgType.Pick;
            //}

            SendMessage(msg);
        }
    }
}
