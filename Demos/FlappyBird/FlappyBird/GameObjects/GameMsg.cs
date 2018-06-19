using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird.GameObjects
{
    public class GameMsg
    {
        public GameMsgType MsgType { get; set; }
        public object Content { get; set; }
    }

    public enum GameMsgType
    {
        /// <summary>
        /// 通过水管
        /// </summary>
        CrossPipe,
        /// <summary>
        /// 小鸟死亡
        /// </summary>
        Died,
        /// <summary>
        /// 吃掉钻石
        /// </summary>
        Pick,
    }
}
