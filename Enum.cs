// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;

namespace TScreen {
    public enum HorizontalAlignment {
        Leading,
        Center,
        Trailing
    }

    public enum VerticalAlignment {
        Bottom,
        Middle,
        Top
    }

    public enum Axis : byte {
        Horizontal,
        Vertical,
        Min,
        Max,
    }

    public enum ValueSpace : byte {
        Viewport,
        Screen,
        World
    }

    public enum UniformScaling : byte {
        None,
        [InspectorName("Width scales Height")] WidthScalesHeight,
        [InspectorName("Height scales Width")] HeightScalesWidth,
        [InspectorName("Min scales Max")] MinScalesMax,
        [InspectorName("Max scales Min")] MaxScalesMin
    }

    public enum LengthMode : byte {
        Position,
        Scale
    }
}