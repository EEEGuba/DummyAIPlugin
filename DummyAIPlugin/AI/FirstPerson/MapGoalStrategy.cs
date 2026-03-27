using System;
using System.Collections.Generic;
using System.Linq;
using DummyAIPlugin.AI.Senses;
using DummyAIPlugin.Navigation;
using LabApi.Features.Wrappers;
using PlayerRoles.FirstPersonControl;
using UnityEngine;

namespace DummyAIPlugin.AI.FirstPerson;

/// <summary>
/// Strategy that moves the FPC-controlled dummy along a path produced by MapSense
/// and marks visited rooms in MapMemory.
/// </summary>
public class MapGoalStrategy(Path path,FirstPersonMovementModule fpcModule, MapSense mapSense, MapMemory memory, Room? targetRoom = null) : IActionStrategy
{
    private readonly FirstPersonMovementModule _fpcModule = fpcModule;
    private readonly MapSense _mapSense = mapSense;
    //private readonly MapMemory _memory = memory ?? throw new ArgumentNullException(nameof(memory));
    //private readonly Room? _targetRoom = targetRoom;
    //private readonly float _stopDistance = Math.Max(0.01f, stopDistance);
    private readonly Path _path = path;
    private List<Vector3> _waypoints = [];
    private int _currentIndex;

    // Can perform if we have a path with remaining waypoints.
    public bool CanPerform => _currentIndex < _waypoints.Count;

    // Complete when we've consumed all waypoints.
    public bool Complete => _currentIndex >= _waypoints.Count;
    private Vector3 _currentPos = path.Parent.Position;
    public void Start()
    {

        LabApi.Features.Console.Logger.Info("PLUH start started");
        // Try to determine current room using the dummy position if available.

        var currentRoom = Room.GetRoomAtPosition(_currentPos);
        LabApi.Features.Console.Logger.Info($"{_currentPos},{currentRoom}");
        var roomPath = _mapSense.ComeToHeavy(_currentPos) ?? [];//List<Room>
        // fallback behaviour: ask MapSense for a default path (example ComeToHeavy)
        LabApi.Features.Console.Logger.Info("Come to Heavy got thrown");

        _waypoints = [.. roomPath.Select(r => r.Position)];
        _currentIndex = 0;

        LabApi.Features.Console.Logger.Info($"MapGoalStrategy.Start: computed {_waypoints.Count} waypoint(s).");
    }
    public void Update()
    {
        _currentIndex = _waypoints.Count+1; //{PATCH FIX}
    }

    public void Stop(){}

}