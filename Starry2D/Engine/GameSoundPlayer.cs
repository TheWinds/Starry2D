using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;

namespace Starry2D.Engine
{
    /// <summary>
    /// 音效播放器
    /// </summary>
    public class GameSoundPlayer
    {
        // 是否正在播放
        private bool isPlaying;
        // 资源缓存
        private Dictionary<Stream, SoundPlayer> sounds=new Dictionary<Stream, SoundPlayer>();
        public void Play(Stream sound,bool interrupt)
        {
            // 缓存
            if (!sounds.ContainsKey(sound))
            {
                sounds[sound] = new SoundPlayer(sound);
            }
            // 不允许打断且正在播放则返回
            if (!interrupt && isPlaying) return;

            // 播放
            var playThread = new Thread(() => {
                play(sounds[sound]);
            });
            playThread.Start();
        }

        private void play(SoundPlayer player)
        {
            isPlaying = true;
            player.PlaySync();
            isPlaying = false;
        }
    }
}
