using Engine.Time;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame
{
    internal class Score
    {
        private readonly GameTimer _timer;

        private int CurrentApples { get; set; }
        private readonly int _maxApples;

        public Score(int maxApples)
        {
            _maxApples = maxApples;
            _timer = new GameTimer();
        }

        public void AddApple() => CurrentApples++;

        public string GetScore() => 
            $"You've scored {CurrentApples}/{_maxApples} points " +
            $"in {GetElapsedTime().ToString("0.000")}s " +
            $"({DateTime.Now.ToShortDateString()})";

        private float GetElapsedTime()
        {
            _timer.Stop();
            return _timer.RealTime;
        }

        internal void Update(Level level)
        {
            _timer.Update();
            CurrentApples = level.GetAtedApples();
        }
    }
}
