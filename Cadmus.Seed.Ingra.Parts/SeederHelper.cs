using Bogus;
using Cadmus.Refs.Bricks;
using System.Collections.Generic;

namespace Cadmus.Seed.Ingra.Parts
{
    internal static class SeederHelper
    {
        /// <summary>
        /// Gets a random number of document references.
        /// </summary>
        /// <param name="min">The min number of references to get.</param>
        /// <param name="max">The max number of references to get.</param>
        /// <returns>References.</returns>
        public static List<DocReference> GetDocReferences(int min, int max)
        {
            List<DocReference> refs = new();

            for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
            {
                refs.Add(new Faker<DocReference>()
                    .RuleFor(r => r.Type, f => f.PickRandom(null, "biblio"))
                    .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                    .RuleFor(r => r.Citation,
                        f => $"{f.Random.Number(1, 24)}.{f.Random.Number(1, 1000)}")
                    .RuleFor(r => r.Note, f => f.Random.Bool(0.25f)
                        ? f.Lorem.Sentence() : null)
                    .Generate());
            }

            return refs;
        }

        public static Assertion GetAssertion()
        {
            return new Faker<Assertion>()
                .RuleFor(a => a.Tag, f => f.PickRandom("a", "b", null))
                .RuleFor(a => a.Rank, f => f.Random.Short(1, 3))
                .RuleFor(a => a.References, GetDocReferences(1, 2))
                .RuleFor(a => a.Note, f => f.Lorem.Sentence().OrNull(f))
                .Generate();
        }

        public static AssertedProperName GetPersonName()
        {
            return new Faker<AssertedProperName>()
                .RuleFor(pn => pn.Language, "eng")
                .RuleFor(pn => pn.Pieces, f =>
                    new List<ProperNamePiece>(new[]
                    {
                        new ProperNamePiece
                        {
                            Type = "first",
                            Value = f.Person.FirstName,
                        },
                        new ProperNamePiece
                        {
                            Type = "last",
                            Value = f.Person.LastName,
                        }
                    }))
                .RuleFor(pn => pn.Assertion, GetAssertion())
                .Generate();
        }
    }
}
