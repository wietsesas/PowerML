using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Detect change points in time series data using singular spectrum analysis (SSA).
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "DetectChangePointBySsaTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddDetectChangePointBySsaTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. The column data is a vector of Double. The vector contains 4 elements: alert (non-zero value means a change point), raw score, p-Value and martingale score.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of column to transform. The column data must be Single. If set to null, the value of the outputColumnName will be used as source.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string InputColumn { get; set; } = string.Empty;

        /// <summary>
        /// The confidence for change point detection in the range [0, 100].
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        public double Confidence { get; set; }

        /// <summary>
        /// The size of the sliding window for computing the p-value.
        /// </summary>
        [Parameter(Mandatory = true, Position = 3)]
        public int ChangeHistoryLength { get; set; }

        /// <summary>
        /// The number of points from the beginning of the sequence used for training.
        /// </summary>
        [Parameter(Mandatory = true, Position = 4)]
        public int TrainingWindowSize { get; set; }

        /// <summary>
        /// An upper bound on the largest relevant seasonality in the input time-series.
        /// </summary>
        [Parameter(Mandatory = true, Position = 5)]
        public int SeasonalityWindowSize { get; set; }

        /// <summary>
        /// The function used to compute the error between the expected and the observed value.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = ErrorFunction.SignedDifference)]
        public ErrorFunction ErrorFunction { get; set; } = ErrorFunction.SignedDifference;

        /// <summary>
        /// The martingale used for scoring.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = MartingaleType.Power)]
        public MartingaleType Martingale { get; set; } = MartingaleType.Power;

        /// <summary>
        /// The epsilon parameter for the Power martingale.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.1)]
        public double Eps { get; set; } = 0.1;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a DetectChangePointBySsaTransform", $"Are you sure you want to add a DetectChangePointBySsaTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.DetectChangePointBySsa(OutputColumn, InputColumn, Confidence, ChangeHistoryLength, TrainingWindowSize, SeasonalityWindowSize, ErrorFunction, Martingale, Eps);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
