using System.Collections.Generic;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Ingra.Parts
{
    /// <summary>
    /// Location in prison part.
    /// <para>Tag: <c>it.vedph.ingra.prison-location</c>.</para>
    /// </summary>
    [Tag("it.vedph.ingra.prison-location")]
    public sealed class PrisonLocationPart : PartBase
    {
        /// <summary>
        /// Gets or sets the prison identifier.
        /// </summary>
        public string PrisonId { get; set; }

        /// <summary>
        /// Gets or sets the cell in the prison.
        /// </summary>
        public string Cell { get; set; }

        /// <summary>
        /// Gets or sets the location in the cell.
        /// </summary>
        public string Location { get; set; }

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

            builder.AddValue("prison-id", PrisonId);
            builder.AddValue("cell", Cell);
            builder.AddValue("loc", Location);

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
                    "prison-id",
                    "The prison's ID."),
                new DataPinDefinition(DataPinValueType.String,
                    "cell",
                    "The cell."),
                new DataPinDefinition(DataPinValueType.String,
                    "loc",
                    "The location in the cell.")
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
            return $"{PrisonId}.{Cell}@{Location}";
        }
    }
}
