using NetworkManagerUtils.Dummies;
using PlayerRoles;

namespace DummyAIPlugin;

/// <summary>
/// Adds and removes AI controlled dummies and keeps track of active AI dummies.
/// </summary>
/// <param name="plugin">Reference to plugin object for access to config.</param>
public class DummiesManager(DummyAIPlugin? plugin)
{
    public const string DefaultDummyName = "Dummy";

    /// <summary>
    /// Contains reference to plugin object for access to config object.
    /// </summary>
    private readonly DummyAIPlugin? _plugin = plugin;

    public bool SpawnDummy(RoleTypeId role, string? nickname = null)
    {
        nickname ??= DefaultDummyName;
        var dummy = DummyUtils.SpawnDummy(nickname);

        if (dummy is null)
        {
            return false;
        }

        return false;
    }

    public PossessionResult PossesDummy(ReferenceHub? targetDummy)
    {
        return PossessionResult.InvalidTarget;
    }

    public int PossesAllDummies()
    {
        return 0;
    }

    public bool UnpossesDummy(ReferenceHub? targetDummy)
    {
        return false;
    }

    public int UnpossesAllDummies()
    {
        return 0;
    }

    public bool DestroyDummy(ReferenceHub? targetDummy)
    {
        return false;
    }

    public int DestroyAllDummies()
    {
        return 0;
    }
}
