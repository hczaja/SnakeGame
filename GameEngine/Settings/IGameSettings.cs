using SFML.Graphics;

namespace Engine.Settings
{
    public interface IGameSettings
    {
        uint WindowWidth { get; }
        uint WindowHeight { get; }
        string GameTitle { get; }
        string GameVersion { get; }
        int FPS { get; }
        bool EnableKeyRepeat { get; }
        bool MouseCursorVisible { get; }
        Font Font { get; }
        Color Theme { get; }
        uint FontSizeBig { get; }
        uint FontSizeSmall { get; }
    }
}
