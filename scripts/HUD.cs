using Godot;
using System;

public partial class HUD : CanvasLayer
{
	public Label TimeLabel;
	public Label ScoreLabel;

	public Global GlobalVars;
	
	public override void _Ready()
	{
		TimeLabel = GetNode<Label>("TimeLabel");
		ScoreLabel = GetNode<Label>("ScoreLabel");
		GlobalVars = GetNode<Global>("/root/GlobalVars");
		TimeLabel.Hide();
	}

	public override void _Process(double delta)
	{
		ScoreLabel.Text = GlobalVars.PlayerScore.ToString();
	}

	private void OnBorderCheckTimeOutsideScreen(long timeLeft)
	{
		double seconds = timeLeft / 1000.0;
		string formattedString = seconds.ToString("0.00");
		TimeLabel.Text = formattedString;
	}
	
	private void OnBorderCheckOnScreenEnterHud()
	{
		TimeLabel.Hide();
	}
	
	private void OnBorderCheckOnScreenExitedHud()
	{
		TimeLabel.Show();
	}
}











