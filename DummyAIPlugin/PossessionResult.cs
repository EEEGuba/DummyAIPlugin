namespace DummyAIPlugin;

/// <summary>
/// Represents a possession operation result.
/// </summary>
public enum PossessionResult : byte
{
    /// <summary>
    /// Dummy was successfully possessed.
    /// </summary>
    Success,

    /// <summary>
    /// The provided target is not a dummy.
    /// </summary>
    InvalidTarget,

    /// <summary>
    /// The dummy is already AI controlled.
    /// </summary>
    AlreadyTaken,
}
