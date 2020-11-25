using System;
using System.Windows;
using System.Windows.Controls;

namespace WPanelToolkit
{
	public class FillGrid : Panel
	{
		int rows = 0;
		int columns = 0;
		Size lastPanelSize = new Size();
		public FillGrid()
			: base()
		{

		}

		protected override Size MeasureOverride(Size availableSize)
		{
			try
			{
				// Giving a layout which yields the squarest children can be done like this:
				// n is number of elements
				// r is rows
				// c is columnts
				// A is aspect ratio (width / height)
				//
				// We get this system of equations:
				// rc = n
				// c = Ar
				// This can be solved like so:
				// c = A(n/c)
				// c^2 = An

				int elements = InternalChildren.Count;
				double aspectRatio = availableSize.Width / availableSize.Height;

				columns = (int)Math.Ceiling(Math.Sqrt(aspectRatio * elements) * 0.7); // The scale factor is not mathematically proven, it just looks nice as it makes square grids more likely. Feel free to tweak
				rows = (int)Math.Ceiling((double)elements / columns);

				while (rows * columns > elements + columns - 1)
				{
					columns--; // The scaling factor sometimes creates layouts which are too wide
				}

				if (rows == 1) // The above check does not account properly for 1 row grids (there should never be extra columns in this case)
				{
					columns = elements;
				}

				double maxWidth = 1; // Sizes of zero make WPF mad
				double maxHeight = 1;

				// In our example, we just have one child.
				// Report that our panel requires just the size of its only child.
				foreach (UIElement child in InternalChildren)
				{
					child.Measure(availableSize);
					maxWidth = child.DesiredSize.Width > maxWidth ? child.DesiredSize.Width : maxWidth;
					maxHeight = child.DesiredSize.Height > maxHeight ? child.DesiredSize.Height : maxHeight;
				}

				Size panelDesiredSize = new Size(maxWidth * columns, maxHeight * rows);
				lastPanelSize = panelDesiredSize;
				return panelDesiredSize;
			}
			catch (Exception e) // Preserves last arrangement if either dimension hits zero
			{
				return lastPanelSize;
			}
		}
		protected override Size ArrangeOverride(Size finalSize)
		{
			try
			{
				double elementWidth = finalSize.Width / columns;
				double elementHeight = finalSize.Height / rows;

				for (int i = 0; i < InternalChildren.Count; i++)
				{
					int x_index = i % columns;
					int y_index = i / columns;

					if (x_index == 0 && y_index == rows - 1 && InternalChildren.Count % columns > 0)
					{
						elementWidth = finalSize.Width / (InternalChildren.Count % columns);
					}

					Rect boundingRect = new Rect(x_index * elementWidth, y_index * elementHeight, elementWidth, elementHeight);

					InternalChildren[i].Arrange(boundingRect);
				}
			}
			catch (Exception e) // Preserves last arrangement if either dimension hits zero
			{

			}

			return finalSize; // Returns the final Arranged size
		}

	}
}
