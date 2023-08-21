using Engine.Core;
using Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects
{
    internal class SnakeHeadObject : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        
        private Vector2f Direction;

        private readonly RectangleShape Rectangle;

        private static readonly Vector2f DirectionLeft = new Vector2f(-1f, 0f);
        private static readonly Texture HeadLeftTexture = new Texture("Assets/SnakeHead_L.png"); 

        private static readonly Vector2f DirectionRight = new Vector2f(1f, 0f);
        private static readonly Texture HeadRightTexture = new Texture("Assets/SnakeHead_R.png"); 
        
        private static readonly Vector2f DirectionUp = new Vector2f(0f, 1f);
        private static readonly Texture HeadUpTexture = new Texture("Assets/SnakeHead_U.png"); 
        
        private static readonly Vector2f DirectionDown = new Vector2f(0f, -1f);
        private static readonly Texture HeadDownTexture = new Texture("Assets/SnakeHead_D.png");

        public SnakeHeadObject(int x, int y) 
        {
            this.X = x; 
            this.Y = y;

            this.Rectangle = new RectangleShape()
            {
                Size = new(Cell.CELL_SIZE, Cell.CELL_SIZE),
                Position = new(x * Cell.CELL_SIZE, y * Cell.CELL_SIZE),
                Texture = new Texture("Assets/SnakeHead_U.png")
            };

            this.TurnUp();
        }

        public void TurnLeft()
        {
            if (Direction.X != DirectionRight.X)
            {
                Direction = DirectionLeft;
                this.Rectangle.Texture = HeadLeftTexture;
            }
        }

        public void TurnUp()
        {
            if (Direction.Y != DirectionDown.Y)
            {
                Direction = DirectionDown;
                this.Rectangle.Texture = HeadUpTexture;
            }
        }

        public void TurnRight()
        {
            if (Direction.X != DirectionLeft.X)
            {
                Direction = DirectionRight;
                this.Rectangle.Texture = HeadRightTexture;
            }
        }

        public void TurnDown()
        {
            if (Direction.Y != DirectionUp.Y)
            {
                Direction = DirectionUp;
                this.Rectangle.Texture = HeadDownTexture;
            }
        }
        
        public void Draw(RenderTarget render)
        {
            render.Draw(this.Rectangle);
        }

        public void Update()
        {
            this.X += (int)this.Direction.X;
            this.Y += (int)this.Direction.Y;

            this.Rectangle.Position = new(
                Cell.CELL_SIZE * X,
                Cell.CELL_SIZE * Y);
        }

        internal void MoveTo(int destinationX, int destinationY)
        {
            this.X = destinationX + (int)Direction.X;
            this.Y = destinationY + (int)Direction.Y;

            this.Rectangle.Position = new(
                Cell.CELL_SIZE * X,
                Cell.CELL_SIZE * Y);
        }
    }
}
