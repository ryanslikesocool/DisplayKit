// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using System;
using UnityEngine;
using Unity.Mathematics;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace TScreen {
    [Serializable]
    public struct ScreenTranslation {
        public static readonly ScreenTranslation Default = new ScreenTranslation();

        public float2 position;
        public bool respectSafeArea;
        public Transform relativeTo;
#if ODIN_INSPECTOR_3
        [HorizontalGroup("H"), LabelText("H Space"), LabelWidth(96)] public ValueSpace horizontalValueSpace;
        [HorizontalGroup("V"), LabelText("V Space"), LabelWidth(96)] public ValueSpace verticalValueSpace;
        [HorizontalGroup("H"), LabelText("Alignment"), LabelWidth(96)] public HorizontalAlignment horizontalAlignment;
        [HorizontalGroup("V"), LabelText("Alignment"), LabelWidth(96)] public VerticalAlignment verticalAlignment;
        [EnableIf("@(horizontalValueSpace == ValueSpace.Viewport || verticalValueSpace == ValueSpace.Viewport)")] public UniformScaling uniformScaling;
#else
        public ValueSpace horizontalValueSpace;
        public ValueSpace verticalValueSpace;
        public HorizontalAlignment horizontalAlignment;
        public VerticalAlignment verticalAlignment;
        public UniformScaling uniformScaling;
#endif

        public ScreenTranslation(
            float2 position,
            bool respectSafeArea = false,
            Transform relativeTo = null,
            ValueSpace horizontalValueSpace = ValueSpace.Screen,
            ValueSpace verticalValueSpace = ValueSpace.Screen,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment verticalAlignment = VerticalAlignment.Middle,
            UniformScaling uniformScaling = UniformScaling.None
        ) {
            this.position = position;
            this.respectSafeArea = respectSafeArea;
            this.relativeTo = relativeTo;
            this.horizontalValueSpace = horizontalValueSpace;
            this.verticalValueSpace = verticalValueSpace;
            this.horizontalAlignment = horizontalAlignment;
            this.verticalAlignment = verticalAlignment;
            this.uniformScaling = uniformScaling;
        }

        public float3 ToWorldPosition(Camera camera, float distance) {
            if (camera == null) {
                camera = Camera.main;
            }

            float2 screenPoint = float2.zero;
            screenPoint.x = horizontalValueSpace switch {
                ValueSpace.Viewport => camera.ViewportToScreenPoint(new float3(position, distance)).x,
                ValueSpace.World => camera.WorldToScreenPoint(new float3(position, distance)).x,
                _ => position.x
            };
            screenPoint.y = verticalValueSpace switch {
                ValueSpace.Viewport => camera.ViewportToScreenPoint(new float3(position, distance)).y,
                ValueSpace.World => camera.WorldToScreenPoint(new float3(position, distance)).y,
                _ => position.y
            };

            if (horizontalValueSpace == ValueSpace.Viewport) {
                screenPoint.x = uniformScaling switch {
                    UniformScaling.HeightScalesWidth => screenPoint.x * Screen.VerticalAspect,
                    UniformScaling.MinScalesMax => Screen.MaxAxis == Screen.Width ? screenPoint.x * Screen.HorizontalAspect : screenPoint.x,
                    UniformScaling.MaxScalesMin => Screen.MinAxis == Screen.Width ? screenPoint.x * Screen.HorizontalAspect : screenPoint.x,
                    _ => screenPoint.x
                };
            }
            if (verticalValueSpace == ValueSpace.Viewport) {
                screenPoint.y = uniformScaling switch {
                    UniformScaling.WidthScalesHeight => screenPoint.y * Screen.HorizontalAspect,
                    UniformScaling.MinScalesMax => Screen.MaxAxis == Screen.Height ? screenPoint.y * Screen.VerticalAspect : screenPoint.y,
                    UniformScaling.MaxScalesMin => Screen.MinAxis == Screen.Height ? screenPoint.y * Screen.VerticalAspect : screenPoint.y,
                    _ => screenPoint.y
                };
            }

            screenPoint.x += horizontalAlignment switch {
                HorizontalAlignment.Leading => 0,
                HorizontalAlignment.Center => Screen.Extents.x,
                HorizontalAlignment.Trailing => Screen.Width,
                _ => 0
            };

            screenPoint.y += verticalAlignment switch {
                VerticalAlignment.Bottom => 0,
                VerticalAlignment.Middle => Screen.Extents.y,
                VerticalAlignment.Top => Screen.Height,
                _ => 0
            };

            if (respectSafeArea) {
                screenPoint.x = math.remap(0, Screen.Width, 0, Screen.SafeAreaScreen.width, screenPoint.x);
                screenPoint.y = math.remap(0, Screen.Height, 0, Screen.SafeAreaScreen.height, screenPoint.y);

                screenPoint.x += Screen.SafeAreaScreen.x;
                screenPoint.y += Screen.SafeAreaScreen.y;
            }

            Vector3 worldPoint = camera.ScreenToWorldPoint(new float3(screenPoint, distance));

            if (relativeTo != null) {
                worldPoint += relativeTo.position;
            }

            return worldPoint;
        }
    }
}