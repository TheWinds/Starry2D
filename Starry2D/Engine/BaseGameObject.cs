using System.Drawing;

namespace Starry2D.Engine
{
    /// <summary>
    /// 对GameObject提供基础实现
    /// </summary>
    public abstract class BaseGameObject : GameObject
    {
        protected override void OnCollision(GameObject obj) {}
        public override void Draw(Graphics g) { }
        public override void Start() { }
        public override void OnReceviedMessage(object sender,object msg) { }
    }
}
