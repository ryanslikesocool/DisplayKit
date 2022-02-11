// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using System;
using UnityEngine;
using Unity.Mathematics;

namespace TScreen {
    [Serializable]
    public struct ScreenSize {
        public static readonly ScreenSize Default = new ScreenSize {
            size = new float2(1),
            respectSafeArea = true,
            horizontalValueSpace = ValueSpace.World,
            verticalValueSpace = ValueSpace.World,
            uniformScaling = UniformScaling.None
        };

        public float2 size;
        public bool respectSafeArea;
        public ValueSpace horizontalValueSpace;
        public ValueSpace verticalValueSpace;
        public UniformScaling uniformScaling;

        public float2 ToWorldSize(Camera camera, float distance) {
            float2 result = new float2(
                horizontalValueSpace.ToWorldSize(camera, size.x, Axis.Horizontal, respectSafeArea, distance),
                verticalValueSpace.ToWorldSize(camera, size.y, Axis.Vertical, respectSafeArea, distance)
            );

            float min = Screen.MinAxis;
            float max = Screen.MaxAxis;

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