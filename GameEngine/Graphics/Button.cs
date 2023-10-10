using Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Graphics
{
    public class Button : IDrawable
    {
        private RectangleShape Rectangle { get; init; }
        public bool IsCovered { get; private set; }

        private readonly Texture _coveredTexture;
        private readonly Texture _uncoveredTexture;
        public Button(
            Vector2f size, 
            Vector2f position, 
            Texture textureUncovered, 
            Texture textureCovered)
        {
            _coveredTexture = textureCovered; 
            _uncoveredTexture = textureUncovered;

            this.Rectangle = new RectangleShape() 
            { 
                Size = size, 
                Position = position, 
                Texture = textureUncovered
            };
        }

        public void DrawBy(RenderTarget render)
        {
            render.Draw(this.Rectangle);
        }

        public void Cover()
        {
            IsCovered = true;
            Rectangle.Texture = _coveredTexture;
        }
        public void Uncover()
        {
            IsCovered = false;
            Rectangle.Texture = _uncoveredTexture;
        }

        public Vector2f GetPosition()
        {
            var bounds = this.Rectangle.GetGlobalBounds();
            return new Vector2f(bounds.Left, bounds.Top);
        }

        public Vector2f GetSize() 
        {
            var bounds = this.Rectangle.GetLocalBounds();
            return new Vector2f(bounds.Width, bounds.Height);
        }
    }
}
