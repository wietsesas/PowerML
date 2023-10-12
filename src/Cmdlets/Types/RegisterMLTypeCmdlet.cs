using PowerML.Validators;
using System;
using System.Collections;
using System.Management.Automation;
using System.Text;

namespace PowerML.Cmdlets
{
    /// <synopsis>
    /// Register a custom type from a type definition.
    /// </synopsis>
    /// <summary>
    /// Register a custom type from a type definition.
    /// </summary>
    /// <category order="20">Custom types</category>
    /// <inputtype name="System.Collections.IDictionary">You can pipe a dictionary with the type definition to this cmdlet.</inputtype>
    /// <outputtype name="System.Type">This cmdlet returns the registered type if -PassThru is used.</outputtype>
    [Cmdlet(VerbsLifecycle.Register, "MLType", ConfirmImpact = ConfirmImpact.Low, DefaultParameterSetName = PSNames.FromFile)]
    [OutputType(typeof(Type))]
    public sealed class RegisterMLTypeCmdlet : PSCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string FromFile = nameof(FromFile);
            public const string FromHashtable = nameof(FromHashtable);
        }

        /// <summary>
        /// The path of the type definition file.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PSNames.FromFile)]
        [ValidatePath(ValidatePathType.FileExists)]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// The type definition from which to create the type.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = PSNames.FromHashtable)]
        public IDictionary? Definition { get; set; } = null;

        /// <summary>
        /// The encoding of the type definition file.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.FromFile)]
        [PSDefaultValue(Value = "UTF8")]
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// Return the registered type.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Create the type
            Type type;
            if (ParameterSetName == PSNames.FromFile)
                type = RegisteredTypes.FromJsonFile(Path, Encoding, true);
            else
                type = RegisteredTypes.FromDictionary(Definition!, true);

            // Return the type if requested
            if (PassThru)
                WriteObject(type, false);
        }


    }
}
