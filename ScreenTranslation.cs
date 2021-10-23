// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using System;
using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace TScreen
{
    [Serializable]
    public struct ScreenTranslation
    {
        public static readonly ScreenTranslation Default = new ScreenTranslation();

        public Vector2 position;
        public bool respectSafeArea;
        public Transform relativeTo;
#if ODIN_INSPECTOR_3
        [HorizontalGroup("H"), LabelText("H Space"), LabelWidth(96)] public ValueSpace horizontalValueSpace;
        [HorizontalGroup("V"), LabelText("V Space"), LabelWidth(96)] public ValueSpace verticalValueSpace;
        [HorizontalGroup("H"), LabelText("Alignment"), LabelWidth(96)] public HorizontalAlignment horizontalAlignment;
        [HorizontalGroup("V"), LabelText("Alignment"), LabelWidth(96)] public VerticalAlignment verticalAlignment;
#else
        public ValueSpace horizontalValueSpace;
        public ValueSpace verticalValueSpace;
        public HorizontalAlignment horizontalAlignment;
        public VerticalAlignment verticalAlignment;
#endif
        public UniformScaling uniformScaling;

        public ScreenTranslation(
            Vector2 position,
            bool respectSafeArea = false,
            Transform relativeTo = null,
            ValueSpace horizontalValueSpace = ValueSpace.Screen,
            ValueSpace verticalValueSpace = ValueSpace.Screen,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment verticalAlignment = VerticalAlignment.Middle,
            UniformScaling uniformScaling = UniformScaling.None
        )
        {
            this.position = position;
            this.respectSafeArea = respectSafeArea;
            this.relativeTo = relativeTo;
            this.horizontalValueSpace = horizontalValueSpace;
            this.verticalValueSpace = verticalValueSpace;
            this.horizontalAlignment = horizontalAlignment;
            this.verticalAlignment = verticalAlignment;
            this.uniformScaling = uniformScaling;
        }

        public Vector2 ToWorldPosition()
        {
            Vector2 result = Vector2.zero;
            result.x = horizontalValueSpace.ToWorldPosition(position.x, Axis.Horizontal, respectSafeArea);
            result.y = verticalValueSpace.ToWorldPosition(position.y, Axis.Vertical, respectSafeArea);

            Vector2 size = respectSafeArea ? Screen.SafeAreaWorld.size : Camera.main.WorldBounds().size;
            Vector2 extents = size * 0.5f;

            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Leading:
                    result.x -= extents.x;
                    break;
                case HorizontalAlignment.Trailing:
                    result.x += extents.x;
                    break;
            }

            switch (verticalAlignment)
            {
                case VerticalAlignment.Top:
                    result.y += extents.y;
                    break;
                case VerticalAlignment.Bottom:
                    result.y -= extents.y;
                    break;
            }

            float min = Mathf.Min(Screen.Width, Screen.Height);
            float max = Mathf.Max(Screen.Width, Screen.Height);

            switch (uniformScaling)
            {
                case UniformScaling.WidthScalesHeight:
                    result.y = (result.x / position.x) * position.y;
                    break;
                case UniformScaling.HeightScalesWidth:
                    result.x = (result.y / position.y) * position.x;
                    break;
                case UniformScaling.MinScalesMax:
                    if (min == Screen.Width)
                    {
                        result.y = (result.x / position.x) * position.y;
                    }
                    else
                    {
                        result.x = (result.y / position.y) * position.x;
                    }
                    break;
                case UniformScaling.MaxScalesMin:
                    if (max == Screen.Width)
                    {
                        result.y = (result.x / size.x) * size.y;
                    }
                    else
                    {
                        result.x = (result.y / size.y) * size.x;
                    }
                    break;
            }

            if (relativeTo != null)
            {
                result += (Vector2)relativeTo.position;
            }

            return result;
        }
    }
}