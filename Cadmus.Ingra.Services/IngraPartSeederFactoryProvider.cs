using Cadmus.Core.Config;
using Cadmus.Seed;
using Cadmus.Seed.Ingra.Parts;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System;
using System.Reflection;
using Cadmus.Seed.Philology.Parts;
using Fusi.Microsoft.Extensions.Configuration.InMemoryJson;
using Cadmus.Seed.General.Parts;

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
                // Cadmus.Seed.General.Parts
                typeof(NotePartSeeder).Assembly,
                // Cadmus.Seed.Philology.Parts
                typeof(ApparatusLayerFragmentSeeder).Assembly,
                // Cadmus.Seed.Ingra.Parts
                typeof(PrisonInfoPartSeeder).GetTypeInfo().Assembly,
            };
            TagAttributeToTypeMap map = new();
            map.Add(seedAssemblies);

            // build the container for seeders
            Container container = new();
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
