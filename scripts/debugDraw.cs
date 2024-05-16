using Godot;
using System;

public partial class debugDraw : Node2D
{
	public Global GlobalVars;
	public PlayerMovement GlobalPlayerNode;
	
	public override void _Ready()
	{
		GlobalVars = GetNode<Global>("/root/GlobalVars");
		GlobalPlayerNode = GetNode<PlayerMovement>("/root/PlayerMovement");
		
	}
	public override void _Draw()
	{
		var center = new Vector2(500, 500);
		var pos = GlobalPlayerNode.Position;
		DrawLine(center, pos, new Color(0, 255, 0));
		foreach (var border in GlobalVars.Borders)
		{
			DrawLine(border[0], border[1], new Color(255, 0, 0));
		}
		
	}

	public override void _Process(double delta)
	{
		QueueRedraw();
	}
}
