using CsvHelper;
using LaptopPricePrediction.Data;
using System.Globalization;

namespace LaptopPricePrediction.Helper
{
    public static class CsvDataHelper
    {
        public static void CleanAndSaveDataToCsv(string baseDataPath, string cleanDataPath)
        {
            using (var csv = new CsvWriter(new StreamWriter(cleanDataPath), CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<LaptopOffer>();
                csv.NextRecord();

                foreach (var x in File.ReadAllLines(baseDataPath).Skip(1).Select(x => x.Split(',')))
                {
                    try
                    {
                        var offer = new LaptopOffer()
                        {
                            LaptopId = x[0],
                            CompanyName = x[1],
                            Product = x[2],
                            TypeName = x[3],
                            Inches = float.Parse(x[4], CultureInfo.InvariantCulture),
                            ScreenResolution = x[5],
                            Cpu = x[6],
                            Ram = float.Parse(x[7].Replace("GB", ""), CultureInfo.InvariantCulture),
                            Memory = x[8],
                            Gpu = x[9],
                            OpSys = x[10],
                            Weight = float.Parse(x[11].Replace("kg", ""), CultureInfo.InvariantCulture),
                            Price = float.Parse(x[12], CultureInfo.InvariantCulture)
                        };

                        csv.WriteRecord(offer);
                        csv.NextRecord();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Skip Id product: {x[0]}");

                    }
                }
            }
        }
    }
}