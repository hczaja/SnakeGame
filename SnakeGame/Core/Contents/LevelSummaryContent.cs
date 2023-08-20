using Engine.Events;
using Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents
{
    internal class LevelSummaryContent : IContent
    {
        private readonly IGameState _state;
        private readonly string _levelId;

        private Text Summary;

        private readonly List<Button> _buttons;
        private int _currentButtonIndex;

        public LevelSummaryContent(IGameState state, string levelId)
        {
            _state = state;
            _levelId = levelId;

            this.Summary = new Text() 
            { 
                Font = state.GetSettings().Font,
                DisplayedString = (string)_state.ActualContent.GetAdditionalData(),
                Position = new (32f, 32f),
                CharacterSize = 32
            };

            this._buttons = new List<Button>();

            Vector2f size = new(64f, 32f);

            var restart = new Button(size, new(Summary.Position.X, Summary.Position.Y + Summary.GetGlobalBounds().Height + 32f),
                new Texture("Assets/Start_0.png"),
                new Texture("Assets/Start_1.png"));
            this._buttons.Add(restart);

            var back = new Button(
                size,
                restart.GetPosition() + new Vector2f(0f, restart.GetSize().Y + 16f),
                new Texture("Assets/Exit_0.png"),
                new Texture("Assets/Exit_1.png"));
            this._buttons.Add(back);

            this._currentButtonIndex = 0;
            this._buttons[this._currentButtonIndex].Cover();
        }

        public void Draw(RenderTarget render)
        {
            render.Draw(Summary);

            foreach (var button in _buttons)
            {
                button.Draw(render);
            }
        }

        public void Handle(KeyboardEvent @event)
        {
            if (@event.Type == KeyboardEventType.Press)
            {
                if (@event.Key == Keyboard.Key.Up)
                {
                    this._currentButtonIndex--;

                    if (this._currentButtonIndex < 0)
                        this._currentButtonIndex = this._buttons.Count - 1;

                    this._currentButtonIndex %= _buttons.Count;
                }
                else if (@event.Key == Keyboard.Key.Down)
                {
                    this._currentButtonIndex++;
                    this._currentButtonIndex %= _buttons.Count;
                }
                else if (@event.Key == Keyboard.Key.Enter)
                {
                    _state.Handle(
                        new ChangeContentEvent(
                            this._currentButtonIndex == 0
                                ? ChangeContentEventType.Game
                                : ChangeContentEventType.LevelsMenu));
                }

                foreach (var button in _buttons)
                    button.Uncover();

                _buttons[this._currentButtonIndex].Cover();
            }
        }

        public void Update() { }

        public string GetLevelId() => _levelId;

        public object GetAdditionalData() => throw new NotImplementedException();
    }
}
