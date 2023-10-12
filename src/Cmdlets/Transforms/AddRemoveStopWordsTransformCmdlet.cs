using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Text;
using System;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Remove default (for the specified language) or specified stop words from input columns.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "RemoveStopWordsTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true, DefaultParameterSetName = PSNames.Default)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddRemoveStopWordsTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string Default = nameof(Default);
            public const string Custom = nameof(Custom);
        }

        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be variable-size vector of text.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column to copy the data from. This estimator operates over vector of text.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// Langauge of the input text column inputColumnName.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.Default)]
        [PSDefaultValue(Value = StopWordsRemovingEstimator.Language.English)]
        public StopWordsRemovingEstimator.Language Language { get; set; } = StopWordsRemovingEstimator.Language.English;

        /// <summary>
        /// Array of words to remove.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.Custom)]
        public string[] Words { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a RemoveStopWordsTransform", $"Are you sure you want to add a RemoveStopWordsTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator;
            if (ParameterSetName == PSNames.Default)
                estimator = Context!.Transforms.Text.RemoveDefaultStopWords(OutputColumn, InputColumn, Language);
            else
                estimator = Context!.Transforms.Text.RemoveStopWords(OutputColumn, InputColumn, Words);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
