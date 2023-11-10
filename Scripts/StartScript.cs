using Godot;
using System;
using System.Diagnostics;

public partial class StartScript : Button
{
	[Export] private NodePath LabelPath;
	private Label myLabel = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += changeText;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void changeText(){
		myLabel = GetNodeOrNull<Label>(LabelPath);
		myLabel.Text = "We did it";
	}
}
