using Godot;
using System;

public partial class Spawner : Node2D
{
	public Global GlabalVars;
	
	[Export]
	public PackedScene AsteroidScene { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlabalVars = GetNode<Global>("/root/GlobalVars");
	}

	public override void _Process(double delta)
	{
		MoveSpawner(delta);
	}

	private void OnAsteroidTimerTimeout()
	{
		Area2D asteroidInstance = AsteroidScene.Instantiate() as Area2D;
		
		// Choose a random location on Path2D.
		// AsteroidPath.ProgressRatio = GD.Randf();
		var randomTop = GD.RandRange(GlabalVars.MinPosition, GlabalVars.MaxPosition);
		var topPosition = new Vector2(randomTop, Position.Y);
		
		var randomBottom = GD.RandRange(GlabalVars.MinPosition, GlabalVars.MaxPosition);
		var bottomPosition = new Vector2(randomBottom, Position.Y - GlabalVars.ScreenHeight);
		
		asteroidInstance.Position = topPosition;
		
		var randomRotation = GD.RandRange(0, 180);
		asteroidInstance.Rotation = randomRotation;
		
		GetTree().Root.AddChild(asteroidInstance);
		
	}
	
	private void MoveSpawner(double delta)
	{
		var velocity = new Vector2();
		velocity.Y += -1;
		velocity = velocity * GlabalVars.GameSpeed;

		Position += velocity * (float)delta;
	}
	
}




