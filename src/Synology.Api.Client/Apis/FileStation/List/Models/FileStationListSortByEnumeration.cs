namespace Synology.Api.Client.Apis.FileStation.List.Models
{
	public static class FileStationListSortByEnumeration
	{
		/// <summary>
		/// Sort by file name
		/// </summary>
		public static readonly string Name = "name";
        /// <summary>
        /// Sort by size
        /// </summary>
        public static readonly string Size = "size";
        /// <summary>
        /// Sort by file owner
        /// </summary>
        public static readonly string FileOwner = "user";
        /// <summary>
        /// Sort by file group
        /// </summary>
        public static readonly string Group = "group";
        /// <summary>
        /// Sort by last modified time
        /// </summary>
        public static readonly string ModifiedTime = "mtime";
        /// <summary>
        /// Sort by last access time
        /// </summary>
        public static readonly string LastAccessTime = "atime";
        /// <summary>
        /// Sort by last change time
        /// </summary>
        public static readonly string LastChangeTime = "ctime";
        /// <summary>
        /// Sort by create time
        /// </summary>
        public static readonly string CreateTime = "crtime";
        /// <summary>
        /// Sort by POSIX permission
        /// </summary>
        public static readonly string PosixPermission = "posix";
        /// <summary>
        /// Sort by file extension
        /// </summary>
        public static readonly string FileExtension = "type";
    }
}

