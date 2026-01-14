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
    /// <param name="belief">Belief to add.</param>
    public void AddBelief(Belief belief) => _beliefs.Add(belief.Name, belief);

    /// <summary>
    /// Adds new belief to the map.
    /// </summary>
    /// <param name="name">Name of new belief.</param>
    /// <param name="condition">New belief's condition.</param>
    public void AddBelief(string name, Func<bool> condition) => AddBelief(new(name, condition));

    /// <summary>
    /// Addds new sense belief to the map.
    /// </summary>
    /// <typeparam name="T">Type of sense used on condition evaluation.</typeparam>
    /// <param name="name">Name of new belief.</param>
    /// <param name="sense">Sense to use on condition evaluation.</param>
    /// <param name="predicate">New belief's sense based condition.</param>
    public void AddSenseBelief<T>(string name, T sense, Predicate<T> predicate) where T : ISense => AddBelief(new(name, () => predicate(sense)));
}
