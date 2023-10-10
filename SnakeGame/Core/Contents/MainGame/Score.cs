using Engine.Graphics;
using Engine.Settings;
using Engine.Time;
using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame
{
    internal class Score : IDrawable
    {
        private readonly GameTimer _timer;

        private int CurrentApples { get; set; }
        private readonly int _maxApples;

        private Text Text { get; }
        private readonly string _levelName;

        public Score(int maxApples, string levelName, IGameSettings settings)
        {
            _maxApples = maxApples;
            _timer = new GameTimer();
            _levelName = levelName;

            Text = new Text()
            {
                CharacterSize = 32,
                Font = settings.Font,
                FillColor = Color.White,
                Position = new (32f, settings.WindowHeight - 64f - 16f),
                DisplayedString = ""
            };
        }

        public void AddApple() => CurrentApples++;

        public string GetFinalScore() => 
            $"You've scored {CurrentApples}/{_maxApples} points " +
            $"in {GetElapsedTime().ToString("0.000")}s " +
            $"({DateTime.Now.ToShortDateString()})";

        private string GetScoreDescription() => $"{_levelName} - Score: {CurrentApples}/{_maxApples}, Time: {GetElapsedTime().ToString("0.000")}s";

        private float GetElapsedTime()
        {
            return _timer.RealTime;
        }

        internal void Update(Level level)
        {
            _timer.Update();
            CurrentApples = level.GetAtedApples();
            Text.DisplayedString = GetScoreDescription();
        }

        public void DrawBy(RenderTarget render)
        {
            render.Draw(Text);
        }
    }
}
