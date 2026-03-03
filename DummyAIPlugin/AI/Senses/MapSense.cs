using System;
using System.Collections.Generic;
using System.Linq;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MapGeneration;
using PlayerRoles.FirstPersonControl;
using UnityEngine;


namespace DummyAIPlugin.AI.Senses;

public class MapSense(ReferenceHub dummy) //Room.GetRoomAtPosition(Vec3)
{
    private readonly IReadOnlyCollection<Room> _RoomList = Room.List;
    private readonly Vector3 _currentDummyPosition = dummy.GetPosition();

    public void LogIt()
    {
        LabApi.Features.Console.Logger.Info($"Room count: {_RoomList.Count}");

        foreach (Room room in _RoomList)
            LabApi.Features.Console.Logger.Info($"Room pos={room.Position} Name={room.Name} NextTo: {string.Join(", ", room.AdjacentRooms.Select(r => r.Name))}");
    }

    // example helper using Room.FindPath
    public List<Room> ComeToHeavy()
    {
        Room? currentRoom = Room.GetRoomAtPosition(_currentDummyPosition);
        List<Room> workList = [];
        if (currentRoom == null)
        {
            LabApi.Features.Console.Logger.Warn("Current Dummy room is null");
            return [];
        }

        var target = _RoomList.FirstOrDefault(r => string.Equals(r.Name.ToString(), "LczCheckpointA", StringComparison.OrdinalIgnoreCase));
        if (target == null)
        {
            LabApi.Features.Console.Logger.Warn($"LczCheckpointA not found");
            return [];
        }
        workList = Room.FindPath(currentRoom, target) ?? [];
        foreach (Room room in workList)
            LabApi.Features.Console.Logger.Info($"Room pos={room.Position} Name={room.Name} NextTo: {string.Join(", ", room.AdjacentRooms.Select(r => r.Name))}");
        return workList;
    }

    public List<Room> GetPath(Room start, Room end)
    {
        return Room.FindPath(start, end);
    }
}