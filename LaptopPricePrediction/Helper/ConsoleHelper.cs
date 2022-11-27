using Microsoft.ML.Data;

namespace LaptopPricePrediction.Helper
{
    public static class ConsoleHelper
    {
        public static void PrintRegressionMetrics(string name, RegressionMetrics metrics)
        {
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Metrics for {name} regression model      ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       Loss Function:        {metrics.LossFunction:0.##}");
            Console.WriteLine($"*       RSquared Score::      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Mean Absolute Error: {metrics.MeanAbsoluteError:#.##}");
            Console.WriteLine($"*       Mean Squared Error: {metrics.MeanSquaredError:#.##}");
            Console.WriteLine($"*       Root Mean Squared Error::      {metrics.RootMeanSquaredError:#.##}");
            Console.WriteLine($"*************************************************");
        }
    }
}
