using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Append a caching checkpoint to an estimator chain.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "CacheCheckpoint", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddCacheCheckpointCmdlet : ContextCmdlet
    {
        /// <summary>
        /// Append the created estimator to the end of this chain.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public EstimatorChain<ITransformer>? AppendTo { get; set; } = null;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>s
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a caching checkpoint to the estimator chain", $"Are you sure you want to add a caching checkpoint to the estimator chain?", "PowerML"))
                return;
            
            // Return the estimator with a cache checkpoint
            WriteObject(AppendTo!.AppendCacheCheckpoint(Context), false);
        }
    }
}
