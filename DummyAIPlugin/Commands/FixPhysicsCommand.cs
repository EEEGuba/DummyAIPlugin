using CommandSystem;
using System;

namespace DummyAIPlugin.Commands;

/// <summary>
/// Command used to fix physics.
/// </summary>
public class FixPhysicsCommand : ICommand
{
    /// <summary>
    /// Contains command name.
    /// </summary>
    public string Command { get; } = "fixphysics";

    /// <summary>
    /// Defines command aliases.
    /// </summary>
    public string[] Aliases { get; } = ["physfix", "fixphys"];

    /// <summary>
    /// Contains command description.
    /// </summary>
    public string Description { get; } = "Reapplies physics system settings required by AI perception systems.";

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

        DummiesManager.PreparePhysics();
        response = "Settings reapplied.";
        return true;
    }
}
