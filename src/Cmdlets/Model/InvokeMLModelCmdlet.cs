using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections;
using System.Management.Automation;

namespace PowerML.Cmdlets.Model
{
    /// <synopsis>
    /// Transform data using a machine learning model.
    /// </synopsis>
    /// <summary>
    /// Transform data using a machine learning model.
    /// </summary>
    /// <category order="35">Model</category>
    /// <inputtype name="System.Object">You can pipe objects of InputType to this cmdlet.</inputtype>
    /// <outputtype name="Microsoft.ML.IDataView">This cmdlet returns the transformed data.</outputtype>
    /// <outputtype name="System.Object">This cmdlet returns the transformed data as objects of type OutputType.</outputtype>
    [Cmdlet(VerbsLifecycle.Invoke, "MLModel", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true, DefaultParameterSetName = PSNames.DataView)]
    [OutputType(typeof(IDataView), ParameterSetName = new[] { PSNames.DataView })]
    [OutputType(typeof(object), ParameterSetName = new[] { PSNames.Object })]
    public sealed class InvokeMLModelCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string DataView = nameof(DataView);
            public const string Object = nameof(Object);
        }

        /// <summary>
        /// The transformer to transform the data.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public TransformerChain<ITransformer>? Model { get; set; } = null;

        /// <summary>
        /// The data to transform.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PSNames.DataView)]
        public IDataView? Data { get; set; } = null;

        /// <summary>
        /// The data for which to get predictions.
        /// </summary>

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = PSNames.Object)]
        public object? InputObject { get; set; } = null;

        /// <summary>
        /// The output type.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.Object)]
        [ArgumentCompleter(typeof(RegisteredTypes.Completer))]
        public string OutputType { get; set; } = string.Empty;

        /// <summary>
        /// The data type.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.Object)]
        [ArgumentCompleter(typeof(RegisteredTypes.Completer))]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = "Type from Object")]
        public string InputType { get; set; } = string.Empty;

        /// <summary>
        /// The input schema definition.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.Object)]
        [PSDefaultValue(Value = null)]
        public SchemaDefinition? InputSchema { get; set; } = null;

        /// <summary>
        /// The output schema definition
        /// </summary>
        [Parameter(ParameterSetName = PSNames.Object)]
        [PSDefaultValue(Value = null)]
        public SchemaDefinition? OutputSchema { get; set; } = null;

        /// <summary>
        /// Ignore missing columns.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.Object)]
        [PSDefaultValue(Value = false)]
        public SwitchParameter IgnoreMissingColumns { get; set; }

        /// <summary>
        /// Do not cache the prediction function.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.Object)]
        [PSDefaultValue(Value = false)]
        public SwitchParameter NoCache { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (!ShouldProcess($"Transform data using a machine learing model", $"Are you sure you want to transform data using a machine learing model?", "PowerML"))
                return;

            switch (ParameterSetName)
            {
                case PSNames.DataView:
                    TransformDataView();
                    break;

                case PSNames.Object:
                    TransformInputObject();
                    break;
            }
        }

        /// <summary>
        /// Transform data.
        /// </summary>
        private void TransformDataView() =>
            WriteObject(Model!.Transform(Data), false);

        /// <summary>
        /// Transform data.
        /// </summary>
        private void TransformInputObject()
        {
            // Unpack object if it is a psobject
            if (InputObject == null) throw new ArgumentNullException(nameof(InputObject));
            if (InputObject is PSObject psoData) InputObject = psoData.BaseObject;

            // Get the input and output type
            Type inputType;
            Type outputType = RegisteredTypes.Get(OutputType);
            if (MyInvocation.BoundParameters.ContainsKey(nameof(InputType))) inputType = RegisteredTypes.Get(InputType);
            else inputType = InputObject.GetType();

            // Create the prediction engine (or get it from the cache if requested) and provide a prediction
            dynamic predictionEngine = PredictionEngines.Create(Context!, Model!, inputType, outputType, IgnoreMissingColumns.ToBool(), InputSchema, OutputSchema, !NoCache.ToBool());
            WriteObject(PredictionEngines.Predict(predictionEngine, inputType, outputType, InputObject), false);
        }
    }
}
