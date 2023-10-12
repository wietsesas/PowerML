using System.Management.Automation;

namespace PowerML.Cmdlets.Types
{
    /// <synopsis>
    /// Get a registered type.
    /// </synopsis>
    /// <summary>
    /// Get a registered type.
    /// </summary>
    /// <category order="20">Custom types</category>
    /// <inputtype name="System.String">You can pipe the name of the type to fetch to this cmdlet.</inputtype>
    /// <outputtype name="System.Type">This cmdlet returns the requested type(s).</outputtype>
    [Cmdlet(VerbsCommon.Get, "MLType", ConfirmImpact = ConfirmImpact.Low)]
    [OutputType(typeof(System.Type))]
    public sealed class GetMLTypeCmdlet : PSCmdlet
    {
        /// <summary>
        /// The type to fetch from the registered types. Leave empty to get all types.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [ArgumentCompleter(typeof(RegisteredTypes.Completer))]
        [ValidateNotNullOrEmpty()]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (MyInvocation.BoundParameters.ContainsKey(nameof(Type)))
                WriteObject(RegisteredTypes.Get(Type), false);
            else
                WriteObject(RegisteredTypes.GetAll(), true);
        }
    }
}
