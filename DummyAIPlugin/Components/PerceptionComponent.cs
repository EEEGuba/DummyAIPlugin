using System;
using UnityEngine;

namespace DummyAIPlugin.Components;

/// <summary>
/// Component used to hook collision effects to perception system.
/// </summary>
public class PerceptionComponent : MonoBehaviour
{
    /// <summary>
    /// Callback triggered on collider entry.
    /// </summary>
    public event Action<Collider>? TriggerEnter;

    /// <summary>
    /// Callback triggered on collider exit.
    /// </summary>
    public event Action<Collider>? TriggerExit;

    /// <summary>
    /// Triggered on collider entry.
    /// </summary>
    /// <param name="other">Detected collider.</param>
    public void OnTriggerEnter(Collider other) => TriggerEnter?.Invoke(other);

    /// <summary>
    /// Triggered on collider exit.
    /// </summary>
    /// <param name="other">Detected collider.</param>
    public void OnTriggerExit(Collider other) => TriggerExit?.Invoke(other);
}
