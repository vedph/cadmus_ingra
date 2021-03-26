using System;
using Xunit;
using Cadmus.Core;
using Cadmus.Seed.Ingra.Parts;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Itinera.Parts;
using Fusi.Antiquity.Chronology;

namespace Cadmus.Ingra.Parts.Test
{
    public sealed class PrisonerInfoPartTest
    {
        private static PrisonerInfoPart GetPart()
        {
            PrisonerInfoPartSeeder seeder = new PrisonerInfoPartSeeder();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (PrisonerInfoPart)seeder.GetPart(item, null, null);
        }

        private static PrisonerInfoPart GetEmptyPart()
        {
            return new PrisonerInfoPart
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
            PrisonerInfoPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            PrisonerInfoPart part2 = TestHelper.DeserializePart<PrisonerInfoPart>(json);

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
            PrisonerInfoPart part = GetEmptyPart();
            part.PrisonerId = "bassus";
            part.PrisonId = "p1";
            part.Sex = 'M';
            part.Name = new PersonName
            {
                Language = "lat",
                Parts = new List<PersonNamePart>(new[]
                    {
                        new PersonNamePart
                        {
                            Type = "first",
                            Value = "Stephanus",
                        },
                        new PersonNamePart
                        {
                            Type = "last",
                            Value = "Bassus",
                        }
                    })
            };
            part.BirthDate = HistoricalDate.Parse("1490");
            part.DeathDate = HistoricalDate.Parse("1520");
            part.Origin = "it";
            part.Charge = "heresy";
            part.Judgement = "conviction";
            part.DetentionStart = HistoricalDate.Parse("1517");
            part.DetentionEnd = HistoricalDate.Parse("1519");

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Equal(11, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "id" && p.Value == "bassus");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "prison-id" && p.Value == "p1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "sex" && p.Value == "M");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "name" && p.Value == "stephanus bassus");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "birth-value" && p.Value == "1490");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "death-value" && p.Value == "1520");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "origin" && p.Value == "it");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "charge" && p.Value == "heresy");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "judgement" && p.Value == "conviction");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "det-start-value" && p.Value == "1517");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "det-end-value" && p.Value == "1519");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
        }
    }
}
