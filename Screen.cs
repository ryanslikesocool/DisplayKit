// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;
using UnityScreen = UnityEngine.Screen;

namespace TScreen
{
    public static class Screen
    {
        private static readonly Vector3 ViewportMin = new Vector3(0, 0, 1);
        private static readonly Vector3 ViewportMax = new Vector3(1, 1, 1);

        public static int Width => UnityScreen.width;
        public static int Height => UnityScreen.height;
        public static Vector2 Size => new Vector2(Width, Height);
        public static Vector2 Extents => Size / 2f;

        public static int MinSize => Mathf.Min(Width, Height);
        public static int MaxSize => Mathf.Max(Width, Height);

        public static float VerticalAspect => Width / (float)Height;
        public static float HorizontalAspect => Height / (float)Width;

        public static Rect SafeAreaScreen => UnityScreen.safeArea;
        public static Rect SafeAreaViewport => SafeAreaScreen.Transform(ValueSpace.Screen, ValueSpace.Viewport);
        public static Rect SafeAreaWorld => SafeAreaScreen.Transform(ValueSpace.Screen, ValueSpace.World);

        public static ScreenOrientation Orientation => UnityScreen.orientation;

        public static int ViewportToScreen(Axis axis, float viewportLength) => axis switch
        {
            Axis.Min => (int)(viewportLength * MinSize),
            Axis.Max => (int)(viewportLength * MaxSize),
            Axis.Horizontal => (int)(viewportLength * Width),
            Axis.Vertical => (int)(viewportLength * Height),
            _ => 0
        };

        public static Vector2 ViewportToScreen(Vector2 viewportPosition) => viewportPosition * Size;

        public static Rect WorldBounds(Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            Vector3 min = camera.ViewportToWorldPoint(ViewportMin);
            Vector3 max = camera.ViewportToWorldPoint(ViewportMax);
            return new Rect((Vector2)min, (Vector2)(max - min));
        }
    }
}