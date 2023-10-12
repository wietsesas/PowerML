using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Image;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Convert a vector of pixels to ImageDataViewType.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ConvertToImageTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddConvertToImageTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName. This column's data type will be MLImage.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of the column with data to be converted to image. This estimator operates over known-sized vector of Single, Double and Byte.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// The height of the output images.
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        public int Height { get; set; }

        /// <summary>
        /// The width of the output images.
        /// </summary>
        [Parameter(Mandatory = true, Position = 3)]
        public int Width { get; set; }

        /// <summary>
        /// Specifies which ImagePixelExtractingEstimator.ColorBits are in present the input pixel vectors. The order of colors is specified in orderOfColors.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = ImagePixelExtractingEstimator.ColorBits.Rgb)]
        public ImagePixelExtractingEstimator.ColorBits Colors { get; set; } = ImagePixelExtractingEstimator.ColorBits.Rgb;

        /// <summary>
        /// The order in which colors are presented in the input vector.
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
        /// The values are scaled by this value before being converted to pixels. Applied to vector value before offsetImage.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1.0f)]
        public float Scale { get; set; } = 1.0f;

        /// <summary>
        /// The offset is subtracted before converting the values to pixels. Applied to vector value after scaleImage.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.0f)]
        public float Offset { get; set; } = 0.0f;

        /// <summary>
        /// Default value for alpha color, would be overridden if colorsPresent contains Alpha.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 255)]
        public int DefaultAlpha { get; set; } = 255;

        /// <summary>
        /// Default value for red color, would be overridden if colorsPresent contains Red.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public int DefaultRed { get; set; } = 0;

        /// <summary>
        /// Default value for green color, would be overridden if colorsPresent contains Green.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public int DefaultGreen { get; set; } = 0;

        /// <summary>
        /// Default value for blue color, would be overridden if colorsPresent contains Blue.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public int DefaultBlue { get; set; } = 0;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a ConvertToImageTransform", $"Are you sure you want to add a ConvertToImageTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.ConvertToImage(Height, Width, OutputColumn, InputColumn, Colors, ColorsOrder, InterleaveColors, Scale, Offset, DefaultAlpha, DefaultRed, DefaultGreen, DefaultBlue);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
