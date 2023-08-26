using Engine.Graphics;
using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Walls
{
    internal class FountainObject : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly Animation Iddle;

        public FountainObject(int x, int y)
        {
            X = x;
            Y = y;

            this.Iddle = new Animation(
                new Texture("Assets/Spritesheets/spritesheet_fountain.png"),
                (int)Cell.CELL_SIZE,
                () => new(X * Cell.CELL_SIZE, Y * Cell.CELL_SIZE),
                3, 0.5f, isLooped: true);

            this.Iddle.Start();
        }

        public void Draw(RenderTarget render)
        {
            Iddle.Draw(render);
        }

        public void Update()
        {

        }
    }
}
