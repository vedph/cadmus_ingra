using Bogus;
using Cadmus.Core;
using Cadmus.Ingra.Parts;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Configuration;
using System;

namespace Cadmus.Seed.Ingra.Parts;

/// <summary>
/// Part seeder for <see cref="PrisonerInfoPart"/>.
/// Tag: <c>seed.it.vedph.ingra.prisoner-info</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.ingra.prisoner-info")]
public sealed class PrisonerInfoPartSeeder : PartSeederBase
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

        int birth = Randomizer.Seed.Next(1500, 1520);

        PrisonerInfoPart part = new Faker<PrisonerInfoPart>()
            .RuleFor(p => p.PrisonerId, f => f.Lorem.Word().ToLowerInvariant())
            .RuleFor(p => p.PrisonId, f => f.Lorem.Word().ToLowerInvariant())
            .RuleFor(p => p.Sex, f => f.PickRandom('M', 'F'))
            .RuleFor(p => p.Name, SeederHelper.GetPersonName())
            .RuleFor(p => p.BirthDate, HistoricalDate.Parse($"{birth} AD"))
            .RuleFor(p => p.DeathDate, HistoricalDate.Parse($"{birth + 40} AD"))
            // TODO: use thesauri for these data
            .RuleFor(p => p.Origin, f => f.PickRandom("it", "fr", "sp"))
            .RuleFor(p => p.Charge, f => f.PickRandom(
                "heresy", "blasphemy", "witchcraft"))
            .RuleFor(p => p.Judgement,
                f => f.PickRandom("conviction", "acquittal"))
            .RuleFor(p => p.DetentionStart,
                HistoricalDate.Parse($"{birth + 20} AD"))
            .RuleFor(p => p.DetentionEnd,
                HistoricalDate.Parse($"{birth + 40} AD"))
            .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}
