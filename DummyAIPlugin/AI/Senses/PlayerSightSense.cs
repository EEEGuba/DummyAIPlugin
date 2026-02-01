using PlayerRoles;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DummyAIPlugin.AI.Senses;

/// <summary>
/// A sight sense specialized in players detection.
/// </summary>
public class PlayerSightSense : SightSense<ReferenceHub>
{
    /// <summary>
    /// Contains detected enemies.
    /// </summary>
    public IEnumerable<ReferenceHub> EnemiesWithinSight { get; }

    /// <summary>
    /// Contains detected teammates.
    /// </summary>
    public IEnumerable<ReferenceHub> TeammatesWithinSight { get; }

    /// <summary>
    /// Contains the faction target dummy belongs to.
    /// </summary>
    public Faction Faction { get; }

    /// <inheritdoc />
    protected override LayerMask LayerMask { get; }

    /// <summary>
    /// Initializes new players sight sense instance.
    /// </summary>
    /// <param name="dummy">Target dummy's reference hub.</param>
    public PlayerSightSense(ReferenceHub dummy) : base(dummy)
    {
        EnemiesWithinSight = ComponentsWithinSight.Where(h => h.GetFaction() != Faction && h.GetFaction() != Faction.Unclassified);
        TeammatesWithinSight = ComponentsWithinSight.Where(h => h.GetFaction() == Faction);
        Faction = dummy.GetFaction();
        LayerMask = LayerMask.GetMask(Perception.HitboxLayer);
    }
}
