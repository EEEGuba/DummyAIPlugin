using System.Collections.Generic;
using UnityEngine;

namespace DummyAIPlugin.Components;

/// <summary>
/// Component used to keep track of colliders.
/// </summary>
public class ColliderDataComponent : MonoBehaviour
{
    /// <summary>
    /// Contains stored colliders data.
    /// </summary>
    public Dictionary<Collider, ColliderData> ColliderDatas { get; } = [];

    /// <summary>
    /// Contains tracked colliders.
    /// </summary>
    private Collider[] _colliders = null!;

    /// <summary>
    /// Initializes the component.
    /// </summary>
    public void Awake()
    {
        _colliders = GetComponents<Collider>();
        
        foreach (var collider in _colliders)
        {
            ColliderDatas[collider] = new(collider.bounds.center, collider.GetInstanceID());
        }
    }

    /// <summary>
    /// Updates the component.
    /// </summary>
    public void Update()
    {
        foreach (var collider in _colliders)
        {
            ColliderDatas[collider] = new(collider.bounds.center, ColliderDatas[collider].InstanceId);
        }
    }
}
