using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PowerML
{
    /// <summary>
    /// Cached prediction engines.
    /// </summary>
    internal static class PredictionEngines
    {
        /// <summary>
        /// The prediciton engine cache.
        /// </summary>
        private static readonly Dictionary<TransformerChain<ITransformer>, dynamic> _predictionEngines = new();

        /// <summary>
        /// Get a cached prediction engine.
        /// </summary>
        public static dynamic? Get(TransformerChain<ITransformer> transformer) =>
            _predictionEngines[transformer];

        /// <summary>
        /// Set a cached prediction engine.
        /// </summary>
        public static void Set(TransformerChain<ITransformer> transformer, dynamic predictionEngine) =>
            _predictionEngines[transformer] = predictionEngine;

        /// <summary>
        /// Remove a cached prediction engine.
        /// </summary>
        public static void Remove(TransformerChain<ITransformer> transformer) =>
            _predictionEngines.Remove(transformer);

        /// <summary>
        /// Clear the cached prediction engines
        /// </summary>
        public static void Clear() =>
            _predictionEngines.Clear();

        /// <summary>
        /// Create a new prediction engine.
        /// </summary>
        public static dynamic Create(MLContext context, TransformerChain<ITransformer> transformer, Type inputType, Type outputType, bool ignoreMissingColumns, SchemaDefinition? inputSchema, SchemaDefinition? outputSchema, bool cache)
        {
            if (cache && _predictionEngines.ContainsKey(transformer))
                return _predictionEngines[transformer];

            // Create the prediction engine
            Type[] genericType = new[] { inputType, outputType };
            MethodInfo createPredictionEngine = typeof(ModelOperationsCatalog).GetMethod("CreatePredictionEngine", 2, new Type[] { typeof(ITransformer), typeof(bool), typeof(SchemaDefinition), typeof(SchemaDefinition) })?.MakeGenericMethod(genericType)
                ?? throw new NullReferenceException("Failed to create the prediction engine");

            dynamic predictionEngine;
            try
            {
                predictionEngine = createPredictionEngine.Invoke(context.Model, new object?[] { transformer, ignoreMissingColumns, inputSchema, outputSchema })
                    ?? throw new NullReferenceException("Failed to create the prediction engine");
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null) throw;
                else throw ex.InnerException;
            }

            if (cache) Set(transformer, predictionEngine);
            return predictionEngine;
                
        }

        /// <summary>
        /// Invoke a prediction engine.
        /// </summary>
        public static dynamic Predict(dynamic predictionEngine, Type inputType, Type outputType, object data)
        {
            Type[] genericType = new[] { inputType, outputType };
            MethodInfo predict;
            object result;

            // Create the prediction function
            predict = typeof(PredictionEngine<,>).MakeGenericType(genericType).GetMethod("Predict", new Type[] { inputType })
                ?? throw new NullReferenceException("Failed to get a prediction");

            // Invoke the prediction function
            try
            {
                result = predict.Invoke(predictionEngine, new object?[] { data })
                    ?? throw new NullReferenceException("Failed to get a prediction");
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null) throw;
                else throw ex.InnerException;
            }

            // Return the result
            return result;
        }
    }
}
