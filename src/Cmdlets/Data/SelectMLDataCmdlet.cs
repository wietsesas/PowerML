using Microsoft.ML;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <synopsis>
    /// Select a subset of data in a DataView.
    /// </synopsis>
    /// <summary>
    /// Select a subset of data in a DataView.
    /// </summary>
    /// <category order="30">Data</category>
    /// <inputtype name="Microsoft.ML.IDataView">You can pipe a data view to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.IDataView">This cmdlet returns a DataView.</outputtype>
    [Cmdlet(VerbsCommon.Select, "MLData", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(IDataView))]
    public sealed class SelectMLDataCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The input data.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public IDataView? Data { get; set; } = null;

        /// <summary>
        /// The name of a column to use for filtering.
        /// </summary>
        [Parameter()]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = null)]
        public string? ByColumn { get; set; } = null;

        /// <summary>
        /// The name of a column to use for filtering.
        /// </summary>
        [Parameter()]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = null)]
        public string? ByKeyColumnFraction { get; set; } = null;

        /// <summary>
        /// Name of the columns to filter on. If a row is has a missing value in any of these columns, it will be dropped from the dataset.
        /// </summary>
        [Parameter()]
        [ValidateNotNull()]
        [PSDefaultValue(Value = null)]
        public string[]? ByMissingValues { get; set; } = null;

        /// <summary>
        /// The inclusive lower bound for FilterByColumn.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = double.NegativeInfinity)]
        public double ByColumnLowerBound { get; set; } = double.NegativeInfinity;

        /// <summary>
        /// The inclusive lower bound for FilterByKeyColumnFraction.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0)]
        public double ByKeyColumnFractionLowerBound { get; set; } = 0;

        /// <summary>
        /// The exclusive upper bound for FilterByColumn.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = double.PositiveInfinity)]
        public double ByColumnUpperBound { get; set; } = double.PositiveInfinity;

        /// <summary>
        /// The exclusive upper bound for FilterByKeyColumnFraction.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1)]
        public double ByKeyColumnFractionUpperBound { get; set; } = 1;

        /// <summary>
        /// Skip count rows in input.
        /// </summary>
        [Parameter()]
        [ValidateRange(ValidateRangeKind.NonNegative)]
        public long Skip { get; set; }

        /// <summary>
        /// Take count rows from input.
        /// </summary>
        [Parameter()]
        [ValidateRange(ValidateRangeKind.NonNegative)]
        public long Take { get; set; }

        /// <summary>
        /// Shuffle the rows of input.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter Shuffle { get; set; }

        /// <summary>
        /// The random seed. If unspecified, the random seed will be instead derived from the Context.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public int? ShuffleSeed { get; set; } = null;

        /// <summary>
        /// The number of rows to hold in the pool. Setting this to 1 will turn off pool shuffling and will only perform a shuffle by reading input in a random order.
        /// </summary>
        [Parameter()]
        [ValidateRange(ValidateRangeKind.Positive)]
        [PSDefaultValue(Value = 1000)]
        public int ShufflePoolSize { get; set; } = 1000;

        /// <summary>
        /// If false, the transform will not attempt to read input in a random order and only use pooling to shuffle. This parameter has no effect if the CanShuffle property of input is false.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontShuffleSource { get; set; }

        /// <summary>
        /// The columns that must be cached whenever anything is cached. An empty array or null value means that columns are cached upon their first access.  
        /// Only applied when the parameter is used.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public string[]? Cache { get; set; } = null;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            IDataView result = Data!;

            // FilterRowsByColumn
            if (!string.IsNullOrEmpty(ByColumn))
                if (ShouldProcess($"Filter rows by column {ByColumn}", $"Are you sure you want to filter rows by column {ByColumn}?", "PowerML"))
                    result = Context!.Data.FilterRowsByColumn(result, ByColumn, ByColumnLowerBound, ByColumnUpperBound);

            // FilterRowsByKeyColumnFraction
            if (!string.IsNullOrEmpty(ByKeyColumnFraction))
                if (ShouldProcess($"Filter rows by key column fraction {ByKeyColumnFraction}", $"Are you sure you want to filter rows by key column fraction {ByKeyColumnFraction}?", "PowerML"))
                    result = Context!.Data.FilterRowsByKeyColumnFraction(result, ByKeyColumnFraction, ByKeyColumnFractionLowerBound, ByKeyColumnFractionUpperBound);

            // FilterRowsByMissingValues
            if (ByMissingValues?.Length > 0)
                if (ShouldProcess($"Filter rows by missing values {string.Join(", ", ByMissingValues)}", $"Are you sure you want to filter rows by missing values {string.Join(", ", ByMissingValues)}?", "PowerML"))
                    result = Context!.Data.FilterRowsByMissingValues(result, ByMissingValues);

            // Skip rows
            if (MyInvocation.BoundParameters.ContainsKey(nameof(Skip)))
                if (ShouldProcess($"Skip {Skip} rows", $"Are you sure you want to skip {Skip} rows?", "PowerML"))
                    result = Context!.Data.SkipRows(result, Skip);

            // Take rows
            if (MyInvocation.BoundParameters.ContainsKey(nameof(Take)))
                if (ShouldProcess($"Take {Take} rows", $"Are you sure you want to take {Take} rows?", "PowerML"))
                    result = Context!.Data.TakeRows(result, Take);

            // Shuffle rows
            if (Shuffle)
                if (ShouldProcess("Shuffle rows", "Are you sure you want to shuffle rows?", "PowerML"))
                    result = Context!.Data.ShuffleRows(result, ShuffleSeed, ShufflePoolSize, !DontShuffleSource.ToBool());

            // Cache data
            if (MyInvocation.BoundParameters.ContainsKey(nameof(Cache)))
                if (ShouldProcess("Create a lazy in-memory cache of the data", "Are you sure you want to create a lazy in-memory cache of the data?", "PowerML"))
                    result = Context!.Data.Cache(result, Cache);

            // Return the data
            WriteObject(result);
        }
    }
}
