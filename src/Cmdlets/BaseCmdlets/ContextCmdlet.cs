using Microsoft.ML;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// The base class for cmdlets who require a MLContext.
    /// </summary>
    public abstract class ContextCmdlet : PSCmdlet
    {
        /// <summary>
        /// The context on which to perform the action. If omitted, the current (cached) context will be used.
        /// </summary>
        [Parameter()]
        [ValidateNotNull()]
        [PSDefaultValue(Value = "Current context")]
        public MLContext? Context { get; set; } = null;

        /// <summary>
        /// Runs before objects are passed through the pipeline.
        /// </summary>
        protected override void BeginProcessing()
        {
            // Set the context to the current context if not provided
            if (!MyInvocation.BoundParameters.ContainsKey(nameof(Context)))
                Context = CurrentContext.GetOrCreate();
        }
    }
}
