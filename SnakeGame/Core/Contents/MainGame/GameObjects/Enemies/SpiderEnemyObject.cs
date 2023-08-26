using Engine.Graphics;
using SFML.Graphics;
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

}
