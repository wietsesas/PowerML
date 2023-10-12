using System;

namespace PowerML.Exceptions
{
    /// <summary>
    /// Base class for path exceptions
    /// </summary>
    public abstract class PathException : Exception
    {
        /// <summary>
        /// Create a new PathException
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="path">Path that caused the exception</param>
        /// <param name="innerException">Original exception</param>
        public PathException(string message, string? path, Exception? innerException) : base($"{message} ({path ?? "null"})", innerException) => Path = path;

        /// <summary>
        /// Path that caused the exception
        /// </summary>
        public string? Path { get; }
    }
}
