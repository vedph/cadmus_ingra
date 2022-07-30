using Bogus;
using Cadmus.Bricks;
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
            List<DocReference> refs = new List<DocReference>();

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

        public static ProperName GetPersonName()
        {
            return new Faker<ProperName>()
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
                .Generate();
        }
    }
}
