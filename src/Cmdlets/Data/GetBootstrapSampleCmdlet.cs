using Microsoft.ML;
using System.IO;
using System.Management.Automation;

namespace PowerML.Cmdlets.Data
{
    /// <synopsis>
    /// Take an approximate bootstrap sample of the input data.
    /// </synopsis>
    /// <summary>
    /// Take an approximate bootstrap sample of the input data.
    /// </summary>
    /// <category order="30">Data</category>
    /// <inputtype name="Microsoft.ML.IDataView">You can pipe a data view to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.IDataView">This cmdlet returns a DataView.</outputtype>
    [Cmdlet(VerbsCommon.Get, "BootstrapSample", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(IDataView))]
    public sealed class GetBootstrapSampleCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The input data.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public IDataView? Data { get; set; } = null;

        /// <summary>
        /// The random seed. If unspecified, the random state will be instead derived from the MLContext.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public int? Seed { get; set; } = null;

        /// <summary>
        /// Whether this is the out-of-bag sample, that is, all those rows that are not selected by the transform. Can be used to create a complementary pair of samples by using the same seed.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter Complement { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (ShouldProcess($"Take an approximate bootstrap sample of the input data.", $"Are you sure you want to take an approximate bootstrap sample of the input data?", "PowerML"))
                WriteObject(Context!.Data.BootstrapSample(Data, Seed, Complement), false);
        }
    }
}
