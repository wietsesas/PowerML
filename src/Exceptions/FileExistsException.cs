using System;

namespace PowerML.Exceptions
{
    /// <summary>
    /// Exception thrown when a file exists
    /// </summary>
    public sealed class FileExistsException : PathException
    {
        /// <summary>
        /// Create a new FileExistsException
        /// </summary>
        /// <param name="path">Path that caused the exception</param>
        public FileExistsException(string? path) : this(path, (Exception?)null) { }

        /// <summary>
        /// Create a new FileExistsException
        /// </summary>
        /// <param name="path">Path that caused the exception</param>
        /// <param name="innerException">Original exception</param>
        public FileExistsException(string? path, Exception? innerException) : this("The file exists", path, innerException) { }

        /// <summary>
        /// Create a new FileExistsException
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="path">Path that caused the exception</param>
        public FileExistsException(string message, string? path) : this(message, path, null) { }

        /// <summary>
        /// Create a new FileExistsException
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="path">Path that caused the exception</param>
        /// <param name="innerException">Original exception</param>
        public FileExistsException(string message, string? path, Exception? innerException) : base(message, path, innerException) { }
    }
}
