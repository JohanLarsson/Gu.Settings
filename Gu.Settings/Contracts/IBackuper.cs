﻿namespace Gu.Settings
{
    using System.IO;

    public interface IBackuper
    {
        /// <summary>
        /// Creates a backup if file exists
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool TryBackup(FileInfo file);

        void Backup(FileInfo file, FileInfo backup);

        /// <summary>
        /// Reads the newest backup if any.
        /// Order:
        /// 1) Soft delete file.
        /// 2) Newest backup if many.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>True if a backup was found and successfully restored. Now File can be read.</returns>
        bool TryRestore(FileInfo file);

        void Restore(FileInfo file);

        void Restore(FileInfo file, FileInfo backup);
        
        void PurgeBackups(FileInfo file);
    }
}