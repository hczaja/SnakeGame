using Engine.Core;
using Engine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Graphics
{
    public interface IContent :
        IDrawable,
        IUpdatable,
        IEventHandler<KeyboardEvent>
    {
        string GetLevelId();
        object GetAdditionalData();
    }
}
