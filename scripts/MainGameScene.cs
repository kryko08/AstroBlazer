using Godot;
using System;

public partial class MainGameScene : Node
{
	private PackedScene _backGroundMusic = GD.Load<PackedScene>("res://scenes/BackGroundMusicPlayer.tscn");
	
	public override void _Ready()
	{
		PlaBackGroundMusic();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void PlaBackGroundMusic()
	{
		var camera = GetNode<Camera2D>("Camera");
		var backGroundMusicInstance = _backGroundMusic.Instantiate();
		camera.AddChild(backGroundMusicInstance);
	}
	
}
