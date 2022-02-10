// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using System;
using UnityEngine;

namespace TScreen {
    [Serializable]
    public struct ScreenSize {
        public static readonly ScreenSize Default = new ScreenSize {
            size = Vector2.one,
            respectSafeArea = true,
            horizontalValueSpace = ValueSpace.World,
            verticalValueSpace = ValueSpace.World,
            uniformScaling = UniformScaling.None
        };

        public Vector2 size;
        public bool respectSafeArea;
        public ValueSpace horizontalValueSpace;
        public ValueSpace verticalValueSpace;
        public UniformScaling uniformScaling;

        public Vector2 ToWorldSize() {
            Vector2 result = new Vector2(
                horizontalValueSpace.ToWorldSize(size.x, Axis.Horizontal, respectSafeArea),
                verticalValueSpace.ToWorldSize(size.y, Axis.Vertical, respectSafeArea)
            );

            float min = Mathf.Min(Screen.Width, Screen.Height);
            float max = Mathf.Max(Screen.Width, Screen.Height);

            switch (uniformScaling) {
                case UniformScaling.WidthScalesHeight:
                    result.y = (result.x / size.x) * size.y;
                    break;
                case UniformScaling.HeightScalesWidth:
                    result.x = (result.y / size.y) * size.x;
                    break;
                case UniformScaling.MinScalesMax:
                    if (min == Screen.Width) {
                        result.y = (result.x / size.x) * size.y;
                    } else {
                        result.x = (result.y / size.y) * size.x;
                    }
                    break;
                case UniformScaling.MaxScalesMin:
                    if (max == Screen.Width) {
                        result.y = (result.x / size.x) * size.y;
                    } else {
                        result.x = (result.y / size.y) * size.x;
                    }
                    break;
            }
            return result;
        }

    }
}