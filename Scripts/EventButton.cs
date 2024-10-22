using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class EventButton : Button
{
	private string buttonText = "";
	Action<int, int> buttonResult;
	public int eventValue;
	public int eventOutcome;
	public EventButton(string buttonText, Action<int, int> buttonResult, int eventOutcome, int eventValue){
		this.buttonText = buttonText;
		this.buttonResult = buttonResult;
		this.eventValue = eventValue;
		this.eventOutcome = eventOutcome;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = buttonText;
		Pressed += () => buttonResult(eventOutcome, eventValue);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}
