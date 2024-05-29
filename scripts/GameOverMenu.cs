using Godot;
using System;

public partial class GameOverMenu : Control
{
	private Label _asteroidLabel;
	public Global GlobalVars;
	
	public override void _Ready()
	{
		_asteroidLabel = GetNode<Label>("NumAsteroidsLabel");
		GlobalVars = GetNode<Global>("/root/GlobalVars");
		_asteroidLabel.Text = GlobalVars.PlayerScore.ToString();
	}
	
	private void OnRestartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/MainGameScene.tscn");
		GlobalVars.RestartPlayerScore();
	}


	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}


