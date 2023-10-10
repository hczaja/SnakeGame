using Engine.Events;
using Engine.GameState;
using Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Snakeventures.Contents;

internal class LevelSummaryContent : IGameContent
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

        Summary = new Text()
        {
            Font = state.GetSettings().Font,
            //DisplayedString = (string)_state.ActualContent.GetAdditionalData(),
            Position = new(32f, 32f),
            CharacterSize = 32
        };

        _buttons = new List<Button>();

        Vector2f size = new(64f, 32f);

        var restart = new Button(size, new(Summary.Position.X, Summary.Position.Y + Summary.GetGlobalBounds().Height + 32f),
            new Texture("Assets/Start_0.png"),
            new Texture("Assets/Start_1.png"));
        _buttons.Add(restart);

        var back = new Button(
            size,
            restart.GetPosition() + new Vector2f(0f, restart.GetSize().Y + 16f),
            new Texture("Assets/Exit_0.png"),
            new Texture("Assets/Exit_1.png"));
        _buttons.Add(back);

        _currentButtonIndex = 0;
        _buttons[_currentButtonIndex].Cover();
    }

    public void DrawBy(RenderTarget render)
    {
        render.Draw(Summary);

        foreach (var button in _buttons)
        {
            button.DrawBy(render);
        }
    }

    public void Handle(KeyboardEvent @event)
    {
        if (@event.Type == KeyboardEventType.Press)
        {
            if (@event.Key == Keyboard.Key.Up)
            {
                _currentButtonIndex--;

                if (_currentButtonIndex < 0)
                    _currentButtonIndex = _buttons.Count - 1;

                _currentButtonIndex %= _buttons.Count;
            }
            else if (@event.Key == Keyboard.Key.Down)
            {
                _currentButtonIndex++;
                _currentButtonIndex %= _buttons.Count;
            }
            else if (@event.Key == Keyboard.Key.Enter)
            {
                //_state.Handle(
                //    new ChangeContentEvent(
                //        this._currentButtonIndex == 0
                //            ? ChangeContentEventType.Game
                //            : ChangeContentEventType.LevelsMenu));
            }

            foreach (var button in _buttons)
                button.Uncover();

            _buttons[_currentButtonIndex].Cover();
        }
    }

    public void Update() { }

    public string GetLevelId() => _levelId;

    public object GetAdditionalData() => throw new NotImplementedException();
}
