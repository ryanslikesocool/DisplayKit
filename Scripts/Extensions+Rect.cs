// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using Unity.Mathematics;
using UnityEngine;

namespace DisplayKit {
    public static partial class Extensions {
        public static Rect Transform(this Rect rect, Camera camera, ValueSpace from, ValueSpace to, float distance) {
            if (from == to) {
                return rect;
            }
            if (camera == null) {
                camera = Camera.main;
            }

            float3 rectPosition = new float3(rect.position, distance);

            rect = from switch {
                ValueSpace.Screen => new Rect(camera.ScreenToWorldPoint(rectPosition), (float2)rect.size * camera.ScreenToWorldScale(distance)),
                ValueSpace.Viewport => new Rect(camera.ViewportToWorldPoint(rectPosition), (float2)rect.size * camera.ViewportToWorldScale(distance)),
                _ => rect
            };

            rect = to switch {
                ValueSpace.Screen => new Rect(camera.WorldToScreenPoint(rectPosition), (float2)rect.size * camera.WorldToScreenScale(distance)),
                ValueSpace.Viewport => new Rect(camera.WorldToViewportPoint(rectPosition), (float2)rect.size * camera.WorldToViewportScale(distance)),
                _ => rect
            };

            return rect;
        }

        public static float HorizontalAspect(this Rect rect) => rect.height / rect.width;
        public static float VerticalAspect(this Rect rect) => rect.width / rect.height;
    }
}