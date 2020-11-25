# WPanelToolkit
A collection of panels for WPF. Available on [NuGet](https://www.nuget.org/packages/WPanelToolkit/), or to be compiled from source.

## FillGrid
A control which puts its elements into a grid, attempting to give each element equal size. If the grid is non-rectangular the last row of elements will be stretched to fill the space (this is the difference between this panel and the built-in `UniformGrid`).

### Examples:
```xaml
<FillGrid>
		<Rectangle MinWidth="200" MinHeight="200" Fill="Red"/>
		<Rectangle MinWidth="200" MinHeight="200" Fill="Green"/>
		<Rectangle MinWidth="200" MinHeight="200" Fill="Blue"/>
		<Rectangle MinWidth="200" MinHeight="200" Fill="Gray"/>
		<Rectangle MinWidth="200" MinHeight="200" Fill="Black"/>
		<Rectangle MinWidth="200" MinHeight="200" Fill="Aqua"/>
</FillGrid>
```

Normal Aspect Ratio:
![Normal Aspect Ratio](/Demo/images/standard_aspect_ratio.png)

Wide Aspect Ratio:
![Wide Aspect Ratio](/Demo/images/very_long_aspect_ratio.png)
