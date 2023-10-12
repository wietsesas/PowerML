using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Transforms a binary classifier raw score into a class probability using logistic regression with fixed parameters or parameters estimated using the training data.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "PlattTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true, DefaultParameterSetName = PSNames.Columns)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddPlattTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string Columns = nameof(Columns);
            public const string Slope = nameof(Slope);
        }

        /// <summary>
        /// The name of the label column.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = PSNames.Columns)]
        [PSDefaultValue(Value = "Label")]
        public string LabelColumn { get; set; } = "Label";

        /// <summary>
        /// The name of the score column.
        /// </summary>
        [Parameter(Position = 1, ParameterSetName = PSNames.Columns)]
        [Parameter(ParameterSetName = PSNames.Slope)]
        [PSDefaultValue(Value = "Score")]
        public string ScoreColumn { get; set; } = "Score";

        /// <summary>
        /// The name of the example weight column (optional).
        /// </summary>
        [Parameter(Position = 2, ParameterSetName = PSNames.Columns)]
        [PSDefaultValue(Value = null)]
        public string? ExampleWeightColumn { get; set; } = null;

        /// <summary>
        /// The slope in the function of the exponent of the sigmoid.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.Slope)]
        public double Slope { get; set; }

        /// <summary>
        /// The offset in the function of the exponent of the sigmoid.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.Slope)]
        public double Offset { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a PlattTransform", $"Are you sure you want to add a PlattTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator;
            if (ParameterSetName == PSNames.Columns)
                estimator = Context!.BinaryClassification.Calibrators.Platt(LabelColumn, ScoreColumn, ExampleWeightColumn);
            else
                estimator = Context!.BinaryClassification.Calibrators.Platt(Slope, Offset, ScoreColumn);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
