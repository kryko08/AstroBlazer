using Godot;
using System;
using GodotPlugins.Game;

public partial class MainMenu : Control
{
	private void OnButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/MainGameScene.tscn");
	}
}
