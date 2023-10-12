using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Predict a target using a linear multiclass classifier. The trained model LinearMulticlassModelParameters produces probabilities of classes.
    /// </summary>
    /// <category order="50">Trainers</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "SdcaNonCalibratedMulticlassTrainer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddSdcaNonCalibratedMulticlassTrainerCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The name of the label column. The column data must be KeyDataViewType.
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
        /// The loss function minimized in the training process. Defaults to LogLoss if not specified.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public ISupportSdcaClassificationLoss? LossFunction { get; set; } = null;

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
            if (!ShouldProcess($"Add a SdcaNonCalibratedMulticlassTrainer", $"Are you sure you want to add a SdcaNonCalibratedMulticlassTrainer?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.MulticlassClassification.Trainers.SdcaNonCalibrated(LabelColumn, FeatureColumn, ExampleWeightColumn, LossFunction, L2Regularization, L1Regularization, MaxIterations);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
