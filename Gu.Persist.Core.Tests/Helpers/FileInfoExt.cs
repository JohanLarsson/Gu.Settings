﻿namespace Gu.Persist.Core.Tests
{
    using System.IO;

    using NUnit.Framework;

    public static class TestFileInfoExt
    {
        public static FileInfo TempFile(this FileInfo file, IRepositorySettings settings)
        {
            return file.WithAppendedExtension(settings.TempExtension);
        }

        public static void CreateFileOnDisk(this FileInfo file)
        {
            CreateFileOnDisk(file, file.FullName);
        }

        public static void CreateFileOnDisk(this FileInfo file, string text)
        {
            Assert.NotNull(file.Directory);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            File.WriteAllText(file.FullName, text);
        }

        public static void WriteAllText(this FileInfo file, string text)
        {
            File.WriteAllText(file.FullName, text);
        }

        public static string ReadAllText(this FileInfo file)
        {
            return File.ReadAllText(file.FullName);
        }
    }
}
