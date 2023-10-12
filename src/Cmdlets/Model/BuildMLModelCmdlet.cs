using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <synopsis>
    /// Train a machine learning model (Fit).
    /// </synopsis>
    /// <summary>
    /// Train a machine learning model (Fit).
    /// </summary>
    /// <category order="35">Model</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to train to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.TransformerChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the trained TransformerChain.</outputtype>
    [Cmdlet(VerbsLifecycle.Build, "MLModel", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(TransformerChain<ITransformer>))]
    public sealed class BuildMLModelCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The estimator from which to train the model.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public EstimatorChain<ITransformer>? Pipeline { get; set; } = null;

        /// <summary>
        /// The data to train the model.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public IDataView? Data { get; set; } = null;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (ShouldProcess($"Train a machine learning model", $"Are you sure you want to train a machine learning model?", "PowerML"))
                try { WriteObject(Pipeline!.Fit(Data), false); }
                catch (Exception ex)
                {
                    if (ex.InnerException == null) throw;
                    else throw ex.InnerException;
                }
        }
    }
}
