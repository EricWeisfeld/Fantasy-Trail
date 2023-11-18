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
        int eventNum = 1;
        int numOfButtons = 0;
        string eventDescription = "";
        string eventButton1 = "";
        switch(eventNum)
        {
            case 1:
            //test event
                eventDescription = "You find a rock";
                eventButton1 = "Who gives a shit";
                numOfButtons = 1;
                break;
            case 2:
                break;
        }

    eventHandler.GenerateEvent(eventDescription, numOfButtons, eventButton1);
    }
}
