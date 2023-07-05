using System;
using Xunit;
using Cadmus.Core;
using Cadmus.Seed.Ingra.Parts;
using System.Collections.Generic;
using System.Linq;
using Fusi.Antiquity.Chronology;
using Cadmus.Refs.Bricks;

namespace Cadmus.Ingra.Parts.Test;

public sealed class GraffitiInfoPartTest
{
    private static GraffitiInfoPart GetPart()
    {
        GraffitiInfoPartSeeder seeder = new GraffitiInfoPartSeeder();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (GraffitiInfoPart)seeder.GetPart(item, null, null);
    }

    private static GraffitiInfoPart GetEmptyPart()
    {
        return new GraffitiInfoPart
        {
            ItemId = Guid.NewGuid().ToString(),
            RoleId = "some-role",
            CreatorId = "zeus",
            UserId = "another",
        };
    }

    [Fact]
    public void Part_Is_Serializable()
    {
        GraffitiInfoPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        GraffitiInfoPart part2 = TestHelper.DeserializePart<GraffitiInfoPart>(json);

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);
        // TODO: check parts data here...
    }

    [Fact]
    public void GetDataPins_Ok()
    {
        GraffitiInfoPart part = GetEmptyPart();
        part.GraffitiId = "gid";
        part.Language = "lat";
        part.Verse = "7s";
        part.Rhyme = "AABB";
        part.Author = "Stephanus";
        part.Identifications.Add(new AssertedCompositeId
        {
            Target = new PinTarget
            {
                Gid = "steph",
            }
        });
        part.Date = HistoricalDate.Parse("1500");

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(6, pins.Count);

        DataPin pin = pins.Find(p => p.Name == "id" && p.Value == "gid");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);

        pin = pins.Find(p => p.Name == "language" && p.Value == "lat");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);

        pin = pins.Find(p => p.Name == "verse" && p.Value == "7s");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);

        pin = pins.Find(p => p.Name == "author" && p.Value == "stephanus");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);

        pin = pins.Find(p => p.Name == "pid" && p.Value == "steph");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);

        pin = pins.Find(p => p.Name == "date-value" && p.Value == "1500");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);
    }
}
