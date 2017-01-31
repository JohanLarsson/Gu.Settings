﻿namespace Gu.Persist.NewtonsoftJson.Tests.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;

    using Gu.Persist.Core.Tests;

    using Newtonsoft.Json;

    using NUnit.Framework;

    using DataRepositorySettings = Gu.Persist.NewtonsoftJson.DataRepositorySettings;

    public class DataRepositoryIssueReproTests
    {
        public DirectoryInfo TargetDirectory => new DirectoryInfo(@"C:\Temp\Gu.Persist\" + this.GetType().FullName);

        [TearDown]
        public void TearDown()
        {
            this.TargetDirectory.DeleteIfExists(true);
        }

        [Test]
        public void SaveThenReadDummyWithArrayOfInts()
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects,
                Culture = CultureInfo.InvariantCulture,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };

            var settings = new DataRepositorySettings(this.TargetDirectory.FullName, jsonSettings, true, true, null);
            var repository = new DataRepository(settings);
            var dummy = new DummyWith<int[]> { Data = new[] { 1, 2, 3 } };
            repository.Save("dummy", dummy);
            var roundtrip = repository.Read<DummyWith<int[]>>("dummy");
            CollectionAssert.AreEqual(dummy.Data, roundtrip.Data);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SaveThenReadDummyWithReadOnlyObservableCollectionOfInts(bool trackingDirty)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Culture = CultureInfo.InvariantCulture,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };

            jsonSettings.Converters.Add(new ReadOnlyObservableCollectionConverter<int>());
            var settings = new DataRepositorySettings(this.TargetDirectory.FullName, jsonSettings, trackingDirty, true, null);
            var repository = new DataRepository(settings);
            var dummy = new DummyWith<ReadOnlyObservableCollection<int>> { Data = new ReadOnlyObservableCollection<int>(new ObservableCollection<int> { 1, 2, 3 }) };
            repository.Save("dummy", dummy);
            var roundtrip = repository.Read<DummyWith<ReadOnlyObservableCollection<int>>>("dummy");
            CollectionAssert.AreEqual(dummy.Data, roundtrip.Data);
        }

        public class DummyWith<T>
        {
            public T Data { get; set; }
        }

        public class ReadOnlyObservableCollectionConverter<T> : JsonConverter
        {
            public override bool CanWrite { get; } = false;

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotSupportedException();
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(ReadOnlyObservableCollection<T>);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var list = serializer.Deserialize<List<T>>(reader);
                return new ReadOnlyObservableCollection<T>(new ObservableCollection<T>(list));
            }
        }
    }
}