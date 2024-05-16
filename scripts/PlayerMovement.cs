using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	[Export]
	const int Speed = 800;
	const int Friction = 200;
	const int Acceleration = 2000;
	
	public Vector2 InputVector = new Vector2(0, 0);

	public Global GlobalVars;

	public override void _Ready()
	{
		GlobalVars = GetNode<Global>("/root/GlobalVars");
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
	public Vector2 GetInput()
	{
		LookAt(GetGlobalMousePosition());
		InputVector.X = Input.GetActionStrength("right") - Input.GetActionStrength("left");
		InputVector.Y = Input.GetActionStrength("down") - Input.GetActionStrength("up");
		return InputVector.Normalized();
	}

	public void AsteroidCollision()
	{
		GlobalVars.GameOver();	
	}
}
