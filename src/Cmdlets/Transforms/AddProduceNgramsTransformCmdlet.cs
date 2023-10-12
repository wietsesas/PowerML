using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Text;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Transform text column into a bag of counts of ngrams (sequences of consecutive words).
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ProduceNgramsTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddProduceNgramsTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be a vector of Single.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column to transform. If set to null, the value of the outputColumnName will be used as source. This estimator operates over vectors of keys data type.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// Ngram length.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 2)]
        public int NgramLength { get; set; } = 2;

        /// <summary>
        /// Number of tokens to skip between each n-gram. By default no token is skipped.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public int SkipLength { get; set; } = 0;

        /// <summary>
        /// Maximum number of n-grams to store in the dictionary.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 10000000)]
        public int MaxNgrams { get; set; } = 10000000;

        /// <summary>
        /// Statistical measure used to evaluate how important a word or n-gram is to a document in a corpus. When maximumNgramsCount is smaller than the total number of encountered n-grams this measure is used to determine which n-grams to keep.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = NgramExtractingEstimator.WeightingCriteria.Tf)]
        public NgramExtractingEstimator.WeightingCriteria Weighting { get; set; } = NgramExtractingEstimator.WeightingCriteria.Tf;

        /// <summary>
        /// Whether to include all n-gram lengths up to ngramLength or only ngramLength.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontUseAllLengths { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a ProduceNgramsTransform", $"Are you sure you want to add a ProduceNgramsTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.Text.ProduceNgrams(OutputColumn, InputColumn, NgramLength, SkipLength, !DontUseAllLengths, MaxNgrams, Weighting);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
    