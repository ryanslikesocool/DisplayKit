// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;
using Unity.Mathematics;

namespace TScreen {
    public static partial class Extensions {
        public static Rect WorldBounds(this Camera camera, float distance) => Screen.WorldBounds(camera, distance);

        public static float ScreenToWorldScale(this Camera camera, float distance) => camera.WorldBounds(distance).width / Screen.Width;
        public static float WorldToScreenScale(this Camera camera, float distance) => Screen.Width / camera.WorldBounds(distance).width;

        public static float2 WorldToViewportScale(this Camera camera, float distance) => Vector2.one / camera.WorldBounds(distance).size;
        public static float2 ViewportToWorldScale(this Camera camera, float distance) => camera.WorldBounds(distance).size;

        public static float ScreenToWorldLength(this Camera camera, float length, float distance) => length * camera.ScreenToWorldScale(distance);
        public static float WorldToScreenLength(this Camera camera, float length, float distance) => length * camera.WorldToScreenScale(distance);

        public static float ViewportToWorldLength(this Camera camera, Axis axis, float length, float distance) {
            float2 scale = camera.ViewportToWorldScale(distance);
            return axis.Define() == Axis.Horizontal ? length * scale.x : length * scale.y;
        }

        public static float WorldToViewportLength(this Camera camera, Axis axis, float length, float distance) {
            float2 scale = camera.WorldToViewportScale(distance);
            return axis.Define() == Axis.Horizontal ? length * scale.x : length * scale.y;
        }

        public static Axis Define(this Axis axis) => axis switch {
            Axis.Min => Screen.Width < Screen.Height ? Axis.Horizontal : Axis.Vertical,
            Axis.Max => Screen.Width > Screen.Height ? Axis.Horizontal : Axis.Vertical,
            _ => axis
        };
    }
}