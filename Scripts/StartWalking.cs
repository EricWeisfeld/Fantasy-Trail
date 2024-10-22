using Godot;
using System;
using System.Diagnostics;

public partial class StartWalking : Button
{
	[Export] private NodePath partyPath;
    private Party party = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		party = GetNodeOrNull<Party>(partyPath);
		Pressed += toggleWalk;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void toggleWalk(){
		if(Text == "Start walking"){
			party.ToggleTraveling(true);
		}
		else if(Text == "Stop walking"){
			party.ToggleTraveling(false);
		}

        //Pressed += changeText;

	}
}
