using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Train a linear regression model using Online Gradient Descent (OGD) for estimating the parameters of the linear regression model.
    /// </summary>
    /// <category order="50">Trainers</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "OnlineGradientDescentTrainer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddOnlineGradientDescentTrainerCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The name of the label column. The column data must be Single.
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
        /// The loss function minimized in the training process. Using, for example, SquaredLoss leads to a least square trainer.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public IRegressionLoss? LossFunction { get; set; } = null;

        /// <summary>
        /// The initial learning rate used by SGD.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.1f)]
        public float LearningRate { get; set; } = 0.1f;

        /// <summary>
        /// Do not decrease learning rate as iterations progress.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontDecreaseLearningRate { get; set; }

        /// <summary>
        /// The L2 weight for regularization.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public float L2Regularization { get; set; } = 0;

        /// <summary>
        /// Number of passes through the training dataset.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1)]
        public int Iterations { get; set; } = 1;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add an OnlineGradientDescentTrainer", $"Are you sure you want to add an OnlineGradientDescentTrainer?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Regression.Trainers.OnlineGradientDescent(LabelColumn, FeatureColumn, null, LearningRate, !DontDecreaseLearningRate, L2Regularization, Iterations);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
