using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace PowerML.Cmdlets.Data
{
    /// <summary>
    /// Convert an IDataView to an enumerable list.
    /// </summary>
    /// <category order="30">Data</category>
    /// <inputtype name="Microsoft.ML.IDataView">You can pipe a data view to this cmdlet.</inputtype>
    /// <outputtype name="System.Collections.Generic.IEnumerable&lt;System.Object&gt;">This cmdlet returns the dataview as an enumerable.</outputtype>
    /// <outputtype name="System.Object">This cmdlet returns an element of the dataview.</outputtype>
    [Cmdlet(VerbsData.ConvertTo, "Enumerable", ConfirmImpact = ConfirmImpact.Low, DefaultParameterSetName = PSNames.ToEnum)]
    [OutputType(typeof(IEnumerable<dynamic>), ParameterSetName = new string[] { PSNames.ToEnum })]
    [OutputType(typeof(object), ParameterSetName = new string[] { PSNames.ToObject })]
    public sealed class ConvertToEnumerableCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string ToEnum = nameof(ToEnum);
            public const string ToObject = nameof(ToObject);
        }

        /// <summary>
        /// The registered data type.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ArgumentCompleter(typeof(RegisteredTypes.Completer))]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The input data.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public IDataView? Data { get; set; } = null;

        /// <summary>
        /// Skips a specified number of elements from the start of the data.
        /// </summary>
        [Parameter()]
        [ValidateRange(ValidateRangeKind.NonNegative)]
        public int Skip { get; set; }

        /// <summary>
        /// Skips a specified number of elements from the end of the data.
        /// </summary>
        [Parameter()]
        [ValidateRange(ValidateRangeKind.NonNegative)]
        public int SkipLast { get; set; }

        /// <summary>
        /// Returns a specified number of elements from the start of the data.
        /// </summary>
        [Parameter()]
        [ValidateRange(ValidateRangeKind.NonNegative)]
        public int Take { get; set; }

        /// <summary>
        /// Returns a specified number of elements from the end of the data.
        /// </summary>
        [Parameter()]
        [ValidateRange(ValidateRangeKind.NonNegative)]
        public int TakeLast { get; set; }

        /// <summary>
        /// Returns the element at the specified index in the data.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.ToObject)]
        [ValidateRange(ValidateRangeKind.NonNegative)]
        public int ElementAt { get; set; }

        /// <summary>
        /// Returns one element from the start the data.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.ToObject)]
        public SwitchParameter First { get; set; }

        /// <summary>
        /// Returns one element from the end the data.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.ToObject)]
        public SwitchParameter Last { get; set; }

        /// <summary>
        /// Whether to ignore the case when a requested column is not present in the data view.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = false)]
        public SwitchParameter IgnoreMissingColumns { get; set; }

        /// <summary>
        /// Optional user-provided schema definition. If it is not present, the schema is inferred from the definition of Type.
        /// </summary>
        public SchemaDefinition? Schema { get; set; } = null;

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Support for -WhatIf and -Confirm
            if (!ShouldProcess("Convert MLData", $"Are you sure you want to convert MLData?", "PowerML"))
                return;

            // Create the enumerable
            IEnumerable<dynamic> enumerable = CreateEnumerable();
            dynamic? data = null;

            // Skip
            if (MyInvocation.BoundParameters.ContainsKey(nameof(Skip)))
                enumerable = enumerable.Skip(Skip);

            // SkipLast
            if (MyInvocation.BoundParameters.ContainsKey(nameof(SkipLast)))
                enumerable = enumerable.SkipLast(SkipLast);

            // Take
            if (MyInvocation.BoundParameters.ContainsKey(nameof(Take)))
                enumerable = enumerable.Take(Take);

            // TakeLast
            if (MyInvocation.BoundParameters.ContainsKey(nameof(TakeLast)))
                enumerable = enumerable.TakeLast(TakeLast);

            // Return the enumeration
            if (ParameterSetName == PSNames.ToEnum)
                WriteObject(enumerable, true);

            // Get a single object from the enumeration
            else
            {
                // ElementAt
                if (MyInvocation.BoundParameters.ContainsKey(nameof(ElementAt)))
                    data = enumerable.ElementAtOrDefault(ElementAt);

                // First
                if (MyInvocation.BoundParameters.ContainsKey(nameof(First)))
                    data = enumerable.FirstOrDefault();

                // Last
                if (MyInvocation.BoundParameters.ContainsKey(nameof(Last)))
                    data = enumerable.LastOrDefault();

                // Return the data
                if (data != null)
                    WriteObject(data, false);
            }
        }

        private IEnumerable<dynamic> CreateEnumerable()
        {
            Type[] genericType = new[] { RegisteredTypes.Get(Type) };
            MethodInfo createEnumerable = typeof(DataOperationsCatalog).GetMethod("CreateEnumerable", 1, new Type[] { typeof(IDataView), typeof(bool), typeof(bool), typeof(SchemaDefinition) })?.MakeGenericMethod(genericType)
                ?? throw new NullReferenceException("Failed to create the enumerable");

            IEnumerable<dynamic>? enumerable;
            try { enumerable = (IEnumerable<dynamic>?)createEnumerable.Invoke(Context!.Data, new object?[] { Data, false, IgnoreMissingColumns.ToBool(), Schema }); }
            catch (Exception ex)
            {
                if (ex.InnerException == null) throw;
                else throw ex.InnerException;
            }
            return enumerable ?? throw new NullReferenceException("Failed to create the enumerable");
        }
    }
}
