using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Train a decision tree ranking model using FastTree.
    /// </summary>
    /// <category order="50">Trainers</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "FastTreeRankingTrainer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddFastTreeRankingTrainerCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The name of the label column. The column data must be Single or KeyDataViewType.
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
        /// The name of the group column.
        /// </summary>
        [Parameter(Position = 2)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "GroupId")]
        public string RowGroupColumn { get; set; } = "GroupId";

        /// <summary>
        /// The name of the example weight column (optional).
        /// </summary>
        [Parameter(Position = 3)]
        [PSDefaultValue(Value = null)]
        public string? ExampleWeightColumn { get; set; } = null;

        /// <summary>
        /// The maximum number of leaves per decision tree.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 20)]
        public int Leaves { get; set; } = 20;

        /// <summary>
        /// Total number of decision trees to create in the ensemble.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 100)]
        public int Trees { get; set; } = 100;

        /// <summary>
        /// The minimal number of data points required to form a new tree leaf.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 10)]
        public int MinExampleCountPerLeaf { get; set; } = 10;

        /// <summary>
        /// The learning rate.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.2)]
        public double LearningRate { get; set; } = 0.2;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a FastTreeRankingTrainer", $"Are you sure you want to add a FastTreeRankingTrainer trainer?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Ranking.Trainers.FastTree(LabelColumn, FeatureColumn, RowGroupColumn, ExampleWeightColumn, Leaves, Trees, MinExampleCountPerLeaf, LearningRate);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
