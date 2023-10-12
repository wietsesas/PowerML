using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Evaluate a machine learning model.
    /// </summary>
    /// <category order="35">Model</category>
    /// <outputtype name="Microsoft.ML.Data.CalibratedBinaryClassificationMetrics">This cmdlet returns evaluation metrics.</outputtype>
    /// <outputtype name="Microsoft.ML.Data.MulticlassClassificationMetrics">This cmdlet returns evaluation metrics.</outputtype>
    /// <outputtype name="Microsoft.ML.Data.RegressionMetrics">This cmdlet returns evaluation metrics.</outputtype>
    [Cmdlet(VerbsDiagnostic.Test, "MLModel", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(CalibratedBinaryClassificationMetrics), ParameterSetName = new string[] { PSNames.BinaryClassification })]
    [OutputType(typeof(MulticlassClassificationMetrics), ParameterSetName = new string[] { PSNames.MulticlassClassification })]
    [OutputType(typeof(RegressionMetrics), ParameterSetName = new string[] { PSNames.Regression })]
    public sealed class TestMLModelCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string BinaryClassification = nameof(BinaryClassification);
            public const string MulticlassClassification = nameof(MulticlassClassification);
            public const string Regression = nameof(Regression);
        }

        /// <summary>
        /// The transformer to evaluate.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public TransformerChain<ITransformer>? Model { get; set; } = null;

        /// <summary>
        /// The data to evaluate the model.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public IDataView? Data { get; set; } = null;

        /// <summary>
        /// Test a binary classification model.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.BinaryClassification)]
        [PSDefaultValue(Value = false)]
        public SwitchParameter BinaryClassification { get; set; }

        /// <summary>
        /// Test a multiclass classification model.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.MulticlassClassification)]
        [PSDefaultValue(Value = false)]
        public SwitchParameter MulticlassClassification { get; set; }

        /// <summary>
        /// Test a regression model.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.Regression)]
        [PSDefaultValue(Value = false)]
        public SwitchParameter Regression { get; set; }

        /// <summary>
        /// The label column name.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.BinaryClassification)]
        [Parameter(ParameterSetName = PSNames.MulticlassClassification)]
        [Parameter(ParameterSetName = PSNames.Regression)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Label")]
        public string LabelColumn { get; set; } = "Label";

        /// <summary>
        /// The score column name.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.BinaryClassification)]
        [Parameter(ParameterSetName = PSNames.MulticlassClassification)]
        [Parameter(ParameterSetName = PSNames.Regression)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Score")]
        public string ScoreColumn { get; set; } = "Score";

        /// <summary>
        /// The probability column name.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.BinaryClassification)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Probability")]
        public string ProbabilityColumn { get; set; } = "Probability";

        /// <summary>
        /// The predicted label column name
        /// </summary>
        [Parameter(ParameterSetName = PSNames.BinaryClassification)]
        [Parameter(ParameterSetName = PSNames.MulticlassClassification)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "PredictedLabel")]
        public string PredictedLabelColumn { get; set; } = "PredictedLabel";

        /// <summary>
        /// TopKPredictionCount
        /// </summary>
        [Parameter(ParameterSetName = PSNames.MulticlassClassification)]
        [PSDefaultValue(Value = 0)]
        public int TopKPredictionCount { get; set; } = 0;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (!ShouldProcess($"Evaluate the model", $"Are you sure you want to evaluate the model?", "PowerML"))
                return;

            IDataView predictions = Model!.Transform(Data);
            object metrics = ParameterSetName switch
            {
                PSNames.BinaryClassification => Context!.BinaryClassification.Evaluate(predictions, LabelColumn, ScoreColumn, ProbabilityColumn, PredictedLabelColumn),
                PSNames.MulticlassClassification => Context!.MulticlassClassification.Evaluate(predictions, LabelColumn, ScoreColumn, PredictedLabelColumn, TopKPredictionCount),
                PSNames.Regression => Context!.Regression.Evaluate(predictions, LabelColumn, ScoreColumn),
                _ => throw new NotImplementedException(),
            };
            WriteObject(metrics, false);
        }
    }
}
