using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Events
{
    public enum ChangeContentEventType
    {
        Unknown, 
        MainMenu, 
        LevelsMenu,
        Game,
        LevelSummary,
        Exit
    }

    public record ChangeContentEvent
    {
        public ChangeContentEventType Type { get; init; }

        public ChangeContentEvent(ChangeContentEventType type)
            => (Type) = type;
    }
}
