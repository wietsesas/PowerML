using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Convert keys back to vectors of original values.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "MapKeyToVectorTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddMapKeyToVectorTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. The data type is a vector of Single representing the input value.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column to transform. If set to null, the value of the outputColumnName will be used as source. This transform operates over keys.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// Whether to combine multiple indicator vectors into a single vector of counts instead of concatenating them. This is only relevant when the input column is a vector of keys.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter OutputCountVector { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a MapKeyToVectorTransform", $"Are you sure you want to add a MapKeyToVectorTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.Conversion.MapKeyToVector(OutputColumn, InputColumn, OutputCountVector);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
