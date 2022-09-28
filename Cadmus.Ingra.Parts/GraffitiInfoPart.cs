using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;

namespace Cadmus.Ingra.Parts
{
    /// <summary>
    /// Graffiti metadata part.
    /// <para>Tag: <c>it.vedph.ingra.graffiti-info</c>.</para>
    /// </summary>
    [Tag("it.vedph.ingra.graffiti-info")]
    public sealed class GraffitiInfoPart : PartBase
    {
        /// <summary>
        /// Gets or sets the graffiti identifier.
        /// </summary>
        public string GraffitiId { get; set; }

        /// <summary>
        /// Gets or sets the language (ISO 639-3 code).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the verse if any.
        /// </summary>
        public string Verse { get; set; }

        /// <summary>
        /// Gets or sets the rhyme scheme if any.
        /// </summary>
        public string Rhyme { get; set; }

        /// <summary>
        /// Gets or sets the author's name.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the identifications for the author of this graffiti.
        /// </summary>
        public List<RankedId> Identifications { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraffitiInfoPart"/>
        /// class.
        /// </summary>
        public GraffitiInfoPart()
        {
            Identifications = new List<RankedId>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new(
                new StandardDataPinTextFilter());

            builder.AddValue("id", GraffitiId);

            if (!string.IsNullOrEmpty(Language))
                builder.AddValue("language", Language);

            if (!string.IsNullOrEmpty(Verse))
                builder.AddValue("verse", Verse);

            if (!string.IsNullOrEmpty(Author))
                builder.AddValue("author", Author, filter: true, filterOptions: true);

            if (Identifications?.Count > 0)
                builder.AddValues("pid", Identifications.Select(i => i.Id));

            if (Date != null)
                builder.AddValue("date-value", Date.GetSortValue());

            return builder.Build(this);
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
              new DataPinDefinition(DataPinValueType.String,
                "id",
                "The graffiti ID."),
              new DataPinDefinition(DataPinValueType.String,
                "language",
                "The graffiti language."),
              new DataPinDefinition(DataPinValueType.String,
                "verse",
                "The graffiti verse."),
              new DataPinDefinition(DataPinValueType.String,
                "author",
                "The graffiti author.",
                "f"),
              new DataPinDefinition(DataPinValueType.String,
                "pid",
                "The list of identified person(s) ID(s)."),
              new DataPinDefinition(DataPinValueType.Decimal,
                "date-value",
                "The graffiti date value."),
            });
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append("[GraffitiInfo]").Append(' ').Append(GraffitiId);

            if (!string.IsNullOrEmpty(Language))
                sb.Append(" [").Append(Language).Append(']');

            if (!string.IsNullOrEmpty(Author))
                sb.Append(", ").Append(Author);

            if (Date != null)
                sb.Append(": ").Append(Date);

            return sb.ToString();
        }
    }
}
