using DummyAIPlugin.Components;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace DummyAIPlugin.AI;

/// <summary>
/// Basic AI perception and senses manager.
/// </summary>
public class Perception
{
    /// <summary>
    /// Perception layer number.
    /// </summary>
    public const int PerceptionLayer = 31;

    /// <summary>
    /// Default collision layer name.
    /// </summary>
    public const string DefaultLayer = "Default";

    /// <summary>
    /// Door collision layer name.
    /// </summary>
    public const string DoorLayer = "Door";

    /// <summary>
    /// Interactable collision layer name.
    /// </summary>
    public const string InteractableLayer = "InteractableNoPlayerCollision";

    /// <summary>
    /// Glass collision layer name.
    /// </summary>
    public const string GlassLayer = "Glass";

    /// <summary>
    /// Hitbox collision layer name.
    /// </summary>
    public const string HitboxLayer = "Hitbox";

    /// <summary>
    /// Stores all used senses.
    /// </summary>
    public List<ISense> Senses { get; }

    /// <summary>
    /// Stores processed senses job enumerators.
    /// </summary>
    private readonly List<IEnumerator<JobHandle>> _processedSenses;

    /// <summary>
    /// Contains a reference to sensing game object.
    /// </summary>
    private readonly GameObject _sensingObject;

    /// <summary>
    /// Perception component used to update senses.
    /// </summary>
    private readonly PerceptionComponent _perceptionComponent;

    /// <summary>
    /// Creates new AI perception object.
    /// </summary>
    /// <param name="dummy">Dummy to attach perception components to.</param>
    public Perception(ReferenceHub dummy)
    {
        Senses = [];
        _processedSenses = [];
        var sensing = new GameObject("DummySense");
        _sensingObject = sensing;
        sensing.layer = PerceptionLayer;
        var sensingTransform = sensing.transform;
        var parentTransform = dummy.transform;
        sensingTransform.position = parentTransform.position;
        sensingTransform.parent = parentTransform;

        var perceptionComponent = sensing.AddComponent<PerceptionComponent>();
        _perceptionComponent = perceptionComponent;
        perceptionComponent.TriggerEnter += OnTriggerEnter;
        perceptionComponent.TriggerExit += OnTriggerExit;

        var sensingTrigger = sensing.AddComponent<SphereCollider>();
        sensingTrigger.isTrigger = true;
        sensingTrigger.radius = 32.0f;

        var sensingRigid = sensing.AddComponent<Rigidbody>();
        sensingRigid.isKinematic = true;
    }

    /// <summary>
    /// Performs cleanup after perception usage.
    /// </summary>
    public void Terminate()
    {
        _perceptionComponent.TriggerEnter -= OnTriggerEnter;
        _perceptionComponent.TriggerExit -= OnTriggerExit;
        Object.Destroy(_sensingObject);
    }

    /// <summary>
    /// Triggers collision entry processing for senses.
    /// </summary>
    /// <param name="other">Detected collider.</param>
    public void OnTriggerEnter(Collider other)
    {
        foreach (var sense in Senses)
        {
            sense.ProcessEnter(other);
        }
    }

    /// <summary>
    /// Triggers collision exit processing for senses.
    /// </summary>
    /// <param name="other">Detected collider.</param>
    public void OnTriggerExit(Collider other)
    {
        foreach (var sense in Senses)
        {
            sense.ProcessExit(other);
        }
    }

    /// <summary>
    /// Performs perception update.
    /// </summary>
    /// <returns>Job handles enumerator.</returns>
    public IEnumerator<JobHandle> Update()
    {
        _processedSenses.Clear();
        var sensesCount = Senses.Count;

        foreach (var sense in Senses)
        {
            _processedSenses.Add(sense.ProcessSensibility());
        }

        var completedCount = 0;
        int jobHandlesCount;
        var jobHandlesBuffer = new NativeArray<JobHandle>(sensesCount, Allocator.Temp);

        while (completedCount < sensesCount)
        {
            completedCount = 0;
            jobHandlesCount = 0;

            for (var i = 0; i < sensesCount; ++i)
            {
                var processSenses = _processedSenses[i];

                if (processSenses.MoveNext())
                {
                    jobHandlesBuffer[jobHandlesCount] = processSenses.Current;
                    ++jobHandlesCount;
                }
                else
                {
                    ++completedCount;
                }
            }

            var jobHandles = jobHandlesBuffer.GetSubArray(0, jobHandlesCount);
            yield return JobHandle.CombineDependencies(jobHandles);
        }

        foreach (var sense in Senses)
        {
            sense.ProcessSensedItems();
        }
    }

    /// <summary>
    /// Retrieves sense of specific type.
    /// </summary>
    /// <typeparam name="T">Type of sense to retrieve.</typeparam>
    /// <returns>Found sense object or <see langword="null" /> if nothing was found.</returns>
    public T? GetSense<T>() where T : class, ISense => Senses.Find(s => s is T) as T;
}
