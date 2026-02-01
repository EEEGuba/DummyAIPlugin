using Interactables;
using UnityEngine;

namespace DummyAIPlugin.AI.Senses;

/// <summary>
/// A sight sense specialized in interactables detection.
/// </summary>
/// <param name="dummy">Target dummy's reference hub.</param>
public class InteractableSightSense(ReferenceHub dummy) : SightSense<InteractableCollider>(dummy)
{
    /// <inheritdoc />
    protected override LayerMask LayerMask { get; } = LayerMask.GetMask(Perception.InteractableLayer);
}
