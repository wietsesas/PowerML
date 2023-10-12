using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Train a boosted decision tree multi-class classification model using LightGBM.
    /// </summary>
    /// <category order="50">Trainers</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "LightGbmMulticlassTrainer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddLightGbmMulticlassTrainerCmdlet : EstimatorChainCmdlet
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
        /// The maximum number of leaves in one tree.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public int? Leaves { get; set; } = null;

        /// <summary>
        /// The minimal number of data points required to form a new tree leaf.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public int? MinExampleCountPerLeaf { get; set; } = null;

        /// <summary>
        /// The learning rate.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public double? LearningRate { get; set; } = null;

        /// <summary>
        /// The number of boosting iterations. A new tree is created in each iteration, so this is equivalent to the number of trees.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 100)]
        public int Iterations { get; set; } = 100;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a LightGbmMulticlassTrainer", $"Are you sure you want to add a LightGbmMulticlassTrainer?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.MulticlassClassification.Trainers.LightGbm(LabelColumn, FeatureColumn, ExampleWeightColumn, Leaves, MinExampleCountPerLeaf, LearningRate, Iterations);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
