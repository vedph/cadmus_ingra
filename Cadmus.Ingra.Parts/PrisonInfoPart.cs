using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Ingra.Parts
{
    /// <summary>
    /// Prison metadata.
    /// <para>Tag: <c>it.vedph.ingra.prison-info</c>.</para>
    /// </summary>
    [Tag("it.vedph.ingra.prison-info")]
    public sealed class PrisonInfoPart : PartBase
    {
        /// <summary>
        /// Gets or sets the prison identifier.
        /// </summary>
        public string PrisonId { get; set; }

        /// <summary>
        /// Gets or sets the prison's place. You can use a comma-delimited
        /// string like that of a reverse lookup geocoding service.
        /// </summary>
        public string Place { get; set; }

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

            builder.AddValue("id", PrisonId);
            if (!string.IsNullOrEmpty(Place))
                builder.AddValue("place", Place, filter: true, filterOptions: true);

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
                    "The prison ID."),
                new DataPinDefinition(DataPinValueType.String,
                    "place",
                    "The prison's place.",
                    "f")
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

            sb.Append("[PrisonInfo]").Append(' ').Append(Id);

            if (!string.IsNullOrEmpty(Place))
                sb.Append(": ").Append(Place);

            return sb.ToString();
        }
    }
}
