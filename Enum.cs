// Developed with love by Ryan Boyer http://ryanjboyer.com <3

namespace TScreen
{
    public enum HorizontalAlignment
    {
        Leading,
        Center,
        Trailing
    }

    public enum VerticalAlignment
    {
        Bottom,
        Middle,
        Top
    }

    public enum Axis : byte
    {
        Horizontal,
        Vertical,
        Min,
        Max,
    }

    public enum ValueSpace : byte
    {
        Viewport,
        Screen,
        World
    }

    public enum UniformScaling : byte
    {
        None,
        WidthScalesHeight,
        HeightScalesWidth,
        MinScalesMax,
        MaxScalesMin
    }

    public enum LengthMode : byte
    {
        Position,
        Scale
    }
}