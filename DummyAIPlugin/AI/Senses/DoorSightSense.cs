using Interactables.Interobjects.DoorUtils;
using UnityEngine;

namespace DummyAIPlugin.AI.Senses;

/// <summary>
/// A sight sense specialized in doors detection.
/// </summary>
/// <param name="dummy">Target dummy's reference hub.</param>
public class DoorSightSense(ReferenceHub dummy) : SightSense<DoorVariant>(dummy)
{
    /// <inheritdoc />
    protected override LayerMask LayerMask { get; } = LayerMask.GetMask(Perception.DoorLayer);
}
