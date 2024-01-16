using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Graphics.Internals;

namespace FastLineChart
{
    public class NumericalAxisExt : NumericalAxis
    {
        protected override void DrawAxis(ICanvas canvas, Rect arrangeRect)
        {
            var axis = this as ChartAxis;

            if (axis.Parent is SfCartesianChart chart && BindingContext is ViewModel viewModel)
            {
                var x = arrangeRect.X + viewModel.Spacing;

                foreach (CartesianSeries series in chart.Series)
                {
                    var style = new ChartAxisLabelStyle() { FontSize = 12, TextColor = (series.Fill as SolidColorBrush).Color, Parent = this.Parent };
                    OnDrawLabels(canvas, series, style, (float)x);
                }
            }
        }

        private void OnDrawLabels(ICanvas canvas, CartesianSeries series, ChartLabelStyle style, float x)
        {
            if (series.ActualYAxis is NumericalAxis yaxis)
            {
                var data = (series.ItemsSource as List<Model>)?.Last();
                if (data != null)
                {
                    var y = yaxis.ValueToPoint(data.Value);
                    var alignment = LabelAlignment.GetAlignment(series);
                    string label = series.Label;

                    var size = label.Measure(style);

                    switch (alignment)
                    {
                        case SeriesLabelAlignment.Bottom:
                            y = y + (float)(size.Height / 2);
                            break;
                        case SeriesLabelAlignment.Top:
                            y = y - (float)(size.Height / 2);
                            break;
                        default:
                            break;
                    }

                    canvas.DrawText(label, x, y, (ITextElement)style);
                }
            }
        }
    }
}
