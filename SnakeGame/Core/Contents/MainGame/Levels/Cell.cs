using Engine.Graphics;
using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.Levels
{
    internal class Cell : IDrawable
    {
        public static readonly float _CellWidth = 32f;
        public static readonly float _CellHeight = 32f;

        private int X { get; }
        private int Y { get; }

        public IGameObject GameObject { get; private set; }
        
        public Cell(int x, int y, IGameObject gameObject = null) =>
            (X, Y, GameObject) = (x, y, gameObject);

        public bool IsEmpty() => GameObject is null || GameObject is EmptyObject;

        public void Draw(RenderTarget render)
        {
            if (IsEmpty()) return;
            GameObject.Draw(render);
        }

        internal void Fill(IGameObject gameObject)
        {
            this.GameObject = gameObject;
        }

        internal bool HasApple() => !IsEmpty() && this.GameObject is AppleObject;
    }
}
