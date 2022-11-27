using Microsoft.ML.Data;

namespace LaptopPricePrediction.Data
{
    public class LaptopOffer
    {
        [LoadColumn(0)]
        public string LaptopId { get; set; }

        [LoadColumn(1)]
        public string CompanyName { get; set; }

        [LoadColumn(2)]
        public string Product { get; set; }

        [LoadColumn(3)]
        public string TypeName { get; set; }

        [LoadColumn(4)]
        public float Inches { get; set; }

        [LoadColumn(5)]
        public string ScreenResolution { get; set; }

        [LoadColumn(6)]
        public string Cpu { get; set; }

        [LoadColumn(7)]
        public float Ram { get; set; }

        [LoadColumn(8)]
        public string Memory { get; set; }

        [LoadColumn(9)]
        public string Gpu { get; set; }

        [LoadColumn(10)]
        public string OpSys  { get; set; }

        [LoadColumn(11)]
        public float Weight { get; set; }

        [LoadColumn(12)]
        public float Price { get; set; }
    }
}
