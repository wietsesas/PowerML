using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;

namespace PowerML.Validators
{
    /// <summary>
    /// Determines how to validate a path
    /// </summary>
    public enum ValidatePathType
    {
        /// <summary>
        /// The path must be valid
        /// </summary>
        ValidPath,

        /// <summary>
        /// The file or directory must exist
        /// </summary>
        Exists,

        /// <summary>
        /// The file or directory must not exist
        /// </summary>
        Absent,

        /// <summary>
        /// The file must exist
        /// </summary>
        FileExists,

        /// <summary>
        /// The file must not exist
        /// </summary>
        FileAbsent,

        /// <summary>
        /// The directory must exist
        /// </summary>
        DirectoryExists,

        /// <summary>
        /// The directory must not exist
        /// </summary>
        DirectoryAbsent
    }

    /// <summary>
    /// Validate a path or list of path objects
    /// </summary>
    public sealed class ValidatePathAttribute : ValidateArgumentsAttribute
    {
        private readonly ValidatePathType _pathType;

        /// <summary>
        /// Create a new ValidatePathAttribute object
        /// </summary>
        /// <param name="pathType">Determines how to validate a path</param>
        public ValidatePathAttribute(ValidatePathType pathType)
        {
            _pathType = pathType;
        }

        /// <summary>
        /// Validate a path or list of path objects
        /// </summary>
        protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
        {
            if (arguments is string path)
                Validate(path);

            else if (arguments is FileSystemInfo fsInfo)
                Validate(fsInfo.FullName);

            else if (arguments is IEnumerable<string> pathList)
                foreach (string pathItem in pathList) Validate(pathItem);

            else if (arguments is IEnumerable<FileSystemInfo> fsInfoList)
                foreach (FileSystemInfo fsInfoItem in fsInfoList) Validate(fsInfoItem.FullName);

            else if (arguments is IEnumerable objectList)
                foreach (object obj in objectList) Validate(obj?.ToString());

            else
                Validate(arguments?.ToString());
        }

        /// <summary>
        /// Validate a string path
        /// </summary>
        /// <param name="path">The path to validate</param>
        private void Validate(string? path)
        {
            if (_pathType == ValidatePathType.ValidPath) PathTools.Validate(path);
            else if (_pathType == ValidatePathType.Exists) PathTools.ValidateExists(path);
            else if (_pathType == ValidatePathType.Absent) PathTools.ValidateAbsent(path);
            else if (_pathType == ValidatePathType.FileExists) PathTools.ValidateFileExists(path);
            else if (_pathType == ValidatePathType.FileAbsent) PathTools.ValidateFileAbsent(path);
            else if (_pathType == ValidatePathType.DirectoryExists) PathTools.ValidateDirectoryExists(path);
            else if (_pathType == ValidatePathType.DirectoryAbsent) PathTools.ValidateDirectoryAbsent(path);
            else throw new InvalidOperationException();
        }
    }
}
