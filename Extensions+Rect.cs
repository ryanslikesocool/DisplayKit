// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;

namespace TScreen
{
    public static partial class Extensions
    {
        public static Rect Transform(this Rect rect, ValueSpace from, ValueSpace to) => rect.Transform(Camera.main, from, to);

        public static Rect Transform(this Rect rect, Camera camera, ValueSpace from, ValueSpace to)
        {
            if (from == to)
            {
                return rect;
            }

            switch (from)
            {
                case ValueSpace.Screen:
                    rect = new Rect(camera.ScreenToWorldPoint(rect.position), rect.size * camera.ScreenToWorldScale());
                    break;
                case ValueSpace.Viewport:
                    rect = new Rect(camera.ViewportToWorldPoint(rect.position), rect.size * camera.ViewportToWorldScale());
                    break;
            }

            switch (to)
            {
                case ValueSpace.Screen:
                    rect = new Rect(camera.WorldToScreenPoint(rect.position), rect.size * camera.WorldToScreenScale());
                    break;
                case ValueSpace.Viewport:
                    rect = new Rect(camera.WorldToViewportPoint(rect.position), rect.size * camera.WorldToViewportScale());
                    break;
            }
            return rect;
        }

        public static float HorizontalAspect(this Rect rect) => rect.height / rect.width;
        public static float VerticalAspect(this Rect rect) => rect.width / rect.height;
    }
}