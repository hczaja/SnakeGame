using Engine.Core;
using Engine.Time;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Graphics
{
    public class Animation : IDrawable
    {
        private readonly GameStopwatch _stopwatch;

        private readonly Texture _texture;
        private readonly int _maxFrames;
        private Sprite Sprite { get; set; }

        private int FrameIndex { get; set; } = 0;
        private bool IsStarted { get; set; } = false;
        private bool IsLooped { get; set; } = false;

        private bool IsFinished => FrameIndex >= _maxFrames;

        private Func<IntRect> GetFrame;
        private Func<Vector2f> GetPosition;

        public Animation(
            Texture texture,
            int frameSize,
            Func<Vector2f> getPosition,
            int maxFrames, 
            float animationTime, 
            bool isLooped = false)
        {
            _texture = texture;

            GetFrame = () => new IntRect(FrameIndex * frameSize, 0, frameSize, frameSize);
            GetPosition = getPosition;

            Sprite = new Sprite(_texture);
            Sprite.Position = GetPosition();
            Sprite.TextureRect = GetFrame();

            _maxFrames = maxFrames;
            _stopwatch = new GameStopwatch(animationTime / _maxFrames);

            IsLooped = isLooped;
        }

        public void Start()
        {
            _stopwatch.Restart();

            FrameIndex = 0;
            IsStarted = true;
        }

        public void Stop()
        {
            IsStarted = false;
        }

        public void Draw(RenderTarget render)
        {
            if (!IsStarted)
                return;

            render.Draw(Sprite);

            if (!_stopwatch.Update())
            {
                if (IsFinished && !IsLooped)
                {
                    Stop();
                    FrameIndex = 0;
                }
                else if (IsFinished)
                    FrameIndex = 0;
                else
                    FrameIndex++;

                Sprite.Position = GetPosition();
                Sprite.TextureRect = GetFrame();

                _stopwatch.Restart();
            }
        }
    }
}
