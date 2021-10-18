using UnityEngine;

namespace TScreen
{
    public static partial class Extensions
    {
        public static float ToWorldSize(this ValueSpace space, float value, Axis axis, bool respectSafeArea)
        {
            switch (space)
            {
                case ValueSpace.Screen:
                    value = Camera.main.ScreenToWorldLength((int)value);
                    break;
                case ValueSpace.Viewport:
                    value = Camera.main.ViewportToWorldLength(axis, value);
                    break;
            }

            if (respectSafeArea)
            {
                Vector2 worldSize = Camera.main.WorldSize();
                Vector2 safeAreaSize = Screen.SafeAreaWorld.size;

                switch (axis.Define())
                {
                    case Axis.Horizontal:
                        value = (value / worldSize.x) * safeAreaSize.x;
                        break;
                    case Axis.Vertical:
                        value = (value / worldSize.y) * safeAreaSize.y;
                        break;
                }
            }

            return value;
        }

        public static float ToWorldPosition(this ValueSpace space, float value, Axis axis, bool respectSafeArea)
        {
            switch (space)
            {
                case ValueSpace.Screen:
                    value = Camera.main.ScreenToWorldLength((int)value);
                    break;
                case ValueSpace.Viewport:
                    value = Camera.main.ViewportToWorldLength(axis, value);
                    break;
            }

            if (respectSafeArea)
            {
                Vector2 worldSize = Camera.main.WorldSize();
                Rect safeArea = Screen.SafeAreaWorld;
                safeArea.position += safeArea.size * 0.5f;

                switch (axis.Define())
                {
                    case Axis.Horizontal:
                        value = (value / worldSize.x) * safeArea.width + safeArea.xMin;
                        break;
                    case Axis.Vertical:
                        value = (value / worldSize.y) * safeArea.height + safeArea.yMin;
                        break;
                }
            }

            return value;
        }
    }
}