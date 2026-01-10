using System.Collections.Generic;

namespace DummyAIPlugin.AI;

/// <summary>
/// Represents a node in action plan analysis.
/// </summary>
/// <param name="parent">Parent node.</param>
/// <param name="action">Action to take.</param>
/// <param name="effects">Effects required by the node.</param>
/// <param name="cost">Total cost of this node.</param>
public class Node(Node? parent, Action? action, HashSet<Belief> effects, float cost)
{
    /// <summary>
    /// Tells whether or not this leaf node is dead.
    /// </summary>
    public bool IsLeafDead => Leaves.Count < 1 && Action is null;

    /// <summary>
    /// Parent node.
    /// </summary>
    public Node? Parent { get; } = parent;

    /// <summary>
    /// Action to take.
    /// </summary>
    public Action? Action { get; } = action;

    /// <summary>
    /// Effects required by the node.
    /// </summary>
    public HashSet<Belief> RequiredEffects { get; } = [..effects];

    /// <summary>
    /// Child leaf nodes.
    /// </summary>
    public List<Node> Leaves { get; } = [];

    /// <summary>
    /// Total cost of this node.
    /// </summary>
    public float Cost { get; } = cost;
}
