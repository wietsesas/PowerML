using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Predict elements in a matrix using matrix factorization (also known as a type of collaborative filtering).
    /// </summary>
    /// <category order="50">Trainers</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "MatrixFactorizationTrainer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddMatrixFactorizationTrainerCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The name of the label column. The column data must be Single.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Label")]
        public string LabelColumn { get; set; } = "Label";

        /// <summary>
        /// The name of the column hosting the matrix's column IDs. The column data must be KeyDataViewType.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string ColumnIndexColumn { get; set; } = string.Empty;

        /// <summary>
        /// The name of the column hosting the matrix's row IDs. The column data must be KeyDataViewType.
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        public string RowIndexColumn { get; set; } = string.Empty;

        /// <summary>
        /// Rank of approximation matrices.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 8)]
        public int ApproximationRank { get; set; } = 8;

        /// <summary>
        /// Initial learning rate. It specifies the speed of the training algorithm.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.1)]
        public double LearningRate { get; set; } = 0.1;

        /// <summary>
        /// The number of training iterations.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 20)]
        public int Iterations { get; set; } = 20;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a MatrixFactorizationTrainer", $"Are you sure you want to add a MatrixFactorizationTrainer?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Recommendation().Trainers.MatrixFactorization(LabelColumn, ColumnIndexColumn, RowIndexColumn, ApproximationRank, LearningRate, Iterations);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
