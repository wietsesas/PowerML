using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Map values to keys (categories) based on the supplied dictionary of mappings.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "MapValueTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddMapValueTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. The data types can be primitives or vectors of numeric, text, boolean, DateTime, DateTimeOffset or DataViewRowId types.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column to transform. If set to null, the value of the outputColumnName will be used as source. The data types can be primitives or vectors of numeric, text, boolean, DateTime, DateTimeOffset or DataViewRowId types.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// An instance of IDataView that contains the keyColumn and valueColumn columns.
        /// </summary>
        [Parameter(Mandatory = true)]
        public IDataView? LookupMap { get; set; } = null;

        /// <summary>
        /// The key column in lookupMap.
        /// </summary>
        [Parameter(Mandatory = true)]
        public DataViewSchema.Column KeyColumn { get; set; }

        /// <summary>
        /// The value column in lookupMap.
        /// </summary>
        [Parameter(Mandatory = true)]
        public DataViewSchema.Column ValueColumn { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a MapValueTransform", $"Are you sure you want to add a MapValueTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.Conversion.MapValue(OutputColumn, LookupMap, KeyColumn, ValueColumn, InputColumn);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
