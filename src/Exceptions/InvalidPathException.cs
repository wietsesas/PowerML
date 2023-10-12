using System;

namespace PowerML.Exceptions
{
    /// <summary>
    /// Exception thrown when a path is invalid
    /// </summary>
    public sealed class InvalidPathException : PathException
    {
        /// <summary>
        /// Create a new InvalidPathException
        /// </summary>
        /// <param name="path">Path that caused the exception</param>
        public InvalidPathException(string? path) : this(path, (Exception?)null) { }

        /// <summary>
        /// Create a new InvalidPathException
        /// </summary>
        /// <param name="path">Path that caused the exception</param>
        /// <param name="innerException">Original exception</param>
        public InvalidPathException(string? path, Exception? innerException) : this("Invaid path", path, innerException) { }

        /// <summary>
        /// Create a new InvalidPathException
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="path">Path that caused the exception</param>
        public InvalidPathException(string message, string? path) : this(message, path, null) { }

        /// <summary>
        /// Create a new InvalidPathException
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="path">Path that caused the exception</param>
        /// <param name="innerException">Original exception</param>
        public InvalidPathException(string message, string? path, Exception? innerException) : base(message, path, innerException) { }
    }
}
