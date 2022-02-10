// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using System;

namespace TScreen {
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

        public float ToWorldLength() => lengthMode == LengthMode.Position ? valueSpace.ToWorldPosition(value, axis, respectSafeArea) : valueSpace.ToWorldSize(value, axis, respectSafeArea);
    }
}