namespace Synology.Api.Client.Apis.FileStation.Search.Models;

public class FileStationSearchStartRequest
{
    /// <summary>
    /// Required. A searched folder path starting with a shared folder. One or more folder paths to be searched, separated by commas "," and around brackets.
    /// </summary>
    public string? FolderPath { get; set; }

    /// <summary>
    /// Optional. If searching files within a folder and subfolders recursively or not.
    /// </summary>
    public bool Recursive { get; set; }

    /// <summary>
    /// Optional. Search for files whose names and extensions match a case-insensitive glob pattern.
    /// Note:
    /// 1. If the pattern doesn't contain any glob syntax (? and *), * of glob syntax will be added at begin and end of
    /// the string automatically for partially matching the pattern.
    /// 2. You can use " " to separate multiple glob patterns.
    /// </summary>
    public string? Pattern { get; set; }

    /// <summary>
    /// Optional. Search for files whose extensions match a file type pattern in a case-insensitive glob pattern.
    /// If you give this criterion, folders aren't matched. Note: You can use commas "," to separate multiple glob patterns.
    /// </summary>
    public string? Extension { get; set; }
    
    /// <summary>
    /// Optional. Search for files whose content matches a case-insensitive glob pattern.
    /// </summary>
    public bool SearchContent { get; set; }
}