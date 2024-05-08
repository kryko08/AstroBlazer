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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
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
		
		// Set the mob's direction perpendicular to the path direction.
		// float direction = AsteroidPath.Rotation + Mathf.Pi / 2;
		
		// Set the mob's position to a random location.
		asteroidInstance.Position = topPosition;
		asteroidInstance.LookAt(bottomPosition);
		// asteroidInstance.Rotation = direction;
		
		// Choose the velocity.
		// var velocity = 10;
		GetTree().Root.AddChild(asteroidInstance);
	}

	private void MoveSpawner(Vector2 newPosition)
	{
		newPosition.Y = newPosition.Y - (GlabalVars.ScreenHeight / 2) - 70;
		Position = newPosition;
	}
	
	private void OnCamera2dCameraPositionChanged(Vector2 cameraPosition)
	{
		MoveSpawner(cameraPosition);
	}
}




