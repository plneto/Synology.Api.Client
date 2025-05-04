using System.Collections.Generic;
using System.Text.Json;

namespace Synology.Api.Client.Extensions;

public static class JsonArrayExtensions
{
    /// <summary>
    /// Serializes one or more folder paths to a JSON array string, e.g. ["path1", "path2"].
    /// This is required by the Synology FileStation API for the folder_path field.
    /// </summary>
    public static string ToJsonArray(this string folderPath)
    {
        return JsonSerializer.Serialize(new[] { folderPath });
    }

    /// <summary>
    /// Serializes multiple folder paths to a JSON array string, if you ever need it.
    /// </summary>
    public static string ToJsonArray(this IEnumerable<string> folderPaths)
    {
        return JsonSerializer.Serialize(folderPaths);
    }
}