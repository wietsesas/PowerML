using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Select one or more columns to keep from the input data.
    /// </summary>
    /// <category order="40">Transforms</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "SelectColumnsTransform", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddSelectColumnsTransformCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The array of column names to keep.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string[] Columns { get; set; } = Array.Empty<string>();

        /// <summary>
        /// If true will keep hidden columns and false will remove hidden columns. Keeping hidden columns, instead of dropping them, is recommended when it is necessary to understand how the inputs of a pipeline map to outputs of the pipeline, for debugging purposes.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter KeepHidden { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a SelectColumnsTransform", $"Are you sure you want to add a SelectColumnsTransform?", "PowerML"))
                return;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.Transforms.SelectColumns(Columns, KeepHidden);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }
    }
}
