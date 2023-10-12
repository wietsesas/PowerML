using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Calculate contribution scores for each element of a feature vector.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "CalculateFeatureContribution", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddCalculateFeatureContributionCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// A ISingleFeaturePredictionTransformer that supports Feature Contribution Calculation, and which will also be used for scoring.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public ISingleFeaturePredictionTransformer<ICalculateFeatureContribution>? Transformer { get; set; } = null;

        /// <summary>
        /// The number of positive contributions to report, sorted from highest magnitude to lowest magnitude. Note that if there are fewer features with positive contributions than numberOfPositiveContributions, the rest will be returned as zeros.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 10)]
        public int PositiveContributions { get; set; } = 10;

        /// <summary>
        /// The number of negative contributions to report, sorted from highest magnitude to lowest magnitude. Note that if there are fewer features with negative contributions than numberOfNegativeContributions, the rest will be returned as zeros.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 10)]
        public int NegativeContributions { get; set; } = 10;

        /// <summary>
        /// Whether the feature contributions should be normalized to the [-1, 1] interval.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontNormalize { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a CalculateFeatureContribution", $"Are you sure you want to add a CalculateFeatureContribution?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.CalculateFeatureContribution(Transformer, PositiveContributions, NegativeContributions, !DontNormalize);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
