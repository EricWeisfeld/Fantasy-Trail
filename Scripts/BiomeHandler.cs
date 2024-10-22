using Godot;
using System;

public partial class BiomeHandler : Node
{
	[Export] private NodePath EventTextPath;
	private Label EventText = null;
	private EventHandler eventHandler = null;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EventText = GetNodeOrNull<Label>(EventTextPath);
		eventHandler = EventText as EventHandler;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void EventPicker(){
		int eventNum = 2;
		int numOfButtons = 0;
		string eventDescription = "";
		string eventButton1 = "";
		string eventButton2 = "";
		string[] eventButtonText = new string [5];
		switch(eventNum)
		{
			case 1:
			//test event
				eventDescription = "You find a rock";
				eventButton1 = "Who gives a shit";
				numOfButtons = 1;
				break;
			case 2:
				eventDescription = "As you are walking, you come across a river. How do you cross it?";
				eventButtonText[0] = "Swim the river";
				eventButtonText[1] = "Knock over a tree";
				numOfButtons = 2;
				break;
		}

	eventHandler.GenerateEvent(eventDescription, numOfButtons, eventButtonText);
	}
}
