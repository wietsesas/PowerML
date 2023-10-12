using System;

namespace PowerML.Exceptions
{
    /// <summary>
    /// Exception thrown when a directory exists
    /// </summary>
    public sealed class DirectoryExistsException : PathException
    {
        /// <summary>
        /// Create a new DirectoryExistsException
        /// </summary>
        /// <param name="path">Path that caused the exception</param>
        public DirectoryExistsException(string? path) : this(path, (Exception?)null) { }

        /// <summary>
        /// Create a new DirectoryExistsException
        /// </summary>
        /// <param name="path">Path that caused the exception</param>
        /// <param name="innerException">Original exception</param>
        public DirectoryExistsException(string? path, Exception? innerException) : this("The directory exists", path, innerException) { }

        /// <summary>
        /// Create a new DirectoryExistsException
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="path">Path that caused the exception</param>
        public DirectoryExistsException(string message, string? path) : this(message, path, null) { }

        /// <summary>
        /// Create a new DirectoryExistsException
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="path">Path that caused the exception</param>
        /// <param name="innerException">Original exception</param>
        public DirectoryExistsException(string message, string? path, Exception? innerException) : base(message, path, innerException) { }
    }
}
