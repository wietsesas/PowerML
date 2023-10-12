using Microsoft.ML.Data;
using Microsoft.ML;
using System.Management.Automation;
using PowerML.Validators;
using Microsoft.ML.Transforms;
using System;

namespace PowerML.Cmdlets.Transforms
{
    /// <synopsis>
    /// Scores a dataset using a pre-trained TensorFlow model.
    /// </synopsis>
    /// <summary>
    /// Scores a dataset using a pre-trained TensorFlow model.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ScoreTensorFlowModelTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true, DefaultParameterSetName = PSNames.FromPath)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public class AddScoreTensorFlowModelTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string FromPath = nameof(FromPath);
            public const string FromModel = nameof(FromModel);
        }

        /// <summary>
        /// The name of the requested model output. The data type is a vector of Single.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string[] OutputColumn { get; set; } = Array.Empty<string>();

        /// <summary>
        /// The name of the model input. The data type is a vector of Single.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string[] InputColumn { get; set; } = Array.Empty<string>();

        /// <summary>
        /// The location of the TensorFlow model to load.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.FromPath)]
        [ValidatePath(ValidatePathType.FileExists)]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// The TensorFlow model.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.FromModel)]
        public TensorFlowModel? Model { get; set; } = null;

        /// <summary>
        /// Add a batch dimension to the input e.g. input = [224, 224, 3] => [-1, 224, 224, 3]. This parameter is used to deal with models that have unknown shape but the internal operators in the model require data to have batch dimension as well.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter AddBatchDimension { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a ScoreTensorFlowModelTransform", $"Are you sure you want to add a ScoreTensorFlowModelTransform?", "PowerML"))
                return;

            // Set the model if a path was given
            if (ParameterSetName == PSNames.FromPath)
                Model = Context!.Model.LoadTensorFlowModel(Path);

            // Create the estimator
            IEstimator<ITransformer> estimator;
            if (OutputColumn.Length == 1 && InputColumn.Length == 1)
                estimator = Model!.ScoreTensorFlowModel(OutputColumn[0], InputColumn[0], AddBatchDimension);
            else
                estimator = Model!.ScoreTensorFlowModel(OutputColumn, InputColumn, AddBatchDimension);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
