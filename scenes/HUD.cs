using Godot;
using System;

public partial class HUD : CanvasLayer
{
	public Label TimeLabel;
	public override void _Ready()
	{
		TimeLabel = GetNode<Label>("TimeLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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






