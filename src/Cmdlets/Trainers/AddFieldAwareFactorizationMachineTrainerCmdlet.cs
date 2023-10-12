using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Predict a target using a field-aware factorization machine model trained using a stochastic gradient method.
    /// </summary>
    /// <category order="50">Trainers</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "FieldAwareFactorizationMachineTrainer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddFieldAwareFactorizationMachineTrainerCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The name of the label column. The column data must be Boolean.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Label")]
        public string LabelColumn { get; set; } = "Label";

        /// <summary>
        /// The name(s) of the feature column(s). The column data must be a known-sized vector of Single.
        /// </summary>
        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Features")]
        public string[] FeatureColumn { get; set; } = new string[] { "Features" };

        /// <summary>
        /// The name of the example weight column (optional).
        /// </summary>
        [Parameter(Position = 2)]
        [PSDefaultValue(Value = null)]
        public string? ExampleWeightColumn { get; set; } = null;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a FieldAwareFactorizationMachineTrainer", $"Are you sure you want to add a FieldAwareFactorizationMachineTrainer?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator;
            if (FeatureColumn.Length > 1)
                estimator = Context!.BinaryClassification.Trainers.FieldAwareFactorizationMachine(FeatureColumn, LabelColumn, ExampleWeightColumn);
            else
                estimator = Context!.BinaryClassification.Trainers.FieldAwareFactorizationMachine(FeatureColumn[0], LabelColumn, ExampleWeightColumn);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
