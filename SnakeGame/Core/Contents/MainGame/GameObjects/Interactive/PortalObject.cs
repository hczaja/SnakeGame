using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Interactive
{
    internal enum PortalType
    {
        Red, Blue
    }

    internal class PortalObject : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public int DestinationX { get; private set; }
        public int DestinationY { get; private set; }

        public PortalType Type { get; private set; }

        private readonly RectangleShape Rectangle;

        public PortalObject(int x, int y, PortalType type)
        {
            X = x;
            Y = y;

            Type = type;

            Rectangle = new RectangleShape()
            {
                Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
                Position = new(x * Cell.CELL_SIZE, y * Cell.CELL_SIZE),
                Texture = new Texture($"Assets/Portal{type}.png")
            };
        }

        public void Draw(RenderTarget render)
        {
            render.Draw(Rectangle);
        }

        public void Update()
        {

        }

        internal void SetDestination(PortalObject end)
        {
            DestinationX = end.X;
            DestinationY = end.Y;
        }
    }
}
