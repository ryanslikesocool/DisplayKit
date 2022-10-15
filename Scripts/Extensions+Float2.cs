// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using Unity.Mathematics;
using UnityEngine;

namespace DisplayKit {
    public static partial class Extensions {
        public static float HorizontalAspect(this float2 vector) => vector.y / vector.x;
        public static float VerticalAspect(this float2 vector) => vector.x / vector.y;
    }
}