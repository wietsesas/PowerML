using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Transform a document (represented as a vector of floats) into a vector of floats over a set of topics.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "LatentDirichletAllocationTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddLatentDirichletAllocationTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This estimator outputs a vector of Single.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column to transform. If set to null, the value of the outputColumnName will be used as source. This estimator operates over a vector of Single.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// The number of topics.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 100)]
        public int Topics { get; set; } = 100;

        /// <summary>
        /// Dirichlet prior on document-topic vectors.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 100f)]
        public float AlphaSum { get; set; } = 100f;

        /// <summary>
        /// Dirichlet prior on vocab-topic vectors.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.01f)]
        public float Beta { get; set; } = 0.01f;

        /// <summary>
        /// Number of Metropolis Hasting step.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 4)]
        public int SamplingSteps { get; set; } = 4;

        /// <summary>
        /// Number of iterations.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 200)]
        public int MaxIterations { get; set; } = 200;

        /// <summary>
        /// Compute log likelihood over local dataset on this iteration interval.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 5)]
        public int LikelihoodInterval { get; set; } = 5;

        /// <summary>
        /// The number of training threads. Default value depends on number of logical processors.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public int Threads { get; set; } = 0;

        /// <summary>
        /// The threshold of maximum count of tokens per doc.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 512)]
        public int MaxTokensPerDocument { get; set; } = 512;

        /// <summary>
        /// The number of words to summarize the topic.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 10)]
        public int SummaryTermsPerTopic { get; set; } = 10;

        /// <summary>
        /// The number of burn-in iterations.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 10)]
        public int BurninIterations { get; set; } = 10;

        /// <summary>
        /// Reset the random number generator for each document.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter ResetRandomGenerator { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a LatentDirichletAllocationTransform", $"Are you sure you want to add a LatentDirichletAllocationTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.Text.LatentDirichletAllocation(OutputColumn, InputColumn, Topics, AlphaSum, Beta, SamplingSteps, MaxIterations, LikelihoodInterval, Threads, MaxTokensPerDocument, SummaryTermsPerTopic, BurninIterations, ResetRandomGenerator);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
