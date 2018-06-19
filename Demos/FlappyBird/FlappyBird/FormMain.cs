using FlappyBird.GameObjects;
using System;
using System.Windows.Forms;
using Starry2D.Engine;

namespace FlappyBird
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        // 游戏引擎
        GameEngine engine;
        // 鸟
        Bird bird;
        // 得分
        Points points;
        // 是否结束
        bool isOver;
        private void gameBegin()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            // 设置背景和高度一样
            Height = Properties.Resources.background_day.Height;
            // 新建引擎
            engine = new GameEngine(CreateGraphics(), Width, Height);
            // 设置碰撞检测算法
            engine.CollisionDetectionFunc = (from, to) =>
            {
                if ((from.GetType() == typeof(Bird))
                    && (to.GetType() == typeof(Pipe)))
                {
                    var pipe = to as Pipe;
                    var birdf = from as Bird;
                    if ((birdf.X + birdf.Width > pipe.X) && (birdf.X + birdf.Width < pipe.X + pipe.Width))
                    {
                        if (pipe.Reverse)
                        {
                            if (birdf.Y < pipe.Y + pipe.Height)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (birdf.Y + birdf.Height > pipe.Y)
                            {
                                return true;
                            }
                        }
                    }
                }
                if ((from.GetType() == typeof(Bird))
                    && (to.GetType() == typeof(Diamonds)))
                {
                    if ((from.X + from.Width > to.X) && (from.X + from.Width < to.X + to.Width))
                    {
                        if (from.Y + from.Height > to.Y)
                        {
                            return true;
                        }
                    }
                }
                return false;
            };
            // 加入背景元素
            engine.AddGameObject(new Sky());
            engine.AddGameObject(new Ground());
            // 加入鸟
            bird = new Bird();
            engine.AddGameObject(bird);
            // 加入分数
            points = new Points();
            engine.AddGameObject(points);
            // 加入初始的管道
            engine.AddGameObject(new Pipe());
            var rPipe = new Pipe();
            rPipe.Reverse = true;
            engine.AddGameObject(rPipe);
            // 产生管道的时钟
            timer1.Interval = 4000;
            // 绑定引擎事消息件
            engine.messageEvent += Engine_messageEvent;
            engine.Debug = true;
            engine.RenderPauseTime = 5;
            // 启动游戏
            engine.Run();
            timer1.Start();
            isOver = false;
        }

        private void gameOver()
        {
            isOver = true;
            timer1.Stop();
            bird.Deleted = true;
            var gameOverObject = new UIPicture(Properties.Resources.gameover);
            gameOverObject.CenterHorizontal = true;
            gameOverObject.CenterVertical = true;
            engine.AddGameObject(gameOverObject);
        }

        private void reBeginGame()
        {
            engine.Stop();
            gameBegin();
        }
        private void Engine_messageEvent(object sender, object e)
        {
            var msg = e as GameMsg;
            switch (msg.MsgType)
            {
                case GameMsgType.CrossPipe:
                    if (isOver) return;
                    points.Point++;
                    engine.PlaySound(Properties.Resources.point, true);
                    break;
                case GameMsgType.Died:

                    gameOver();
                    break;
                default:
                    break;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            gameBegin();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            engine.AddGameObject(new Pipe());
            var rPipe = new Pipe();
            rPipe.Reverse = true;
            engine.AddGameObject(rPipe);
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (isOver)
                {
                    reBeginGame();
                    return;
                }
                bird.Up();
                engine.PlaySound(Properties.Resources.wing);
            }
        }

        private void FormMain_Click(object sender, EventArgs e)
        {
        }
    }
}
