using Microsoft.ML;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <synopsis>
    /// Create a new MLContext.
    /// </synopsis>
    /// <summary>
    /// Create a new MLContext.
    /// </summary>
    /// <category order="10">MLContext</category>
    /// <outputtype name="Microsoft.ML.MLContext">This cmdlet returns the newly created MLContext if -NoCache or -PassThru is used.</outputtype>
    [Cmdlet(VerbsCommon.New, "MLContext", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(MLContext))]
    public sealed class NewMLContextCmdlet : PSCmdlet
    {
        /// <summary>
        /// Seed for MLContext's random number generator.
        /// Many operations in ML.NET require randomness, such as random data shuffling, random sampling, random parameter initialization, random permutation, random feature selection, and many more. MLContext's random number generator is the global source of randomness for all of such random operations.
        /// If a fixed seed is provided by seed, MLContext environment becomes deterministic, meaning that the results are repeatable and will remain the same across multiple runs.For instance in many of ML.NET's API reference example code snippets, a seed is provided. That's because we want the users to get the same output as what's included in example comments, when they run the example on their own machine.
        /// Generally though, repeatability is not a requirement and that's the default behavior. If a seed is not provided by seed, i.e. it's set to null, MLContext environment becomes non - deterministic and outputs change across multiple runs.
        /// There are many operations in ML.NET that don't use any randomness, such as min-max normalization, concatenating columns, missing value indication, etc. The behavior of those operations are deterministic regardless of the seed value.
        /// Also ML.NET trainers don't use randomness *after* the training is finished. So, the predictions from a loaded model don't depend on the seed value.
        /// </summary>
        [Parameter(Position = 0)]
        [PSDefaultValue(Value = null)]
        public int? Seed { get; set; } = null;

        /// <summary>
        /// Do not set the created context as current (cached) context and return the created context (No need to use -PassThru).
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter NoCache { get; set; }

        /// <summary>
        /// Return the created context.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Return if not really running
            if (!ShouldProcess($"Create a new context", $"Are you sure you want to create a new context?", "PowerML"))
                return;

            MLContext context = new(Seed);

            // Cache the context if requested
            if (NoCache)
                PassThru = true;
            else if (ShouldProcess($"Update the current (cached) context", $"Are you sure you want to update the current (cached) context?", "PowerML"))
                CurrentContext.Set(context);
            
            // Return the context if requested
            if (PassThru)
                WriteObject(context, false);
        }

    }
}
