using Microsoft.ML;
using Microsoft.ML.Data;
using PowerML.Validators;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Transform the input data with an imported ONNX model.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ApplyOnnxModelTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true, DefaultParameterSetName = PSNames.None)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddApplyOnnxModelTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string None = nameof(None);
            public const string Single = nameof(Single);
            public const string Multiple = nameof(Multiple);
        }

        /// <summary>
        /// The output column resulting from the transformation.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PSNames.Single)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// The output columns resulting from the transformation.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.Multiple)]
        public string[] OutputColumns { get; set; } = Array.Empty<string>();

        /// <summary>
        /// The input column.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PSNames.Single)]
        public string InputColumn { get; set; } = string.Empty;

        /// <summary>
        /// The input columns.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.Multiple)]
        public string[] InputColumns { get; set; } = Array.Empty<string>();

        /// <summary>
        /// The path of the file containing the ONNX model.
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        [ValidatePath(ValidatePathType.FileExists)]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// ONNX shapes to be used over those loaded from modelFile. For keys use names as stated in the ONNX model, e.g. "input". Stating the shapes with this parameter is particularly useful for working with variable dimension inputs and outputs.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public IDictionary<string, int[]>? ShapeDictionary { get; set; } = null;

        /// <summary>
        /// Optional GPU device ID to run execution on, null to run on CPU.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public int? GpuDeviceId { get; set; } = null;

        /// <summary>
        /// If GPU error, raise exception or fallback to CPU.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter FallbackToCpu { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add an ApplyOnnxModelTransform", $"Are you sure you want to add an ApplyOnnxModelTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator;
            if (ParameterSetName == PSNames.None && ShapeDictionary != null)
                estimator = Context!.Transforms.ApplyOnnxModel(Path, ShapeDictionary, GpuDeviceId, FallbackToCpu);
            else if (ParameterSetName == PSNames.None)
                estimator = Context!.Transforms.ApplyOnnxModel(Path, GpuDeviceId, FallbackToCpu);
            else if (ParameterSetName == PSNames.Single && ShapeDictionary != null)
                estimator = Context!.Transforms.ApplyOnnxModel(OutputColumn, InputColumn, Path, ShapeDictionary, GpuDeviceId, FallbackToCpu);
            else if (ParameterSetName == PSNames.Single)
                estimator = Context!.Transforms.ApplyOnnxModel(OutputColumn, InputColumn, Path, GpuDeviceId, FallbackToCpu);
            else if (ParameterSetName == PSNames.Multiple && ShapeDictionary != null)
                estimator = Context!.Transforms.ApplyOnnxModel(OutputColumns, InputColumns, Path, ShapeDictionary, GpuDeviceId, FallbackToCpu);
            else
                estimator = Context!.Transforms.ApplyOnnxModel(OutputColumns, InputColumns, Path, GpuDeviceId, FallbackToCpu);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
