using System.ComponentModel;

namespace DummyAIPlugin;

/// <summary>
/// Contains plugin configuration.
/// </summary>
public class Config
{
    /// <summary>
    /// Whether or not the plugin should spawn an SCP-049 AI dummy if SCP-049 isn't present after round start.
    /// </summary>
    [Description("Whether or not the plugin should spawn an SCP-049 AI dummy if SCP-049 isn't present after round start")]
    public bool SpawnSCP049IfNotPresentOnStart { get; set; } = true;
}
