using SFML.Graphics;
using static SFML.Window.Mouse;

namespace Engine.Events;

public enum MouseEventType
{
    Unknown, Move, Press, Release
}

public record MouseEvent
{
    public MouseEventType Type { get; init; }
    public float X { get; init; }
    public float Y { get; init; }
    public Button Button { get; init; }

    public MouseEvent(MouseEventType type, float x, float y, Button button)
        => (Type, X, Y, Button) = (type, x, y, button);

    public static bool IsMouseEventRaisedIn(FloatRect rect, MouseEvent e) =>
        rect.Left < e.X && e.X < rect.Left + rect.Width &&
        rect.Top < e.Y && e.Y < rect.Top + rect.Height;
}
