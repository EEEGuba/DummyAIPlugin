using System.Collections.Generic;

namespace DummyAIPlugin.AI;

/// <summary>
/// Represents an AI action.
/// </summary>
public class Action
{
    /// <summary>
    /// Used for easier action construction.
    /// </summary>
    /// <param name="name">Name of created action.</param>
    /// <param name="strategy">Actual implementation of created action.</param>
    /// <param name="cost">Cost of created action.</param>
    public class Builder(string name, IActionStrategy strategy, float cost = 1.0f)
    {
        /// <summary>
        /// Contains constructed action.
        /// </summary>
        private readonly Action _action = new(name, cost, strategy);

        /// <summary>
        /// Adds new precondition to the action.
        /// </summary>
        /// <param name="precondition">Precondition to add.</param>
        /// <returns>This builder instance.</returns>
        public Builder AddPrecondition(Belief precondition)
        {
            _action.Preconditions.Add(precondition);
            return this;
        }

        /// <summary>
        /// Adds new effect to the action.
        /// </summary>
        /// <param name="effect">Effect to add.</param>
        /// <returns>This builder instance.</returns>
        public Builder AddEffect(Belief effect)
        {
            _action.Effects.Add(effect);
            return this;
        }

        /// <summary>
        /// Builds the action.
        /// </summary>
        /// <returns>Newly constructed action.</returns>
        public Action Build() => _action;
    }

    /// <summary>
    /// Tells whether or not this action is completed.
    /// </summary>
    public bool Complete => _strategy.Complete;

    /// <summary>
    /// Contains action name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Cost for performing this action.
    /// </summary>
    public float Cost { get; private set; }

    /// <summary>
    /// Stores conditions which need to be met before execution.
    /// </summary>
    public HashSet<Belief> Preconditions { get; }

    /// <summary>
    /// Stores effects this action will cause.
    /// </summary>
    public HashSet<Belief> Effects { get; }

    /// <summary>
    /// Contains action strategy implementation.
    /// </summary>
    private readonly IActionStrategy _strategy;

    /// <summary>
    /// Creates new action.
    /// </summary>
    /// <param name="name">Action name.</param>
    /// <param name="cost">Action cost.</param>
    /// <param name="strategy">Action strategy implementation.</param>
    private Action(string name, float cost, IActionStrategy strategy)
    {
        Name = name;
        Cost = cost;
        Preconditions = [];
        Effects = [];
        _strategy = strategy;
    }

    /// <summary>
    /// Starts the action.
    /// </summary>
    public void Start() => _strategy.Start();

    /// <summary>
    /// Performs action update.
    /// </summary>
    public void Update()
    {
        if (_strategy.CanPerform)
        {
            _strategy.Update();
        }

        if (!_strategy.Complete)
        {
            return;
        }

        foreach (var effect in Effects)
        {
            effect.Evaluate();
        }
    }

    /// <summary>
    /// Stops the action.
    /// </summary>
    public void Stop() => _strategy.Stop();
}
