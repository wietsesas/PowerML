using Microsoft.ML;
using System.Management.Automation;

namespace PowerML.Cmdlets.Data
{
    /// <synopsis>
    /// Detect timeseries anomalies for entire input using SRCNN algorithm.
    /// </synopsis>
    /// <summary>
    /// Detect timeseries anomalies for entire input using SRCNN algorithm.
    /// </summary>
    /// <category order="30">Data</category>
    /// <inputtype name="Microsoft.ML.IDataView">You can pipe a data view to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.IDataView">This cmdlet returns the transformed data view.</outputtype>
    [Cmdlet(VerbsDiagnostic.Measure, "EntireAnomalyBySrCnn", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(IDataView))]
    public sealed class MeasureEntireAnomalyBySrCnnCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The input data.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public IDataView? Data { get; set; } = null;

        /// <summary>
        /// Name of the column resulting from data processing of inputColumnName. The column data is a vector of Double. The length of this vector varies depending on DetectMode.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of column to process. The column data must be Double.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string InputColumn { get; set; } = string.Empty;

        /// <summary>
        /// The threshold to determine an anomaly. An anomaly is detected when the calculated SR raw score for a given point is more than the set threshold. This threshold must fall between [0,1]. The default value is 0.3.
        /// </summary>
        [Parameter()]
        [ValidateRange(0, 1)]
        [PSDefaultValue(Value = 0.3)]
        public double Threshold { get; set; } = 0.3;

        /// <summary>
        /// Divide the input data into batches to fit srcnn model. When set to -1, use the whole input to fit model instead of batch by batch, when set to a positive integer, use this number as batch size. Must be -1 or a positive integer no less than 12. The default value is 1024.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1024)]
        public int BatchSize { get; set; } = 1024;

        /// <summary>
        /// Sensitivity of boundaries, only useful when srCnnDetectMode is AnomalyAndMargin. Must be in [0,100]. The default value is 99.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 99)]
        public double Sensitivity { get; set; } = 99;

        /// <summary>
        /// An enum type of SrCnnDetectMode. When set to AnomalyOnly, the output vector would be a 3-element Double vector of (IsAnomaly, RawScore, Mag). When set to AnomalyAndExpectedValue, the output vector would be a 4-element Double vector of (IsAnomaly, RawScore, Mag, ExpectedValue). When set to AnomalyAndMargin, the output vector would be a 7-element Double vector of (IsAnomaly, AnomalyScore, Mag, ExpectedValue, BoundaryUnit, UpperBoundary, LowerBoundary). The RawScore is output by SR to determine whether a point is an anomaly or not, under AnomalyAndMargin mode, when a point is an anomaly, an AnomalyScore will be calculated according to sensitivity setting. The default value is AnomalyOnly.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = Microsoft.ML.TimeSeries.SrCnnDetectMode.AnomalyOnly)]
        public Microsoft.ML.TimeSeries.SrCnnDetectMode DetectMode { get; set; } = Microsoft.ML.TimeSeries.SrCnnDetectMode.AnomalyOnly;

        /// <summary>
        /// The period of the series. The default value is 0.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public int Period { get; set; } = 0;

        /// <summary>
        /// The Deseasonality modes of SrCnn models. The de-seasonality mode is invoked when the period of the series is greater than 0. The default value is Stl.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = Microsoft.ML.TimeSeries.SrCnnDeseasonalityMode.Stl)]
        public Microsoft.ML.TimeSeries.SrCnnDeseasonalityMode DeseasonalityMode { get; set; } = Microsoft.ML.TimeSeries.SrCnnDeseasonalityMode.Stl;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (ShouldProcess($"Detect timeseries anomalies for entire input using SRCNN algorithm.", $"Are you sure you want to detect timeseries anomalies for entire input using SRCNN algorithm?", "PowerML"))
            {
                if (Period > 0)
                {
                    Microsoft.ML.TimeSeries.SrCnnEntireAnomalyDetectorOptions options = new()
                    {
                        Threshold = Threshold,
                        BatchSize = BatchSize,
                        Sensitivity = Sensitivity,
                        DetectMode = DetectMode,
                        Period = Period,
                        DeseasonalityMode = DeseasonalityMode
                    };
                    WriteObject(Context!.AnomalyDetection.DetectEntireAnomalyBySrCnn(Data, OutputColumn, InputColumn, options), false);
                }
                else
                    WriteObject(Context!.AnomalyDetection.DetectEntireAnomalyBySrCnn(Data, OutputColumn, InputColumn, Threshold, BatchSize, Sensitivity, DetectMode), false);
            }
        }
    }
}
