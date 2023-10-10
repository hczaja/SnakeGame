using Engine.Events;
using Engine.Graphics;
using SFML.Graphics;
using Engine.GameObjects;
using Engine.GameState;
using Snakeventures.Actors;
using Engine.Actors;

namespace Snakeventures.Levels;

internal class Level :
    IDrawable, IUpdatable, IEventHandler<KeyboardEvent>
{
    public string Name { get; }

    private readonly int _maxCells;

    private readonly RectangleShape _background;
    private readonly Sidebar _sidebar;

    private int N { get; }
    private int M { get; }

    private Cell[,] Cells { get; }

    private SnakeActor Player { get; set; }

    private readonly IGameState _state;

    public Level(string name, int n, int m, IGameState state)
    {
        _state = state;

        var settings = state.GetSettings();

        _sidebar = new Sidebar(settings);
        _maxCells = (int)settings.WindowHeight / (int)Cell.CELL_SIZE;

        _background = new RectangleShape()
        {
            Size = new(_maxCells * Cell.CELL_SIZE, _maxCells * Cell.CELL_SIZE),
            Texture = new Texture("Assets/Levels/01_01.png")
        };

        Name = name;
        N = n;
        M = m;

        Cells = new Cell[N, M];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                Cells[i, j] = new Cell(i, j);
            }
        }
    }

    public void FillCell(int n, int m, GameActor gameObject)
    {
        Cells[n, m].Fill(gameObject);
        if (gameObject is SnakeActor snake)
            Player = snake;
    }

    public void DrawBy(RenderTarget render)
    {
        render.Draw(_background);
        _sidebar.DrawBy(render);

        foreach (var cell in Cells)
        {
            cell.DrawBy(render);
        }
    }

    public void Update()
    {
        _sidebar.Update();

        Player.Update();
        _sidebar.SetEnergy(Player.CurrentEnergy);
        _sidebar.SetSpeed(Player.CurrentSpeed);

        foreach (var cell in Cells)
        {
            cell.Update();
        }

        CheckCollisions();
    }

    private void CheckCollisions()
    {
        var obj = Cells[Player.X, Player.Y].GameObject;
        if (obj is not null)
        {
            if (obj is AppleObject apple)
            {
                Cells[Player.X, Player.Y].Fill(EmptyObject.Instance);

                if (apple.IsRed())
                {
                    //Player.Elongates();

                    if (CheckVictoryConditions())
                    {
                        //_state.Handle(new ChangeContentEvent(ChangeContentEventType.LevelSummary));
                    }
                }
                else if (apple.IsYellow())
                {
                    //Player.ChargeEnergy();
                }
                else if (apple.IsGreen())
                {

                }

            }
            else if (obj is PortalObject portal)
            {
                //Player.EntersPortal(portal.DestinationX, portal.DestinationY);
            }
            else if (obj is WallObject /*|| Player.EatsOwnTail()*/)
            {
                //_state.Handle(new ChangeContentEvent(ChangeContentEventType.LevelSummary));
            }
        }
    }

    private bool CheckVictoryConditions()
    {
        for (var i = 0; i < N; i++)
        {
            for (var j = 0; j < M; j++)
            {
                if (Cells[i, j].HasApple())
                    return false;
            }
        }

        return true;
    }

    public int GetMaxApples()
    {
        int max = 0;
        for (var i = 0; i < N; i++)
        {
            for (var j = 0; j < M; j++)
            {
                if (Cells[i, j].HasApple())
                    max++;
            }
        }

        return max;
    }

    public int GetAtedApples() => 0; // Player.Length; 

    public void Handle(KeyboardEvent @event)
    {
        Player.Handle(@event);
    }
}
