using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Graphics.Internals;

namespace MultipleFastLineChartDemo
{
    public class NumericalAxisExt : NumericalAxis, ITextElement
    {
        ViewModel viewModel;

        float xValue;

        float yValue;

        public FontAttributes FontAttributes => FontAttributes.None;

        public string FontFamily => Font.Family != null ? Font.Family : string.Empty;

        public double FontSize
        {
            get
            {
#if ANDROID || IOS
                return 12; // Android size
#else
        return 14; // Default size for other platforms
#endif
            }
        }
        public Microsoft.Maui.Font Font => Microsoft.Maui.Font.Default;

        private Color _textColor = Colors.Black;
        public Color TextColor { get => _textColor; set => _textColor = value; }

        public NumericalAxisExt()
        {
            viewModel = new ViewModel();
        }

        protected override void DrawAxis(ICanvas canvas, Rect arrangeRect)
        {
            var axis = this as ChartAxis;

            if (axis.Parent is SfCartesianChart chart)

                foreach (var series in chart.Series)
                {
                    if (series is CartesianSeries cartesianSeries)
                    {
                        if (cartesianSeries.Label == "World" && viewModel.WorldData is List<Model> worldData)
                        {
                            var worldDataCount = worldData.Count - 1;
                            xValue = (float)worldData[worldDataCount].Year;
                            yValue = (float)worldData[worldDataCount].Value;
                        }
                        else if (cartesianSeries.Label == "Brazil" && viewModel.BrazilData is List<Model> brazilData)
                        {
                            var brazilDataCount = brazilData.Count - 1;
                            xValue = (float)brazilData[brazilDataCount].Year;
                            yValue = (float)brazilData[brazilDataCount].Value;
                        }
                        else if (cartesianSeries.Label == "China" && viewModel.ChinaData is List<Model> chinaData)
                        {
                            var chinaDataCount = chinaData.Count - 1;
                            xValue = (float)chinaData[chinaDataCount].Year;
                            yValue = (float)chinaData[chinaDataCount].Value;
                        }
                        else if (cartesianSeries.Label == "United Kingdom" && viewModel.UKData is List<Model> ukData)
                        {
                            var ukDataCount = ukData.Count - 1;
                            xValue = (float)ukData[ukDataCount].Year;
                            yValue = (float)ukData[ukDataCount].Value;
                        }
                        else if (viewModel.USData is List<Model> usData)
                        {
                            var usDataCount = usData.Count - 1;
                            xValue = (float)usData[usDataCount].Year;
                            yValue = (float)usData[usDataCount].Value;
                        }

                        var x = chart.ValueToPoint(chart.XAxes[0], xValue) + 5;
                        var y = chart.ValueToPoint(chart.YAxes[0], yValue) - 10;

                        if (chart.Title is View titleView)
                            y -= (float)titleView.DesiredSize.Height;

                        var width = arrangeRect.Width;
                        var height = 15;

                        if (cartesianSeries.Fill is SolidColorBrush seriesFill)
                            TextColor = seriesFill.Color;

                        // For overlapped label
                        if (cartesianSeries.Label == "China" || cartesianSeries.Label == "United States")
                        {
                            canvas.DrawText(cartesianSeries.Label, new Rect(x, (float)y + 5, width, height), HorizontalAlignment.Left, VerticalAlignment.Center, this);
                        }
                        // For provide additional height
                        else if (cartesianSeries.Label == "United Kingdom")
                        {
                            canvas.DrawText(cartesianSeries.Label, new Rect(x, (float)y, width, height + 5), HorizontalAlignment.Left, VerticalAlignment.Center, this);
                        }
                        // For overlapped label
                        else if (cartesianSeries.Label == "Brazil")
                        {
                            canvas.DrawText(cartesianSeries.Label, new Rect(x, (float)y - 3, width, height), HorizontalAlignment.Left, VerticalAlignment.Center, this);
                        }
                        else
                        {
                            canvas.DrawText(cartesianSeries.Label, new Rect(x, (float)y, width, height), HorizontalAlignment.Left, VerticalAlignment.Center, this);
                        }
                    }
                }
        }

        public void OnFontFamilyChanged(string oldValue, string newValue)
        {

        }

        public void OnFontSizeChanged(double oldValue, double newValue)
        {

        }

        public double FontSizeDefaultValueCreator()
        {
            return 0;
        }

        public void OnFontAttributesChanged(FontAttributes oldValue, FontAttributes newValue)
        {

        }

        public void OnFontChanged(Microsoft.Maui.Font oldValue, Microsoft.Maui.Font newValue)
        {

        }
    }
}
