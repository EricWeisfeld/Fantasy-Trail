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
		Pressed += changeText;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void changeText(){
		if(Text == "Start walking"){
	    	Text = "Stop walking";
			party.ToggleTraveling(true);
		}
		else if(Text == "Stop walking"){
	    	Text = "Start walking";
			party.ToggleTraveling(false);
		}

        //Pressed += changeText;

	}
}
