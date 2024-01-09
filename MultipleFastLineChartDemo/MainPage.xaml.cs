using Syncfusion.Maui.Charts;

namespace MultipleFastLineChartDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SfCartesianChart_TrackballCreated(object sender, Syncfusion.Maui.Charts.TrackballEventArgs e)
        {
            var trackballInfoList = e.TrackballPointsInfo;

            foreach (var trackballInfo in trackballInfoList)
            {
                if (trackballInfo.Series.Label == "United Kingdom")
                {
                    trackballInfo.Label = $"UK : {trackballInfo.Label}%";
                }
                else if (trackballInfo.Series.Label == "United States")
                {
                    trackballInfo.Label = $"US : {trackballInfo.Label}%";
                }
                else
                {
                    trackballInfo.Label = $"{trackballInfo.Series.Label} : {trackballInfo.Label}%";
                }

                ChartMarkerSettings chartMarkerSettings = new ChartMarkerSettings();
                chartMarkerSettings.Stroke = trackballInfo.Series.Fill;
                chartMarkerSettings.StrokeWidth = 2;
                chartMarkerSettings.Fill = Colors.White;

                trackballInfo.MarkerSettings = chartMarkerSettings;
            }
        }
    }
}
