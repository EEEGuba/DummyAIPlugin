using System;
using System.Collections.Generic;

namespace DummyAIPlugin.AI;

/// <summary>
/// Used to easily create new beliefs.
/// </summary>
/// <param name="beliefs">Beliefs map to fill.</param>
public class BeliefFactory(Dictionary<string, Belief> beliefs)
{
    /// <summary>
    /// Contains the beliefs map to fill.
    /// </summary>
    private readonly Dictionary<string, Belief> _beliefs = beliefs;

    /// <summary>
    /// Adds new belief to the map.
    /// </summary>
    /// <param name="key">Name of new belief.</param>
    /// <param name="condition">New belief's condition.</param>
    public void AddBelief(string key, Func<bool> condition) => _beliefs.Add(key, new(key, condition));
}
