using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Cadmus.Itinera.Parts;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;

namespace Cadmus.Ingra.Parts
{
    /// <summary>
    /// Prisoner information.
    /// <para>Tag: <c>it.vedph.ingra.prisoner-info</c>.</para>
    /// </summary>
    [Tag("it.vedph.ingra.prisoner-info")]
    public sealed class PrisonerInfoPart : PartBase
    {
        /// <summary>
        /// Gets or sets the prisoner identifier.
        /// </summary>
        public string PrisonerId { get; set; }

        /// <summary>
        /// Gets or sets the prison identifier.
        /// </summary>
        public string PrisonId { get; set; }

        /// <summary>
        /// Gets or sets the sex: <c>M</c>=male, <c>F</c>=female, else
        /// unknown.
        /// </summary>
        public char Sex { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public PersonName Name { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        public HistoricalDate BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the death date.
        /// </summary>
        public HistoricalDate DeathDate { get; set; }

        /// <summary>
        /// Gets or sets the geographical origin.
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets the charge.
        /// </summary>
        public string Charge { get; set; }

        /// <summary>
        /// Gets or sets the judgement in the prisoner's trial.
        /// </summary>
        public string Judgement { get; set; }

        /// <summary>
        /// Gets or sets the detention start.
        /// </summary>
        public HistoricalDate DetentionStart { get; set; }

        /// <summary>
        /// Gets or sets the detention end.
        /// </summary>
        public HistoricalDate DetentionEnd { get; set; }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                DataPinHelper.DefaultFilter);

            builder.AddValue("id", PrisonerId);
            builder.AddValue("prison-id", PrisonId);
            if (Sex != '\0') builder.AddValue("sex", new string(Sex, 1));
            if (Name != null)
            {
                builder.AddValue("name",
                    DataPinHelper.DefaultFilter.Apply(Name.GetFullName(), true));
            }

            if (BirthDate != null)
                builder.AddValue("birth-value", BirthDate.GetSortValue());

            if (DeathDate != null)
                builder.AddValue("death-value", DeathDate.GetSortValue());

            if (!string.IsNullOrEmpty(Origin))
                builder.AddValue("origin", Origin, filter: true, filterOptions: true);

            if (!string.IsNullOrEmpty(Charge))
                builder.AddValue("charge", Charge, filter: true, filterOptions: true);

            if (!string.IsNullOrEmpty(Judgement))
                builder.AddValue("judgement", Judgement, filter: true, filterOptions: true);

            if (DetentionStart != null)
                builder.AddValue("det-start-value", DetentionStart.GetSortValue());

            if (DetentionEnd != null)
                builder.AddValue("det-end-value", DetentionEnd.GetSortValue());

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
                    "The prisoner's ID."),
                new DataPinDefinition(DataPinValueType.String,
                    "prison-id",
                    "The prison's ID."),
                new DataPinDefinition(DataPinValueType.String,
                    "sex",
                    "The prisoner's sex, if any."),
                new DataPinDefinition(DataPinValueType.String,
                    "name",
                    "The prisoner's name.",
                    "F"),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "birth-value",
                    "The person's birth date's value."),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "death-value",
                    "The person's death date's value."),
                new DataPinDefinition(DataPinValueType.String,
                    "origin",
                    "The prisoner's geographic origin.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "charge",
                    "The prisoner's charge.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "judgement",
                    "The judgement after the prisoner's trial.",
                    "f"),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "det-start-value",
                    "The prisoner's detention start value."),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "det-end-value",
                    "The prisoner's detention end value.")
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
            StringBuilder sb = new StringBuilder();

            sb.Append("[PrisonerInfo] ").Append(PrisonerId);
            if (Name != null)
                sb.Append(": ").Append(Name.GetFullName());

            return sb.ToString();
        }
    }
}
