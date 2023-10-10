using Engine.Actors;
using Engine.Events;
using SFML.Graphics;
using Snakeventures.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakeventures.Actors
{
    internal enum PortalType
    {
        Red, Blue
    }

    internal class PortalObject : GameActor
    {
        public override int X { get; protected set; }
        public override int Y { get; protected set; }

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

        public override void DrawBy(RenderTarget render)
        {
            render.Draw(Rectangle);
        }

        public override void Update()
        {

        }

        internal void SetDestination(PortalObject end)
        {
            DestinationX = end.X;
            DestinationY = end.Y;
        }

        public override void CheckCollisions()
        {
            throw new NotImplementedException();
        }

        public override void Handle(KeyboardEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
