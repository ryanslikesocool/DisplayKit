using System;
using UnityEngine;

namespace TScreen
{
    [Serializable]
    public struct ScreenSize
    {
        public static readonly ScreenSize Default = new ScreenSize
        {
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

        public Vector2 ToWorldSize()
        {
            Vector2 result = new Vector2(
                horizontalValueSpace.ToWorldSize(size.x, Axis.Horizontal, respectSafeArea),
                verticalValueSpace.ToWorldSize(size.y, Axis.Vertical, respectSafeArea)
            );

            switch (uniformScaling)
            {
                case UniformScaling.WidthMatchHeight:
                    result.x = result.y;
                    break;
                case UniformScaling.HeightMatchWidth:
                    result.y = result.x;
                    break;
                case UniformScaling.WidthScalesHeight:
                    result.y = (result.x / size.x) * size.y;
                    break;
                case UniformScaling.HeightScalesWidth:
                    result.x = (result.y / size.y) * size.x;
                    break;
            }
            return result;
        }

    }
}