using System;
using UnityEngine;

namespace DummyAIPlugin.AI;

/// <summary>
/// Represents an AI belief.
/// </summary>
/// <param name="name">Belief name.</param>
/// <param name="condition">Belief condition.</param>
/// <param name="observedLocation">Belief's observed location.</param>
public class Belief(string name, Func<bool> condition, Func<Vector3>? observedLocation = null)
{
    /// <summary>
    /// Retrieves zero vector.
    /// </summary>
    /// <returns>Zero vector.</returns>
    private static Vector3 GetZeroVector() => Vector3.zero;

    /// <summary>
    /// Retrieves observed location.
    /// </summary>
    public Vector3 Location => _observedLocation();

    /// <summary>
    /// Contains belief name.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Stores belief's condition.
    /// </summary>
    private readonly Func<bool> _condition = condition;

    /// <summary>
    /// Stores belief's observed location.
    /// </summary>
    private readonly Func<Vector3> _observedLocation = observedLocation ?? GetZeroVector;

    /// <summary>
    /// Evaluates this belief.
    /// </summary>
    /// <returns>Condition evaluation result.</returns>
    public bool Evaluate() => _condition();
}
