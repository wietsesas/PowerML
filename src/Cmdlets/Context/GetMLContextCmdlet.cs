using Microsoft.ML;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <synopsis>
    /// Get the current (cached) MLContext.
    /// </synopsis>
    /// <summary>
    /// Get the current (cached) MLContext.
    /// </summary>
    /// <category order="10">MLContext</category>
    /// <outputtype name="Microsoft.ML.MLContext">This cmdlet returns the current (cached) MLContext.</outputtype>
    [Cmdlet(VerbsCommon.Get, "MLContext", ConfirmImpact = ConfirmImpact.Low)]
    [OutputType(typeof(MLContext))]
    public sealed class GetMLContextCmdlet : PSCmdlet
    {
        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Return the current context
            var context = CurrentContext.Get();
            if (context != null)
                WriteObject(context, false);
        }
    }
}
