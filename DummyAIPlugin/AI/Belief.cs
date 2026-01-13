using System;

namespace DummyAIPlugin.AI;

/// <summary>
/// Represents an AI belief.
/// </summary>
/// <param name="name">Belief name.</param>
/// <param name="condition">Belief condition.</param>
public class Belief(string name, Func<bool> condition)
{
    /// <summary>
    /// Contains belief name.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Stores belief's condition.
    /// </summary>
    private readonly Func<bool> _condition = condition;

    /// <summary>
    /// Evaluates this belief.
    /// </summary>
    /// <returns>Condition evaluation result.</returns>
    public bool Evaluate() => _condition();
}
