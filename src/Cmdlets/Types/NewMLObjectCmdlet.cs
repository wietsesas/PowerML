using System.Collections;
using System.Management.Automation;

namespace PowerML.Cmdlets.Data
{
    /// <synopsis>
    /// Create a new object of the specified registered type with the specified properties.
    /// </synopsis>
    /// <summary>
    /// Create a new object of the specified registered type with the specified properties.
    /// </summary>
    /// <category order="20">Custom types</category>
    /// <inputtype name="System.Collections.IDictionary">You can pipe a dictionary with the properties for the new object to this cmdlet.</inputtype>
    /// <outputtype name="System.Object">This cmdlet returns the newly created object.</outputtype>
    [Cmdlet(VerbsCommon.New, "MLObject", ConfirmImpact = ConfirmImpact.Low)]
    [OutputType(typeof(object))]
    public sealed class NewMLObjectCmdlet : PSCmdlet
    {
        /// <summary>
        /// Create an object of this registered type.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ArgumentCompleter(typeof(RegisteredTypes.Completer))]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The properties to assign to the object.
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public IDictionary? Properties { get; set; } = null;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            WriteObject(RegisteredTypes.CreateObject(Type, Properties), false);
        }
    }
}
