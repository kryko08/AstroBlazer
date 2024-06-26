using Godot;
using System;

public partial class FastCoin : Node2D
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
	
	private void OnNotifierScreenExited()
	{
		QueueFree();
	}


	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			var anim = body.GetNode<AnimationPlayer>("GunState/FastGunAnimationPlayer");
			anim.Play("fast_gun");
			
			_pickUpAnimationPlayer.Play("pick_up");
		}
	}
}
