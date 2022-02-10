// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;

namespace TScreen {
    public static partial class Extensions {
        public static Rect Transform(this Rect rect, ValueSpace from, ValueSpace to) => rect.Transform(Camera.main, from, to);

        public static Rect Transform(this Rect rect, Camera camera, ValueSpace from, ValueSpace to) {
            if (from == to) {
                return rect;
            }

            rect = from switch {
                ValueSpace.Screen => new Rect(camera.ScreenToWorldPoint(rect.position), rect.size * camera.ScreenToWorldScale()),
                ValueSpace.Viewport => new Rect(camera.ViewportToWorldPoint(rect.position), rect.size * camera.ViewportToWorldScale()),
                _ => rect
            };

            rect = to switch {
                ValueSpace.Screen => new Rect(camera.WorldToScreenPoint(rect.position), rect.size * camera.WorldToScreenScale()),
                ValueSpace.Viewport => new Rect(camera.WorldToViewportPoint(rect.position), rect.size * camera.WorldToViewportScale()),
                _ => rect
            };

            return rect;
        }

        public static float HorizontalAspect(this Rect rect) => rect.height / rect.width;
        public static float VerticalAspect(this Rect rect) => rect.width / rect.height;
    }
}