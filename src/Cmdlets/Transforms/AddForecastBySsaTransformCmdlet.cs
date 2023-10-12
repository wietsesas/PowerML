using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;
using System.Management.Automation;

namespace PowerML.Cmdlets.Transforms
{
    /// <synopsis>
    /// Singular Spectrum Analysis (SSA) model for univariate time-series forecasting.
    /// </synopsis>
    /// <summary>
    /// Singular Spectrum Analysis (SSA) model for univariate time-series forecasting.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ForecastBySsaTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddForecastBySsaTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of column to transform. If set to null, the value of the outputColumnName will be used as source. The vector contains Alert, Raw Score, P-Value as first three values.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string InputColumn { get; set; } = string.Empty;

        /// <summary>
        /// The length of the window on the series for building the trajectory matrix (parameter L).
        /// </summary>
        [Parameter(Mandatory = true)]
        public int WindowSize { get; set; }

        /// <summary>
        /// The length of series that is kept in buffer for modeling (parameter N).
        /// </summary>
        [Parameter(Mandatory = true)]
        public int SeriesLength { get; set; }

        /// <summary>
        /// The length of series from the beginning used for training.
        /// </summary>
        [Parameter(Mandatory = true)]
        public int TrainSize { get; set; }

        /// <summary>
        /// The number of values to forecast.
        /// </summary>
        [Parameter(Mandatory = true)]
        public int Horizon { get; set; }

        /// <summary>
        /// The flag determining whether the model is adaptive.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter IsAdaptive { get; set; }

        /// <summary>
        /// The discount factor in [0,1] used for online updates.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1)]
        public float DiscountFactor { get; set; } = 1;

        /// <summary>
        /// The rank selection method.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = RankSelectionMethod.Exact)]
        public RankSelectionMethod RankSelectionMethod { get; set; } = RankSelectionMethod.Exact;

        /// <summary>
        /// The desired rank of the subspace used for SSA projection (parameter r). This parameter should be in the range in [1, windowSize]. If set to null, the rank is automatically determined based on prediction error minimization.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public int? Rank { get; set; } = null;

        /// <summary>
        /// The maximum rank considered during the rank selection process. If not provided (i.e. set to null), it is set to windowSize - 1.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public int? MaxRank { get; set; } = null;

        /// <summary>
        /// The flag determining whether the model should be stabilized.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter ShouldNotStabilize { get; set; }

        /// <summary>
        /// The flag determining whether the meta information for the model needs to be maintained.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter ShouldMaintainInfo { get; set; }

        /// <summary>
        /// The maximum growth on the exponential trend.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public GrowthRatio? MaxGrowth { get; set; } = null;

        /// <summary>
        /// The name of the confidence interval lower bound column. If not specified then confidence intervals will not be calculated.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public string? ConfidenceLowerBoundColumn { get; set; } = null;

        /// <summary>
        /// The name of the confidence interval upper bound column. If not specified then confidence intervals will not be calculated.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public string? ConfidenceUpperBoundColumn { get; set; } = null;

        /// <summary>
        /// The confidence level for forecasting.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.95f)]
        public float ConfidenceLevel { get; set; } = 0.95f;

        /// <summary>
        /// Set this to true if horizon will change after training(at prediction time).
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter VariableHorizon { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a ForecastBySsaTransform", $"Are you sure you want to add a ForecastBySsaTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Forecasting.ForecastBySsa(OutputColumn, InputColumn, WindowSize, SeriesLength, TrainSize, Horizon, IsAdaptive, DiscountFactor, RankSelectionMethod, Rank, MaxRank, !ShouldNotStabilize, ShouldMaintainInfo, MaxGrowth, ConfidenceLowerBoundColumn, ConfidenceUpperBoundColumn, ConfidenceLevel, VariableHorizon);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
