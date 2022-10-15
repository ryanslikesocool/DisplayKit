// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;

namespace DisplayKit {
    public enum UniformScaling : byte {
        None,
        [InspectorName("Width scales Height")] WidthScalesHeight,
        [InspectorName("Height scales Width")] HeightScalesWidth,
        [InspectorName("Min scales Max")] MinScalesMax,
        [InspectorName("Max scales Min")] MaxScalesMin
    }
}