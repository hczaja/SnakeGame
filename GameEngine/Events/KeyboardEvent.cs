﻿using static SFML.Window.Keyboard;

namespace Engine.Events;

public enum KeyboardEventType
{
    Unknown, Press, Release
}

public record KeyboardEvent
{
    public KeyboardEventType Type { get; init; }
    public Key Key { get; init; }

    public KeyboardEvent(KeyboardEventType type, Key key)
        => (Type, Key) = (type, key);
}
