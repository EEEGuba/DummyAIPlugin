using DummyAIPlugin.Utils;
using UnityEngine;

namespace DummyAIPlugin.AI;

/// <summary>
/// AI idle action.
/// </summary>
public class IdleStrategy : IActionStrategy
{
    /// <inheritdoc />
    public bool CanPerform => true;

    /// <inheritdoc />
    public bool Complete { get; private set; }

    /// <summary>
    /// Contains countdown timer instance.
    /// </summary>
    private readonly CountdownTimer _timer;

    /// <summary>
    /// Creates new Idle action strategy.
    /// </summary>
    /// <param name="duration">Idling duration.</param>
    public IdleStrategy(float duration)
    {
        Complete = false;
        _timer = new CountdownTimer(duration, () => Complete = false, () => Complete = true);
    }

    /// <inheritdoc />
    public void Start() => _timer.Start();

    /// <inheritdoc />
    public void Update() => _timer.Tick(Time.deltaTime);

    /// <inheritdoc />
    public void Stop() {}
}
