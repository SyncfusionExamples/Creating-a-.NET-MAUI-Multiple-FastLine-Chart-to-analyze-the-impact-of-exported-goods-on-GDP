
namespace MultipleFastLineChartDemo
{
    public class Model
    {
        public double Year { get; set; }
        public double Value { get; set; }
        public string Country { get; set; }

        public Model(double year, double value, string country)
        {
            Year = year;
            Value = value;
            Country = country;
        }
    }
}
