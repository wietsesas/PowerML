using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Detect anomalies in the input time series data using the Spectral Residual (SR) algorithm.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "DetectAnomalyBySrCnnTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddDetectAnomalyBySrCnnTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. The column data is a vector of Double. The vector contains 3 elements: alert (1 means anomaly while 0 means normal), raw score, and magnitude of spectual residual.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of column to transform. The column data must be Single.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string InputColumn { get; set; } = string.Empty;

        /// <summary>
        /// The size of the sliding window for computing spectral residual.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 64)]
        public int WindowSize { get; set; } = 64;

        /// <summary>
        /// The number of points to add back of training window. No more than windowSize, usually keep default value.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 5)]
        public int BackAddWindowSize { get; set; } = 5;

        /// <summary>
        /// The number of pervious points used in prediction. No more than windowSize, usually keep default value.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 5)]
        public int LookaheadWindowSize { get; set; } = 5;

        /// <summary>
        /// The size of sliding window to generate a saliency map for the series. No more than windowSize, usually keep default value.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 3)]
        public int AveragingWindowSize { get; set; } = 3;

        /// <summary>
        /// The size of sliding window to calculate the anomaly score for each data point. No more than windowSize.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 21)]
        public int JudgementWindowSize { get; set; } = 21;

        /// <summary>
        /// The threshold to determine anomaly, score larger than the threshold is considered as anomaly. Should be in (0,1)
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.3)]
        public double Threshold { get; set; } = 0.3;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a DetectAnomalyBySrCnnTransform", $"Are you sure you want to add a DetectAnomalyBySrCnnTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.DetectAnomalyBySrCnn(OutputColumn, InputColumn, WindowSize, BackAddWindowSize, LookaheadWindowSize, AveragingWindowSize, JudgementWindowSize, Threshold);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
