using Engine.Events;
using Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public interface ICoreGameState :
        IDrawable,
        IUpdatable,
        IEventHandler<KeyboardEvent>
    {
        IGameSettings GetSettings();
    }
}
