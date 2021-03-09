using SFML.Graphics;
using SFML.System;

namespace Daikon.Components
{
    public interface IStupidShape
    {
        RectangleShape[] GetShape(Vector2f origin);
        void Rotate();
    }
}