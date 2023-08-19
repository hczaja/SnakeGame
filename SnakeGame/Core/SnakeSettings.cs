using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Core;

namespace SnakeGame
{
    internal class SnakeSettings : IGameSettings
    {
        public uint WindowWidth => 800;
        public uint WindowHeight => 600;
        public string GameTitle => "Snake";
        public string GameVersion => throw new NotImplementedException();
        public int FPS => 30;
    }
}
