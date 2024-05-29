using Godot;
using System;

public partial class HUD : CanvasLayer
{
	public Label TimeLabel;
	public Label ScoreLabel;

	public Global GlobalVars;

	private AnimationPlayer _timeLeftAnimationPlayer;
	private Animation _timeLeftAnimation;

	private AnimationPlayer _scoreIncreaseAnimationPlayer;
	
	
	public override void _Ready()
	{
		TimeLabel = GetNode<Label>("TimeLabel");
		ScoreLabel = GetNode<Label>("ScoreLabel");
		GlobalVars = GetNode<Global>("/root/GlobalVars");
		TimeLabel.Hide();
		
		_timeLeftAnimationPlayer = GetNode<AnimationPlayer>("TimeLeftAnimationPlayer");
		_timeLeftAnimation = _timeLeftAnimationPlayer.GetAnimation("time_left");
		_timeLeftAnimation.LoopMode = Animation.LoopModeEnum.Pingpong;
		_timeLeftAnimationPlayer.Play("time_left");

		_scoreIncreaseAnimationPlayer = GetNode<AnimationPlayer>("ScoreIncreaseAnimationPlayer");
		
	}
	
	public override void _Process(double delta)
	{
		ScoreLabel.Text = GlobalVars.PlayerScore.ToString();
	}

	private void OnPlayerEnteredScreen()
	{
		TimeLabel.Hide();
	}


	private void OnPlayerLeftScreen()
	{
		TimeLabel.Show();
	}


	private void OnTimeOutsideScreen(ulong timeLeft)
	{
		double seconds = timeLeft / 1000.0;
		string formattedString = seconds.ToString("0");
		TimeLabel.Text = formattedString;
	}
}













