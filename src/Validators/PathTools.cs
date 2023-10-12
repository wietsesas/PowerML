using PowerML.Exceptions;
using System;
using System.IO;

namespace PowerML.Validators
{
    /// <summary>
    /// Path validation tools
    /// </summary>
    internal static class PathTools
    {
        /// <summary>
        /// Check if a path is valid
        /// </summary>
        /// <param name="path">Path to check</param>
        /// <returns>Full path of the file or directory</returns>
        public static string Validate(string? path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new InvalidPathException(path);
            try { path = Path.GetFullPath(path); }
            catch (Exception ex) { throw new InvalidPathException(path, ex); }
            return path;
        }

        /// <summary>
        /// Check if a file or directory exists
        /// </summary>
        /// <param name="path">Path to check</param>
        /// <returns>Full path of the file or directory</returns>
        public static string ValidateExists(string? path)
        {
            path = Validate(path);
            if (File.Exists(path)) return path;
            if (Directory.Exists(path)) return path;
            throw new FileNotFoundException("File or directory not found", path);
        }

        /// <summary>
        /// Check if a file or directory does not exist
        /// </summary>
        /// <param name="path">Path to check</param>
        /// <param name="delete">Delete the file or directory if it exists</param>
        /// <returns>Full path of the file or directory</returns>
        public static string ValidateAbsent(string? path, bool delete = false)
        {
            path = Validate(path);
            if (File.Exists(path))
            {
                if (delete) File.Delete(path);
                else throw new FileExistsException(path);
            }
            else if (Directory.Exists(path))
            {
                if (delete) Directory.Delete(path, true);
                else throw new DirectoryExistsException(path);
            }
            return path;
        }

        /// <summary>
        /// Check if a file does not exist
        /// </summary>
        /// <param name="path">Path to check</param>
        /// <param name="delete">Delete the file if it exists</param>
        /// <returns>Full path of the file</returns>
        public static string ValidateFileAbsent(string? path, bool delete = false)
        {
            path = Validate(path);
            if (File.Exists(path))
            {
                if (delete) File.Delete(path);
                else throw new FileExistsException(path);
            }
            return path;
        }

        /// <summary>
        /// Check if a directory does not exist
        /// </summary>
        /// <param name="path">Path to check</param>
        /// <param name="delete">Delete the directory if it exists</param>
        /// <returns>Full path of the directory</returns>
        public static string ValidateDirectoryAbsent(string? path, bool delete = false)
        {
            path = Validate(path);
            if (Directory.Exists(path))
            {
                if (delete) Directory.Delete(path, true);
                else throw new DirectoryExistsException(path);
            }
            return path;
        }

        /// <summary>
        /// Check if a file exists
        /// </summary>
        /// <param name="path">Path to check</param>
        /// <returns>Full path of the file</returns>
        public static string ValidateFileExists(string? path)
        {
            path = Validate(path);
            if (File.Exists(path)) return path;
            throw new FileNotFoundException("File not found", path);
        }

        /// <summary>
        /// Check if a directory exists
        /// </summary>
        /// <param name="path">Path to check</param>
        /// <param name="create">Create the directory if it does not exist</param>
        /// <returns>Full path of the directory</returns>
        public static string ValidateDirectoryExists(string? path, bool create = false)
        {
            path = Validate(path);
            if (Directory.Exists(path)) return path;
            if (create) return Directory.CreateDirectory(path).FullName;
            throw new FileNotFoundException("Directory not found", path);
        }
    }
}
