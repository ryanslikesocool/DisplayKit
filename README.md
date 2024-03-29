# DisplayKit
Screen transforms made easy for Unity.

**Recommended Installation** (Unity Package Manager)\
- "Add package from git URL..."
- `https://github.com/ryanslikesocool/DisplayKit.git`

**Alternate Installation**
- Get the latest [release](https://github.com/ryanslikesocool/DisplayKit/releases)
- Open with the desired Unity project
- Import into Plugins

## Notice
DisplayKit works best with an Orthographic camera.  Some features may work with a perspective camera, but it is not currently supported.

## Structs
### ScreenLength
- `value`: the length in `valueSpace` units
- `lengthMode`: the `LengthMode` to use for this value
- `respectSafeArea`: should the length respect the screen safe area?
- `valueSpace`: the `ValueSpace` to use when calculating the result for `value`
- `axis`: the `Axis` to base the result calculation on

### ScreenSize
- `size`: the size in (`horizontalValueSpace`, `verticalValueSpace`) units
- `respectSafeArea`: should the size respect the screen safe area?
- `horizontalValueSpace`: the `ValueSpace` to use when calculating the result for `size.x`
- `verticalValueSpace`: the `ValueSpace` to use when calculating the result for `size.y`
- `uniformScaling`: the `UniformScaling` to use when calculating the result

### ScreenTranslation
- `position`: the position in (`horizontalValueSpace`, `verticalValueSpace`) units
- `respectSafeArea`: should the position respect the screen safe area?
- `relativeTo`: the `UnityEngine.Transform` to act as the "center" of the position calculation.  If none is present, "center" can be the center of the screen or `Vector3.zero`, depending on the value spaces used.
- `horizontalValueSpace`: the `ValueSpace` to use when calculating the result for `position.x`
- `verticalValueSpace`: the `ValueSpace` to use when calculating the result for `position.y`
- `horizontalAlignment`: the `HorizontalAlignment` to use when calculating the result for `position.x`
- `verticalAlignment`: the `VerticalAlignment` to use when calculating the result for `position.y`
- `uniformScaling`: the `UniformScaling` to use when calculating the result

## Enumerations
### LengthMode
- `Position`: "positioned" value.  The camera placement in the world affects the result
- `Scale`: "unpositioned" value.  The camera placement in the world does not affect the result

### ValueSpace
- `Viewport`: "normalized" screen space.  Values in the range [0...1],[0...1] will appear on screen.  Lengths on different axes will behave differently, if the screen is not square
- `Screen`: "pixel" space.  Values in the range [0...ScreenWidth],[0...ScreenHeight] will appear on screen
- `World`: world space, aka "units"

### Axis
- `Horizontal`: the horizontal screen axis
- `Vertical`: the vertical screen axis
- `Min`: the smallest screen axis
- `Max`: the largest screen axis

### UniformScaling
- `None`: no scaling is applied.  x and y values are calculated independently of eachother
- `WidthScalesHeight`: the x axis controls the y axis' scaling
- `HeightScalesWidth`: the y axis controls the x axis' scaling
- `MinScalesMax`: the min axis controls the max axis' scaling
- `MaxScalesMin`: the max axis controls the min axis' scaling

### VerticalAlignment
- `Bottom`: the bottom edge of the screen/safe area
- `Middle`: the middle of the screen/safe area on the y axis
- `Top`: the top edge of the screen/safe area

### HorizontalAlignment
- `Leading`: the left edge of the screen/safe area
- `Center`: the center of the screen/safe area on the x axis
- `Trailing`: the right edge of the screen/safe area
