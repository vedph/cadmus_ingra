using Bogus;
using Cadmus.Core;
using Cadmus.Ingra.Parts;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Ingra.Parts
{
    /// <summary>
    /// Part seeder for <see cref="GraffitiInfoPart"/>.
    /// Tag: <c>seed.it.vedph.ingra.graffiti-info</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.ingra.graffiti-info")]
    public sealed class GraffitiInfoPartSeeder : PartSeederBase
    {
        private static List<RankedId> GetIds(int min, int max)
        {
            List<RankedId> ids = new List<RankedId>();
            int count = Randomizer.Seed.Next(min, max + 1);
            for (int n = 1; n <= count; n++)
            {
                ids.Add(new RankedId
                {
                    Id = "p" + n,
                    Rank = (short)n
                });
            }
            return ids;
        }

        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="roleId">The optional part role ID.</param>
        /// <param name="factory">The part seeder factory. This is used
        /// for layer parts, which need to seed a set of fragments.</param>
        /// <returns>A new part.</returns>
        /// <exception cref="ArgumentNullException">item or factory</exception>
        public override IPart GetPart(IItem item, string roleId,
            PartSeederFactory factory)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            GraffitiInfoPart part = new Faker<GraffitiInfoPart>()
                .RuleFor(p => p.GraffitiId, f => f.Lorem.Word())
                .RuleFor(p => p.Language, f => f.PickRandom("ita", "lat"))
                .RuleFor(p => p.Verse, f => f.PickRandom(null, "7s"))
                .RuleFor(p => p.Rhyme, f => f.PickRandom("AABB", "ABAB"))
                .RuleFor(p => p.Author, f => f.Person.FirstName)
                .RuleFor(p => p.Identifications, GetIds(1, 3))
                .RuleFor(p => p.Date, f => HistoricalDate.Parse
                    ($"{f.Random.Number(1500, 1600)} AD"))
                .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
