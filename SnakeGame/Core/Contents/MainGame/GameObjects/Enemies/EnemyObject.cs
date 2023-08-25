using Engine.Graphics;
using Engine.Time;
using SFML.Graphics;
using SFML.System;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Enemies
{
    internal class SpiderEnemyObject : EnemyObject
    {
        private static readonly float ActionTime = 1f;

        private Dictionary<string, Animation> Animations;
        private static string IDDLE_ANIMATION = "Iddle";

        protected override Animation CurrentAnimation { get; }

        public SpiderEnemyObject(int x, int y) 
            : base(x, y, ActionTime)
        {
            Animations = new()
            {
                {
                    IDDLE_ANIMATION, new Animation(
                        new Texture("Assets/Spritesheets/Spritesheet_Spider_Iddle.png"),
                        (int)Cell.CELL_SIZE, () => GetPosition(),
                        2, 0.5f, isLooped: true)
                },
            };

            CurrentAnimation = Animations[IDDLE_ANIMATION];
            CurrentAnimation.Start();
        }

        protected override void DoSomething()
        {

        }
    }

    internal abstract class EnemyObject : IGameObject
    {
        private readonly GameStopwatch _stopwatch;

        public int X { get; private set; }
        public int Y { get; private set; }

        protected abstract Animation CurrentAnimation { get; }

        public EnemyObject(int x, int y, float actionTime)
        {
            X = x;
            Y = y;

            _stopwatch = new GameStopwatch(actionTime);
        }

        public void Draw(RenderTarget render)
        {
            CurrentAnimation.Draw(render);
        }

        public void Update()
        {
            if (!_stopwatch.Update())
            {
                DoSomething();
                _stopwatch.Restart();
            }
        }

        protected abstract void DoSomething();

        protected virtual Vector2f GetPosition() => new(Cell.CELL_SIZE * X, Cell.CELL_SIZE * Y);
    }
}
