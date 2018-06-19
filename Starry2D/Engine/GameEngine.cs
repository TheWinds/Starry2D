using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace Starry2D.Engine
{
    public class GameEngine
    {
        
        private Graphics baseGraphics;
        private Graphics bufferGraphics;
        // 背景颜色
        private Color background;
        // 渲染暂停时间
        private int renderPauseTime;
        // 清除画刷
        private Brush clearBrush;       
        // 更新Action
        private event Action updateEvent;
        // 更新进程
        private Thread updateThread;
        // 渲染进程
        private Thread renderThread;
        // 是否正在进行
        private bool isRuning = false;
        // 用于缓冲的buffer
        private Bitmap buffer;
        // 每一秒的渲染次数
        private int renderTimesPeerSecond = 0;
        // fps
        private float fps = 0;
        // 音效播放器
        private GameSoundPlayer gameSoundPlayer;
        // FPS计数对象
        private FPSGameObj fpsGameObj;
        
        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color Background
        {
            get
            {
                return background;
            }
            set
            {
                background = value;
                clearBrush = new SolidBrush(background);
            }
        }
        /// <summary>
        /// 渲染暂停时间 (ms)
        /// </summary>
        public int RenderPauseTime
        {
            get
            {
                return renderPauseTime == 0 ? 5 : renderPauseTime;
            }
            set
            {
                renderPauseTime = value;
            }
        }
        /// <summary>
        /// 碰撞检测方法
        /// </summary>
        public Func<GameObject, GameObject, bool> CollisionDetectionFunc;
        /// <summary>
        /// 游戏对象列表
        /// </summary>
        public List<GameObject> GameObjects { get; private set; }
        /// <summary>
        ///  宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 调试模式
        /// </summary>
        public bool Debug { get; set; }
        // 消息事件
        public event EventHandler<object> messageEvent;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fromGraphics">基础Graphics</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public GameEngine(Graphics fromGraphics, int width, int height)
        {
            Width = width;
            Height = height;
            // 创建游戏对象集合
            GameObjects = new List<GameObject>();
            baseGraphics = fromGraphics;
            // 启动更新线程
            updateThread = new Thread(update);
            updateThread.IsBackground = true;
            // 启动渲染线程
            renderThread = new Thread(render);
            renderThread.IsBackground = true;
            // 创建缓冲Bitmap
            buffer = new Bitmap(width, height);
            // 创建缓冲画布
            bufferGraphics = Graphics.FromImage(buffer);
            var clipRect = new RectangleF(0, 0, Width, Height);
            bufferGraphics.SetClip(clipRect);
            baseGraphics.SetClip(clipRect);
            // 创建音效播放器
            gameSoundPlayer = new GameSoundPlayer();
        }

        /// <summary>
        /// 运行
        /// </summary>
        public void Run()
        {
            isRuning = true;
            updateThread.Start();
            renderThread.Start();
            var countThread = new Thread(countFPS);
            countThread.IsBackground = true;
            countThread.Start();
        }

        /// <summary>
        /// 停止游戏释放资源
        /// </summary>
        public void Stop()
        {
            isRuning = false;
            lock (GameObjects)
            {
                GameObjects.Clear();
            }
            buffer.Dispose();
            bufferGraphics.Dispose();
        }

        /// <summary>
        /// 统计FPS
        /// </summary>
        private void countFPS()
        {

            while (isRuning)
            {
                Thread.Sleep(1000);
                fps = renderTimesPeerSecond;
                renderTimesPeerSecond = 0;
                if (Debug)
                {
                    if (fpsGameObj == null)
                    {
                        fpsGameObj = new FPSGameObj();
                        AddGameObject(fpsGameObj);
                    }
                    fpsGameObj.FPS = (int)fps;
                }
            }

        }


        /// <summary>
        ///  渲染线程
        /// </summary>
        private void render()
        {
            while (isRuning)
            {
                // 清除画布
                bufferGraphics.Clear(Background);
                // 绘制所有 GameObject
                lock (GameObjects)
                {
                    foreach (var obj in GameObjects.OrderBy(o => o.ZIndex))
                    {
                        obj?.Draw(bufferGraphics);
                        //if (Debug) obj?.drawBox(graphics);
                    }
                    // 将buffer画到 Graphics 上
                    GDIUtils.DrawImage(baseGraphics, buffer, Width, Height);
                }
                renderTimesPeerSecond++;
                // 暂停 5ms
                Thread.Sleep(renderPauseTime);
            }
        }

        // 碰撞检测
        private void collisionDetecte()
        {
            if (CollisionDetectionFunc == null) return;
            // 遍历所有游戏对象
            for (int i = 0; i < GameObjects.Count; i++)
            {
                for (int j = i + 1; j < GameObjects.Count; j++)
                {
                    var from = GameObjects[i];
                    var to = GameObjects[j];
                    if (from.CheckCollision && to.CheckCollision)
                    {
                        // 检测碰撞
                        if (CollisionDetectionFunc(from, to))
                        {
                            from.onCollision(to);
                            to.onCollision(from);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 更新线程
        /// </summary>
        private void update()
        {
            while (isRuning)
            {
                // 暂停 15ms
                Thread.Sleep(15);
                // 通知 GameObject Update自身属性
                lock (GameObjects)
                {
                    // 碰撞检测
                    collisionDetecte();
                    // 执行GameObject的update方法
                    updateEvent?.Invoke();                    
                }
            }
        }

        /// <summary>
        /// 添加 GameObject 到引擎中
        /// </summary>
        /// <param name="obj">GameObject</param>
        public void AddGameObject(GameObject obj)
        {
            lock (GameObjects)
            {
                GameObjects.Add(obj);
            }

            updateEvent += obj.update;
            obj.Engine = this;
            messageEvent += obj.OnReceviedMessage;
            obj.Start();
        }

        /// <summary>
        /// 从引擎移除 GameObject
        /// </summary>
        /// <param name="obj">GameObject</param>
        public void RemoveGameObject(GameObject obj)
        {
            lock (GameObjects)
            {
                GameObjects.Remove(obj);
            }
            updateEvent -= obj.update;
            messageEvent -= obj.OnReceviedMessage;
            obj.Engine = null;
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="msg">消息</param>
        public void Broadcast(GameObject sender, object msg)
        {
            lock (GameObjects)
            {
                messageEvent?.Invoke(sender, msg);
            }
        }

        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="sound">声音文件</param>
        public void PlaySound(Stream sound)
        {
            gameSoundPlayer.Play(sound, false);
        }

        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="sound">声音文件</param>
        /// <param name="breaking">是否可以打断</param>
        public void PlaySound(Stream sound, bool breaking)
        {
            gameSoundPlayer.Play(sound, true);
        }       
    }
}
