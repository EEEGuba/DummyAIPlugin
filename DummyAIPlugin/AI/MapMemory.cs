using System;
using System.Collections.Generic;
using System.Linq;
using LabApi.Features.Wrappers;


namespace DummyAIPlugin.AI;

// Simple per-AI map memory built from a collection of Room objects.
// Stores a node per Room with basic metadata and a visited flag.
public class MapMemory
{
    private readonly Dictionary<Room, Node> _map;

    public MapMemory(IReadOnlyCollection<Room> rooms)
    {
        _map = [];
        UpdateFromRooms(rooms);
    }

    public IEnumerable<Node> Nodes => _map.Values;

    public int Count => _map.Count;

    public void UpdateFromRooms(IReadOnlyCollection<Room> rooms)
    {
        if (rooms == null) throw new ArgumentNullException(nameof(rooms));

        // Add new rooms
        foreach (var r in rooms)
        {
            if (r == null) continue;
            if (!_map.ContainsKey(r)) _map[r] = new Node(r);
        }

        // Remove rooms that no longer exist in the provided collection
        var toRemove = _map.Keys.Except(rooms).ToList();
        foreach (var key in toRemove) _map.Remove(key);
    }

    public bool TryGetNode(Room room, out Node? node)
    {
        if (room == null) { node = null; return false; }
        return _map.TryGetValue(room, out node);
    }

    public Node GetOrCreateNode(Room room)
    {
        if (room == null) throw new ArgumentNullException(nameof(room));
        if (!_map.TryGetValue(room, out var node))
        {
            node = new Node(room);
            _map[room] = node;
        }
        return node;
    }

    public bool IsVisited(Room room)
    {
        return TryGetNode(room, out var n) && n.Visited;
    }

    public void MarkVisited(Room room, bool visited = true)
    {
        var n = GetOrCreateNode(room);
        n.Visited = visited;
    }

    public void SetMetadata(Room room, string key, object value)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        var n = GetOrCreateNode(room);
        n.Metadata[key] = value;
    }

    public bool TryGetMetadata<T>(Room room, string key, out T value)
    {
        value = default;
        if (key == null) return false;
        if (!TryGetNode(room, out var n)) return false;
        if (!n.Metadata.TryGetValue(key, out var o)) return false;
        if (o is T t)
        {
            value = t;
            return true;
        }
        try
        {
            value = (T)Convert.ChangeType(o, typeof(T));
            return true;
        }
        catch
        {
            return false;
        }
    }

    // Simple node representing a Room in the memory map.
    public class Node(Room room)
    {
        public Room Room { get; } = room ?? throw new ArgumentNullException(nameof(room));
        public bool Visited { get; set; }
        public Dictionary<string, object> Metadata { get; } = new Dictionary<string, object>();
    }
}
