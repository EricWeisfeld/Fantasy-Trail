using Godot;
using System;
using System.Diagnostics.Tracing;

public partial class EventHandler : Label
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void GenerateEvent(string eventText, int numOfButtons, string Button1Text){
        Text = eventText;
        for(int i = 0; i < numOfButtons; i++){
            var button1 = new Button();
            button1.Text = Button1Text;
            AddChild(button1);
        }
    }

    public void GenerateEvent(string eventText, int numOfButtons, string Button1Text, string Button2Text){
        Text = eventText;
        for(int i = 0; i < numOfButtons; i++){
            var button1 = new Button();
            button1.Text = Button1Text;
            AddChild(button1);
        }
    }
}
