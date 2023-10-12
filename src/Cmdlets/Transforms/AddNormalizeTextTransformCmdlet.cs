using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Text;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Change case, remove diacritical marks, punctuation marks, and numbers.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "NormalizeTextTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddNormalizeTextTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type is a scalar or a vector of text depending on the input column data type.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column to transform. If set to null, the value of the outputColumnName will be used as source. This estimator operates on text or vector of text data types.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// Casing text using the rules of the invariant culture.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = TextNormalizingEstimator.CaseMode.Lower)]
        public TextNormalizingEstimator.CaseMode CaseMode { get; set; } = TextNormalizingEstimator.CaseMode.Lower;

        /// <summary>
        /// Whether to keep diacritical marks or remove them.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter KeepDiacritics { get; set; }

        /// <summary>
        /// Whether to keep punctuation marks or remove them.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter RemovePunctuations { get; set; }

        /// <summary>
        /// Whether to keep numbers or remove them.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter RemoveNumbers { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a NormalizeTextTransform", $"Are you sure you want to add a NormalizeTextTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.Text.NormalizeText(OutputColumn, InputColumn, CaseMode, KeepDiacritics, !RemovePunctuations, !RemoveNumbers);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
