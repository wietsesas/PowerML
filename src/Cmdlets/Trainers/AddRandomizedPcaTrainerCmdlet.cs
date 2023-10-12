using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Train an approximate PCA using Randomized SVD algorithm.
    /// </summary>
    /// <category order="50">Trainers</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "RandomizedPcaTrainer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddRandomizedPcaTrainerCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The name of the feature column. The column data must be a known-sized vector of Single.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Features")]
        public string FeatureColumn { get; set; } = "Features";

        /// <summary>
        /// The name of the example weight column (optional). To use the weight column, the column data must be of type Single.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? ExampleWeightColumn { get; set; } = null;

        /// <summary>
        /// The number of components in the PCA.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 20)]
        public int Rank { get; set; } = 20;

        /// <summary>
        /// Oversampling parameter for randomized PCA training.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 20)]
        public int Oversampling { get; set; } = 20;

        /// <summary>
        /// If enabled, data is not centered to be zero mean.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontEnsureZeroMean { get; set; }

        /// <summary>
        /// The seed for random number generation.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public int? Seed { get; set; } = null;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a RandomizedPcaTrainer", $"Are you sure you want to add a RandomizedPcaTrainer?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.AnomalyDetection.Trainers.RandomizedPca(FeatureColumn, ExampleWeightColumn, Rank, Oversampling, !DontEnsureZeroMean, Seed);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
