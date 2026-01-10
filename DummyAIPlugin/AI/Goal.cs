using System.Collections.Generic;

namespace DummyAIPlugin.AI;

/// <summary>
/// Represents an AI goal.
/// </summary>
public class Goal
{
    /// <summary>
    /// Used for easier goal construction.
    /// </summary>
    /// <param name="name">Name of created goal.</param>
    /// <param name="priority">Priority of created goal.</param>
    public class Builder(string name, float priority = 0.0f)
    {
        /// <summary>
        /// Contains constructed goal.
        /// </summary>
        private readonly Goal _goal = new(name, priority);

        /// <summary>
        /// Adds new desired effect to the goal.
        /// </summary>
        /// <param name="effect">Desired effect to add.</param>
        /// <returns>This builder instance.</returns>
        public Builder AddDesiredEffect(Belief effect)
        {
            _goal.DesiredEffects.Add(effect);
            return this;
        }

        /// <summary>
        /// Builds the goal.
        /// </summary>
        /// <returns>Newly constructed goal.</returns>
        public Goal Build() => _goal;
    }

    /// <summary>
    /// Contains goal name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Contains goal priority value.
    /// </summary>
    public float Priority { get; private set; }

    /// <summary>
    /// Stores effects this goal needs to achieve.
    /// </summary>
    public HashSet<Belief> DesiredEffects { get; }

    /// <summary>
    /// Creates new goal.
    /// </summary>
    /// <param name="name">Goal name.</param>
    /// <param name="priority">Goal priority.</param>
    private Goal(string name, float priority)
    {
        Name = name;
        Priority = priority;
        DesiredEffects = [];
    }
}
