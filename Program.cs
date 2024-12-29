using Microsoft.ML;
using AI_File_Classifier;
using System.Data;
using static Microsoft.ML.DataOperationsCatalog;
using System.Linq;
using System.Reflection.PortableExecutable;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

string _appPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) ?? ".";
string _dataPath = Path.Combine(_appPath, "..", "..", "..", "Data", "Business_Files_Dataset.csv");
string _modelPath = Path.Combine(_appPath, "..", "..", "..", "Models", "AIFC_model.zip");

MLContext _mlContext = new MLContext(seed: 0);

// Load Data
IDataView _trainingDataView = _mlContext.Data.LoadFromTextFile<AIFCData>(_dataPath, separatorChar: ',', hasHeader: true);

// Split Data
TrainTestData dataSplit = _mlContext.Data.TrainTestSplit(_trainingDataView, testFraction: 0.2);

// Build Pipeline
var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", "Label")
    .Append(_mlContext.Transforms.Text.FeaturizeText("Features", "Text"))
    .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
    .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

// Train the Model
Console.WriteLine("=============== Training the model ===============");
ITransformer trainedModel = pipeline.Fit(dataSplit.TrainSet);

// Evaluate the Model
Console.WriteLine("=============== Evaluating the model ===============");
var testMetrics = _mlContext.MulticlassClassification.Evaluate(trainedModel.Transform(dataSplit.TestSet));
Console.WriteLine($"* Metrics:");
Console.WriteLine($"  MicroAccuracy:    {testMetrics.MicroAccuracy:0.###}");
Console.WriteLine($"  MacroAccuracy:    {testMetrics.MacroAccuracy:0.###}");
Console.WriteLine($"  LogLoss:          {testMetrics.LogLoss:0.###}");
Console.WriteLine($"  LogLossReduction: {testMetrics.LogLossReduction:0.###}");

// Save the Model
Console.WriteLine("=============== Saving the model ===============");
_mlContext.Model.Save(trainedModel, _trainingDataView.Schema, _modelPath);
Console.WriteLine($"Model saved to {_modelPath}");

// Predict a Single Example
var predEngine = _mlContext.Model.CreatePredictionEngine<AIFCData, AIFCPrediction>(trainedModel);
AIFCData sampleData = new AIFCData { Text = "2024-12-24 Invoice #12345 Acme Inc. $1,250.00", Label = "Invoice" };
var prediction = predEngine.Predict(sampleData);
Console.WriteLine($"=============== Single Prediction - Result: {prediction.Label} ===============");
