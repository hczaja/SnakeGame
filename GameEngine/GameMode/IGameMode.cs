using Engine.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.GameMode;

public interface IGameMode
{
    IGameLevel CurrentLevel { get; }
    
    void InitGame();

    void InitLevel();
    void ChangeLevel(IGameLevel level);
}
