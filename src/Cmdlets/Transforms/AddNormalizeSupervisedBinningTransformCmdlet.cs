using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Assign the input value to a bin based on its correlation with label column.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "NormalizeSupervisedBinningTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddNormalizeSupervisedBinningTransformCmdlet : EstimatorChainCmdlet
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
        /// Name of the label column for supervised binning.
        /// </summary>
        [Parameter(Position = 2)]
        [PSDefaultValue(Value = "Label")]
        public string LabelColumn { get; set; } = "Label";

        /// <summary>
        /// Maximum number of examples used to train the normalizer.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1000000000)]
        public long MaxExamples { get; set; } = 1000000000;

        /// <summary>
        /// Maximum number of bins (power of 2 recommended).
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1024)]
        public int MaxBins { get; set; } = 1024;

        /// <summary>
        /// Minimum number of examples per bin.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 10)]
        public int MinExamplesPerBin { get; set; } = 10;

        /// <summary>
        /// Whether to map zero to zero, preserving sparsity.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontFixZero { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a NormalizeSupervisedBinningTransform", $"Are you sure you want to add a NormalizeSupervisedBinningTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.NormalizeSupervisedBinning(OutputColumn, InputColumn, LabelColumn, MaxExamples, !DontFixZero, MaxBins, MinExamplesPerBin);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
