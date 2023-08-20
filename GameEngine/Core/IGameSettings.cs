using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public interface IGameSettings
    {
        uint WindowWidth { get; }
        uint WindowHeight { get; }
        string GameTitle { get; }
        string GameVersion { get; }
        int FPS { get; }
        bool EnableKeyRepeat { get; }
    }
}
