using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Train logistic regression using a parallel stochastic gradient method. The trained model is calibrated and can produce probability by feeding the output value of the linear function to a PlattCalibrator.
    /// </summary>
    /// <category order="50">Trainers</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "SgdCalibratedBinaryTrainer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddSgdCalibratedBinaryTrainerCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The name of the label column, or dependent variable. The column data must be Boolean.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Label")]
        public string LabelColumn { get; set; } = "Label";

        /// <summary>
        /// The name of the feature column. The column data must be a known-sized vector of Single.
        /// </summary>
        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Features")]
        public string FeatureColumn { get; set; } = "Features";

        /// <summary>
        /// The name of the example weight column (optional).
        /// </summary>
        [Parameter(Position = 2)]
        [PSDefaultValue(Value = null)]
        public string? ExampleWeightColumn { get; set; } = null;

        /// <summary>
        /// The maximum number of passes through the training dataset; set to 1 to simulate online learning.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 20)]
        public int Iterations { get; set; } = 20;

        /// <summary>
        /// The initial learning rate used by SGD.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.01)]
        public double LearningRate { get; set; } = 0.01;

        /// <summary>
        /// The L2 weight for regularization.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1E-06f)]
        public float L2Regularization { get; set; } = 1E-06f;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a SgdCalibratedBinaryTrainer", $"Are you sure you want to add a SgdCalibratedBinaryTrainer?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.BinaryClassification.Trainers.SgdCalibrated(LabelColumn, FeatureColumn, ExampleWeightColumn, Iterations, LearningRate, L2Regularization);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
