using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starry2D.Engine;

namespace Starry2DDemos.GameObjects.StarrySky
{
    public class Star : BaseGameObject
    {
        const float maxSize = 2f;
        const float minSize = 0.01f;
        private float v;
        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        public Star()
        {
            var randSize = new Random(GetRandomSeed()).NextDouble();
            Width = Height = (float)(maxSize + randSize * (maxSize - minSize));
            
            for (int i = 0; i < 5; i++)
            {
                if (Width > (maxSize + minSize) / 5)
                {
                    randSize = new Random(GetRandomSeed()).NextDouble();
                    Width = Height = (float)(maxSize + randSize * (maxSize - minSize));
                    continue;
                }
                break;
            }

            v = (float)(4 / Math.Sqrt(Width));
        }
        public override void Start()
        {
            var rand = new Random(GetRandomSeed());
            X = rand.Next(Engine.Width);
            Y = rand.Next(Engine.Height);
        }
        public override void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.White, X, Y, Width, Height);
        }
        protected override void Update()
        {
            Y -= v;
            if (Y<=-Width)
            {
                RemoveSelf();
            }
        }
    }
}
