using Godot;
using System;

public partial class InputComponent : Node2D
{
	public float moveInput;
	public bool runInput;
	public bool jumpInput;
	public bool crouchInput;

	public float GetMoveInput() {
		return moveInput;
	}

	public bool GetRunInput() {
		return runInput;
	}

	public bool GetJumpInput() {
		return jumpInput;
	}

	public bool GetCrouchInput() {
		return crouchInput;
	}

    public void GetInputs(double delta)
    {
        moveInput = Input.GetAxis("left", "right");
		runInput = Input.IsActionPressed("sprint");
		jumpInput = Input.IsActionPressed("jump");
		crouchInput = Input.IsActionPressed("crouch");
    }
}
