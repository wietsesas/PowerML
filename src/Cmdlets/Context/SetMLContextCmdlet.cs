using Microsoft.ML;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <synopsis>
    /// Set the current (cached) MLContext.
    /// </synopsis>
    /// <summary>
    /// Set the current (cached) MLContext.
    /// </summary>
    /// <category order="10">MLContext</category>
    /// <inputtype name="Microsoft.ML.MLContext">You can pipe the MLContext to set to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.MLContext">This cmdlet returns the current (cached) MLContext if -PassThru is used.</outputtype>
    [Cmdlet(VerbsCommon.Set, "MLContext", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(MLContext))]
    public sealed class SetMLContextCmdlet : PSCmdlet
    {
        /// <summary>
        /// The context to set as current (cached) MLContext.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [AllowNull()]
        public MLContext? Context { get; set; } = null;

        /// <summary>
        /// Return the context.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Set the current context
            if (ShouldProcess($"Update the current context", $"Are you sure you want to update the current context?", "PowerML"))
                CurrentContext.Set(Context);

            // Return the context if requested
            if (PassThru && Context != null)
                WriteObject(Context, false);
        }
    }
}
