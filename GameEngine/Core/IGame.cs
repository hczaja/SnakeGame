﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public interface IGame
    {
        void _window_MouseMoved(object? sender, EventArgs e);
        void _window_KeyPressed(object? sender, KeyEventArgs e);
        void _window_KeyReleased(object? sender, KeyEventArgs e);
        void _window_MouseButtonReleased(object? sender, MouseButtonEventArgs e);
        void _window_MouseButtonPressed(object? sender, MouseButtonEventArgs e);
        void Draw(RenderTarget window);
        void Update();
    }
}
