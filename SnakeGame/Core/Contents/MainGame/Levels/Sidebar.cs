using Engine.Graphics;
using Engine.Settings;
using Engine.Time;
using SFML.Graphics;
using Snakeventures.Actors;

namespace SnakeGame.Core.Contents.MainGame.Levels;

internal class Sidebar : IDrawable
{
    private readonly RectangleShape _background;

    private static readonly float OFFSET_PX = 4f;

    private readonly Text _levelName;

    private readonly GameTimer _timer;
    private readonly Text _timerText;

    private static readonly float ENERGY_BAR_MAX = 236f;
    private readonly RectangleShape _energyBar;
    private readonly Text _energyText;

    private static readonly float SPEED_BAR_MAX = 56f;
    private readonly RectangleShape _speedBar;

    private float EnergyValue { get; set; } = 1f;
    private float SpeedValue { get; set; } = 1f;
    private readonly float _maxSpeedValue;

    public Sidebar(IGameSettings settings)
    {
        _background = new RectangleShape()
        {
            Size = new(settings.WindowWidth - settings.WindowHeight, settings.WindowHeight),
            Position = new(settings.WindowHeight, 0f),
            Texture = new Texture("Assets/GUI/sidebar.png")
        };

        _levelName = new Text("01-01 : Training", settings.Font, settings.FontSizeBig)
        {
            Position = new(_background.Position.X + 3 * OFFSET_PX, 3 * OFFSET_PX)
        };

        _timer = new GameTimer();
        _timerText = new Text("00:000", settings.Font, settings.FontSizeSmall)
        {
            Position = new(_background.Position.X + 3 * OFFSET_PX, 12 * OFFSET_PX)
        };

        _energyBar = new RectangleShape()
        {
            Size = new(ENERGY_BAR_MAX, 35),
            Position = new(settings.WindowHeight + 10, 218),
            FillColor = new Color(217, 160, 102)
        };
        _energyText = new Text("100%", settings.Font, settings.FontSizeBig)
        {
            Position = new(
                _energyBar.Position.X + 3 * OFFSET_PX,
                _energyBar.Position.Y + OFFSET_PX),
            FillColor = settings.Theme
        };

        _maxSpeedValue = SnakeActor.MAX_SPEED;
        _speedBar = new RectangleShape()
        {
            Size = new(SPEED_BAR_MAX, 27),
            Position = new(settings.WindowHeight + 10, 181),
            FillColor = new Color(217, 160, 102)
        };
    }

    public void DrawBy(RenderTarget render)
    {
        render.Draw(_energyBar);
        render.Draw(_background);
        render.Draw(_levelName);
        render.Draw(_timerText);
        render.Draw(_energyText);
        render.Draw(_speedBar);
    }

    public void Update()
    {
        _timer.Update();

        _timerText.DisplayedString = $"{_timer.RealTime.ToString("0.000")}s";

        _energyBar.Size = new(ENERGY_BAR_MAX * EnergyValue, _energyBar.Size.Y);
        _energyText.DisplayedString = $"{(EnergyValue * 100).ToString("00")}%";

        _speedBar.Size = new(SPEED_BAR_MAX / (1f / _maxSpeedValue) * (1f / SpeedValue), _speedBar.Size.Y);
    }

    public void SetEnergy(float energy)
    {
        EnergyValue = energy;
        if (EnergyValue < 0)
            EnergyValue = 0;
        if (EnergyValue > 1f)
            EnergyValue = 1f;
    }

    public void SetSpeed(float speed)
    {
        SpeedValue = speed;
    }
}
