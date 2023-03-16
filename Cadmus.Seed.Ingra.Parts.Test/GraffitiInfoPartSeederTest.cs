using Cadmus.Core;
using Cadmus.Ingra.Parts;
using Fusi.Tools.Configuration;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Ingra.Parts.Test;

public sealed class GraffitiInfoPartSeederTest
{
    private static readonly PartSeederFactory _factory;
    private static readonly SeedOptions _seedOptions;
    private static readonly IItem _item;

    static GraffitiInfoPartSeederTest()
    {
        _factory = TestHelper.GetFactory();
        _seedOptions = _factory.GetSeedOptions();
        _item = _factory.GetItemSeeder().GetItem(1, "facet");
    }

    [Fact]
    public void TypeHasTagAttribute()
    {
        Type t = typeof(GraffitiInfoPartSeeder);
        TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
        Assert.NotNull(attr);
        Assert.Equal("seed.it.vedph.ingra.graffiti-info", attr.Tag);
    }

    [Fact]
    public void Seed_Ok()
    {
        GraffitiInfoPartSeeder seeder = new GraffitiInfoPartSeeder();
        seeder.SetSeedOptions(_seedOptions);

        IPart part = seeder.GetPart(_item, null, _factory);

        Assert.NotNull(part);

        GraffitiInfoPart p = part as GraffitiInfoPart;
        Assert.NotNull(p);

        TestHelper.AssertPartMetadata(p);

        // TODO: assert properties like:
        // Assert.NotEmpty(p.Works);
    }
}
