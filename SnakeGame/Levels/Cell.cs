﻿using Engine.Actors;
using Engine.GameObjects;
using Engine.Graphics;
using SFML.Graphics;
using Snakeventures.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakeventures.Levels
{
    internal class Cell : IDrawable, IUpdatable
    {
        public static readonly float CELL_SIZE = 24f;

        private int X { get; }
        private int Y { get; }

        public GameActor GameObject { get; private set; }

        public Cell(int x, int y, GameActor gameObject = null) =>
            (X, Y, GameObject) = (x, y, gameObject);

        public bool IsEmpty() => GameObject is null || GameObject is EmptyObject;

        public void DrawBy(RenderTarget render)
        {
            if (IsEmpty()) return;
            GameObject.DrawBy(render);
        }
        internal void Fill(GameActor gameObject)
        {
            GameObject = gameObject;
        }

        internal bool HasApple() => !IsEmpty() && GameObject is AppleObject;

        public void Update()
        {
            if (IsEmpty()) return;
            GameObject.Update();
        }
    }
}
