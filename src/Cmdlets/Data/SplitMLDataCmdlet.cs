using Microsoft.ML;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <synopsis>
    /// Split the dataset into a train and test set or into cross-validation folds of train and test sets.
    /// </synopsis>
    /// <summary>
    /// Split the dataset into the train set and test set according to the given fraction, or split the dataset into cross-validation folds of train sets and test sets. Respects the samplingKeyColumnName if provided.
    /// </summary>
    /// <category order="30">Data</category>
    /// <inputtype name="Microsoft.ML.IDataView">You can pipe a data view to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.TrainTestData">This cmdlet returns a TrainTestData object.</outputtype>
    [Cmdlet(VerbsCommon.Split, "MLData", ConfirmImpact = ConfirmImpact.Low, DefaultParameterSetName = PSNames.Fraction, SupportsShouldProcess = true)]
    [OutputType(typeof(DataOperationsCatalog.TrainTestData))]
    public sealed class SplitMLDataCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string Fraction = nameof(Fraction);
            public const string Folds = nameof(Folds);
        }

        /// <summary>
        /// The dataset to split.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public IDataView? Data { get; set; } = null;

        /// <summary>
        /// The fraction of data to go into the test set.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.Fraction)]
        [ValidateRange(ValidateRangeKind.Positive)]
        [PSDefaultValue(Value = 0.1)]
        public double TestFraction { get; set; } = 0.1;

        /// <summary>
        /// Number of cross-validation folds.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.Folds)]
        [ValidateRange(ValidateRangeKind.Positive)]
        public int Folds { get; set; }

        /// <summary>
        /// Name of a column to use for grouping rows. If two examples share the same value of the samplingKeyColumnName, they are guaranteed to appear in the same subset (train or test). This can be used to ensure no label leakage from the train to the test set. Note that when performing a Ranking Experiment, the samplingKeyColumnName must be the GroupId column. If null no row grouping will be performed.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public string? SamplingKeyColumn { get; set; } = null;

        /// <summary>
        /// Seed for the random number generator used to select rows for the train-test split.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public int? Seed { get; set; } = null;


        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (ParameterSetName == PSNames.Fraction)
            {
                if (ShouldProcess($"Split data into the train set and test set according to fraction {TestFraction}", $"Are you sure you want split data into the train set and test set according to fraction {TestFraction}?", "Split-MLData"))
                    WriteObject(Context!.Data.TrainTestSplit(Data, TestFraction, SamplingKeyColumn, Seed), false);
            }
            else
            {
                if (ShouldProcess($"Split data into {Folds} cross-validation folds of train set and test set", $"Are you sure you want split data into {Folds} cross-validation folds of train set and test set?", "Split-MLData"))
                    WriteObject(Context!.Data.CrossValidationSplit(Data, Folds, SamplingKeyColumn, Seed), true);
            }
        }
    }
}
