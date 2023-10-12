using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Train a binary logistic regression classification model using the stochastic dual coordinate ascent method. The trained model is calibrated and can produce probability by feeding the output value of the linear function to a PlattCalibrator.
    /// </summary>
    /// <category order="50">Trainers</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "SdcaLogisticRegressionBinaryTrainer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddSdcaLogisticRegressionBinaryTrainerCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The name of the label column. The column data must be Single.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Label")]
        public string LabelColumn { get; set; } = "Label";

        /// <summary>
        /// The name of the feature column. The column data must be a known-sized vector of Single.
        /// </summary>
        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Features")]
        public string FeatureColumn { get; set; } = "Features";

        /// <summary>
        /// The name of the example weight column (optional).
        /// </summary>
        [Parameter(Position = 2)]
        [PSDefaultValue(Value = null)]
        public string? ExampleWeightColumn { get; set; } = null;

        /// <summary>
        /// The L1 regularization hyperparameter. Higher values will tend to lead to more sparse model.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public float? L1Regularization { get; set; } = null;

        /// <summary>
        /// The L2 weight for regularization.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public float? L2Regularization { get; set; } = null;

        /// <summary>
        /// The maximum number of passes to perform over the data.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public int? MaxIterations { get; set; } = null;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a SdcaLogisticRegressionBinaryTrainer", $"Are you sure you want to add a SdcaLogisticRegressionBinaryTrainer?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.BinaryClassification.Trainers.SdcaLogisticRegression(LabelColumn, FeatureColumn, ExampleWeightColumn, L2Regularization, L1Regularization, MaxIterations);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
