using CommandSystem;
using System;

namespace DummyAIPlugin.Commands;

/// <summary>
/// Command used to generate NavMesh at runtime.
/// </summary>
/// <param name="navMeshManager">Reference to NavMesh manager.</param>
public class GenerateNavMeshCommand(NavMeshManager navMeshManager) : ICommand
{
    /// <summary>
    /// Contains command name.
    /// </summary>
    public string Command { get; } = "gennavmesh";

    /// <summary>
    /// Defines command aliases.
    /// </summary>
    public string[] Aliases { get; } = ["gennav", "navmesh"];

    /// <summary>
    /// Contains command description.
    /// </summary>
    public string Description { get; } = "Generates new nav mesh for AI navigation system.";

    /// <summary>
    /// Contains a reference to NavMesh manager.
    /// </summary>
    private readonly NavMeshManager _navMeshManager = navMeshManager;

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="arguments">Command arguments provided by sender.</param>
    /// <param name="sender">Command sender.</param>
    /// <param name="response">Response to display in sender's console.</param>
    /// <returns><see langword="true"/> if command executed successfully, <see langword="false"/> otherwise.</returns>
    public bool Execute(ArraySegment<string?> arguments, ICommandSender? sender, out string response)
    {
        var problem = DummyAIParentCommand.CheckPerms(sender);

        if (problem is not null)
        {
            response = problem;
            return false;
        }

        _navMeshManager.TerminateNavMesh();
        _navMeshManager.InitializeNavMesh();
        response = "Navmesh generated.";
        return true;
    }
}
