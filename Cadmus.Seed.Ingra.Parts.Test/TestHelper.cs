﻿using Cadmus.Core;
using Cadmus.Core.Config;
using Cadmus.Ingra.Parts;
using Fusi.Microsoft.Extensions.Configuration.InMemoryJson;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace Cadmus.Seed.Ingra.Parts.Test
{
    static internal class TestHelper
    {
        static public Stream GetResourceStream(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            return Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    $"Cadmus.Seed.Ingra.Parts.Test.Assets.{name}");
        }

        static public string LoadResourceText(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            using (StreamReader reader = new StreamReader(
                GetResourceStream(name),
                Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        static public PartSeederFactory GetFactory()
        {
            // map
            TagAttributeToTypeMap map = new TagAttributeToTypeMap();
            map.Add(new[]
            {
                // Cadmus.Core
                typeof(StandardItemSortKeyBuilder).Assembly,
                // Cadmus.Ingra.Parts
                typeof(DrawingInfoPart).Assembly
                // Cadmus.Philology.Parts
                // typeof(ApparatusLayerFragment).Assembly
            });

            // container
            Container container = new Container();
            PartSeederFactory.ConfigureServices(
                container,
                new StandardPartTypeProvider(map),
                new[]
                {
                    // Cadmus.Seed.Parts
                    // typeof(NotePartSeeder).Assembly,
                    // Cadmus.Seed.Philology.Parts
                    // typeof(ApparatusLayerFragmentSeeder).Assembly
                    // Cadmus.Seed.Ingra.Parts
                    typeof(DrawingInfoPartSeeder).Assembly
                });

            // config
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddInMemoryJson(LoadResourceText("SeedConfig.json"));
            var configuration = builder.Build();

            return new PartSeederFactory(container, configuration);
        }

        static public void AssertPartMetadata(IPart part)
        {
            Assert.NotNull(part.Id);
            Assert.NotNull(part.ItemId);
            Assert.NotNull(part.UserId);
            Assert.NotNull(part.CreatorId);
        }
    }
}
