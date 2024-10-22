using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;


public partial class EventHandler : Label
{
    [Export] private NodePath partyPath;
    private Party party = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        party = GetNodeOrNull<Party>(partyPath);

        //Dictionary<>
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void GenerateEvent(string eventText, int numOfButtons, string[] ButtonText){
        Text = eventText;
        for(int i = 0; i < numOfButtons; i++){
            var button1 = new EventButton(ButtonText[i], EventOutcome, 1, 5); //this needs a param
            //button1.Text = ButtonText[i];
            AddChild(button1);
            button1.Position = new Vector2(120*i,50);
        }
    }

    public void EventOutcome(int outcome, int value){
        if(outcome == 1){
            party.reduceStamina(value);
            Text = "You lost some stamina, unlucky";
        }
        EndEvent();
    }

    public void UpdateStamina(int value){
        party.reduceStamina(value);
    }

    public void EndEvent(){
        var children = GetChildren();
        foreach(var child in children){
            child.QueueFree();
        }
    }

}
