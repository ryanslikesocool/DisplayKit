// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using System;

namespace TScreen
{
    [Flags]
    public enum Edge : byte
    {
        Top = 1 << 0,
        Bottom = 1 << 1,
        Leading = 1 << 2,
        Trailing = 1 << 3,
        Vertical = Top | Bottom,
        Horizontal = Leading | Trailing,
        All = Top | Bottom | Leading | Trailing
    }

    public enum HorizontalAlignment
    {
        Leading,
        Center,
        Trailing,
        Scale,
        Fixed
    }

    public enum VerticalAlignment
    {
        Bottom,
        Middle,
        Top,
        Scale,
        Fixed
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