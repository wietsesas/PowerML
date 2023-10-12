using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Scale input vectors by their lp-norm, where p is 1, 2 or infinity. Defaults to the l2 (Euclidean distance) norm.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "NormalizeLpNormTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddNormalizeLpNormTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be the same as the input column's data type.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column to normalize. If set to null, the value of the outputColumnName will be used as source. This estimator operates over known-sized vectors of Single.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// Type of norm to use to normalize each sample. The indicated norm of the resulting vector will be normalized to one.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = LpNormNormalizingEstimatorBase.NormFunction.L2)]
        public LpNormNormalizingEstimatorBase.NormFunction Norm { get; set; } = LpNormNormalizingEstimatorBase.NormFunction.L2;

        /// <summary>
        /// If true, subtract mean from each value before normalizing and use the raw input otherwise.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter EnsureZeroMean { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a NormalizeLpNormTransform", $"Are you sure you want to add a NormalizeLpNormTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.NormalizeLpNorm(OutputColumn, InputColumn, Norm, EnsureZeroMean);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
