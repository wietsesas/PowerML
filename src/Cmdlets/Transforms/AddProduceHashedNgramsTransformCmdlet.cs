using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Transform text column into a vector of hashed ngram counts.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ProduceHashedNgramsTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true, DefaultParameterSetName = PSNames.Single)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddProduceHashedNgramsTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string Single = nameof(Single);
            public const string Multiple = nameof(Multiple);
        }

        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be vector of Single.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be vector of Single.
        /// </summary>
        [Parameter(Position = 1, ParameterSetName = PSNames.Single)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// Name of the multiple columns to take the data from. This estimator operates over vector of key type.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.Multiple)]
        public string[] InputColumns { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Number of bits to hash into. Must be between 1 and 30, inclusive.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 16)]
        public int Bits { get; set; } = 16;

        /// <summary>
        /// Ngram length.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 2)]
        public int NgramLength { get; set; } = 2;

        /// <summary>
        /// Maximum number of tokens to skip when constructing an n-gram.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public int SkipLength { get; set; } = 0;

        /// <summary>
        /// Hashing seed.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 314489979)]
        public uint Seed { get; set; } = 314489979;

        /// <summary>
        /// During hashing we construct mappings between original values and the produced hash values. Text representation of original values are stored in the slot names of the annotations for the new column.Hashing, as such, can map many initial values to one. maximumNumberOfInverts specifies the upper bound of the number of distinct input values mapping to a hash that should be retained. 0 does not retain any input values. -1 retains all input values mapping to each hash.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public int MaxInverts { get; set; } = 0;

        /// <summary>
        /// Whether to include all n-gram lengths up to ngramLength or only ngramLength.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontUseAllLengths { get; set; }

        /// <summary>
        /// Whether the position of each source column should be included in the hash (when there are multiple source columns).
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontUseOrderedHashing { get; set; }

        /// <summary>
        /// Whether to rehash unigrams.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter RehashUnigrams { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a ProduceHashedNgramsTransform", $"Are you sure you want to add a ProduceHashedNgramsTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator;
            if (ParameterSetName == PSNames.Single)
                estimator = Context!.Transforms.Text.ProduceHashedNgrams(OutputColumn, InputColumn, Bits, NgramLength, SkipLength, !DontUseAllLengths, Seed, !DontUseOrderedHashing, MaxInverts, RehashUnigrams);
            else
                estimator = Context!.Transforms.Text.ProduceHashedNgrams(OutputColumn, InputColumns, Bits, NgramLength, SkipLength, !DontUseAllLengths, Seed, !DontUseOrderedHashing, MaxInverts, RehashUnigrams);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
