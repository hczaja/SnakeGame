using Engine.Core;
using Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects
{
    internal interface IGameObject : IDrawable, IUpdatable
    {
        int X { get; }
        int Y { get; }
    }
}
