using UnityEngine;

namespace DummyAIPlugin.Components;

/// <summary>
/// Stores collider data.
/// </summary>
/// <param name="center">Collider center vector.</param>
/// <param name="instanceId">Collider instance id.</param>
public readonly struct ColliderData(Vector3 center, int instanceId)
{
    /// <summary>
    /// Collider center vector.
    /// </summary>
    public Vector3 Center { get; } = center;

    /// <summary>
    /// Collider instance id.
    /// </summary>
    public int InstanceId { get; } = instanceId;
}
