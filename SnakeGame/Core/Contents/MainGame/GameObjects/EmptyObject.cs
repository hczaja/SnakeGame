using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects
{
    internal class EmptyObject : IGameObject
    {
        public static EmptyObject Instance = new EmptyObject();

        public int X => throw new NotImplementedException();

        public int Y => throw new NotImplementedException();

        public void Draw(RenderTarget render)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
