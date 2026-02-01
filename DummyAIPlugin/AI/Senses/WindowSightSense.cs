using UnityEngine;

namespace DummyAIPlugin.AI.Senses;

/// <summary>
/// A sight sense specialized in windows detection.
/// </summary>
/// <param name="dummy">Target dummy's reference hub.</param>
public class WindowSightSense(ReferenceHub dummy) : SightSense<BreakableWindow>(dummy)
{
    /// <inheritdoc />
    protected override LayerMask LayerMask { get; } = LayerMask.GetMask(Perception.GlassLayer);
}
