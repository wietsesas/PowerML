using Microsoft.ML;

namespace PowerML
{
    /// <summary>
    /// Holds the current context.
    /// </summary>
    internal static class CurrentContext
    {
        /// <summary>
        /// The current context.
        /// </summary>
        private static MLContext? _context = null;

        /// <summary>
        /// Returns the current context.
        /// </summary>
        public static MLContext? Get() =>
            _context;

        /// <summary>
        /// Returns the current context. If there is no current context, a context is created.
        /// </summary>
        public static MLContext GetOrCreate()
        {
            _context ??= new();
            return _context;
        }

        /// <summary>
        /// Set the current context.
        /// </summary>
        public static void Set(MLContext? context) =>
            _context = context;
    }
}
