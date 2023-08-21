using Engine.Core;
using Engine.Events;
using Engine.Graphics;
using Engine.Time;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects
{
    internal class SnakeObject : IGameObject
    {
        private readonly SnakeHeadObject _head;
        private readonly List<SnakeBodyObject> _body;

        private readonly GameStopwatch _stopwatch;

        private static readonly float REGULAR_SPEED = 1f;
        private static readonly float ACCELERATION = 0.05f;
        private static readonly float MAX_SPEED = 0.25f;

        private float BaseSpeed { get; set; } = REGULAR_SPEED;
        private float CurrentSpeed { get; set; } = REGULAR_SPEED;
        private bool Accelerate { get; set; } = false;

        public SnakeObject(int x, int y)
        {
            this._head = new SnakeHeadObject(x, y);

            this._body = new List<SnakeBodyObject>();
            var part = new SnakeBodyObject(x, y + 1);
            this._body.Add(part);

            _stopwatch = new GameStopwatch(BaseSpeed);
        }

        public int X => this._head.X;
        public int Y => this._head.Y;

        public int Length => this._body.Count - 1;

        public bool IsInPortal { get; private set; }

        public void Draw(RenderTarget render)
        {
            this._head.Draw(render);
            foreach (var body in this._body) { body.Draw(render); }
        }

        public void Handle(KeyboardEvent @event)
        {
            if (@event.Type == KeyboardEventType.Press)
            {
                switch (@event.Key)
                {
                    case Keyboard.Key.Left:
                        _head.TurnLeft();
                        break;
                    case Keyboard.Key.Right:
                        _head.TurnRight();
                        break;
                    case Keyboard.Key.Up:
                        _head.TurnUp();
                        break;
                    case Keyboard.Key.Down:
                        _head.TurnDown();
                        break;
                    case Keyboard.Key.Space:
                        SpeedUp();
                        break;
                }
            }
            else if (@event.Type == KeyboardEventType.Release)
            {
                switch (@event.Key)
                {
                    case Keyboard.Key.Space:
                        SlowDown();
                        break;
                }
            }
        }

        private void SpeedUp() => Accelerate = true;

        private void SlowDown() => Accelerate = false;

        public void Update()
        {
            UpdateSpeed();
            if (!_stopwatch.Update())
            {
                UpdatePositions();

                _stopwatch.Restart();
            }
        }

        private void UpdatePositions()
        {
            for (int i = _body.Count - 1; i > 0; i--)
            {
                _body[i].Move(_body[i - 1].X, _body[i - 1].Y);
                _body[i].Update();
            }

            _body[0].Move(_head.X, _head.Y);
            _body[0].Update();

            this._head.Update();
        }

        private void UpdateSpeed()
        {
            if (Accelerate)
            {
                if (CurrentSpeed > MAX_SPEED)
                    CurrentSpeed -= ACCELERATION;

                _stopwatch.ChangeTimer(CurrentSpeed);
            }
            else
            {
                if (CurrentSpeed != BaseSpeed)
                {
                    CurrentSpeed = BaseSpeed;
                    _stopwatch.ChangeTimer(CurrentSpeed);
                }
            }
        }

        internal void Elongates() => this._body.Add(new SnakeBodyObject(-1, -1));

        internal bool EatsOwnTail() => _body.ToList().Any(b => X == b.X && Y == b.Y);

        internal void EntersPortal(int destinationX, int destinationY)
        {
            if (!IsInPortal)
            {
                IsInPortal = true;
                _head.MoveTo(destinationX, destinationY);
            }
            else
                IsInPortal = false;
        }
    }
}
