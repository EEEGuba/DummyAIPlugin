namespace DummyAIPlugin.Navigation;

/// <summary>
/// Represents a source objects collection mode.
/// </summary>
public enum CollectObjects : byte
{
    /// <summary>
    /// All objects should be used.
    /// </summary>
    All,

    /// <summary>
    /// Only objects within a volume should be used.
    /// </summary>
    Volume,

    /// <summary>
    /// Only child objects should be used.
    /// </summary>
    Children,
}
