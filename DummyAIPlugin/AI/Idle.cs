using DummyAIPlugin.Utils;
using UnityEngine;

namespace DummyAIPlugin.AI;

/// <summary>
/// AI idle action.
/// </summary>
public class Idle : IActionStrategy
{
    /// <inheritdoc />
    public bool CanPerform => true;

    /// <inheritdoc />
    public bool Complete { get; private set; }

    /// <summary>
    /// Contains countdown timer instance.
    /// </summary>
    private readonly CountdownTimer timer;

    /// <summary>
    /// Creates new Idle action strategy.
    /// </summary>
    /// <param name="duration">Idling duration.</param>
    public Idle(float duration)
    {
        Complete = false;
        timer = new CountdownTimer(duration, () => Complete = false, () => Complete = true);
    }

    /// <inheritdoc />
    public void Start() => timer.Start();

    /// <inheritdoc />
    public void Update() => timer.Tick(Time.deltaTime);

    /// <inheritdoc />
    public void Stop() {}
}
