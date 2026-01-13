using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

namespace DummyAIPlugin.AI;

/// <summary>
/// Implement this interface to create new AI sense.
/// </summary>
public interface ISense
{
    /// <summary>
    /// Callback triggered on collider entry.
    /// </summary>
    /// <param name="other">Detected collider.</param>
    void ProcessEnter(Collider other);

    /// <summary>
    /// Callback triggered on collider exit.
    /// </summary>
    /// <param name="other">Detected collider.</param>
    void ProcessExit(Collider other);

    /// <summary>
    /// Updates perception sensibility.
    /// </summary>
    /// <returns>Job handles enumerator.</returns>
    IEnumerator<JobHandle> ProcessSensibility();

    /// <summary>
    /// Processes all sensed items.
    /// </summary>
    void ProcessSensedItems();
}
