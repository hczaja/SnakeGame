using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects
{
    internal class WallObject : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly RectangleShape Rectangle;

        public WallObject(int x, int y)
        {
            this.Rectangle = new RectangleShape()
            {
                Size = new(32f, 32f),
                Position = new(x * 32f, y * 32f),
                Texture = new Texture("Assets/Wall.png")
            };
        }

        public void Draw(RenderTarget render)
        {
            render.Draw(Rectangle);
        }

        public void Update()
        {

        }
    }
}
