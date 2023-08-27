using Engine.Core;
using Engine.Events;
using Engine.Graphics;
using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.GameObjects;
using SnakeGame.Core.Contents.MainGame.GameObjects.Walls;
using SnakeGame.Core.Contents.MainGame.GameObjects.Player;
using SnakeGame.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Time;

namespace SnakeGame.Core.Contents.MainGame.Levels
{
    internal class LevelSideBar : IDrawable
    {
        private readonly RectangleShape _shape;

        private static readonly float OFFSET_PX = 4f;

        private readonly Text _levelName;

        private readonly GameTimer _timer;
        private readonly Text _timerText;

        private static readonly float ENERGY_BAR_MAX = 236f;
        private readonly RectangleShape _energyBar;
        private readonly Text _energyText;

        private float EnergyValue { get; set; } = 1f;

        public LevelSideBar(IGameSettings settings) 
        {
            _shape = new RectangleShape()
            {
                Size = new(settings.WindowWidth - settings.WindowHeight, settings.WindowHeight),
                Position = new(settings.WindowHeight, 0f),
                Texture = new Texture("Assets/GUI/sidebar.png")
            };

            _levelName = new Text("01-01 : Training", settings.Font, settings.FontSizeBig)
            {
                Position = new (_shape.Position.X + 3 * OFFSET_PX, 3 * OFFSET_PX)
            };
            
            _timer = new GameTimer();
            _timerText = new Text("00:000", settings.Font, settings.FontSizeSmall)
            {
                Position = new (_shape.Position.X + 3 * OFFSET_PX, 12 * OFFSET_PX)
            };

            _energyBar = new RectangleShape()
            {
                Size = new(ENERGY_BAR_MAX, 35),
                Position = new(settings.WindowHeight + 10, 218),
                FillColor = new Color(217, 160, 102)
            };
            _energyText = new Text("100%", settings.Font, settings.FontSizeBig)
            {
                Position = new (
                    _energyBar.Position.X + 3 * OFFSET_PX,
                    _energyBar.Position.Y + OFFSET_PX),
                FillColor = settings.Theme
            };
        }

        public void Draw(RenderTarget render)
        {
            render.Draw(_shape);
            render.Draw(_levelName);
            render.Draw(_timerText);
            render.Draw(_energyBar);
            render.Draw(_energyText);
        }

        public void Update()
        {
            _timer.Update();

            _timerText.DisplayedString = $"{_timer.RealTime.ToString("0.000")}s";

            _energyBar.Size = new(ENERGY_BAR_MAX * EnergyValue, _energyBar.Size.Y);
            _energyText.DisplayedString = $"{(EnergyValue * 100).ToString("00")}%";
        }

        public void SetEnergy(float energy)
        {
            EnergyValue = energy;
            if (EnergyValue < 0)
                EnergyValue = 0;
            if (EnergyValue > 1f)
                EnergyValue = 1f;
        }
    }

    internal class Level : 
        IDrawable, IUpdatable, IEventHandler<KeyboardEvent>
    {
        public string Name { get; }

        private readonly int _maxCells;

        private readonly RectangleShape _background;
        private readonly LevelSideBar _sidebar;

        private int N { get; }
        private int M { get; }

        private Cell[,] Cells { get; }

        private SnakeObject Player { get; set; }

        private readonly IGameState _state;

        public Level(string name, int n, int m, IGameState state)
        {
            _state = state;

            var settings = state.GetSettings();

            _sidebar = new LevelSideBar(settings);
            _maxCells = (int)settings.WindowHeight / (int)Cell.CELL_SIZE;

            _background = new RectangleShape()
            {
                Size = new (_maxCells * Cell.CELL_SIZE, _maxCells * Cell.CELL_SIZE),
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

        public void FillCell(int n, int m, IGameObject gameObject) 
        {
            this.Cells[n, m].Fill(gameObject);
            if (gameObject is SnakeObject snake)
                Player = snake;
        }

        public void Draw(RenderTarget render)
        {
            render.Draw(_background);
            _sidebar.Draw(render);

            foreach (var cell in Cells) 
            { 
                cell.Draw(render);
            }
        }

        public void Update()
        {
            _sidebar.Update();

            this.Player.Update();
            _sidebar.SetEnergy(this.Player.CurrentEnergy);

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
                        Player.Elongates();

                        if (CheckVictoryConditions())
                        {
                            _state.Handle(new ChangeContentEvent(ChangeContentEventType.LevelSummary));
                        }
                    }
                    else if (apple.IsYellow())
                    {
                        Player.ChargeEnergy();
                    }
                    else if (apple.IsGreen())
                    {

                    }

                }
                else if (obj is PortalObject portal)
                {
                    Player.EntersPortal(portal.DestinationX, portal.DestinationY);
                }
                else if (obj is WallObject || Player.EatsOwnTail())
                {
                    _state.Handle(new ChangeContentEvent(ChangeContentEventType.LevelSummary));
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

        public int GetAtedApples() => this.Player.Length; 

        public void Handle(KeyboardEvent @event)
        {
            this.Player.Handle(@event);
        }
    }
}
