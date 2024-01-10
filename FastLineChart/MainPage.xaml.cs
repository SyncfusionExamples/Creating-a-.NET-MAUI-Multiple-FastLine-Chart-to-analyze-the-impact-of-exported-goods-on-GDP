using Syncfusion.Maui.Charts;

namespace FastLineChart
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SfCartesianChart_TrackballCreated(object sender, TrackballEventArgs e)
        {
            var trackballInfoList = e.TrackballPointsInfo;

            foreach (var trackballInfo in trackballInfoList)
            {
                string prefix = trackballInfo.Series.Label == "United Kingdom" ? "UK" :
                trackballInfo.Series.Label == "United States" ? "US" :
                trackballInfo.Series.Label;
                trackballInfo.Label = $"{prefix} : {trackballInfo.Label}%";

                ChartMarkerSettings chartMarkerSettings = new ChartMarkerSettings();
                chartMarkerSettings.Stroke = trackballInfo.Series.Fill;
                chartMarkerSettings.StrokeWidth = 2;
                chartMarkerSettings.Fill = Colors.White;

                trackballInfo.MarkerSettings = chartMarkerSettings;
            }
        }
    }

    public enum SeriesLabelAlignment
    {
        Center,
        Top,
        Bottom
    }

    public static class LabelAlignment
    {
        public static readonly BindableProperty AlignmentProperty =
            BindableProperty.CreateAttached(
                "Alignment", typeof(SeriesLabelAlignment), typeof(LabelAlignment),
                defaultValue: SeriesLabelAlignment.Center,
                propertyChanged: OnAlignmentPropertyChanged);

        public static SeriesLabelAlignment GetAlignment(BindableObject view)
        {
            return (SeriesLabelAlignment)view.GetValue(AlignmentProperty);
        }

        public static void SetAlignment(BindableObject view, string value)
        {
            view.SetValue(AlignmentProperty, value);
        }

        private static void OnAlignmentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }
    }
}
