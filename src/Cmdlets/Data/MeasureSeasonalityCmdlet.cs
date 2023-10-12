using Microsoft.ML;
using System.Management.Automation;

namespace PowerML.Cmdlets.Data
{
    /// <synopsis>
    /// Detects this predictable interval (or period) by adopting techniques of fourier analysis.
    /// </synopsis>
    /// <summary>
    /// In time series data, seasonality (or periodicity) is the presence of variations that occur at specific regular intervals, such as weekly, monthly, or quarterly.  
    /// This method detects this predictable interval (or period) by adopting techniques of fourier analysis. Assuming the input values have the same time interval (e.g., sensor data collected at every second ordered by timestamps), this method takes a list of time-series data, and returns the regular period for the input seasonal data, if a predictable fluctuation or pattern can be found that recurs or repeats over this period throughout the input values.  
    /// Returns -1 if no such pattern is found, that is, the input values do not follow a seasonal fluctuation.
    /// </summary>
    /// <category order="30">Data</category>
    /// <inputtype name="Microsoft.ML.IDataView">You can pipe a data view to this cmdlet.</inputtype>
    /// <outputtype name="System.Int32">This cmdlet returns the regular interval for the input as seasonal data, otherwise return -1.</outputtype>
    [Cmdlet(VerbsDiagnostic.Measure, "Seasonality", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(int))]
    public sealed class MeasureSeasonalityCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The input data.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public IDataView? Data { get; set; } = null;

        /// <summary>
        /// Name of column to process. The column data must be Double.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string InputColumn { get; set; } = string.Empty;

        /// <summary>
        /// An upper bound on the number of values to be considered in the input values. When set to -1, use the whole input to fit model; when set to a positive integer, only the first windowSize number of values will be considered. The default value is -1.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = -1)]
        public int WindowSize { get; set; } = -1;

        /// <summary>
        /// Randomness threshold that specifies how confidently the input values follow a predictable pattern recurring as seasonal data. The range is between [0, 1]. The default value is 0.95.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.95)]
        public double RandomnessThreshold { get; set; } = 0.95;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (ShouldProcess($"Detect seasonality", $"Are you sure you want to detect seasonality?", "PowerML"))
                WriteObject(Context!.AnomalyDetection.DetectSeasonality(Data, InputColumn, WindowSize, RandomnessThreshold), false);
        }
    }
}
