using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Convert one or more text columns into hash-based one-hot encoded vectors.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "OneHotHashEncodingTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddOneHotHashEncodingTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be a vector of Single if outputKind is Bag, Indicator, and Binary. If outputKind is Key, this column's data type will be a key in the case of a scalar input column or a vector of keys in the case of a vector input column.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of column to transform. If set to null, the value of the outputColumnName will be used as source. This column's data type can be scalar or vector of numeric, text, boolean, DateTime or DateTimeOffset.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// The conversion mode.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = OneHotEncodingEstimator.OutputKind.Indicator)]
        public OneHotEncodingEstimator.OutputKind OutputKind { get; set; } = OneHotEncodingEstimator.OutputKind.Indicator;

        /// <summary>
        ///Number of bits to hash into. Must be between 1 and 30, inclusive.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 16)]
        public int Bits { get; set; } = 16;

        /// <summary>
        /// Hashing seed.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 314489979)]
        public uint Seed { get; set; } = 314489979;

        /// <summary>
        /// During hashing we constuct mappings between original values and the produced hash values. Text representation of original values are stored in the slot names of the metadata for the new column.Hashing, as such, can map many initial values to one. maximumNumberOfInverts specifies the upper bound of the number of distinct input values mapping to a hash that should be retained. 0 does not retain any input values. -1 retains all input values mapping to each hash.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public int MaxInverts { get; set; } = 0;

        /// <summary>
        /// Whether the position of each term should be included in the hash.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontUseOrderedHashing { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a OneHotHashEncodingTransform", $"Are you sure you want to add a OneHotHashEncodingTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.Categorical.OneHotHashEncoding(OutputColumn, InputColumn, OutputKind, Bits, Seed, !DontUseOrderedHashing, MaxInverts);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
