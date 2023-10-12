using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using Microsoft.ML.Transforms.Onnx;
using System;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Applies a pre-trained deep neural network (DNN) model to transform an input image into a feature vector.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "DnnFeaturizeImageTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddDnnFeaturizeImageTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The name of the column resulting from the transformation of inputColumnName.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of column to transform. If set to null, the value of the outputColumnName will be used as source.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// An extension method on the DnnImageModelSelector that creates a chain of two OnnxScoringEstimator (one for preprocessing and one with a pretrained image DNN) with specific models included in a package together with that extension method.
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        public Func<DnnImageFeaturizerInput, EstimatorChain<ColumnCopyingTransformer>>? ModelFactory { get; set; } = null;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a DnnFeaturizeImageTransform", $"Are you sure you want to add a DnnFeaturizeImageTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.DnnFeaturizeImage(OutputColumn, ModelFactory, InputColumn);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
