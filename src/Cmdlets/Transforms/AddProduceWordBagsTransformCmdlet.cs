using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Text;
using System;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Transform text column into a bag of counts of ngrams vector.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ProduceWordBagsTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true, DefaultParameterSetName = PSNames.Single)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddProduceWordBagsTransformCmdlet : EstimatorChainCmdlet
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
        /// Name of the column resulting from the transformation of inputColumnNames. This column's data type will be known-size vector of Single.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column to take the data from. This estimator operates over vector of text.
        /// </summary>
        [Parameter(Position = 1, ParameterSetName = PSNames.Single)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// Names of the multiple columns to take the data from. This estimator operates over vector of text.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.Multiple)]
        public string[] InputColumns { get; set; } = Array.Empty<string>();

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
        /// Maximum number of n-grams to store in the dictionary.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 10000000)]
        public int MaxNgrams { get; set; } = 10000000;

        /// <summary>
        /// Statistical measure used to evaluate how important a word is to a document in a corpus.
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
            if (!ShouldProcess($"Add a ProduceWordBagsTransform", $"Are you sure you want to add a ProduceWordBagsTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator;
            if (ParameterSetName == PSNames.Single)
                estimator = Context!.Transforms.Text.ProduceWordBags(OutputColumn, InputColumn, NgramLength, SkipLength, !DontUseAllLengths, MaxNgrams, Weighting);
            else
                estimator = Context!.Transforms.Text.ProduceWordBags(OutputColumn, InputColumns, NgramLength, SkipLength, !DontUseAllLengths, MaxNgrams, Weighting);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
