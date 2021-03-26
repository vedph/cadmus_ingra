using Cadmus.Core;
using Cadmus.Ingra.Parts;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Ingra.Parts.Test
{
    public sealed class PrisonInfoPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static PrisonInfoPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(PrisonInfoPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.ingra.prison-info", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            PrisonInfoPartSeeder seeder = new PrisonInfoPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            PrisonInfoPart p = part as PrisonInfoPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            // TODO: assert properties like:
            // Assert.NotEmpty(p.Works);
        }
    }
}
