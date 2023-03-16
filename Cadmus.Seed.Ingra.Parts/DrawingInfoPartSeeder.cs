using Bogus;
using Cadmus.Core;
using Cadmus.Ingra.Parts;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Ingra.Parts;

/// <summary>
/// Part seeder for <see cref="DrawingInfoPart"/>.
/// Tag: <c>seed.it.vedph.ingra.drawing-info</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.ingra.drawing-info")]
public sealed class DrawingInfoPartSeeder : PartSeederBase
{
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

        DrawingInfoPart part = new Faker<DrawingInfoPart>()
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            // TODO: add thesaurus-derived values for subjects
            .RuleFor(p => p.Subjects, f =>
                new List<string>(new[] {f.PickRandom("man", "animal")}))
            .RuleFor(p => p.Date, f =>
                HistoricalDate.Parse($"{f.Random.Number(1500, 1600)} AD"))
            .RuleFor(p => p.Color, f => f.PickRandom("black", "red"))
            .RuleFor(p => p.Links, f => f.Random.Bool(0.2f)?
                new List<TaggedId>(new TaggedId[]
                {
                    new TaggedId
                    {
                        Id = f.Lorem.Word().ToLowerInvariant(),
                        Tag = f.Lorem.Word().ToLowerInvariant()
                    }
                }) : null)
            .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}
