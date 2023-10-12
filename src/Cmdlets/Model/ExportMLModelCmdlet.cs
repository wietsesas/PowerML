using Microsoft.ML;
using Microsoft.ML.Data;
using PowerML.Exceptions;
using PowerML.Validators;
using System.IO;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <synopsis>
    /// Export a machine learning model.
    /// </synopsis>
    /// <summary>
    /// Export a machine learning model.
    /// </summary>
    /// <category order="35">Model</category>
    /// <outputtype name="Microsoft.ML.Data.TransformerChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the TransformerChain if -PassThru is used.</outputtype>
    [Cmdlet(VerbsData.Export, "MLModel", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(TransformerChain<ITransformer>))]
    public sealed class ExportMLModelCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The model to save to file.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public TransformerChain<ITransformer>? Model { get; set; } = null;

        /// <summary>
        /// The location where to save the model.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidatePath(ValidatePathType.ValidPath)]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// The data schema.
        /// </summary>
        [Parameter(Mandatory = true)]
        public DataViewSchema? Schema { get; set; } = null;

        /// <summary>
        /// Overwrite existing files.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Return the model.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Throw an exception when the file already exists and Force is not used
            if (File.Exists(Path) && !Force)
                throw new FileExistsException(Path);

            // Save the model
            if (ShouldProcess($"Export the model to {System.IO.Path.GetFileName(Path)}", $"Are you sure you want to export the model to {System.IO.Path.GetFileName(Path)}?", "PowerML"))
                Context!.Model.Save(Model!, Schema!, Path);

            // Return the model if requested
            if (PassThru)
                WriteObject(Model, false);
        }
    }
}
