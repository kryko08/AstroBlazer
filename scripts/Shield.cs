using Godot;
using System;

public partial class Shield : Node2D
{
	private AnimationPlayer _idleAnimationPlayer;
	private Animation _idleAnimation;
	
	private AnimationPlayer _pickUpAnimationPlayer;
	public override void _Ready()
	{
		_idleAnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_idleAnimation = _idleAnimationPlayer.GetAnimation("shrink_animation");
		_idleAnimation.LoopMode = Animation.LoopModeEnum.Pingpong;
		_idleAnimationPlayer.Play("shrink_animation");

		_pickUpAnimationPlayer = GetNode<AnimationPlayer>("PickUpAnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			GD.Print("player picked up coin");
			HandleShieldEquip(body);
			_pickUpAnimationPlayer.Play("pick_up");
		}
	}

	private void HandleShieldEquip(Node2D body)
	{
		var script = body as PlayerMovement;
		script.EquipShield();
	}
	
}



