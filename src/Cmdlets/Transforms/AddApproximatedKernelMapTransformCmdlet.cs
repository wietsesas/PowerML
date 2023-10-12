using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Map each input vector onto a lower dimensional feature space, where inner products approximate a kernel function, so that the features can be used as inputs to the linear algorithms.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ApproximatedKernelMapTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddApproximatedKernelMapTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. The data type on this column will be a known-sized vector of Single.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of column to transform. If set to null, the value of the outputColumnName will be used as source. This estimator operates on known-sized vector of Single data type.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// The dimension of the feature space to map the input to.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1000)]
        public int Rank { get; set; } = 1000;

        /// <summary>
        /// If true, use both of cos and sin basis functions to create two features for every random Fourier frequency. Otherwise, only cos bases would be used. Note that if set to true, the dimension of the output feature space will be 2*rank.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter UseCosAndSinBases { get; set; }

        /// <summary>
        /// The argument that indicates which kernel to use. The two available implementations are GaussianKernel and LaplacianKernel.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public KernelBase? Generator { get; set; } = null;

        /// <summary>
        /// The seed of the random number generator for generating the new features (if unspecified, the global random is used).
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
            if (!ShouldProcess($"Add a ApproximatedKernelMapTransform", $"Are you sure you want to add a ApproximatedKernelMapTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.ApproximatedKernelMap(OutputColumn, InputColumn, Rank, UseCosAndSinBases, Generator, Seed);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
