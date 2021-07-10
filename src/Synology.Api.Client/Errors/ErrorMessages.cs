using System.Collections.Generic;

namespace Synology.Api.Client.Errors
{
    public static class ErrorMessages
    {
        public static Dictionary<int, string> CommonErrors => new Dictionary<int, string>
        {
            { 100, "Unknown error" },
            { 101, "No parameter of API, method or version" },
            { 102, "The requested API does not exist" },
            { 103, "The requested method does not exist" },
            { 104, "The requested version does not support the functionality" },
            { 105, "The logged in session does not have permission" },
            { 106, "Session timeout" },
            { 107, "Session interrupted by duplicate login" },
            { 119, "SID not found" },
        };

        // SYNO.API.Auth
        public static Dictionary<int, string> AuthApiErrors => new Dictionary<int, string>
        {
            { 400, "No such account or incorrect password" },
            { 401, "Account disabled" },
            { 402, "Permission denied" },
            { 403, "2-step verification code required" },
            { 404, "Failed to authenticate 2-step verification code" },
        };

        public static Dictionary<int, string> FileStationApiErrors => new Dictionary<int, string>
        {
            // All APIs
            { 400, "Invalid parameter of file operation" },
            { 401, "Unknown error of file operation" },
            { 402, "System is too busy" },
            { 403, "Invalid user does this file operation" },
            { 404, "Invalid group does this file operation" },
            { 405, "Invalid user and group does this file operation" },
            { 406, "Can't get user/group information from the account server" },
            { 407, "Operation not permitted" },
            { 408, "No such file or directory" },
            { 409, "Non-supported file system" },
            { 410, "Failed to connect internet-based file system (e.g., CIFS)" },
            { 411, "Read-only file system" },
            { 412, "Filename too long in the non-encrypted file system" },
            { 413, "Filename too long in the encrypted file system" },
            { 414, "File already exists" },
            { 415, "Disk quota exceeded" },
            { 416, "No space left on device" },
            { 417, "Input/output error" },
            { 418, "Illegal name or path" },
            { 419, "Illegal file name" },
            { 420, "Illegal file name on FAT file system" },
            { 421, "Device or resource busy" },
            { 599, "No such task of the file operation" },

            // Favorite API - SYNO.FileStation.Favorite
            { 800, "A folder path of favorite folder is already added to user's favorites" },
            { 801, "A name of favorite folder conflicts with an existing folder path in the user's favorites" },
            { 802, "There are too many favorites to be added" },

            // Delete API - SYNO.FileStation.Delete
            { 900, "Failed to delete file(s)/folder(s). More information in <errors> object." },

            // Copy Move API - SYNO.FileStation.CopyMove
            { 1000, "Failed to copy files/folders. More information in <errors> object" },
            { 1001, "Failed to move files/folders. More information in <errors> object" },
            { 1002, "An error occurred at the destination. More information in <errors> object" },
            { 1003, "Cannot overwrite or skip the existing file because no overwrite parameter is given" },
            { 1004, "File cannot overwrite a folder with the same name, or folder cannot overwrite a file with the same name" },
            { 1006, "Cannot copy/move file/folder with special characters to a FAT32 file system" },
            { 1007, "Cannot copy/move a file bigger than 4G to a FAT32 file system" },

            // Create Folder API - SYNO.FileStation.CreateFolder
            { 1100, "Failed to create a folder. More information in <errors> object" },
            { 1101, "The number of folders to the parent folder would exceed the system limitation"},

            // Rename API - SYNO.FileStation.Rename
            { 1200, "Failed to rename it. More information in <errors> object" },

            // Compress API - SYNO.FileStation.Compress
            { 1300, "Failed to compress files/folders" },
            { 1301, "Cannot create the archive because the given archive name is too long" },

            // Extract API - SYNO.FileStation.Extract
            { 1400, "Failed to extract files" },
            { 1401, "Cannot open the file as archive" },
            { 1402, "Failed to read archive data error" },
            { 1403, "Wrong password" },
            { 1404, "Failed to get the file and dir list in an archive" },
            { 1405, "Failed to find the item ID in an archive file" },

            // Upload API - SYNO.FileStation.Upload
            { 1800, "There is no Content-Length information in the HTTP header or the received size doesn't match the value of Content-Length information in the HTTP header" },
            { 1801, "Wait too long, no date can be received from client (Default maximum wait time is 3600 seconds)" },
            { 1802, "No filename information in the last part of file content" },
            { 1803, "Upload connection is cancelled" },
            { 1804, "Failed to upload oversized file to FAT file system" },
            { 1805, "Can't overwrite or skip the existing file, if no overwrite parameter is given" },

            // Sharing API - SYNO.FileStation.Sharing
            { 2000, "Sharing link does not exist" },
            { 2001, "Cannot generate sharing link because too many sharing links exist" },
            { 2002, "Failed to access sharing links" }
        };

        // SYNO.DownloadStation.Task
        public static Dictionary<int, string> DownloadStationTaskApiErrors => new Dictionary<int, string>
        {
            { 400, "File upload failed" },
            { 401, "Max number of tasks reached" },
            { 402, "Destination denied" },
            { 403, "Destination does not exist" },
            { 404, "Invalid task id" },
            { 405, "Invalid task action" },
            { 406, "No default destination" },
            { 407, "Set destination failed" },
            { 408, "File does not exist" }
        };

        // SYNO.DownloadStation.BTSearch
        public static Dictionary<int, string> DownloadStationBtSearchApiErrors => new Dictionary<int, string>
        {
            { 400, "Unknown error" },
            { 401, "Invalid parameter" },
            { 402, "Parse the user setting failed" },
            { 403, "Get category failed" },
            { 404, "Get the search result from DB failed" },
            { 405, "Get the user setting failed" }
        };
    }
}
