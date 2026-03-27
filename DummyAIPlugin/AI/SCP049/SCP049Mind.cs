using DummyAIPlugin.AI.FirstPerson;
using DummyAIPlugin.AI.Senses;
using DummyAIPlugin.Navigation;
using LabApi.Features.Wrappers;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.PlayableScps.Scp049;
using System.Linq;
using UnityEngine;

namespace DummyAIPlugin.AI.SCP049;

/// <summary>
/// AI mind for SCP-049 dummies.
/// </summary>
public class SCP049Mind : Mind,IPathParent
{
    /// <summary>
    /// Sight sense for players detection.
    /// </summary>
    public PlayerSightSense PlayersSense { get; }

    /// <summary>
    /// Map sense for map memory and pathfinding.
    /// </summary>
    public MapSense? MapSense { get; }
    public new MapMemory? MapMemory { get; }
    private readonly FpcMotor _fpcMotor;
    public Vector3 Position => _fpcMotor.Position;

    public Vector3 Destination {get;set;}
    public Path Path{get;}
    /// <summary>
    /// Creates new mind instance.
    /// </summary>
    /// <param name="perception">Perception component used for senses updating.</param>
    /// <param name="role">SCP-049 role reference.</param>
    /// <param name="hub">Target dummy's reference hub.</param>
    public SCP049Mind(Perception perception, Scp049Role role, ReferenceHub hub)
    {
        _fpcMotor = role.FpcModule.Motor;
        PlayersSense = new(hub);
        MapSense = new(hub);
        MapMemory = new MapMemory(Room.List);
        perception.Senses.Add(PlayersSense);
        PlayersSense.FOV = 45.0f;
        var factory = new BeliefFactory(Beliefs);
        const string TargetDetected = "TargetDetected";
        const string NoTargets = "NoTargets";
        Path=new(this);
        factory.AddPredicateBelief(TargetDetected, PlayersSense, s => s.ComponentsWithinSight.Any());
        factory.AddPredicateBelief(NoTargets, PlayersSense, s => !s.ComponentsWithinSight.Any());

        Actions.Add(new Action.Builder("Idle", new IdleStrategy(0.01f))
            .AddEffect(Beliefs[TargetDetected])
            .Build());

        Actions.Add(new Action.Builder("Jump", new JumpStrategy(role.FpcModule))//role.module may break this with duplicate roles, playerID+role may be needed
            .AddPrecondition(Beliefs[TargetDetected])
            .AddEffect(Beliefs[NoTargets])
            .Build());

        Actions.Add(new Action.Builder("GoTo", new MapGoalStrategy(Path,role.FpcModule, MapSense, MapMemory, null),0)
            .AddPrecondition(Beliefs[TargetDetected])
            .AddEffect(Beliefs[NoTargets])
            .Build());

        Goals.Add(new Goal.Builder("IndicateDetection")
            .AddDesiredEffect(Beliefs[NoTargets])
            .Build());

        Goals.Add(new Goal.Builder("DoNothing")
            .AddDesiredEffect(Beliefs[TargetDetected])
            .Build());
    }
}
