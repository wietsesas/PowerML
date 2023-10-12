using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Predict a target using a maximum entropy multiclass classifier trained with L-BFGS method.
    /// </summary>
    /// <category order="50">Trainers</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "LbfgsMaximumEntropyMulticlassTrainer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddLbfgsMaximumEntropyMulticlassTrainerCmdlet : EstimatorChainCmdlet
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
        /// The L1 regularization hyperparameter. Higher values will tend to lead to more sparse model.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1)]
        public float L1Regularization { get; set; } = 1;

        /// <summary>
        /// The L2 weight for regularization.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1)]
        public float L2Regularization { get; set; } = 1;

        /// <summary>
        /// Threshold for optimizer convergence.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1E-07f)]
        public float OptimizationTolerance { get; set; } = 1E-07f;

        /// <summary>
        /// Memory size for LbfgsLogisticRegressionBinaryTrainer. Low=faster, less accurate.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 20)]
        public int HistorySize { get; set; } = 20;

        /// <summary>
        /// Enforce non-negative weights.
        /// </summary>
        [PSDefaultValue(Value = false)]
        public SwitchParameter EnforceNonNegativity { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a LbfgsMaximumEntropyMulticlassTrainer", $"Are you sure you want to add a LbfgsMaximumEntropyMulticlassTrainer?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.MulticlassClassification.Trainers.LbfgsMaximumEntropy(LabelColumn, FeatureColumn, ExampleWeightColumn, L1Regularization, L2Regularization, OptimizationTolerance, HistorySize, EnforceNonNegativity);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
