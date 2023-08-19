using Engine.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class GameEngine
    {
        private readonly GameWindow _window;
        private readonly GameClock _clock;

        public GameEngine(IGame game, IGameSettings settings)
        {
            this._window = new GameWindow(game, settings);
            this._clock = new GameClock(settings);
        }

        public void Start()
        {
            while (this._window.IsOpen())
            {
                this._clock.Update();
                if (this._clock.IsUpdated())
                {
                    this._window.DispatchEvents();

                    this._window.Update();

                    this._window.Clear();
                    this._window.Draw();
                    this._window.Display();

                    this._clock.Restart();
                }
            }
        }
    }
}
