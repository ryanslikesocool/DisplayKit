// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;
using Unity.Mathematics;
using UnityScreen = UnityEngine.Screen;

namespace TScreen {
    public static class Screen {
        private static readonly float2 ViewportMin = new float2(0, 0);
        private static readonly float2 ViewportMax = new float2(1, 1);

        public static int Width => UnityScreen.width;
        public static int Height => UnityScreen.height;
        public static float2 Size => new float2(Width, Height);
        public static float2 Extents => Size * 0.5f;

        public static int MinAxis => math.min(Width, Height);
        public static int MaxAxis => math.max(Width, Height);

        public static float VerticalAspect => Width / (float)Height;
        public static float HorizontalAspect => Height / (float)Width;

        public static Rect Rect => new Rect(0, 0, Width, Height);

        public static Rect SafeAreaScreen => UnityScreen.safeArea;
        public static Rect SafeAreaViewport(Camera camera, float distance) => SafeAreaScreen.Transform(camera, ValueSpace.Screen, ValueSpace.Viewport, distance);
        public static Rect SafeAreaWorld(Camera camera, float distance) => SafeAreaScreen.Transform(camera, ValueSpace.Screen, ValueSpace.World, distance);

        public static ScreenOrientation Orientation => UnityScreen.orientation;

        public static int ViewportToScreen(Axis axis, float viewportLength) => axis switch {
            Axis.Min => (int)(viewportLength * MinAxis),
            Axis.Max => (int)(viewportLength * MaxAxis),
            Axis.Horizontal => (int)(viewportLength * Width),
            Axis.Vertical => (int)(viewportLength * Height),
            _ => 0
        };

        public static float2 ViewportToScreen(float2 viewportPosition) => viewportPosition * Size;

        public static Rect WorldBounds(Camera camera, float distance) {
            if (camera == null) {
                camera = Camera.main;
            }

            float3 viewportMin = new float3(ViewportMin.x, ViewportMin.y, distance);
            float3 viewportMax = new float3(ViewportMax.x, ViewportMax.y, distance);

            float3 min = camera.ViewportToWorldPoint(viewportMin);
            float3 max = camera.ViewportToWorldPoint(viewportMax);

            return new Rect(min.xy, (max - min).xy);
        }

        public static float2 ViewportPoint(HorizontalAlignment horizontal, VerticalAlignment vertical, bool respectSafeArea) {
            float2 screenPoint = ScreenPoint(horizontal, vertical, respectSafeArea);
            return screenPoint / Size;
        }

        public static float2 ScreenPoint(HorizontalAlignment horizontal, VerticalAlignment vertical, bool respectSafeArea) {
            Rect boundingRect = respectSafeArea ? SafeAreaScreen : Rect;

            float2 point = float2.zero;
            point.x = horizontal switch {
                HorizontalAlignment.Leading => boundingRect.xMin,
                HorizontalAlignment.Center => boundingRect.center.x,
                HorizontalAlignment.Trailing => boundingRect.xMax,
                _ => boundingRect.center.x
            };
            point.y = vertical switch {
                VerticalAlignment.Top => boundingRect.yMax,
                VerticalAlignment.Middle => boundingRect.center.y,
                VerticalAlignment.Bottom => boundingRect.yMin,
                _ => boundingRect.center.y
            };
            return point;
        }

        public static float3 TransformPoint(Camera camera, float3 value, ValueSpace from, ValueSpace to) {
            if (from == to) {
                return value;
            }

            float3 screenPoint = from switch {
                ValueSpace.World => camera.WorldToScreenPoint(value),
                ValueSpace.Viewport => camera.ViewportToScreenPoint(value),
                _ => value
            };

            return to switch {
                ValueSpace.World => camera.ScreenToWorldPoint(screenPoint),
                ValueSpace.Viewport => camera.ScreenToViewportPoint(screenPoint),
                _ => screenPoint
            };
        }
    }
}