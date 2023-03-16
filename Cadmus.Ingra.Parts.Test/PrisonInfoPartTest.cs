using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Ingra.Parts;

namespace Cadmus.Ingra.Parts.Test;

public sealed class PrisonInfoPartTest
{
    private static PrisonInfoPart GetPart()
    {
        PrisonInfoPartSeeder seeder = new PrisonInfoPartSeeder();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (PrisonInfoPart)seeder.GetPart(item, null, null);
    }

    private static PrisonInfoPart GetEmptyPart()
    {
        return new PrisonInfoPart
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
        PrisonInfoPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        PrisonInfoPart part2 = TestHelper.DeserializePart<PrisonInfoPart>(json);

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
        PrisonInfoPart part = GetEmptyPart();
        part.PrisonId = "pal";
        part.Place = "Palermo";

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(2, pins.Count);

        DataPin pin = pins.Find(p => p.Name == "id" && p.Value == "pal");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);

        pin = pins.Find(p => p.Name == "place" && p.Value == "palermo");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);
    }
}
