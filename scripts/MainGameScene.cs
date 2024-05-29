using Godot;
using System;

public partial class MainGameScene : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartGame();
	}

	void StartGame()
	{
		var timer = new Timer();
		AddChild(timer);
		timer.OneShot = true;
		timer.Start(3);
		timer.Timeout += () =>
		{
			StartCamera();
			StartSpawner();
		};
	}

	public void GameOver()
	{
		var camera = GetNode("Camera") as CameraMovement;
		camera.StopMovingCamera();

		var timer = new Timer();
		AddChild(timer);
		timer.Start(3);
		timer.Timeout += () =>
		{
			GetTree().ChangeSceneToFile("res://scenes/GameOverMenu.tscn");
		};
	}

	void StartCamera()
	{
		var camera = GetNode("Camera") as CameraMovement;
		camera.StartMovingCamera();
	}

	void StartSpawner()
	{
		var script = GetNode("Spawner") as Spawner;
		script.StartMovingSpawner();

		var asteroidTimer = GetNode<Timer>("Spawner/AsteroidTimer");
		asteroidTimer.Start();

		var specialItemSpawner = GetNode<Timer>("Spawner/SpecialItemTimer");
		specialItemSpawner.Start();

	}
	
}
