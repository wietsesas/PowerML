using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Reduce the dimensions of the input feature vector by applying the Principal Component Analysis algorithm.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ProjectToPrincipalComponentsTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddProjectToPrincipalComponentsTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of column to transform. If set to null, the value of the outputColumnName will be used as source.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// The name of the example weight column (optional).
        /// </summary>
        [Parameter(Position = 2)]
        [PSDefaultValue(Value = null)]
        public string? ExampleWeightColumn { get; set; } = null;

        /// <summary>
        /// The number of principal components.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 20)]
        public int Rank { get; set; } = 20;

        /// <summary>
        /// Oversampling parameter for randomized PrincipalComponentAnalysis training.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 20)]
        public int OverSampling { get; set; } = 20;

        /// <summary>
        /// The seed for random number generation.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public int? Seed { get; set; } = null;

        /// <summary>
        /// Disable center data to be zero mean.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontEnsureZeroMean { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add an ProjectToPrincipalComponentsTransform", $"Are you sure you want to add an ProjectToPrincipalComponentsTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.ProjectToPrincipalComponents(OutputColumn, InputColumn, ExampleWeightColumn, Rank, OverSampling, !DontEnsureZeroMean, Seed);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
