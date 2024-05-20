using Godot;
using System;

public partial class AnimationComponent : Node2D
{
	
	[Export]
	public AnimatedSprite2D animatedSprite2D;
	[Export]
	public InputComponent inputComponent;

	public void UpdateAnimations() {
		if (inputComponent.GetMoveInput() == 0) {
			animatedSprite2D.Play("Idle");
		}
		
		if (inputComponent.GetMoveInput() < 0) {
			if (inputComponent.GetRunInput() == true) {
				animatedSprite2D.Play("Run");
			}
			else {
				animatedSprite2D.Play("Walk");
			}
			animatedSprite2D.FlipH = true;
		}
		
		if (inputComponent.GetMoveInput() > 0) {
			if (inputComponent.GetRunInput() == true) {
				animatedSprite2D.Play("Run");
			}
			else {
				animatedSprite2D.Play("Walk");
			}

			animatedSprite2D.FlipH = false;
		}
	}
}
