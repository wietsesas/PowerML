using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Image;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Resize images.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ResizeImagesTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddResizeImagesTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be the same as that of the input column.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column with images. This estimator operates over MLImage.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// The transformed image height.
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        public int Height { get; set; }

        /// <summary>
        /// The transformed image width.
        /// </summary>
        [Parameter(Mandatory = true, Position = 3)]
        public int Width { get; set; }

        /// <summary>
        /// The type of image resizing as specified in ImageResizingEstimator.ResizingKind.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = ImageResizingEstimator.ResizingKind.IsoCrop)]
        public ImageResizingEstimator.ResizingKind ResizeKind { get; set; } = ImageResizingEstimator.ResizingKind.IsoCrop;

        /// <summary>
        /// Where to place the anchor, to start cropping. Options defined in ImageResizingEstimator.Anchor
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = ImageResizingEstimator.Anchor.Center)]
        public ImageResizingEstimator.Anchor CropAnchor { get; set; } = ImageResizingEstimator.Anchor.Center;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a ResizeImagesTransform", $"Are you sure you want to add a ResizeImagesTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.ResizeImages(OutputColumn, Width, Height, InputColumn, ResizeKind, CropAnchor);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
