﻿#pragma warning disable 1573
namespace Gu.Persist.RuntimeXml
{
    using System;
    using System.IO;

    using Gu.Persist.Core;

    /// <summary>
    /// A repository reading and saving files using <see cref="System.Runtime.Serialization.DataContractSerializer"/>
    /// </summary>
    public class SingletonRepository : SingletonRepository<RepositorySettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonRepository"/> class.
        /// Uses <see cref="Directories.Default"/>
        /// </summary>
        public SingletonRepository()
            : this(Directories.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonRepository"/> class.
        /// It will use XmlRepositorySettings.DefaultFor(directory) as settings.
        /// </summary>
        public SingletonRepository(PathAndSpecialFolder directory)
            : this(directory.CreateDirectoryInfo())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonRepository"/> class.
        /// It will use XmlRepositorySettings.DefaultFor(directory) as settings.
        /// </summary>
        public SingletonRepository(DirectoryInfo directory)
            : base(directory, () => CreateDefaultSettings(directory), Serialize<RepositorySettings>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonRepository"/> class.
        /// If <paramref name="directory"/> contains a settings file it is read and used.
        /// If not a new default setting is created and saved.
        /// </summary>
        /// <param name="settingsCreator">Creates settings if file is missing</param>
        public SingletonRepository(PathAndSpecialFolder directory, Func<RepositorySettings> settingsCreator)
            : base(directory.CreateDirectoryInfo(), settingsCreator, Serialize<RepositorySettings>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonRepository"/> class.
        /// If <paramref name="directory"/> contains a settings file it is read and used.
        /// If not a new default setting is created and saved.
        /// </summary>
        /// <param name="settingsCreator">Creates settings if file is missing</param>
        public SingletonRepository(DirectoryInfo directory, Func<RepositorySettings> settingsCreator)
            : base(directory, settingsCreator, Serialize<RepositorySettings>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonRepository"/> class.
        /// If <paramref name="directory"/> contains a settings file it is read and used.
        /// If not a new setting is created and saved.
        /// </summary>
        /// <param name="directory">The directory where files will be saved.</param>
        /// <param name="backuper">
        /// The backuper.
        /// Note that a custom backuper may not use the backupsettings.
        /// </param>
        /// <param name="settingsCreator">Creates settings if file is missing</param>
        public SingletonRepository(PathAndSpecialFolder directory, IBackuper backuper, Func<RepositorySettings> settingsCreator)
            : base(directory.CreateDirectoryInfo(), backuper, settingsCreator, Serialize<RepositorySettings>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonRepository"/> class.
        /// If <paramref name="directory"/> contains a settings file it is read and used.
        /// If not a new setting is created and saved.
        /// </summary>
        /// <param name="directory">The directory where files will be saved.</param>
        /// <param name="backuper">
        /// The backuper.
        /// Note that a custom backuper may not use the backupsettings.
        /// </param>
        /// <param name="settingsCreator">Creates settings if file is missing</param>
        public SingletonRepository(DirectoryInfo directory, IBackuper backuper, Func<RepositorySettings> settingsCreator)
            : base(directory, backuper, settingsCreator, Serialize<RepositorySettings>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonRepository"/> class.
        /// </summary>
        public SingletonRepository(RepositorySettings settings)
            : base(settings, Serialize<RepositorySettings>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonRepository"/> class.
        /// </summary>
        public SingletonRepository(RepositorySettings settings, IBackuper backuper)
            : base(settings, backuper, Serialize<RepositorySettings>.Default)
        {
        }
    }
}