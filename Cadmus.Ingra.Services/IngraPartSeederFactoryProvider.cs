﻿using Cadmus.Core.Config;
using Cadmus.Seed;
using Cadmus.Seed.Parts.General;
using Cadmus.Seed.Philology.Parts.Layers;
using Cadmus.Seed.Ingra.Parts;
using Fusi.Microsoft.Extensions.Configuration.InMemoryJson;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System;
using System.Reflection;
using Cadmus.Seed.Itinera.Parts.Epistolography;

namespace Cadmus.Ingra.Services
{
    /// <summary>
    /// Ingra part seeders provider.
    /// </summary>
    /// <seealso cref="IPartSeederFactoryProvider" />
    public sealed class IngraPartSeederFactoryProvider :
        IPartSeederFactoryProvider
    {
        /// <summary>
        /// Gets the part/fragment seeders factory.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>Factory.</returns>
        /// <exception cref="ArgumentNullException">profile</exception>
        public PartSeederFactory GetFactory(string profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));

            // build the tags to types map for parts/fragments
            Assembly[] seedAssemblies = new[]
            {
                // Cadmus.Seed.Parts
                typeof(NotePartSeeder).Assembly,
                // Cadmus.Seed.Philology.Parts
                typeof(ApparatusLayerFragmentSeeder).Assembly,
                // Cadmus.Seed.Itinera.Parts
                typeof(DocReferencesPartSeeder).GetTypeInfo().Assembly,
                // Cadmus.Seed.Ingra.Parts
                typeof(PrisonInfoPartSeeder).GetTypeInfo().Assembly,
            };
            TagAttributeToTypeMap map = new TagAttributeToTypeMap();
            map.Add(seedAssemblies);

            // build the container for seeders
            Container container = new Container();
            PartSeederFactory.ConfigureServices(
                container,
                new StandardPartTypeProvider(map),
                seedAssemblies);

            container.Verify();

            // load seed configuration
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddInMemoryJson(profile);
            var configuration = builder.Build();

            return new PartSeederFactory(container, configuration);
        }
    }
}
