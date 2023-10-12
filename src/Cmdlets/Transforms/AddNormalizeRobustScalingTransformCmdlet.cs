using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Scale each value using statistics that are robust to outliers that will center the data around 0 and scales the data according to the quantile range.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "NormalizeRobustScalingTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddNormalizeRobustScalingTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. The data type on this column is the same as the input column.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column to transform. If set to null, the value of the outputColumnName will be used as source. The data type on this column should be Single, Double or a known-sized vector of those types.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// Maximum number of examples used to train the normalizer.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1000000000)]
        public long MaxExamples { get; set; } = 1000000000;

        /// <summary>
        /// Quantile min used to scale the data.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 25)]
        public uint QuantileMin { get; set; } = 25;

        /// <summary>
        /// Quantile max used to scale the data.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 75)]
        public uint QuantileMax { get; set; } = 75;

        /// <summary>
        /// Whether to center the data around 0 by removing the median.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontCenterData { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a NormalizeRobustScalingTransform", $"Are you sure you want to add a NormalizeRobustScalingTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.NormalizeRobustScaling(OutputColumn, InputColumn, MaxExamples, !DontCenterData, QuantileMin, QuantileMax);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
