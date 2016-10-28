﻿namespace Gu.Persist.SystemXml
{
    using System.IO;
    using Gu.Persist.Core;

    /// <inheritdoc/>
    public class XmlEqualsComparer<T> : SerializedEqualsComparer<T>
    {
        /// <summary>
        /// The default instance.
        /// </summary>
        public new static readonly XmlEqualsComparer<T> Default = new XmlEqualsComparer<T>();

        /// <inheritdoc/>
        protected override MemoryStream GetStream(T item)
        {
            return XmlFile.ToStream(item);
        }
    }
}