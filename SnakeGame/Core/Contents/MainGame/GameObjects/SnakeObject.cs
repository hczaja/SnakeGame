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
        private readonly SnakeHead _head;
        private readonly List<SnakeBody> _body;

        private readonly GameStopwatch _stopwatch;

        private static readonly float REGULAR_SPEED = 1f;
        private static readonly float ACCELERATION = 0.05f;
        private static readonly float MAX_SPEED = 0.25f;
        private float Speed { get; set; } = REGULAR_SPEED;

        public SnakeObject(int x, int y)
        {
            this._head = new SnakeHead(x, y);

            this._body = new List<SnakeBody>();
            var part = new SnakeBody(x, y + 1);
            this._body.Add(part);

            _stopwatch = new GameStopwatch(REGULAR_SPEED);
        }

        public int X => this._head.X;
        public int Y => this._head.Y;

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
                        if (Speed > MAX_SPEED)
                            Speed -= ACCELERATION;

                        _stopwatch.ChangeTimer(Speed);
                        break;
                }
            }
            else if (@event.Type == KeyboardEventType.Release)
            {
                switch (@event.Key)
                {
                    case Keyboard.Key.Space:
                        _stopwatch.ChangeTimer(REGULAR_SPEED);
                        break;
                }
            }
        }

        public void Update()
        {
            if (!_stopwatch.Update())
            {
                _stopwatch.Restart();

                for (int i = _body.Count - 1; i > 0; i--) 
                {
                    _body[i].Move(_body[i - 1].X, _body[i - 1].Y);
                    _body[i].Update();
                }
                _body[0].Move(_head.X, _head.Y);
                _body[0].Update();

                this._head.Update();
            }
        }

        internal void Elongate()
        {
            SnakeBody body = new SnakeBody(-1, -1);
            this._body.Add(body);
        }
    }
}
