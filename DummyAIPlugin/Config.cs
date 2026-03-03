using System.ComponentModel;

namespace DummyAIPlugin;

/// <summary>
/// Contains plugin configuration.
/// </summary>
public class Config
{
    /// <summary>
    /// Whether or not spectators should be able to see AI thoughts when spectating AI-controlled dummies.
    /// </summary>
    [Description("Whether or not spectators should be able to see AI thoughts when spectating AI-controlled dummies")]
    public bool EnableMindVisualizations { get; set; } = false;

    /// <summary>
    /// Whether or not the plugin should spawn an SCP-049 AI dummy if SCP-049 isn't present after round start.
    /// </summary>
    [Description("Whether or not the plugin should spawn an SCP-049 AI dummy if SCP-049 isn't present after round start")]
    public bool SpawnScp049IfNotPresentOnStart { get; set; } = true;

    /// <summary>
    /// Whether or not the individual dummy should forget the map layout he saw in its previous life.
    /// </summary>
    [Description("Whether or not the individual dummy should forget the map layout he saw in its previous life")]
    public bool ForgetMapOnDeath { get; set; } = false;
}
