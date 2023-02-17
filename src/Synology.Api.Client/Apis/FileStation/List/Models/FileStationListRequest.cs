using System;
using System.Collections.Generic;

namespace Synology.Api.Client.Apis.FileStation.List.Models
{
    public class FileStationListRequest
    {
        /// <summary>
        /// Create a request to enumerate files in a given folder
        /// </summary>
        /// <param name="folderPath">A listed folder path started with a shared folder.</param>
        /// <param name="offset">Optional. Specify how many files are skipped before beginning to return listed files.</param>
        /// <param name="limit">Optional. Number of files requested. 0 indicates to list all files with a given folder.</param>
        /// <param name="sortBy">Optional. Specify which file information to sort on. See <see cref="FileStationListSortByEnumeration"/>.</param>
        /// <param name="sortDirection">Optional. Specify to sort ascending or to sort descending. Options include: asc: sort ascending. desc: sort descending.</param>
        /// <param name="patterns">Optional. Given glob pattern(s) to find files whose names and extensions match a case-insensitive glob pattern.</param>
        /// <param name="fileType">Optional. "file": only enumerate regular files; "dir": only enumerate folders; "all" enumerate regular files and folders.</param>
        /// <param name="goToPath">Optional. Folder path starting with a shared folder. Return all files and sub-folders within <paramref name="folderPath"/> path until <paramref name="goToPath"/> path recursively.</param>
        /// <exception cref="ArgumentException">Exception thrown when an argument from the request is not valid</exception>
        public FileStationListRequest(
            string folderPath,
            int offset = 0,
            int limit = 0,
            string sortBy = null,
            string sortDirection = null,
            IEnumerable<string> patterns = null,
            string fileType = null,
            string goToPath = null)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentException("FolderPath cannot be null or whitespace.", nameof(folderPath));
            }

            this.FolderPath = folderPath;
            this.Offset = offset;
            this.Limit = limit;
            this.SortBy = sortBy;
            this.SortDirection = sortDirection;
            this.Patterns = patterns;
            this.FileType = fileType;
            this.GoToPath = goToPath;
        }

        public string FolderPath { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public IEnumerable<string> Patterns { get; set; } = new List<string>();
        public string FileType { get; set; }
        public string GoToPath { get; set; }
    }
}

