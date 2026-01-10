namespace DummyAIPlugin.AI;

/// <summary>
/// Implement this interface to create new AI action.
/// </summary>
public interface IActionStrategy
{
    /// <summary>
    /// Tells whether or not this action be performed.
    /// </summary>
    bool CanPerform { get; }

    /// <summary>
    /// Tells whether or not this action is completed.
    /// </summary>
    bool Complete { get; }

    /// <summary>
    /// Starts the action.
    /// </summary>
    void Start();

    /// <summary>
    /// Performs action update.
    /// </summary>
    void Update();

    /// <summary>
    /// Stops the action.
    /// </summary>
    void Stop();
}
