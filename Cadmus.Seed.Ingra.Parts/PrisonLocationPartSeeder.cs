using Bogus;
using Cadmus.Core;
using Cadmus.Ingra.Parts;
using Fusi.Tools.Configuration;
using System;

namespace Cadmus.Seed.Ingra.Parts;

/// <summary>
/// Seeder for <see cref="PrisonLocation"/> part.
/// Tag: <c>seed.it.vedph.ingra.prison-location</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.ingra.prison-location")]
public sealed class PrisonLocationPartSeeder : PartSeederBase
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

        PrisonLocationPart part = new Faker<PrisonLocationPart>()
            .RuleFor(p => p.PrisonId, f => f.Lorem.Word().ToLowerInvariant())
            .RuleFor(p => p.Cell, f => $"{f.Random.Number(1, 30)}")
            .RuleFor(p => p.Location,
                f => $"{f.PickRandom('A','B','C','D','E')}{f.Random.Number(1, 10)}")
            .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}
