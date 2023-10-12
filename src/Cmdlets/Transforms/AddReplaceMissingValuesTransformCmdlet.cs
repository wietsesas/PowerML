using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Create a new output column, the value of which is set to a default value if the value is missing from the input column, and the input value otherwise.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ReplaceMissingValuesTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddReplaceMissingValuesTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be the same as that of the input column.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column to copy the data from. This estimator operates over scalar or vector of Single or Double.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// The type of replacement to use as specified in MissingValueReplacingEstimator.ReplacementMode
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = MissingValueReplacingEstimator.ReplacementMode.DefaultValue)]
        public MissingValueReplacingEstimator.ReplacementMode ReplacementMode { get; set; } = MissingValueReplacingEstimator.ReplacementMode.DefaultValue;

        /// <summary>
        /// Per-slot imputation of replacement is performed. Otherwise, replacement value is imputed for the entire vector column. This setting is ignored for scalars and variable vectors, where imputation is always for the entire column.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontImputeBySlot { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add an ReplaceMissingValuesTransform", $"Are you sure you want to add an ReplaceMissingValuesTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.ReplaceMissingValues(OutputColumn, InputColumn, ReplacementMode, !DontImputeBySlot);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
