using LaptopPricePrediction.Data;
using LaptopPricePrediction.Helper;
using Microsoft.ML;

string _baseDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "laptop_price.csv");
string _trainDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "laptop_price_clean_data.csv");
string _testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "laptop_price_test.csv");
string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");

CsvDataHelper.CleanAndSaveDataToCsv(_baseDataPath, _trainDataPath);

MLContext _mlContext = new MLContext(seed: 0);

TrainModel(_mlContext, _trainDataPath);

TestSinglePrediction(_mlContext);


ITransformer TrainModel(MLContext mlContext, string dataPath)
{

    IDataView trainingData = _mlContext.Data.LoadFromTextFile<LaptopOffer>(dataPath, hasHeader: true, separatorChar: ',');
    var processPipeline =
        _mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(LaptopOffer.Price))
                                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "LaptopIdCat", inputColumnName: nameof(LaptopOffer.LaptopId)))
                                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "CompanyCat", inputColumnName: nameof(LaptopOffer.CompanyName)))
                                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "ProductCat", inputColumnName: nameof(LaptopOffer.Product)))
                                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "TypNameCat", inputColumnName: nameof(LaptopOffer.TypeName)))
                                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "ScreenResolutionCat", inputColumnName: nameof(LaptopOffer.ScreenResolution)))
                                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "CpuCat", inputColumnName: nameof(LaptopOffer.Cpu)))
                                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "MemoryCat", inputColumnName: nameof(LaptopOffer.Memory)))
                                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "GpuCat", inputColumnName: nameof(LaptopOffer.Gpu)))
                                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "OpSys", inputColumnName: nameof(LaptopOffer.OpSys)))
                                .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(LaptopOffer.Ram)))
                                .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(LaptopOffer.Weight)))
                                .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(LaptopOffer.Inches)))
                                .Append(mlContext.Transforms.Concatenate("Features", "LaptopIdCat", "CompanyCat", "ProductCat", "TypNameCat",
                                "ScreenResolutionCat", "CpuCat", "MemoryCat", "GpuCat", "OpSys",
                                nameof(LaptopOffer.Ram), nameof(LaptopOffer.Weight), nameof(LaptopOffer.Inches)));

    var trainer = mlContext.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "Features");
    var trainingPipeline = processPipeline.Append(trainer);

    Console.WriteLine("=============== Training the model ===============");
    var trainedModel = trainingPipeline.Fit(trainingData);

    mlContext.Model.Save(trainedModel, trainingData.Schema, _modelPath);

    return trainedModel;

}

void TestSinglePrediction(MLContext mlContext)
{
    ITransformer trainedModel = mlContext.Model.Load(_modelPath, out var modelInputSchema);

    var predEngine = mlContext.Model.CreatePredictionEngine<LaptopOffer, PricePrediction>(trainedModel);

    var resultprediction = predEngine.Predict(LaptopOfferSample.Offer);

    Console.WriteLine($"**********************************************************************");
    Console.WriteLine($"Predicted fare: {resultprediction.Price:0.####}, actual fare: 1339.69");
    Console.WriteLine($"**********************************************************************");
}


