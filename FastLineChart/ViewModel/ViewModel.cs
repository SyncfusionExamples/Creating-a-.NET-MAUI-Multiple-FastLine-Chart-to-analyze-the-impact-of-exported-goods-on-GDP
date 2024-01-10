using System.Collections.ObjectModel;
using System.Reflection;

namespace FastLineChart
{
    public class ViewModel
    {
        public List<Model>? AllCountriesData { get; set; }
        public List<Model>? BrazilData { get; set; }
        public List<Model>? ChinaData { get; set; }
        public List<Model>? UKData { get; set; }
        public List<Model>? USData { get; set; }
        public List<Model>? WorldData { get; set; }
        public float Spacing => 5;

        public ObservableCollection<SolidColorBrush> PaletteBrushes { get; set; }
        public ViewModel()
        {
            AllCountriesData = new List<Model>(ReadCSV());
            BrazilData = AllCountriesData.Where(d => d.Country == "Brazil").ToList();
            ChinaData = AllCountriesData.Where(d => d.Country == "China").ToList();
            UKData = AllCountriesData.Where(d => d.Country == "United Kingdom").ToList();
            USData = AllCountriesData.Where(d => d.Country == "United States").ToList();
            WorldData = AllCountriesData.Where(d => d.Country == "World").ToList();
        }

        public static IEnumerable<Model> ReadCSV()
        {
            Assembly executingAssembly = typeof(App).GetTypeInfo().Assembly;
            Stream inputStream = executingAssembly.GetManifestResourceStream("FastLineChart.Resources.Raw.exportdata.csv");

            string line;
            List<string> lines = new();

            using StreamReader reader = new(inputStream);
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }

            return lines.Select(line =>
            {
                string[] data = line.Split(',');

                return new Model(Convert.ToDouble(data[0]), Math.Round(Convert.ToDouble(data[1]), 1), data[2]);
            });
        }
    }
}
