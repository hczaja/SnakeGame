using Engine.Graphics;
using Engine.Random;
using SFML.Graphics;
using SnakeGame.Core.Contents.MainGame.GameObjects.Other;
using SnakeGame.Core.Contents.MainGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.GameObjects.Enemies
{
    internal class BlackSpiderEnemyObject : EnemyObject
    {
        private static readonly float ActionTime = 2.5f;

        private Dictionary<string, Animation> Animations;

        private static string IDDLE_ANIMATION = "Iddle";
        private static string MOVE_ANIMATION = "Move";

        protected override Animation CurrentAnimation { get; set; }

        public BlackSpiderEnemyObject(int x, int y)
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
                {
                    MOVE_ANIMATION, new Animation(
                        new Texture("Assets/Spritesheets/Spritesheet_Spider_Iddle.png"),
                        (int)Cell.CELL_SIZE, () => GetPosition(),
                        2, 0.5f, isLooped: true)
                }
            };

            CurrentAnimation = Animations[IDDLE_ANIMATION];
            CurrentAnimation.Start();
        }

        protected override void DoSomething()
        {
            int number = Dice.Instance.Roll6k();
            if (number >= 3)
                Move();
            else
                Iddle();
        }

        private void Move()
        {
            CurrentAnimation = Animations[MOVE_ANIMATION];
            CurrentAnimation.Start();

            this.X += 1;
        }

        private void Iddle()
        {
            CurrentAnimation = Animations[IDDLE_ANIMATION];
            CurrentAnimation.Start();
        }
    }

}
