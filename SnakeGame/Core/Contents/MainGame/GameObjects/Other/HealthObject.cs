using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Other
{
    internal class HealthObject : IGameObject
    {
        private readonly List<HeartObject> _hearts;

        public int X => throw new NotSupportedException();

        public int Y => throw new NotSupportedException();

        public HealthObject(IGameObject parent)
        {
            _hearts = new List<HeartObject>();
            _hearts.Add(new HeartObject(parent));
        }

        public void Draw(RenderTarget render)
        {
            foreach (var heart in _hearts)
            {
                heart.Draw(render);
            }
        }

        public void Update()
        {

        }
    }
}
