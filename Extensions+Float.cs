// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;
using Unity.Mathematics;

namespace TScreen {
    public static partial class Extensions {
        public static float ToWorldSize(this ValueSpace space, Camera camera, float value, Axis axis, bool respectSafeArea, float distance) {
            if (camera == null) {
                camera = Camera.main;
            }

            value = space switch {
                ValueSpace.Screen => camera.ScreenToWorldLength((int)value, distance),
                ValueSpace.Viewport => camera.ViewportToWorldLength(axis, value, distance),
                _ => value
            };

            if (respectSafeArea) {
                float2 worldSize = camera.WorldBounds(distance).size;
                float2 safeAreaSize = Screen.SafeAreaWorld(camera, distance).size;

                value = axis.Define() switch {
                    Axis.Horizontal => (value / worldSize.x) * safeAreaSize.x,
                    Axis.Vertical => (value / worldSize.y) * safeAreaSize.y,
                    _ => value
                };
            }

            return value;
        }

        public static float ToWorldPosition(this ValueSpace space, Camera camera, float value, Axis axis, bool respectSafeArea, float distance) {
            if (camera == null) {
                camera = Camera.main;
            }

            value = space switch {
                ValueSpace.Screen => camera.ScreenToWorldLength((int)value, distance),
                ValueSpace.Viewport => camera.ViewportToWorldLength(axis, value, distance),
                _ => value
            };

            if (respectSafeArea) {
                float2 worldSize = camera.WorldBounds(distance).size;
                Rect safeArea = Screen.SafeAreaWorld(camera, distance);
                safeArea.position += safeArea.size * 0.5f;

                value = axis.Define() switch {
                    Axis.Horizontal => (value / worldSize.x) * safeArea.width + safeArea.xMin,
                    Axis.Vertical => (value / worldSize.y) * safeArea.height + safeArea.yMin,
                    _ => value
                };
            }

            return value;
        }
    }
}