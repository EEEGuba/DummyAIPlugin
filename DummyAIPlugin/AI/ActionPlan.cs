using System.Collections.Generic;

namespace DummyAIPlugin.AI;

/// <summary>
/// Represents an AI action plan.
/// </summary>
/// <param name="goal">Goal the agent wants to achieve.</param>
/// <param name="actions">Actions AI decided to take.</param>
/// <param name="totalCost">Total cost of all actions combined.</param>
public class ActionPlan(Goal goal, Stack<Action> actions, float totalCost)
{
    /// <summary>
    /// Goal the agent wants to achieve.
    /// </summary>
    public Goal AgentGoal { get; } = goal;

    /// <summary>
    /// Actions AI decided to take.
    /// </summary>
    public Stack<Action> Actions { get; } = actions;

    /// <summary>
    /// Total cost of all actions combined.
    /// </summary>
    public float TotalCost { get; set; } = totalCost;
}
