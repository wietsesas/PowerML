using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Convert one or more text columns into one-hot encoded vectors.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "OneHotEncodingTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddOneHotEncodingTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be a vector of Single if outputKind is Bag, Indicator, and Binary. If outputKind is Key, this column's data type will be a key in the case of a scalar input column or a vector of keys in the case of a vector input column.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of column to convert to one-hot vectors. If set to null, the value of the outputColumnName will be used as source. This column's data type can be scalar or vector of numeric, text, boolean, DateTime or DateTimeOffset,
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// Output kind: Bag (multi-set vector), Ind (indicator vector), Key (index), or Binary encoded indicator vector.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = OneHotEncodingEstimator.OutputKind.Indicator)]
        public OneHotEncodingEstimator.OutputKind OutputKind { get; set; } = OneHotEncodingEstimator.OutputKind.Indicator;

        /// <summary>
        /// Maximum number of terms to keep per column when auto-training.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1000000)]
        public int MaxKeys { get; set; } = 1000000;

        /// <summary>
        /// How items should be ordered when vectorized. If ByOccurrence choosen they will be in the order encountered. If ByValue, items are sorted according to their default comparison, for example, text sorting will be case sensitive (for example, 'A' then 'Z' then 'a').
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = ValueToKeyMappingEstimator.KeyOrdinality.ByOccurrence)]
        public ValueToKeyMappingEstimator.KeyOrdinality KeyOrdinality { get; set; } = ValueToKeyMappingEstimator.KeyOrdinality.ByOccurrence;

        /// <summary>
        /// Specifies an ordering for the encoding. If specified, this should be a single column data view, and the key-values will be taken from that column. If unspecified, the ordering will be determined from the input data upon fitting.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public IDataView? KeyData { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add an OneHotEncodingTransform", $"Are you sure you want to add an OneHotEncodingTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.Categorical.OneHotEncoding(OutputColumn, InputColumn, OutputKind, MaxKeys, KeyOrdinality, KeyData);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
