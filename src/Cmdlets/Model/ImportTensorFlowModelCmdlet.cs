using Microsoft.ML;
using Microsoft.ML.Transforms;
using PowerML.Validators;
using System.Management.Automation;

namespace PowerML.Cmdlets.Model
{
    /// <synopsis>
    /// Load TensorFlow model into memory.
    /// </synopsis>
    /// <summary>
    /// Load TensorFlow model into memory. This is the convenience method that allows the model to be loaded once and subsequently use it for querying schema and creation of TensorFlowEstimator.
    /// </summary>
    /// <category order="35">Model</category>
    /// <outputtype name="Microsoft.ML.Data.TransformerChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the imported TensorFlow model.</outputtype>
    [Cmdlet(VerbsData.Import, "TensorFlowModel", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(TensorFlowModel))]
    public sealed class ImportTensorFlowModelCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The location of the TensorFlow model to load.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidatePath(ValidatePathType.FileExists)]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// If the first dimension of the output is unknown, should it be treated as batched or not.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter OutputAsBatched { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (ShouldProcess($"Import the TensorFlow model from {System.IO.Path.GetFileName(Path)}", $"Are you sure you want to import the TensorFlow model from {System.IO.Path.GetFileName(Path)}?", "PowerML"))
                WriteObject(Context!.Model.LoadTensorFlowModel(Path, OutputAsBatched), false);
        }
    }
}
