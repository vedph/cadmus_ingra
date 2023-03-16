namespace Cadmus.Ingra.Parts;

/// <summary>
/// An ID with a rank.
/// </summary>
public class RankedId
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the rank for this identification; 0=not specified,
    /// else a rank value where 1=maximum probability.
    /// </summary>
    public short Rank { get; set; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"{Id}: {Rank}";
    }
}
