using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Hash the value in the input column.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "HashTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddHashTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be a vector of keys, or a scalar of key based on whether the input column data types are vectors or scalars.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column whose data will be hashed. If set to null, the value of the outputColumnName will be used as source. This estimator operates over vectors or scalars of text, numeric, boolean, key or DataViewRowId data types.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// Number of bits to hash into. Must be between 1 and 31, inclusive.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 31)]
        public int Bits { get; set; } = 31;

        /// <summary>
        /// During hashing we construct mappings between original values and the produced hash values. Text representation of original values are stored in the slot names of the annotations for the new column.Hashing, as such, can map many initial values to one. maximumNumberOfInvertsSpecifies the upper bound of the number of distinct input values mapping to a hash that should be retained. 0 does not retain any input values. -1 retains all input values mapping to each hash.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public int MaxInverts { get; set; } = 0;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a HashTransform", $"Are you sure you want to add a HashTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.Conversion.Hash(OutputColumn, InputColumn, Bits, MaxInverts);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
