using System.Reflection;

namespace MultipleFastLineChartDemo
{
    public class ViewModel
    {
        List<Model>? allCountriesdata;

        public List<Model>? AllCountriesData
        {
            get
            {
                return allCountriesdata;
            }
            set
            {
                allCountriesdata = value;
            }
        }

        List<Model>? brazilData;
        public List<Model>? BrazilData
        {
            get
            {
                return brazilData;
            }
            set
            {
                brazilData = value;
            }
        }

        List<Model>? chinaData;
        public List<Model>? ChinaData
        {
            get
            {
                return chinaData;
            }
            set
            {
                chinaData = value;
            }
        }

        List<Model>? ukData;
        public List<Model>? UKData
        {
            get
            {
                return ukData;
            }
            set
            {
                ukData = value;
            }
        }

        List<Model>? usData;
        public List<Model>? USData
        {
            get
            {
                return usData;
            }
            set
            {
                usData = value;
            }
        }

        List<Model>? worldData;
        public List<Model>? WorldData
        {
            get
            {
                return worldData;
            }
            set
            {
                worldData = value;
            }
        }

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
            Stream inputStream = executingAssembly.GetManifestResourceStream("MultipleFastLineChartDemo.Resources.Raw.exportdata.csv");

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
