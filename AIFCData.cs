using Microsoft.ML.Data;

namespace AI_File_Classifier
{
    public class AIFCData
    {
        [LoadColumn(0)]
        public string Text { get; set; }

        [LoadColumn(1)]
        public string Label { get; set; }
    }

    public class AIFCPrediction
    {
        [ColumnName("PredictedLabel")]
        public string Label { get; set; }
    }
}
