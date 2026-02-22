using PlayerRoles.FirstPersonControl;

namespace DummyAIPlugin.AI.FirstPerson;

/// <summary>
/// First person jump action.
/// </summary>
/// <param name="fpcModule">First person control module to use.</param>
public class JumpStrategy(FirstPersonMovementModule fpcModule) : IActionStrategy
{
    /// <inheritdoc />
    public bool CanPerform => !_controller.IsJumping && fpcModule.IsGrounded;//changed to also check if dummy is touching ground
    /// <inheritdoc />
    public bool Complete => _controller.IsJumping;

    /// <summary>
    /// Contains used first person jump controller.
    /// </summary>
    private readonly FpcJumpController _controller = fpcModule.Motor.JumpController;

    /// <inheritdoc />
    public void Start()
    {
        if (CanPerform)//made an if that can probably be simplified but proof of fix mostly
        {
            _controller.ForceJump(fpcModule.Motor.MainModule.JumpSpeed);//changed 1.0f to fpcModule.Motor.MainModule.JumpSpeed, 1.0f does a single impulse instead of the slow impulse fade so it jiggles without this
        }
    }
    /// <inheritdoc />
    public void Update() { }

    /// <inheritdoc />
    public void Stop() { }
}
