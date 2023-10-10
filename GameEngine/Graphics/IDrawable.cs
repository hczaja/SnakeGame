using SFML.Graphics;

namespace Engine.Graphics;

public interface IDrawable
{
    void DrawBy(RenderTarget target);
}
