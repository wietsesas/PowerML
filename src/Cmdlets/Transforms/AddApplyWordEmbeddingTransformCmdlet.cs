using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Text;
using PowerML.Validators;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Convert vectors of text tokens into sentence vectors using a pre-trained model.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ApplyWordEmbeddingTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true, DefaultParameterSetName = PSNames.PretrainedModel)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddApplyWordEmbeddingTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string PretrainedModel = nameof(PretrainedModel);
            public const string CustomModel = nameof(CustomModel);
        }

        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be a vector of Single.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PSNames.PretrainedModel)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PSNames.CustomModel)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column to transform. If set to null, the value of the outputColumnName will be used as source. This estimator operates over known-size vector of text data type.
        /// </summary>
        [Parameter(Position = 1, ParameterSetName = PSNames.PretrainedModel)]
        [Parameter(Position = 1, ParameterSetName = PSNames.CustomModel)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// The embeddings PretrainedModelKind to use.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.PretrainedModel)]
        [PSDefaultValue(Value = WordEmbeddingEstimator.PretrainedModelKind.SentimentSpecificWordEmbedding)]
        public WordEmbeddingEstimator.PretrainedModelKind ModelKind { get; set; } = WordEmbeddingEstimator.PretrainedModelKind.SentimentSpecificWordEmbedding;

        /// <summary>
        /// The path of the pre-trained embeddings model to use.
        /// </summary>
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = PSNames.CustomModel)]
        [ValidatePath(ValidatePathType.FileExists)]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add an ApplyWordEmbeddingTransform", $"Are you sure you want to add an ApplyWordEmbeddingTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator;
            if (ParameterSetName == PSNames.PretrainedModel)
                estimator = Context!.Transforms.Text.ApplyWordEmbedding(OutputColumn, InputColumn, ModelKind);
            else
                estimator = Context!.Transforms.Text.ApplyWordEmbedding(OutputColumn, Path, InputColumn);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
