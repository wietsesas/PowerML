using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Detect change points in independent and identically distributed (IID) time series data using adaptive kernel density estimations and martingale scores.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "DetectIidChangePointTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddDetectIidChangePointTransformCmdlet : EstimatorChainCmdlet
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
        /// The length of the sliding window on p-values for computing the martingale score.
        /// </summary>
        [Parameter(Mandatory = true, Position = 3)]
        public int ChangeHistoryLength { get; set; }

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
            if (!ShouldProcess($"Add a DetectIidChangePointTransform", $"Are you sure you want to add a DetectIidChangePointTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.DetectIidChangePoint(OutputColumn, InputColumn, Confidence, ChangeHistoryLength, Martingale, Eps);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
