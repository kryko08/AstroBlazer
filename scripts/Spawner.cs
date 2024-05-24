using Godot;
using System;

public partial class Spawner : Node2D
{
	public Global GlabalVars;
	
	[Export]
	public PackedScene AsteroidScene { get; set; }
	
	[Export]
	public PackedScene CoinScene { set; get; }
	
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
		var asteroidInstance = AsteroidScene.Instantiate() as Node2D;
		
		var randomTop = GD.RandRange(GlabalVars.MinPosition, GlabalVars.MaxPosition);
		var topPosition = new Vector2(randomTop, Position.Y);
		
		var randomBottom = GD.RandRange(GlabalVars.MinPosition, GlabalVars.MaxPosition);
		var bottomPosition = new Vector2(randomBottom, Position.Y - GlabalVars.ScreenHeight);
		
		asteroidInstance.Position = topPosition;
		
		var randomRotation = GD.RandRange(0, 180);
		asteroidInstance.Rotation = randomRotation;
		
		GetTree().Root.AddChild(asteroidInstance);
		
	}
	private void OnCoinTimerTimeout()
	{
		var fastCoinInstance = CoinScene.Instantiate() as Node2D;
		
		var randomTop = GD.RandRange(GlabalVars.MinPosition, GlabalVars.MaxPosition);
		var topPosition = new Vector2(randomTop, Position.Y);
		fastCoinInstance.GlobalPosition = topPosition;
		
		GetTree().Root.AddChild(fastCoinInstance);
		
	}
	
	private void MoveSpawner(double delta)
	{
		var velocity = new Vector2();
		velocity.Y += -1;
		velocity = velocity * GlabalVars.GameSpeed;

		Position += velocity * (float)delta;
	}
	
	
}






