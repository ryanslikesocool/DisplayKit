// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;

namespace TScreen {
    public static partial class Extensions {
        public static float HorizontalAspect(this Vector2 vector) => vector.y / vector.x;
        public static float VerticalAspect(this Vector2 vector) => vector.x / vector.y;
    }
}