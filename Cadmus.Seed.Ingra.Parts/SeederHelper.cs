using Bogus;
using Cadmus.Bricks;
using Cadmus.Itinera.Parts;
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
                    .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                    .RuleFor(r => r.Author, f => f.Lorem.Word())
                    .RuleFor(r => r.Work, f => f.Lorem.Word())
                    .RuleFor(r => r.Location,
                        f => $"{f.Random.Number(1, 24)}.{f.Random.Number(1, 1000)}")
                    .RuleFor(r => r.Note, f => f.Lorem.Sentence())
                    .Generate());
            }

            return refs;
        }

        public static PersonName GetPersonName()
        {
            return new Faker<PersonName>()
                .RuleFor(pn => pn.Language, "eng")
                .RuleFor(pn => pn.Parts, f =>
                    new List<PersonNamePart>(new[]
                    {
                        new PersonNamePart
                        {
                            Type = "first",
                            Value = f.Person.FirstName,
                        },
                        new PersonNamePart
                        {
                            Type = "last",
                            Value = f.Person.LastName,
                        }
                    }))
                .Generate();
        }
    }
}
