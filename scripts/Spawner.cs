using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using AstroBlazer.scripts.SpawnerState;

public partial class Spawner : Node2D
{
	public Global GlobalVars;

	// Preloads
	public PackedScene AsteroidScene;
	public PackedScene ShieldScene;
	public PackedScene FastCoinScene;
	
	//States
	private SpawnerMovingState _movingState;
	private SpawnerStoppedState _stoppedState;
	private SpawnerState _currentState;
	
	
	public override void _Ready()
	{
		GlobalVars = GetNode<Global>("/root/GlobalVars");

		AsteroidScene = GD.Load<PackedScene>("res://scenes/Asteroid.tscn");
		ShieldScene = GD.Load<PackedScene>("res://scenes/utils/Shield.tscn");
		FastCoinScene = GD.Load<PackedScene>("res://scenes/coins/FastCoin.tscn");

		_movingState = new SpawnerMovingState();
		_movingState.SetContext(this);

		_stoppedState = new SpawnerStoppedState();
		_stoppedState.SetContext(this);

		_currentState = _stoppedState;
	}

	public override void _Process(double delta)
	{
		_currentState.OnProcess(delta);
	}

	private void OnAsteroidTimerTimeout()
	{
		var asteroidInstance = AsteroidScene.Instantiate() as Node2D;
		
		var randomTop = GD.RandRange(GlobalVars.MinPosition, GlobalVars.MaxPosition);
		var topPosition = new Vector2(randomTop, Position.Y);
		var randomRotation = GD.RandRange(0, 180);
		
		GetTree().Root.AddChild(asteroidInstance);
		asteroidInstance.Position = topPosition;
		asteroidInstance.Rotation = randomRotation;
		
	}
	private void OnSpecialItemTimerTimeout()
	{
		var powerUpList = new List<PackedScene>()
		{
			ShieldScene,
			FastCoinScene
		};

		var numberGenerator = new RandomNumberGenerator();
		var randInt = numberGenerator.RandiRange(0, powerUpList.Count - 1);
		var specialItemInstance = powerUpList[randInt].Instantiate() as Node2D;
		
		var randomTop = GD.RandRange(GlobalVars.MinPosition, GlobalVars.MaxPosition);
		var topPosition = new Vector2(randomTop, Position.Y);
		specialItemInstance.GlobalPosition = topPosition;
		
		GetTree().Root.AddChild(specialItemInstance);
		
	}
	

	public void StartMovingSpawner()
	{
		_currentState = _movingState;
	}

	public void StopMovingSpawner()
	{
		_currentState = _stoppedState;
	}
	
	
}
