using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace PowerML.Cmdlets
{
    /// <synopsis>
    /// Get data from memory, files or database.
    /// </synopsis>
    /// <summary>
    /// Get data from memory, files or database.
    /// </summary>
    /// <category order="30">Data</category>
    /// <outputtype name="Microsoft.ML.IDataView">This cmdlet returns a DataView.</outputtype>
    [Cmdlet(VerbsData.Import, "MLData", ConfirmImpact = ConfirmImpact.Low, DefaultParameterSetName = PSNames.FromMemory, SupportsShouldProcess = true)]
    [OutputType(typeof(IDataView))]
    public sealed class ImportMLDataCmdlet : ContextCmdlet
    {
        /// <summary>
        /// The parameter sets used in this cmdlet.
        /// </summary>
        private static class PSNames
        {
            public const string FromMemory = nameof(FromMemory);
            public const string FromFiles = nameof(FromFiles);
            public const string FromDatabase = nameof(FromDatabase);
        }

        /// <summary>
        /// The registered data type.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ArgumentCompleter(typeof(RegisteredTypes.Completer))]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The data from memory.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.FromMemory)]
        public IEnumerable? Data { get; set; } = null;

        /// <summary>
        /// The path to the data file (wildcards supported).
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PSNames.FromFiles)]
        [SupportsWildcards()]
        public string[] Path { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Column separator character.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.FromFiles)]
        [ValidateNotNullOrEmpty()]
        [PSDefaultValue(Value = '\t')]
        public char Separator { get; set; } = '\t';

        /// <summary>
        /// Whether the file has a header. When true, the loader will skip the first line.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.FromFiles)]
        [PSDefaultValue(Value = false)]
        public SwitchParameter HasHeader { get; set; }

        /// <summary>
        /// Whether the input may include double-quoted values. This parameter is used to distinguish separator characters in an input value from actual separators. When true, separators within double quotes are treated as part of the input value. When false, all separators, even those whitin quotes, are treated as delimiting a new column. It is also used to distinguish empty values from missing values. When true, missing value are denoted by consecutive separators and empty values by "". When false, empty values are denoted by consecutive separators and missing values by the default missing value for each type documented in DataKind.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.FromFiles)]
        [PSDefaultValue(Value = false)]
        public SwitchParameter AllowQuoting { get; set; }

        /// <summary>
        /// Remove trailing whitespace from lines.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.FromFiles)]
        [PSDefaultValue(Value = false)]
        public SwitchParameter TrimWhitespace { get; set; }

        /// <summary>
        /// Whether the input may include sparse representations. For example, a row containing "5 2:6 4:3" means that there are 5 columns, and the only non-zero are columns 2 and 4, which have values 6 and 3, respectively. Column indices are zero-based, so columns 2 and 4 represent the 3rd and 5th columns. A column may also have dense values followed by sparse values represented in this fashion. For example, a row containing "1 2 5 2:6 4:3" represents two dense columns with values 1 and 2, followed by 5 sparsely represented columns with values 0, 0, 6, 0, and 3. The indices of the sparse columns start from 0, even though 0 represents the third column.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.FromFiles)]
        [PSDefaultValue(Value = false)]
        public SwitchParameter AllowSparse { get; set; }

        /// <summary>
        /// The factory used to create the DbConnection.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.FromDatabase)]
        public DbProviderFactory? DbProvider { get; set; } = null;

        /// <summary>
        /// The string used to open the connection.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.FromDatabase)]
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// The text command to run against the data source.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PSNames.FromDatabase)]
        public string Command { get; set; } = string.Empty;

        /// <summary>
        /// The timeout (in seconds) for the database command.
        /// </summary>
        [Parameter(ParameterSetName = PSNames.FromDatabase)]
        [ValidateRange(ValidateRangeKind.Positive)]
        public int CommandTimeout { get; set; }

        /// <summary>
        /// Runs for every object passed through the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case PSNames.FromMemory:
                    if (ShouldProcess($"Load data from memory", $"Are you sure you want to load data from memory?", "PowerML"))
                        WriteObject(LoadFromEnumerable(), false);
                    break;

                case PSNames.FromFiles:
                    if (ShouldProcess($"Load data from {Path.Length} file(s)", $"Are you sure you want to load data from {Path.Length} file(s)?", "PowerML"))
                        WriteObject(LoadFromTextFile(), false);
                    break;

                case PSNames.FromDatabase:
                    if (ShouldProcess($"Load data from a database", $"Are you sure you want to load data a database?", "PowerML"))
                        WriteObject(LoadFromDatabase(), false);
                    break;
            }
        }

        /// <summary>
        /// Load data from an enumerable.
        /// </summary>
        private IDataView LoadFromEnumerable()
        {
            Type objectType = RegisteredTypes.Get(Type);
            Type genericListType = typeof(List<>).MakeGenericType(new[] { objectType });
            
            dynamic objectList = Activator.CreateInstance(genericListType)
                ?? throw new NullReferenceException("Failed to import data");

            MethodInfo addToList = genericListType.GetMethod("Add", 0, new Type[] { objectType })
                ?? throw new NullReferenceException("Failed to import data");

            foreach (object? obj in Data!)
            {
                if (obj == null) continue;
                if (obj is PSObject pso)
                    try { addToList.Invoke(objectList, new object[] { pso.BaseObject }); }
                    catch (Exception ex)
                    {
                        if (ex.InnerException == null) throw;
                        else throw ex.InnerException;
                    }
                else
                    try { addToList.Invoke(objectList, new object[] { obj }); }
                    catch (Exception ex)
                    {
                        if (ex.InnerException == null) throw;
                        else throw ex.InnerException;
                    }
            }

            // Get the correct generic method
            MethodInfo loadFromEnumerable = typeof(DataOperationsCatalog).GetMethods().FirstOrDefault(mi => mi.Name == "LoadFromEnumerable" && mi.GetParameters().Any(pi => pi.Name == "schemaDefinition"))?.MakeGenericMethod(new[] { objectType })
                ?? throw new NullReferenceException("Failed to import data");

            IDataView? result;
            try { result = (IDataView?)loadFromEnumerable.Invoke(Context!.Data, new object?[] { objectList, null }); }
            catch (Exception ex)
            {
                if (ex.InnerException == null) throw;
                else throw ex.InnerException;
            }

            return result
                ?? throw new NullReferenceException("Failed to import data");
        }

        /// <summary>
        /// Load data from text files.
        /// </summary>
        private IDataView LoadFromTextFile()
        {
            Type type = RegisteredTypes.Get(Type);
            Type[] genericType = new[] { type };
            MethodInfo createTextLoader = typeof(TextLoaderSaverCatalog).GetMethod("CreateTextLoader", 1, new Type[] { typeof(DataOperationsCatalog), typeof(char), typeof(bool), typeof(IMultiStreamSource), typeof(bool), typeof(bool), typeof(bool) })?.MakeGenericMethod(genericType)
                ?? throw new NullReferenceException("Failed to import the data");

            TextLoader? textLoader;
            try { textLoader = (TextLoader?)createTextLoader.Invoke(typeof(TextLoaderSaverCatalog), new object?[] { Context!.Data, Separator, HasHeader.ToBool(), null, AllowQuoting.ToBool(), TrimWhitespace.ToBool(), AllowSparse.ToBool() }); }
            catch (Exception ex)
            {
                if (ex.InnerException == null) throw;
                else throw ex.InnerException;
            }
            textLoader = textLoader ?? throw new NullReferenceException("Failed to import the data");
            return textLoader.Load(Path);
        }

        /// <summary>
        /// Load data from a database.
        /// </summary>
        private IDataView LoadFromDatabase()
        {
            DatabaseSource source;
            if (MyInvocation.BoundParameters.ContainsKey(nameof(CommandTimeout)))
                source = new(DbProvider, ConnectionString, Command, CommandTimeout);
            else
                source = new(DbProvider, ConnectionString, Command);

            Type type = RegisteredTypes.Get(Type);
            Type[] genericType = new[] { type };
            MethodInfo createDatabaseLoader = typeof(DatabaseLoaderCatalog).GetMethod("CreateDatabaseLoader", 1, new Type[] { typeof(DataOperationsCatalog) })?.MakeGenericMethod(genericType)
                ?? throw new NullReferenceException("Failed to import the data");

            DatabaseLoader? databaseLoader;
            try { databaseLoader = (DatabaseLoader?)createDatabaseLoader.Invoke(typeof(DatabaseLoaderCatalog), new object?[] { Context!.Data }); }
            catch (Exception ex)
            {
                if (ex.InnerException == null) throw;
                else throw ex.InnerException;
            }
            databaseLoader = databaseLoader ?? throw new NullReferenceException("Failed to import the data");
            return databaseLoader.Load(source);
        }
    }
}
