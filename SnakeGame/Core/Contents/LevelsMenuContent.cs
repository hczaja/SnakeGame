using Engine.Core;
using Engine.Events;
using Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents
{
    internal class LevelsMenuContent : IContent
    {
        private readonly IGameState _state;

        private readonly List<Button> _buttons;
        private int _currentButtonIndex;

        public LevelsMenuContent(IGameState state)
        {
            _state = state;

            this._buttons = new List<Button>();

            Vector2f size = new(64f, 32f);

            var level1 = new Button(size, new(32f, 32f),
                new Texture("Assets/Level_1_0.png"),
                new Texture("Assets/Level_1_1.png"));
            this._buttons.Add(level1);

            var level2 = new Button(
                size,
                level1.GetPosition() + new Vector2f(0f, level1.GetSize().Y + 16f),
                new Texture("Assets/Level_2_0.png"),
                new Texture("Assets/Level_2_1.png"));
            this._buttons.Add(level2);
            
            var level3 = new Button(
                size,
                level2.GetPosition() + new Vector2f(0f, level2.GetSize().Y + 16f),
                new Texture("Assets/Level_3_0.png"),
                new Texture("Assets/Level_3_1.png"));
            this._buttons.Add(level3);

            var exitButton = new Button(
                size,
                level3.GetPosition() + new Vector2f(0f, level3.GetSize().Y + 16f),
                new Texture("Assets/Exit_0.png"),
                new Texture("Assets/Exit_1.png"));
            this._buttons.Add(exitButton);

            this._currentButtonIndex = 0;
            this._buttons[this._currentButtonIndex].Cover();
        }

        public void Draw(RenderTarget render)
        {
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
                            this._currentButtonIndex == this._buttons.Count - 1
                                ? ChangeContentEventType.MainMenu
                                : ChangeContentEventType.Game));
                }

                foreach (var button in _buttons)
                    button.Uncover();

                _buttons[this._currentButtonIndex].Cover();
            }
        }

        public void Update() { }

        public string GetLevelId() => (this._currentButtonIndex + 1).ToString();

        public object GetAdditionalData() => throw new NotImplementedException();
    }
}
