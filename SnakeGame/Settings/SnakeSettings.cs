using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Settings;
using SFML.Graphics;

namespace SnakeGame
{
    internal class SnakeSettings : IGameSettings
    {
        public uint WindowWidth => 1024;
        public uint WindowHeight => 768;
        public string GameTitle => "Snake";
        public string GameVersion => throw new NotImplementedException();
        public int FPS => 30;

        public bool EnableKeyRepeat => false;
        public bool MouseCursorVisible => false;

        public Font Font => new Font("Assets/rainyhearts.ttf");
        public Color Theme => new Color(24, 9, 20);

        public uint FontSizeBig => 24;
        public uint FontSizeSmall => 16;

    }
}
