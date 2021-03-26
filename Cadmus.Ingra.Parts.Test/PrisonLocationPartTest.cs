using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Ingra.Parts;

namespace Cadmus.Ingra.Parts.Test
{
    public sealed class PrisonLocationPartTest
    {
        private static PrisonLocationPart GetPart()
        {
            PrisonLocationPartSeeder seeder = new PrisonLocationPartSeeder();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (PrisonLocationPart)seeder.GetPart(item, null, null);
        }

        private static PrisonLocationPart GetEmptyPart()
        {
            return new PrisonLocationPart
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
            PrisonLocationPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            PrisonLocationPart part2 = TestHelper.DeserializePart<PrisonLocationPart>(json);

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
            PrisonLocationPart part = GetEmptyPart();
            part.PrisonId = "pal";
            part.Cell = "12";
            part.Location = "B7";

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Equal(3, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "prison-id" && p.Value == "pal");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "cell" && p.Value == "12");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "loc" && p.Value == "B7");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
        }
    }
}
