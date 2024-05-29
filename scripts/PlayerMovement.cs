using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	// Movement
	[Export]
	const int Speed = 800;
	const int Friction = 200;
	const int Acceleration = 2000;
	public Vector2 InputVector = new Vector2(0, 0);

	// Global Game Manager
	public Global GlobalVars;

	// Shield Power Up
	private AnimationPlayer _shieldAnimationPlayer;
	
	// Border Check
	public ulong LastInSight;
	public bool IsVisible;
	
	[Signal]
	public delegate void PlayerLeftScreenEventHandler();

	[Signal]
	public delegate void PlayerEnteredScreenEventHandler();

	[Signal]
	public delegate void TimeOutsideScreenEventHandler(ulong timeLeft);

	public override void _Ready()
	{
		GlobalVars = GetNode<Global>("/root/GlobalVars");
		_shieldAnimationPlayer = GetNode<AnimationPlayer>("Shield/AnimationPlayer");
	}


	public override void _PhysicsProcess(double delta)
	{
		var input = GetInput();

		if (input == Vector2.Zero)
		{
			if (Velocity.Length() > Friction * delta)
			{
				Velocity -= Velocity.Normalized() * (Friction * (float)delta);
			} else
			{
				Velocity = Vector2.Zero;
			}
		}
		else
		{
			Velocity += InputVector * Acceleration * (float)delta;
			Velocity = Velocity.LimitLength(1.5f * Speed);
		}
		MoveAndSlide();
	}
	
	public override void _Process(double delta)
	{
		if (IsVisible == false)
		{
			var currentTime = Time.GetTicksMsec();
			var timeLeft = 10000 - (currentTime - LastInSight);
			EmitSignal(SignalName.TimeOutsideScreen, timeLeft);
			
			if (currentTime - LastInSight >= 10000)
			{
				EmitSignal(SignalName.PlayerEnteredScreen);
				Die();
				
				var main = GetTree().Root.GetNode("Main") as MainGameScene;
				main.GameOver();
			}
		}
	}
	
	public Vector2 GetInput()
	{
		LookAt(GetGlobalMousePosition());
		InputVector.X = Input.GetActionStrength("right") - Input.GetActionStrength("left");
		InputVector.Y = Input.GetActionStrength("down") - Input.GetActionStrength("up");
		return InputVector.Normalized();
	}

	public void EquipShield()
	{
		_shieldAnimationPlayer.Play("shield_animation");
	}
	
	private void OnScreenNotifierScreenEntered()
	{
		IsVisible = true;
		EmitSignal(SignalName.PlayerEnteredScreen);
	}


	private void OnScreenNotifierScreenExited()
	{
		IsVisible = false;
		LastInSight = Time.GetTicksMsec();
		EmitSignal(SignalName.PlayerLeftScreen);
	}

	public void Die()
	{
		var deathAnimPlayer = GetNode<AnimationPlayer>("DeathAnimationPlayer");
		deathAnimPlayer.Play("die");
	}
	
}


