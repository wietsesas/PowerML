using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Vision;
using PowerML.Validators;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// Train a Deep Neural Network (DNN) to classify images.
    /// </summary>
    /// <category order="50">Trainers</category>
    /// <inputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">You can pipe the EstimatorChain to append to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.Data.EstimatorChain&lt;Microsoft.ML.ITransformer&gt;">This cmdlet returns the appended EstimatorChain.</outputtype>
    [Cmdlet(VerbsCommon.Add, "ImageClassificationTrainer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(EstimatorChain<ITransformer>))]
    public sealed class AddImageClassificationTrainerCmdlet : EstimatorChainCmdlet
    {
        /// <summary>
        /// The name of the label column. The default value is "Label".
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Label")]
        public string LabelColumn { get; set; } = "Label";

        /// <summary>
        /// The name of the feature column. The column data must be a known-sized vector of Single. The default value is "Features".
        /// </summary>
        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Features")]
        public string FeatureColumn { get; set; } = "Features";

        /// <summary>
        /// The name of the score column. The default value is "Score".
        /// </summary>
        [Parameter(Position = 2)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Score")]
        public string? ScoreColumn { get; set; } = "Score";

        /// <summary>
        /// The name of the predicted label column. The default value is "PredictedLabel".
        /// </summary>
        [Parameter(Position = 3)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "PredictedLabel")]
        public string? PredictedLabelColumn { get; set; } = "PredictedLabel";

        /// <summary>
        /// The validation set used while training to improve model quality. The default value is null.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = null)]
        public IDataView? ValidationSet { get; set; } = null;

        /// <summary>
        /// When validation set is not passed then a fraction of train set is used as validation. To disable this behavior set ValidationSetFraction to null. Accepts value between 0 and 1.0. The default value is 0.1 or 10% of the trainset.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.1f)]
        public float? ValidationSetFraction { get; set; } = 0.1f;

        /// <summary>
        /// Specifies the model architecture to be used in the case of image classification training using transfer learning. The default value is ResnetV250.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = ImageClassificationTrainer.Architecture.ResnetV250)]
        public ImageClassificationTrainer.Architecture Arch { get; set; } = ImageClassificationTrainer.Architecture.ResnetV250;

        /// <summary>
        /// Number of samples to use for mini-batch training. The default value is 10.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 10)]
        public int BatchSize { get; set; } = 10;

        /// <summary>
        /// Number of training iterations. The default value is 200.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 200)]
        public int Epoch { get; set; } = 200;

        /// <summary>
        /// Learning rate to use during optimization. The default value is 0.01.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 0.01f)]
        public float LearningRate { get; set; } = 0.01f;

        /// <summary>
        /// Callback to report statistics on accuracy/cross entropy during training phase. The default value is null.
        /// </summary>
        [Parameter()]
        [ValidateNotNull()]
        [PSDefaultValue(Value = null)]
        public ScriptBlock? MetricsCallback { get; set; } = null;

        /// <summary>
        /// Indicates the path where the image bottleneck cache files and trained model are saved. The default value is a new temporary directory.
        /// </summary>
        [Parameter()]
        [ValidatePath(ValidatePathType.DirectoryExists)]
        [PSDefaultValue(Value = "New temporary directory")]
        public string? WorkspacePath { get; set; } = null;

        /// <summary>
        /// Indicates the file name within the workspace to store trainset bottleneck values for caching. The default value is "trainSetBottleneckFile.csv".
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = "trainSetBottleneckFile.csv")]
        public string TrainSetBottleneckCachedValuesPath { get; set; } = "trainSetBottleneckFile.csv";

        /// <summary>
        /// Indicates the file name within the workspace to store validationset bottleneck values for caching. The default value is "validationSetBottleneckFile.csv".
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = "validationSetBottleneckFile.csv")]
        public string ValidationSetBottleneckCachedValuesPath { get; set; } = "validationSetBottleneckFile.csv";

        /// <summary>
        /// Final model and checkpoint files/folder prefix for storing graph files. The default value is "custom_retrained_model_based_on_".
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = 200)]
        public string FinalModelPrefix { get; set; } = "custom_retrained_model_based_on_";

        /// <summary>
        /// Indicates to not evaluate the model on train set after every epoch.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter DontTestOnTrainSet { get; set; }

        /// <summary>
        /// Indicates to not re-compute cached bottleneck trainset values if already available in the bin folder.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter ReuseTrainSetBottleneckCachedValues { get; set; }

        /// <summary>
        /// Indicates to not re-compute cached bottleneck validationset values if already available in the bin folder.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter ReuseValidationSetBottleneckCachedValues { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess($"Add a ImageClassificationTrainer", $"Are you sure you want to add a ImageClassificationTrainer?", "PowerML"))
                return;

            ImageClassificationTrainer.Options classifierOptions = new()
            {
                LabelColumnName = LabelColumn,
                FeatureColumnName = FeatureColumn,
                ScoreColumnName = ScoreColumn,
                PredictedLabelColumnName = PredictedLabelColumn,
                ValidationSet = ValidationSet,
                ValidationSetFraction = ValidationSetFraction,
                BatchSize = BatchSize,
                LearningRate = LearningRate,
                Arch = Arch,
                Epoch = Epoch,
                MetricsCallback = null,
                TrainSetBottleneckCachedValuesFileName = TrainSetBottleneckCachedValuesPath,
                ValidationSetBottleneckCachedValuesFileName = ValidationSetBottleneckCachedValuesPath,
                FinalModelPrefix = FinalModelPrefix,
                TestOnTrainSet = !DontTestOnTrainSet,
                ReuseTrainSetBottleneckCachedValues = ReuseTrainSetBottleneckCachedValues,
                ReuseValidationSetBottleneckCachedValues = ReuseValidationSetBottleneckCachedValues
            };

            // Set the workspace path if provided
            if (WorkspacePath != null)
                classifierOptions.WorkspacePath = WorkspacePath;

            // Set the metrics callback if provided
            if (MetricsCallback != null)
                classifierOptions.MetricsCallback = MetricsCallbackAction;

            // Create the estimator
            IEstimator<ITransformer> estimator = Context!.MulticlassClassification.Trainers.ImageClassification(classifierOptions);

            // Append and return the estimator
            estimator = AppendEstimator(estimator);
            WriteObject(estimator, false);
        }

        private void MetricsCallbackAction(ImageClassificationTrainer.ImageClassificationMetrics metrics)
        {
            Dictionary<string, ScriptBlock> functions = new();
            List<PSVariable> variables = new() { new("_", metrics, ScopedItemOptions.AllScope) };
            object[] args = Array.Empty<object>();
            MetricsCallback?.InvokeWithContext(functions, variables, args);
        }
    }
}
