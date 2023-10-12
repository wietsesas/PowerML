using Microsoft.ML;
using Microsoft.ML.Data;
using PowerML.Validators;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <synopsis>
    /// Import a machine learning model.
    /// </synopsis>
    /// <summary>
    /// Import a machine learning model.
    /// </summary>
    /// <category order="35">Model</category>
    /// <outputtype name="Microsoft.ML.Data.TransformerChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the imported TransformerChain.</outputtype>
    [Cmdlet(VerbsData.Import, "MLModel", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(TransformerChain<ITransformer>))]
    public sealed class ImportMLModelCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The location of the model to load.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidatePath(ValidatePathType.FileExists)]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (ShouldProcess($"Import the model from {System.IO.Path.GetFileName(Path)}", $"Are you sure you want to import the model from {System.IO.Path.GetFileName(Path)}?", "PowerML"))
                WriteObject(Context!.Model.Load(Path, out _), false);
        }
    }
}
