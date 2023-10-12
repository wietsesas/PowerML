using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.ML.Data;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace PowerML
{
    /// <summary>
    /// Registered types.
    /// </summary>
    internal static class RegisteredTypes
    {
        /// <summary>
        /// The registered type argument completer.
        /// </summary>
        internal class Completer : IArgumentCompleter
        {
            public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, System.Management.Automation.Language.CommandAst commandAst, IDictionary fakeBoundParameters) =>
                _types.Where(kv => kv.Key.ToLower().Contains(wordToComplete.ToLower())).Select(kv => new CompletionResult(kv.Key));
        }

        /// <summary>
        /// The type dictionary.
        /// </summary>
        private static readonly Dictionary<string, Type> _types = new();

        /// <summary>
        /// Get a registered type.
        /// </summary>
        public static Type Get(string typeName)
        {
            if (!_types.ContainsKey(typeName))
                throw new PSArgumentNullException(nameof(typeName), $"Type {typeName} is not a registered type");
            return _types[typeName];
        }

        /// <summary>
        /// Get all registered types.
        /// </summary>
        public static Type[] GetAll() =>
            _types.Values.ToArray();

        /// <summary>
        /// Register a type.
        /// </summary>
        public static void Set(Type type) =>
            _types[type.Name] = type;

        /// <summary>
        /// Remove the specified registered type.
        /// </summary>
        public static void Remove(string typeName) =>
            _types.Remove(typeName);

        /// <summary>
        /// Clear the registered types.
        /// </summary>
        public static void Clear() =>
            _types.Clear();

        /// <summary>
        /// Create a new object of the specified type.
        /// </summary>
        public static object CreateObject(string typeName, IDictionary? properties)
        {
            Type type = Get(typeName);

            object result = Activator.CreateInstance(type) ??
                throw new InvalidOperationException($"Failed to create an instance of type {type.FullName}");

            if (properties?.Keys.Count > 0)
            {
                foreach (object key in properties.Keys)
                {
                    if (key.ToString() == null) continue;
                    string propertyName = key.ToString()!;
                    PropertyInfo? property = type.GetProperty(propertyName) ??
                        throw new InvalidOperationException($"Property {propertyName} does not exist on the object");

                    try
                    {
                        var value = Convert.ChangeType(properties[key], property.PropertyType);
                        property.SetValue(result, value);
                    }
                    catch (Exception ex) { throw new InvalidOperationException($"Failed to set property {propertyName}", ex); }
                }
            }

            return result;
        }

        /// <summary>
        /// Get a custom type from a dictionary.
        /// </summary>
        public static Type FromDictionary(IDictionary definition, bool register)
        {
            // Get the base type
            Type? baseType = null;
            if (definition.Contains("BaseType")) baseType = FromObject(definition["BaseType"]);

            // Validation
            if (!definition.Contains("Name") || definition["Name"] == null || definition["Name"] is not string typeName || string.IsNullOrEmpty(typeName))
                throw new ArgumentException("The type must have a name", nameof(definition));
            if (!definition.Contains("Properties") || definition["Properties"] == null || definition["Properties"] is not IDictionary propertiesDictionary || propertiesDictionary.Count == 0)
                throw new ArgumentException("The type must have properties", nameof(definition));

            // Create the type
            TypeBuilder tb = CreateTypeBuilder(typeName, $"PML.{Guid.NewGuid()}", null, baseType);

            // Parse the properties
            foreach (var key in propertiesDictionary.Keys)
            {
                // Validation
                if (key == null || key is not string propertyName)
                    throw new ArgumentException("Property names must be of type String", nameof(definition));
                if (propertiesDictionary[key] == null || propertiesDictionary[key] is not IDictionary propertyDefinition)
                    throw new ArgumentException($"Property definitions must be a dictionary (Property {propertyName})", nameof(definition));


                Type? propertyType = null;
                string? columnName = null;
                int[]? loadColumn = null;
                int[]? loadColumnRange = null;
                int[]? vectorType = null;

                // Get the property type
                if (propertyDefinition.Contains("Type")) propertyType = FromObject(propertyDefinition["Type"]);
                if (propertyType == null) throw new ArgumentException($"Property {propertyName} must have a type", nameof(definition));

                // Get the loadColumn values
                if (propertyDefinition.Contains("LoadColumn"))
                {
                    Exception? innerException = null;
                    if (propertyDefinition["LoadColumn"] is not null and int dictLoadColumn)
                        loadColumn = new int[] { dictLoadColumn };
                    else if (propertyDefinition["LoadColumn"] is not null and IEnumerable dictLoadColumnList)
                        try { loadColumn = dictLoadColumnList.Cast<int>().ToArray(); }
                        catch (Exception ex) { innerException = ex; }

                    if (loadColumn == null || loadColumn.Length < 1)
                        throw new ArgumentException("LoadColumn must be of type Int32, or an array of type Int32 and cannot be null or empty", nameof(definition), innerException);
                }

                // Get the LoadColumnRange values
                if (propertyDefinition.Contains("LoadColumnRange"))
                {
                    Exception? innerException = null;
                    if (loadColumn != null)
                        throw new ArgumentException("LoadColumn and LoadColumnRange cannot be used together", nameof(definition));
                    else if (propertyDefinition["LoadColumnRange"] is not null and IEnumerable dictLoadColumnRange)
                        try { loadColumnRange = dictLoadColumnRange.Cast<int>().ToArray(); }
                        catch (Exception ex) { innerException = ex; }

                    if (loadColumnRange == null || loadColumnRange.Length != 2)
                        throw new ArgumentException("LoadColumnRange must be an array of type Int32 with two elements", nameof(definition), innerException);
                }

                // Get the ColumnName value
                if (propertyDefinition.Contains("ColumnName"))
                {
                    if (propertyDefinition["ColumnName"] is not null and string dictColumnName && dictColumnName.Length > 0)
                        columnName = dictColumnName;
                    else
                        throw new ArgumentException("ColumnName must be of type String and cannot be null or empty", nameof(definition));
                }

                // Get the VectorType values
                if (propertyDefinition.Contains("VectorType"))
                {
                    Exception? innerException = null;
                    if (propertyDefinition["VectorType"] is not null and int dictVectorType)
                        vectorType = new int[] { dictVectorType };
                    else if (propertyDefinition["VectorType"] is not null and IEnumerable dictLoadVectorTypeList)
                        try { vectorType = dictLoadVectorTypeList.Cast<int>().ToArray(); }
                        catch (Exception ex) { innerException = ex; }

                    if (vectorType == null || vectorType.Length < 1)
                        throw new ArgumentException("VectorType must be of type Int32, or an array of type Int32 and cannot be null or empty", nameof(definition), innerException);
                }

                // Create the property on the TypeBuilder
                CreateProperty(tb, propertyName, propertyType, loadColumn, loadColumnRange, columnName, vectorType);
            }

            Type type = tb.CreateType();
            if (register) Set(type);
            return type;
        }

        /// <summary>
        /// Get a type from a json token.
        /// </summary>
        public static Type FromJsonFile(string path, Encoding encoding, bool register)
        {
            string json = File.ReadAllText(path, encoding);
            JToken jToken = JToken.Parse(json);

            // Validation
            if (jToken.Type != JTokenType.Object) throw new JsonException("The root item must be an object");

            // Parse the json root object
            Type? baseType = null;
            string? typeName = null;
            JObject jRoot = (JObject)jToken;
            JObject? jProperties = null;
            foreach (JProperty jType in jRoot.Properties())
            {
                switch (jType.Name)
                {
                    case "BaseType":
                        if (jType.Value.Type == JTokenType.String)
                            baseType = FromObject(jType.ToObject<string>());
                        break;

                    case "Name":
                        if (jType.Value.Type == JTokenType.String)
                            typeName = jType.ToObject<string>();
                        break;

                    case "Properties":
                        if (jType.Value is JObject jObject)
                            jProperties = jObject;
                        break;

                    default:
                        break;
                }
            }

            // Validation
            if (string.IsNullOrEmpty(typeName)) throw new JsonException("The type must have a name");
            if (jProperties == null) throw new JsonException("The type must have properties");

            // Create the type
            TypeBuilder tb = CreateTypeBuilder(typeName, $"PML.{Guid.NewGuid()}", null, baseType);

            // Parse the json properties object
            foreach (JProperty jPropertyObject in jProperties.Properties())
            {
                // Validation
                if (jPropertyObject.Value.Type != JTokenType.Object) throw new JsonException($"Property {jPropertyObject.Name} must be an object");

                // Parse the json property definition object       
                JObject jProperty = (JObject)jPropertyObject.Value;
                Type? propertyType = null;
                string? columnName = null;
                int[]? loadColumn = null;
                int[]? loadColumnRange = null;
                int[]? vectorType = null;
                foreach (JProperty jDefinition in jProperty.Properties())
                {
                    switch (jDefinition.Name)
                    {
                        case "Type":
                            if (jDefinition.Value.Type == JTokenType.String)
                                propertyType = FromObject(jDefinition.ToObject<string>());
                            break;

                        case "ColumnName":
                            if (jDefinition.Value.Type == JTokenType.String)
                                columnName = jDefinition.ToObject<string>();
                            break;

                        case "LoadColumn":
                            if (jDefinition.Value.Type == JTokenType.Integer)
                                loadColumn = new int[] { jDefinition.ToObject<int>() };
                            else if (jDefinition.Value.Type == JTokenType.Array)
                            {
                                JArray jArray = (JArray)jDefinition.Value;
                                if (jArray.Count > 0)
                                {
                                    loadColumn = new int[jArray.Count];
                                    for (int i = 0; i < jArray.Count; i++)
                                        if (jArray[i].Type == JTokenType.Integer)
                                            loadColumn[i] = jArray[i].ToObject<int>();
                                        else
                                            throw new JsonException($"LoadColumn must be an integer or an array of integers (Property {jPropertyObject.Name})");
                                }
                            }
                            else
                                throw new JsonException($"LoadColumn must be an integer or an array of integers (Property {jPropertyObject.Name})");
                            break;

                        case "LoadColumnRange":
                            if (jDefinition.Value.Type == JTokenType.Array)
                            {
                                JArray jArray = (JArray)jDefinition.Value;
                                if (jArray.Count > 0)
                                {
                                    if (jArray.Count == 2 && jArray[0].Type == JTokenType.Integer && jArray[1].Type == JTokenType.Integer)
                                    {
                                        loadColumnRange = new int[2];
                                        loadColumnRange[0] = jArray[0].ToObject<int>();
                                        loadColumnRange[1] = jArray[1].ToObject<int>();
                                    }
                                    else
                                        throw new JsonException($"LoadColumnRange must be an array of 2 integers (Property {jPropertyObject.Name})");
                                }
                            }
                            else
                                throw new JsonException($"LoadColumnRange must be an array of 2 integers (Property {jPropertyObject.Name})");
                            break;

                        case "VectorType":
                            if (jDefinition.Value.Type == JTokenType.Array)
                            {
                                JArray jArray = (JArray)jDefinition.Value;
                                if (jArray.Count > 0)
                                {
                                    vectorType = new int[jArray.Count];
                                    for (int i = 0; i < jArray.Count; i++)
                                        if (jArray[i].Type == JTokenType.Integer)
                                            vectorType[i] = jArray[i].ToObject<int>();
                                        else
                                            throw new JsonException($"VectorType must be an array of integers (Property {jPropertyObject.Name})");
                                }
                            }
                            else
                                throw new JsonException($"VectorType must be an array of integers (Property {jPropertyObject.Name})");
                            break;

                        default:
                            break;
                    }
                }

                // Validation
                if (propertyType == null) throw new JsonException($"Property {jPropertyObject.Name} must have a type");

                // Add the property
                CreateProperty(tb, jPropertyObject.Name, propertyType, loadColumn, loadColumnRange, columnName, vectorType);
            }

            Type type = tb.CreateType();
            if (register) Set(type);
            return type;
        }

        /// <summary>
        /// Get a type by name.
        /// </summary>
        private static Type? FromName(string typeName)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies().Reverse())
            {
                Type? type = assembly.GetType(typeName);
                if (type != null) return type;
            }
            return null;
        }

        /// <summary>
        /// Get a type from an object (type or type name).
        /// </summary>
        private static Type? FromObject(object? type)
        {
            if (type == null)
                return null;
            else if (type is Type parsedType)
                return parsedType;
            else if (type is string parsedTypeName)
                return parsedTypeName.ToLower().Trim() switch
                {
                    "bool" => typeof(bool),
                    "bool[]" => typeof(bool[]),
                    "byte" => typeof(byte),
                    "byte[]" => typeof(byte[]),
                    "char" => typeof(char),
                    "char[]" => typeof(char[]),
                    "decimal" => typeof(decimal),
                    "decimal[]" => typeof(decimal[]),
                    "double" => typeof(double),
                    "double[]" => typeof(double[]),
                    "float" => typeof(float),
                    "float[]" => typeof(float[]),
                    "int" => typeof(int),
                    "int[]" => typeof(int[]),
                    "long" => typeof(long),
                    "long[]" => typeof(long[]),
                    "sbyte" => typeof(sbyte),
                    "sbyte[]" => typeof(sbyte[]),
                    "short" => typeof(short),
                    "short[]" => typeof(short[]),
                    "uint" => typeof(uint),
                    "uint[]" => typeof(uint[]),
                    "ulong" => typeof(ulong),
                    "ulong[]" => typeof(ulong[]),
                    "ushort" => typeof(ushort),
                    "ushort[]" => typeof(ushort[]),
                    "string" => typeof(string),
                    "string[]" => typeof(string[]),
                    "datetime" => typeof(DateTime[]),
                    "datetime[]" => typeof(DateTime[]),
                    _ => FromName(parsedTypeName)
                };
            else
                throw new ArgumentException($"The type could not be resolved (type: {type?.ToString()})", nameof(type));
        }

        /// <summary>
        /// Create a TypeBuilder object.
        /// </summary>
        private static TypeBuilder CreateTypeBuilder(string name, string assemblyName, string? ns = null, Type? baseType = null)
        {
            ns ??= assemblyName;
            TypeBuilder tb = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run).DefineDynamicModule("MainModule").DefineType($"{ns}.{name}", TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout, baseType);
            tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            return tb;
        }

        /// <summary>
        /// Create a property on a TypeBuilder object.
        /// </summary>
        private static void CreateProperty(TypeBuilder tb, string propertyName, Type type, int[]? loadColumn, int[]? loadColumnRange, string? columnName, int[]? vectorType)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, type, FieldAttributes.Private);
            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, type, null);

            // Create the Get method for the property
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, type, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);
            propertyBuilder.SetGetMethod(getPropMthdBldr);

            // Create the Set method for the property
            MethodBuilder setPropMthdBldr = tb.DefineMethod("set_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[] { type });
            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();
            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);
            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);
            propertyBuilder.SetSetMethod(setPropMthdBldr);

            // Add LoadColumn attribute
            if (loadColumn != null)
            {
                if (loadColumn.Length == 1)
                {
                    ConstructorInfo ac = typeof(LoadColumnAttribute).GetConstructor(new Type[] { typeof(int) }) ?? throw new NullReferenceException($"Failed to create the LoadColumnAttribute of {propertyName}");
                    CustomAttributeBuilder ab = new(ac, new object[] { loadColumn[0] });
                    propertyBuilder.SetCustomAttribute(ab);
                }
                else if (loadColumn.Length > 1)
                {
                    ConstructorInfo ac = typeof(LoadColumnAttribute).GetConstructor(new Type[] { typeof(int[]) }) ?? throw new NullReferenceException($"Failed to create the LoadColumnAttribute of {propertyName}");
                    CustomAttributeBuilder ab = new(ac, new object[] { loadColumn });
                    propertyBuilder.SetCustomAttribute(ab);
                }
            }
            else if (loadColumnRange != null)
            {
                ConstructorInfo ac = typeof(LoadColumnAttribute).GetConstructor(new Type[] { typeof(int), typeof(int) }) ?? throw new NullReferenceException($"Failed to create the LoadColumnAttribute of {propertyName}");
                CustomAttributeBuilder ab = new(ac, new object[] { loadColumnRange[0], loadColumnRange[1] });
                propertyBuilder.SetCustomAttribute(ab);
            }

            // Add ColumnName attribute
            if (columnName != null)
            {
                ConstructorInfo ac = typeof(ColumnNameAttribute).GetConstructor(new Type[] { typeof(string) }) ?? throw new NullReferenceException($"Failed to create the ColumnNameAttribute of {propertyName}");
                CustomAttributeBuilder ab = new(ac, new object[] { columnName });
                propertyBuilder.SetCustomAttribute(ab);
            }

            // Add VectorType attribute
            if (vectorType != null)
            {
                if (vectorType.Length == 1)
                {
                    ConstructorInfo ac = typeof(VectorTypeAttribute).GetConstructor(new Type[] { typeof(int) }) ?? throw new NullReferenceException($"Failed to create the VectorTypeAttribute of {propertyName}");
                    CustomAttributeBuilder ab = new(ac, new object[] { vectorType[0] });
                    propertyBuilder.SetCustomAttribute(ab);
                }
                else if (vectorType.Length > 1)
                {
                    ConstructorInfo ac = typeof(VectorTypeAttribute).GetConstructor(new Type[] { typeof(int[]) }) ?? throw new NullReferenceException($"Failed to create the VectorTypeAttribute of {propertyName}");
                    CustomAttributeBuilder ab = new(ac, new object[] { vectorType });
                    propertyBuilder.SetCustomAttribute(ab);
                }
            }
        }
    }
}
