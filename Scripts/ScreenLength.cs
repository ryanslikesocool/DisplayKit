// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using UnityEngine;

namespace DisplayKit {
    [Serializable]
    public struct ScreenLength {
        public static readonly ScreenLength Default = new ScreenLength {
            value = 1,
            lengthMode = LengthMode.Scale,
            respectSafeArea = false,
            valueSpace = ValueSpace.World,
            axis = Axis.Horizontal
        };

        public float value;
        public LengthMode lengthMode;
        public bool respectSafeArea;
        public ValueSpace valueSpace;
        public Axis axis;

        public ScreenLength(float value, bool respectSafeArea = false, LengthMode lengthMode = LengthMode.Scale, ValueSpace valueSpace = ValueSpace.Screen, Axis axis = Axis.Min) {
            this.value = value;
            this.lengthMode = lengthMode;
            this.valueSpace = valueSpace;
            this.axis = axis;
            this.respectSafeArea = respectSafeArea;
        }

        public float ToWorldLength(Camera camera, float distance) => lengthMode == LengthMode.Position ? valueSpace.ToWorldPosition(camera, value, axis, respectSafeArea, distance) : valueSpace.ToWorldSize(camera, value, axis, respectSafeArea, distance);
    }
}