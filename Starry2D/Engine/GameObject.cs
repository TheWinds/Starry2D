using System.Drawing;

namespace Starry2D.Engine
{
    /// <summary>
    /// 游戏对象
    /// </summary>
    public abstract class GameObject
    {
        public GameObject() {
            //默认延迟帧数为1
            Delay = 1;
        }
        /// <summary>
        /// 从属的游戏引擎
        /// </summary>
        public GameEngine Engine { get; set; }
        /// <summary>
        /// X 坐标
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// Y 坐标
        /// </summary>
        public float Y { get; set; }
        /// <summary>
        /// 延迟帧数
        /// </summary>
        public int Delay { get; set; }
        /// <summary>
        /// 当前对象是否为删除状态
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 当前对象是否为正在碰撞状态
        /// </summary>
        public bool Collising { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public float Height { get; set; }
        /// <summary>
        /// Z轴偏移
        /// </summary>
        public int ZIndex { get; set; }
        /// <summary>
        /// 是否进行碰撞检查
        /// </summary>
        public bool CheckCollision { get; set; }
        /// <summary>
        /// 决定如何绘制自身
        /// </summary>
        /// <param name="canvas">Graphics</param>
        public abstract void Draw(Graphics canvas);
        /// <summary>
        /// 游戏对象被加入引擎后执行
        /// </summary>
        public abstract void Start();
        /// <summary>
        /// 更新自身属性
        /// </summary>
        protected abstract void Update();

        /// <summary>
        /// 延迟帧数计数
        /// </summary>
        private int delayCnt = 0;

        public void update() {
            // 如果标记为删除，则从引擎中移除
            if (Deleted)
            {
                RemoveSelf();
                return;
            }
            // 如果延迟帧数计数等于设置的延迟帧数
            // 执行Update方法
            if (delayCnt==Delay)
            {
                Update();
                // 碰撞带来的效果已经在Update中被处理
                // 取消正在碰撞的状态
                Collising = false;
                delayCnt = 0;
                return;
            }

            delayCnt++;
        }
        /// <summary>
        /// 从引擎中移除自身
        /// </summary>
        public void RemoveSelf()
        {
            Engine.RemoveGameObject(this);
        }
        /// <summary>
        /// 碰撞检测
        /// </summary>
        /// <param name="objs"></param>
        public void onCollision(GameObject obj) {
            // 如果处于碰撞状态则忽略新的碰撞事件
            // 因为并发执行的Update会不断触发新的碰撞事件
            // 所以应该保证碰撞事件被消费后再处理下一次碰撞
            if (Collising) return;
            // 标记为正在发生碰撞
            Collising = true;
            OnCollision(obj);
        }
        /// <summary>
        /// 碰撞检测
        /// </summary>
        /// <param name="objs"></param>
        protected abstract void OnCollision(GameObject obj);
        /// <summary>
        /// 绘制游戏对象的边界
        /// </summary>
        /// <param name="g"></param>
        public void drawBox(Graphics g)
        {
            g.DrawRectangle(Pens.Red, X, Y, Width, Height);
        }
        /// <summary>
        /// 发送消息与其他游戏对象进行通信
        /// </summary>
        /// <param name="msg">消息</param>
        public void SendMessage(object msg) {
            Engine.Broadcast(this, msg);
        }

        /// <summary>
        /// 收到消息后被触发
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="args">消息</param>
        public abstract void OnReceviedMessage(object sender,object args);
    }
}
