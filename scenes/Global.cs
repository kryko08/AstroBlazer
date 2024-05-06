using Godot;
using System;
using System.Collections.Generic;

public partial class Global : Node
{
	public readonly int MinPosition = -100;
	public readonly int MaxPosition = 1100;
	
	// Top, Right, Bottom, Left
	public readonly List<List<Vector2>> Borders = new List<List<Vector2>>()
	{
		new List<Vector2> { new Vector2(-70, -70), new Vector2(1070, -70) },
		new List<Vector2> { new Vector2(1070, -70), new Vector2(1070, 1070) },
		new List<Vector2> { new Vector2(1070, 1070), new Vector2(-70, 1070) },
		new List<Vector2> { new Vector2(-70, 1070), new Vector2(-70, -70) }
	};

	public void GameOver()
	{
		GD.Print("Game Over");
	}
	
}
	