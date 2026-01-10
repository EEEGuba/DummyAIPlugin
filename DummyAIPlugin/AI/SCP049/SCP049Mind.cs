using DummyAIPlugin.AI.FirstPerson;
using PlayerRoles.FirstPersonControl;

namespace DummyAIPlugin.AI.SCP049;

/// <summary>
/// AI mind for SCP-049 dummies.
/// </summary>
public class SCP049Mind : Mind
{
    /// <summary>
    /// Creates new mind instance.
    /// </summary>
    /// <param name="fpcModule">First person module to use.</param>
    public SCP049Mind(FirstPersonMovementModule fpcModule)
    {
        var fpcMotor = fpcModule.Motor;
        var factory = new BeliefFactory(Beliefs);

        const string Nothing = "Nothing";
        const string Moving = "AgentMoving";

        factory.AddBelief(Nothing, () => false);
        factory.AddBelief(Moving, () => fpcMotor.MovementDetected);

        Actions.Add(new Action.Builder("Relax", new Idle(5.0f))
            .AddEffect(Beliefs[Nothing])
            .Build());

        Actions.Add(new Action.Builder("Wander Around", new Wander(fpcModule, 25.0f))
            .AddEffect(Beliefs[Moving])
            .Build());

        Goals.Add(new Goal.Builder("Chill Out", 1.0f)
            .AddDesiredEffect(Beliefs[Nothing])
            .Build());

        Goals.Add(new Goal.Builder("Wander", 1.0f)
            .AddDesiredEffect(Beliefs[Moving])
            .Build());
    }
}
