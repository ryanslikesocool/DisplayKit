// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;

namespace TScreen
{
    public static partial class Extensions
    {
        public static Vector2 WorldSize(this Camera camera) => Screen.WorldSize(camera);

        public static float ScreenToWorldScale(this Camera camera) => camera.WorldSize().x / Screen.Width;
        public static float WorldToScreenScale(this Camera camera) => Screen.Width / camera.WorldSize().x;

        public static Vector2 WorldToViewportScale(this Camera camera) => Vector2.one / camera.WorldSize();
        public static Vector2 ViewportToWorldScale(this Camera camera) => camera.WorldSize();

        public static float ScreenToWorldLength(this Camera camera, float length) => length * camera.ScreenToWorldScale();
        public static float WorldToScreenLength(this Camera camera, float length) => length * camera.WorldToScreenScale();

        public static float ViewportToWorldLength(this Camera camera, Axis axis, float length)
        {
            Vector2 scale = camera.ViewportToWorldScale();
            return axis.Define() == Axis.Horizontal ? length * scale.x : length * scale.y;
        }

        public static float WorldToViewportLength(this Camera camera, Axis axis, float length)
        {
            Vector2 scale = camera.WorldToViewportScale();
            return axis.Define() == Axis.Horizontal ? length * scale.x : length * scale.y;
        }

        public static Axis Define(this Axis axis) => axis switch
        {
            Axis.Min => Screen.Width < Screen.Height ? Axis.Horizontal : Axis.Vertical,
            Axis.Max => Screen.Width > Screen.Height ? Axis.Horizontal : Axis.Vertical,
            _ => axis
        };
    }
}