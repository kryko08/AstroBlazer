using Godot;
using System;

public partial class CameraMovement : Camera2D
{
	public Global GlabalVars;

	[Signal]
	public delegate void CameraPositionChangedEventHandler(Vector2 position);

	public override void _Ready()
	{
		GlabalVars = GetNode<Global>("/root/GlobalVars");
		GD.Print("Hello from camera script");
	}

	public override void _Process(double delta)
	{
		MoveCamera(delta);
		EmitSignal(SignalName.CameraPositionChanged, Position);
	}

	public void MoveCamera(double delta)
	{
		var velocity = new Vector2();
		velocity.Y += -1;
		velocity = velocity * GlabalVars.GameSpeed;

		Position += velocity * (float)delta;
	}

}
