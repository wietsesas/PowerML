using Microsoft.ML;
using Microsoft.ML.Data;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Select the features on which the data in the label column is most dependent.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "SelectFeaturesBasedOnMutualInformationTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddSelectFeaturesBasedOnMutualInformationTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// Name of the column resulting from the transformation of inputColumnName.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string OutputColumn { get; set; } = string.Empty;

        /// <summary>
        /// Name of column to transform. If set to null, the value of the outputColumnName will be used as source.
        /// </summary>
        [Parameter(Position = 1)]
        [PSDefaultValue(Value = null)]
        public string? InputColumn { get; set; } = null;

        /// <summary>
        /// The name of the label column.
        /// </summary>
        [Parameter(Position = 2)]
        [PSDefaultValue(Value = "Label")]
        public string LabelColumn { get; set; } = "Label";

        /// <summary>
        /// The maximum number of slots to preserve in the output. The number of slots to preserve is taken across all input columns.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 1000)]
        public int SlotsInOutput { get; set; } = 1000;

        /// <summary>
        /// Max number of bins used to approximate mutual information between each input column and the label column. Power of 2 recommended.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 256)]
        public int Bins { get; set; } = 256;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a SelectFeaturesBasedOnMutualInformationTransform", $"Are you sure you want to add a SelectFeaturesBasedOnMutualInformationTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.FeatureSelection.SelectFeaturesBasedOnMutualInformation(OutputColumn, InputColumn, LabelColumn, SlotsInOutput, Bins);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
