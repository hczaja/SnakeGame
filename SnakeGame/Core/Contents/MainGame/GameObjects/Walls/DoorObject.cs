using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Walls
{
    internal class DoorObject : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly RectangleShape Shape;

        private bool IsClosed { get; set; } = true;

        private static readonly Texture OPENED_TEXTURE = new Texture("Assets/Doors/door_opened.png");
        private static readonly Texture CLOSED_TEXTURE = new Texture("Assets/Doors/door_closed.png");

        public DoorObject(int x, int y)
        {
            Shape = new RectangleShape()
            {
                Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
                Position = new(x * Cell.CELL_SIZE, y * Cell.CELL_SIZE),
                Texture = CLOSED_TEXTURE
            };
        }

        public void Draw(RenderTarget render)
        {
            render.Draw(Shape);
        }

        public void Update()
        {

        }
    }
}
