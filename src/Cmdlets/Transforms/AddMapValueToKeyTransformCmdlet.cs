using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Map values to keys (categories) by creating the mapping from the input data.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "MapValueToKeyTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddMapValueToKeyTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column containing the keys.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column containing the categorical values. If set to null, the value of the outputColumnName is used. The input data types can be numeric, text, boolean, DateTime or DateTimeOffset.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// Maximum number of keys to keep per column when training.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1000000)]
        public int MaxKeys { get; set; } = 1000000;

        /// <summary>
        /// The order in which keys are assigned. If set to ByOccurrence, keys are assigned in the order encountered. If set to ByValue, values are sorted, and keys are assigned based on the sort order.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = ValueToKeyMappingEstimator.KeyOrdinality.ByOccurrence)]
        public ValueToKeyMappingEstimator.KeyOrdinality KeyOrdinality { get; set; } = ValueToKeyMappingEstimator.KeyOrdinality.ByOccurrence;

        /// <summary>
        /// Use a pre-defined mapping between values and keys, instead of building the mapping from the input data during training. If specified, this should be a single column IDataView containing the values. The keys are allocated based on the value of keyOrdinality.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public IDataView? KeyData { get; set; } = null;

        /// <summary>
        /// If set to true, use text type for values, regardless of the actual input type. When doing the reverse mapping, the values are text rather than the original input type.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter AddKeyValueAnnotationsAsText { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a MapValueToKeyTransform", $"Are you sure you want to add a MapValueToKeyTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.Conversion.MapValueToKey(OutputColumn, InputColumn, MaxKeys, KeyOrdinality, AddKeyValueAnnotationsAsText, KeyData);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
