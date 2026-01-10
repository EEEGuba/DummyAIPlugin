using System;

namespace DummyAIPlugin.Utils;

/// <summary>
/// A simple countdown timer.
/// </summary>
/// <param name="value">Initial time value.</param>
/// <param name="onStart">Action to perform on start.</param>
/// <param name="onStop">Action to perform on stop.</param>
public class CountdownTimer(float value, Action? onStart = null, Action? onStop = null)
{
    /// <summary>
    /// Tells whether or not the timer has finished the countdown.
    /// </summary>
    public bool IsFinished => Time <= 0.0f;

    /// <summary>
    /// Contains initial time value.
    /// </summary>
    public float InitialTime { get; private set; } = value;

    /// <summary>
    /// Contains remaining time amount.
    /// </summary>
    public float Time { get; set; } = 0.0f;

    /// <summary>
    /// Tells whether or not the timer is running.
    /// </summary>
    public bool IsRunning { get; protected set; } = false;

    /// <summary>
    /// Action to perform on start.
    /// </summary>
    public Action? OnTimerStart { get; } = onStart;

    /// <summary>
    /// Action to perform on stop.
    /// </summary>
    public Action? OnTimerStop { get; } = onStop;

    /// <summary>
    /// Performs a timer tick.
    /// </summary>
    /// <param name="deltaTime">Delta time.</param>
    public void Tick(float deltaTime)
    {
        if (!IsRunning)
        {
            return;
        }

        if (Time > 0.0f)
        {
            Time -= deltaTime;
        }
        else
        {
            Stop();
        }
    }

    /// <summary>
    /// Starts the timer.
    /// </summary>
    public void Start()
    {   
        if (!IsRunning)
        {
            Time = InitialTime;
            IsRunning = true;
            OnTimerStart?.Invoke();
        }
    }

    /// <summary>
    /// Stops the timer.
    /// </summary>
    public void Stop()
    {
        if (IsRunning)
        {
            IsRunning = false;
            OnTimerStop?.Invoke();
        }
    }

    /// <summary>
    /// Resumes the timer.
    /// </summary>
    public void Resume() => IsRunning = true;

    /// <summary>
    /// Pauses the timer.
    /// </summary>
    public void Pause() => IsRunning = false;

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void Reset() => Time = InitialTime;

    /// <summary>
    /// Resets the timer and sets new initial time value.
    /// </summary>
    /// <param name="newTime">New initial time.</param>
    public void Reset(float newTime)
    {
        InitialTime = newTime;
        Reset();
    }
}
