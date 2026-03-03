using System;
using System.Collections.Generic;
using System.Linq;
using DummyAIPlugin.AI.Senses;
using LabApi.Features.Wrappers;
using PlayerRoles.FirstPersonControl;
using UnityEngine;

namespace DummyAIPlugin.AI.FirstPerson;

/// <summary>
/// Strategy that moves the FPC-controlled dummy along a path produced by MapSense
/// and marks visited rooms in MapMemory.
/// </summary>
public class MapGoalStrategy(FirstPersonMovementModule fpcModule, MapSense mapSense, MapMemory memory, Room? targetRoom = null) : IActionStrategy
{
    private readonly FirstPersonMovementModule _fpcModule = fpcModule ?? throw new ArgumentNullException(nameof(fpcModule));
    private readonly MapSense _mapSense = mapSense ?? throw new ArgumentNullException(nameof(mapSense));
    //private readonly MapMemory _memory = memory ?? throw new ArgumentNullException(nameof(memory));
    //private readonly Room? _targetRoom = targetRoom;
    //private readonly float _stopDistance = Math.Max(0.01f, stopDistance);

    private List<Vector3> _waypoints = [];
    private int _currentIndex;
    private bool _started;

    // Can perform if we have a path with remaining waypoints.
    public bool CanPerform => _waypoints != null && _currentIndex < _waypoints.Count;

    // Complete when we've consumed all waypoints.
    public bool Complete => _waypoints == null || _currentIndex >= _waypoints.Count;

    public void Start()
    {
        if (_started) return;
        _started = true;
        LabApi.Features.Console.Logger.Info("PLUH start started");
        // Try to determine current room using the dummy position if available.
        Vector3 currentPos = Vector3.zero;
        try
        {
            // Most FirstPerson modules expose the character/world position through Motor/MainModule.
            // We try to access it defensively; if unavailable we fallback to Vector3.zero.
            var main = _fpcModule.Motor.MainModule;
            var posProp = main.GetType().GetProperty("Position");
            if (posProp != null) currentPos = (Vector3)posProp.GetValue(main);
        }
        catch
        {
            LabApi.Features.Console.Logger.Info("we fucked up on position");
            // ignore reflection errors, keep position at zero
        }

        Room? currentRoom = Room.GetRoomAtPosition(currentPos);

        List<Room> roomPath;
        // fallback behaviour: ask MapSense for a default path (example ComeToHeavy)
        roomPath = _mapSense.ComeToHeavy() ?? [];
        LabApi.Features.Console.Logger.Info("Come to Heavy got thrown");

        _waypoints = [.. roomPath.Select(r => r.Position)];
        _currentIndex = 0;

        LabApi.Features.Console.Logger.Info($"MapGoalStrategy.Start: computed {_waypoints.Count} waypoint(s).");
    }

    public void Update()
    {
        if (!CanPerform) return;

        // Determine current world position (best-effort).
        Vector3 currentPos = Vector3.zero;
        try
        {
            var main = _fpcModule.Motor.MainModule;
            var posProp = main.GetType().GetProperty("Position");
            if (posProp != null) currentPos = (Vector3)posProp.GetValue(main);
        }
        catch { /* ignore */ }

        var targetPos = _waypoints[_currentIndex];
        var toTarget = targetPos - currentPos;
        //var dist = toTarget.magnitude;

        // If close enough, mark the corresponding room visited and advance.
        //if (dist <= _stopDistance)
        //{
        //    var room = Room.GetRoomAtPosition(targetPos);
        //    if (room != null)
        //    {
        //        _memory.MarkVisited(room, true);
        //        LabApi.Features.Console.Logger.Debug($"MapGoalStrategy: marked visited {room.Name}");
        //    }
        //    _currentIndex++;
        //    return;
        //}

        // Compute direction on the XZ plane to move towards.
        var dir = new Vector3(toTarget.x, 0f, toTarget.z).normalized;

        // Attempt to drive the FPC movement input. APIs differ between projects,
        // attempt a few common member names using reflection so this strategy is resilient.
        TrySetMoveInput(dir);
    }

    public void Stop()
    {
        // Try to zero movement input when stopping.
        TrySetMoveInput(Vector3.zero);
    }

    private void TrySetMoveInput(Vector3 worldDir)
    {
        try
        {
            var main = _fpcModule.Motor.MainModule;
            // Preferred: methods accepting a Vector3 or Vector2 named SetMoveInput / SetDesiredDirection / SetInput
            var t = main.GetType();

            // Vector2 variant (forward, right) based on worldDir projected to local.
            var setInput2 = t.GetMethod("SetMoveInput", [typeof(UnityEngine.Vector2)]);
            if (setInput2 != null)
            {
                // Convert world worldDir to local-forward/right is not available generically,
                // so use forward/back/strafe approximation based on world X/Z.
                var vec2 = new Vector2(worldDir.z, worldDir.x);
                setInput2.Invoke(main, [vec2]);
                return;
            }

            // Vector3 variant
            var setInput3 = t.GetMethod("SetDesiredDirection", [typeof(UnityEngine.Vector3)]) ??
                            t.GetMethod("SetMoveInput", [typeof(UnityEngine.Vector3)]);
            if (setInput3 != null)
            {
                setInput3.Invoke(main, [worldDir]);
                return;
            }

            // Fallback: try a property DesiredVelocity or MoveDirection
            var prop = t.GetProperty("DesiredVelocity") ?? t.GetProperty("MoveDirection");
            if (prop != null && prop.PropertyType == typeof(UnityEngine.Vector3))
            {
                prop.SetValue(main, worldDir);
                return;
            }
        }
        catch (Exception ex)
        {
            LabApi.Features.Console.Logger.Warn($"MapGoalStrategy: setting move input failed: {ex.Message}");
        }
    }
}