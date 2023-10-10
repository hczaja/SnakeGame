using Engine.Events;
using Engine.GameState;
using Engine.Graphics;
using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame;
using SnakeGame.Core.Contents.MainGame.Levels;

namespace SnakeGame.Core.Contents;

internal class GameContent : IGameContent
{
    private readonly IGameState _state;

    private readonly Level _level;
    private readonly Score _score;

    private readonly string _levelId;

    public GameContent(IGameState state, string levelId)
    {
        _state = state;
        _levelId = levelId;

        var loader = new LevelLoader();
        _level = loader.Load($"Assets/Levels/Level{levelId}.txt", _state);

        _score = new Score(
            _level.GetMaxApples(),
            _level.Name,
            _state.GetSettings());
    }

    public void DrawBy(RenderTarget render) 
    {
        _level.DrawBy(render);
        _score.DrawBy(render);
    }

    public string GetLevelId() => _levelId;

    public object GetAdditionalData()
    {
        _score.Update(_level);
        return _score.GetFinalScore();
    }

    public void Handle(KeyboardEvent @event) 
    {
        _level.Handle(@event);
    }

    public void Update() 
    {
        _level.Update();
        _score.Update(_level);
    }
}
