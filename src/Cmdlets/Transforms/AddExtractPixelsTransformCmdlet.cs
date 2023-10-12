using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Image;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Convert pixels from input image into a vector of numbers.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ExtractPixelsTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddExtractPixelsTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be a known-sized vector of Single or Byte depending on outputAsFloatArray.
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
        /// The colors to extract from the image.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = ImagePixelExtractingEstimator.ColorBits.Rgb)]
        public ImagePixelExtractingEstimator.ColorBits Colors { get; set; } = ImagePixelExtractingEstimator.ColorBits.Rgb;

        /// <summary>
        /// The order in which to extract colors from pixel.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = ImagePixelExtractingEstimator.ColorsOrder.ARGB)]
        public ImagePixelExtractingEstimator.ColorsOrder ColorsOrder { get; set; } = ImagePixelExtractingEstimator.ColorsOrder.ARGB;

        /// <summary>
        /// Whether the pixels are interleaved, meaning whether they are in ColorsOrder order, or separated in the planar form: all the values for one color for all pixels, then all the values for another color and so on.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter InterleaveColors { get; set; }

        /// <summary>
        /// Scale each pixel's color value by this amount. Applied to color value after offsetImage.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1.0f)]
        public float Scale { get; set; } = 1.0f;

        /// <summary>
        /// Offset each pixel's color value by this amount. Applied to color value before scaleImage.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.0f)]
        public float Offset { get; set; } = 0.0f;

        /// <summary>
        /// Output array as byte array. Output as byte array ignores offsetImage and scaleImage.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter AsByteArray { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a ExtractPixelsTransform", $"Are you sure you want to add a ExtractPixelsTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.ExtractPixels(OutputColumn, InputColumn, Colors, ColorsOrder, InterleaveColors, Offset, Scale, !AsByteArray);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
