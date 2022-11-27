using Microsoft.ML.Data;

namespace LaptopPricePrediction.Data
{
    public class PricePrediction
    {
        [ColumnName("Score")]
        public float Price;
    }
}
