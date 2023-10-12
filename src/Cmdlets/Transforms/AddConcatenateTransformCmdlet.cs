using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Concatenate one or more input columns into a new output column.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ConcatenateTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddConcatenateTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnNames. This column's data type will be a vector of the input columns' data type.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the columns to concatenate. This estimator operates over any data type except key type. If more that one column is provided, they must all have the same data type.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string[] InputColumns { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a ConcatenateTransform", $"Are you sure you want to add a ConcatenateTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.Concatenate(OutputColumn, InputColumns);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
