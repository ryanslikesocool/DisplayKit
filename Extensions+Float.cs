// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;

namespace TScreen {
    public static partial class Extensions {
        public static float ToWorldSize(this ValueSpace space, float value, Axis axis, bool respectSafeArea) {
            value = space switch {
                ValueSpace.Screen => Camera.main.ScreenToWorldLength((int)value),
                ValueSpace.Viewport => Camera.main.ViewportToWorldLength(axis, value),
                _ => value
            };

            if (respectSafeArea) {
                Vector2 worldSize = Camera.main.WorldBounds().size;
                Vector2 safeAreaSize = Screen.SafeAreaWorld.size;

                value = axis.Define() switch {
                    Axis.Horizontal => (value / worldSize.x) * safeAreaSize.x,
                    Axis.Vertical => (value / worldSize.y) * safeAreaSize.y,
                    _ => value
                };
            }

            return value;
        }

        public static float ToWorldPosition(this ValueSpace space, float value, Axis axis, bool respectSafeArea) {
            switch (space) {
                case ValueSpace.Screen:
                    value = Camera.main.ScreenToWorldLength((int)value);
                    break;
                case ValueSpace.Viewport:
                    value = Camera.main.ViewportToWorldLength(axis, value);
                    break;
            }

            if (respectSafeArea) {
                Vector2 worldSize = Camera.main.WorldBounds().size;
                Rect safeArea = Screen.SafeAreaWorld;
                safeArea.position += safeArea.size * 0.5f;

                switch (axis.Define()) {
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