using System;
using Xunit;
using Cadmus.Core;
using Cadmus.Seed.Ingra.Parts;
using System.Collections.Generic;
using System.Linq;
using Fusi.Antiquity.Chronology;

namespace Cadmus.Ingra.Parts.Test;

public sealed class DrawingInfoPartTest
{
    private static DrawingInfoPart GetPart()
    {
        DrawingInfoPartSeeder seeder = new DrawingInfoPartSeeder();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (DrawingInfoPart)seeder.GetPart(item, null, null);
    }

    private static DrawingInfoPart GetEmptyPart()
    {
        return new DrawingInfoPart
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
        DrawingInfoPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        DrawingInfoPart part2 = TestHelper.DeserializePart<DrawingInfoPart>(json);

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);
        // TODO: check parts data here...
    }

    [Fact]
    public void GetDataPins_Tag_1()
    {
        DrawingInfoPart part = GetEmptyPart();
        part.Description = "A description";
        part.Subjects.Add("subject-1");
        part.Subjects.Add("subject-2");
        part.Date = HistoricalDate.Parse("1523 AD");
        part.Color = "red";
        part.Links.Add(new TaggedId
        {
            Id = "target-1",
            Tag = "tag"
        });

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(5, pins.Count);

        DataPin pin = pins.Find(
            p => p.Name == "subject" && p.Value == "subject-1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);

        pin = pins.Find(
            p => p.Name == "subject" && p.Value == "subject-2");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);

        pin = pins.Find(p => p.Name == "date-value" && p.Value == "1523");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);

        pin = pins.Find(p => p.Name == "color" && p.Value == "red");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);

        pin = pins.Find(p => p.Name == "target-id" && p.Value == "target-1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);
    }
}
