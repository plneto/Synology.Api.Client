using System;

namespace Synology.Api.Client.Apis.FileStation.Search.Models
{
    public class FileStationSearchStartRequest
    {
        /// <summary>
        /// Create a request to search a folder
        /// </summary>
        /// <param name="folderPath">A searched folder path starting with a shared folder. One or more folder paths to be searched, separated by commas "," and around brackets.</param>
        /// <param name="recursive">Optional. If searching files within a folder and subfolders recursively or not.</param>
        /// <param name="pattern">Optional. Search for files whose names and extensions match a case-insensitive glob pattern.</param>
        /// <param name="extension">Optional. Search for files whose extensions match a file type pattern in a case-insensitive glob pattern.</param>
        /// <exception cref="ArgumentException">Exception thrown when an argument from the request is not valid</exception>
        public FileStationSearchStartRequest(
            string folderPath, 
            bool recursive = true, 
            string pattern = "",
            string extension = "")
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentException("FolderPath cannot be null or white space.");
            }
            
            FolderPath = folderPath;
            Pattern = pattern;
            Recursive = recursive;
            Extension = extension;
        }
        
        /// <summary>
        /// A searched folder path starting with a shared folder. One or more folder paths to be searched, separated by commas "," and around brackets.
        /// </summary>
        public string FolderPath { get; }

        /// <summary>
        /// Optional. If searching files within a folder and subfolders recursively or not.
        /// </summary>
        public bool Recursive { get; }

        /// <summary>
        /// Optional. Search for files whose names and extensions match a case-insensitive glob pattern.
        /// Note:
        /// 1. If the pattern doesn't contain any glob syntax (? and *), * of glob syntax will be added at begin and end of
        /// the string automatically for partially matching the pattern.
        /// 2. You can use " " to separate multiple glob patterns.
        /// </summary>
        public string Pattern { get; }

        /// <summary>
        /// Optional. Search for files whose extensions match a file type pattern in a case-insensitive glob pattern.
        /// If you give this criterion, folders aren't matched. Note: You can use commas "," to separate multiple glob patterns.
        /// </summary>
        public string Extension { get; }
    }
}