using Godot;
using System;

public partial class Party : Node
{
    [Export] private NodePath testHeroPath;
    private TestHero testHero = null;
    [Export] private NodePath distanceTrackerPath;
	private Label distanceTracker = null;
    [Export] private NodePath testStaminaTrackerPath;
	private Label testStaminaTracker = null;
    public int distance;
    [Export] private NodePath BiomeHandlerPath;
	private BiomeHandler BiomeHandler = null;

    //Hero Variables
    public TestHero Hero1;

    public Timer timer = null;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GenerateHero();
        BiomeHandler = GetNodeOrNull<BiomeHandler>(BiomeHandlerPath);
        //testHero = GetNodeOrNull<TestHero>(testHeroPath);
        //testStaminaTracker = GetNodeOrNull<Label>(testStaminaTrackerPath);
        Godot.Collections.Array<Node> children = GetChildren();
        for(int x = 0; x < children.Count; x ++ ){
            if(children[x].Name.Equals("TestHeroStamina")){
                testStaminaTracker = children[x] as Label;
            }
        }
        testStaminaTracker.Text = "Hero1 Stamina: " + Hero1.Stamina;
        distanceTracker = GetNodeOrNull<Label>(distanceTrackerPath);
        timer = new Timer();
        AddChild(timer);
        timer.Timeout += IncreaseDistance;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    
    public void ToggleTraveling(bool traveling){
        if(traveling == true){
            timer.Start();
            Hero1.setWalking(traveling);
        }
        else if(traveling == false){
            timer.Stop();
            Hero1.setWalking(traveling);
        }
    }

    public void IncreaseDistance(){
        distance = distance + 1;
        if(distance%10 == 0){
            Hero1.Stamina -= 1;
        }
        distanceTracker.Text = "Distance: " + distance;
        testStaminaTracker.Text = "Hero1 Stamina: " + Hero1.Stamina;

        if(distance%5 == 0){
            BiomeHandler.EventPicker();
        }
    }

    public void GenerateHero(){
        Hero1 = new TestHero();
        AddChild(Hero1);
    }

}
