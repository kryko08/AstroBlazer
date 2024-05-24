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
			GD.Print("player picked up coin");
			HandleGunSwap(body);
			_pickUpAnimationPlayer.Play("pick_up");
			var timer = GetNode<Timer>("Timer");
			timer.Start();
		}
	}

	void HandleGunSwap(Node2D body)
	{
		var script = body.GetNode<Node2D>("GunState") as PlayerGunStateManager;
		var fastGunState = body.GetNode<Marker2D>("GunState/FastGun") as IGunState;
		script.ChangeState(fastGunState);
	}
	
	private void OnTimerTimeout()
	{
		GD.Print("timer");
		var script = GetNode("/root/Player/GunState") as PlayerGunStateManager;
		script.ChangeToDefaultState();
		QueueFree();
	}
}



