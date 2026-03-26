using UnityEngine;

namespace DummyAIPlugin.Navigation;

/// <summary>
/// Provides information about agent location and destination.
/// </summary>
public interface IPathParent
{
    /// <summary>
    /// Retrieves current agent position.
    /// </summary>
    public Vector3 Position { get; }

    /// <summary>
    /// Retrieves agent's desired location.
    /// </summary>
    public Vector3 Destination { get; }
}
