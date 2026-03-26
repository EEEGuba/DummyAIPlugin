using DummyAIPlugin.AI;
using DummyAIPlugin.Components;
using LabApi.Features.Wrappers;
using UnityEngine;
using UnityEngine.AI;

namespace DummyAIPlugin;

/// <summary>
/// Manages NavMesh instance for AI navigation system.
/// </summary>
public class NavMeshManager
{
    /// <summary>
    /// Contains a reference to current NavMesh object instance.
    /// </summary>
    private GameObject? _navMeshObject = null;

    /// <summary>
    /// Contains a reference to current <see cref="NavMeshSurface" /> component.
    /// </summary>
    private NavMeshSurface? _navMeshSurface = null;

    /// <summary>
    /// Initializes AI navigation NavMesh.
    /// </summary>
    public void InitializeNavMesh()
    {
        if (_navMeshObject)
        {
            LabApi.Features.Console.Logger.Info("NavMesh already exists, skipping generation.");
            return;
        }

        if (!Round.IsRoundInProgress)
        {
            LabApi.Features.Console.Logger.Info("Round is not in progress, skipping NavMesh generation.");
            return;
        }

        LabApi.Features.Console.Logger.Info("Started NavMesh generation.");
        _navMeshObject = new GameObject("NavMesh");
        var surface = _navMeshObject.AddComponent<NavMeshSurface>();
        _navMeshSurface = surface;

        surface.LayerMask = LayerMask.GetMask(Perception.DefaultLayer, Perception.InvisibleLayer, Perception.FenceLayer);
        surface.UseGeometry = NavMeshCollectGeometry.PhysicsColliders;
        surface.OverrideVoxelSize = true;
        surface.VoxelSize = 0.05f;
        surface.OverrideTileSize = true;
        surface.TileSize = 128;

        surface.BuildNavMesh();
        LabApi.Features.Console.Logger.Info("NavMesh generation completed.");
    }

    /// <summary>
    /// Terminates AI navigation NavMesh.
    /// </summary>
    public void TerminateNavMesh()
    {
        LabApi.Features.Console.Logger.Info("Terminating NavMesh instance.");
        _navMeshSurface?.RemoveData();
        Object.Destroy(_navMeshObject);
        _navMeshSurface = null;
        _navMeshObject = null;
    }
}
