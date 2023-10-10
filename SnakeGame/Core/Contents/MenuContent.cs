using Engine.Core;
using Engine.Events;
using Engine.GameState;
using Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SnakeGame.Core.Events;

namespace SnakeGame.Core.Contents
{
    internal class MenuContent : IGameContent
    {
        private readonly IGameState _state;

        private readonly List<Button> _buttons;
        private int _currentButtonIndex;

        public MenuContent(IGameState state)
        {
            _state = state;

            this._buttons = new List<Button>();

            Vector2f size = new(64f, 32f);

            var startButton = new Button(size, new(32f, 32f),
                new Texture("Assets/Start_0.png"),
                new Texture("Assets/Start_1.png"));
            this._buttons.Add(startButton);

            var exitButton = new Button(
                size, 
                startButton.GetPosition() + new Vector2f(0f, startButton.GetSize().Y + 16f),
                new Texture("Assets/Exit_0.png"),
                new Texture("Assets/Exit_1.png"));
            this._buttons.Add(exitButton);

            this._currentButtonIndex = 0;
            this._buttons[this._currentButtonIndex].Cover();
        }

        public void DrawBy(RenderTarget render)
        {
            foreach (var button in _buttons)
            {
                button.DrawBy(render);
            }
        }

        public string GetLevelId() => throw new NotImplementedException();

        public object GetAdditionalData() => throw new NotImplementedException();

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
                    //_state.Handle(
                    //    new ChangeContentEvent(
                    //        this._currentButtonIndex == 0
                    //            ? ChangeContentEventType.LevelsMenu
                    //            : ChangeContentEventType.Exit));
                }

                foreach (var button in _buttons)
                    button.Uncover();

                _buttons[this._currentButtonIndex].Cover();
            }
        }

        public void Update() { }
    }
}
