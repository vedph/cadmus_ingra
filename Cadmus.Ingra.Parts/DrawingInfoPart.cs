using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;

namespace Cadmus.Ingra.Parts
{
    /// <summary>
    /// Information about a drawing.
    /// <para>Tag: <c>it.vedph.ingra.drawing-info</c>.</para>
    /// </summary>
    [Tag("it.vedph.ingra.drawing-info")]
    public sealed class DrawingInfoPart : PartBase
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the subjects.
        /// </summary>
        public List<string> Subjects { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the links to graffiti.
        /// </summary>
        public List<TaggedId> Links { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawingInfoPart"/>
        /// class.
        /// </summary>
        public DrawingInfoPart()
        {
            Subjects = new List<string>();
            Links = new List<TaggedId>();
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
                DataPinHelper.DefaultFilter);

            if (Subjects?.Count > 0)
                builder.AddValues("subject", Subjects);

            if (Date != null)
                builder.AddValue("date-value", Date.GetSortValue());

            if (!string.IsNullOrEmpty(Color))
                builder.AddValue("color", Color, filter: true, filterOptions: true);

            if (Links?.Count > 0)
                builder.AddValues("target-id", Links.Select(l => l.Id));

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
                   "subject",
                   "The list of subjects in the drawing.",
                   "M"),
                new DataPinDefinition(DataPinValueType.Decimal,
                   "date",
                   "The drawing's date."),
                new DataPinDefinition(DataPinValueType.String,
                   "color",
                   "The drawing's color.",
                   "f"),
                new DataPinDefinition(DataPinValueType.String,
                   "target-id",
                   "The list of target graffiti IDs.",
                   "M")
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

            sb.Append("[DrawingInfo]");

            if (Subjects?.Count > 0)
                sb.Append(' ').Append(string.Join(", ", Subjects));

            return sb.ToString();
        }
    }
}
