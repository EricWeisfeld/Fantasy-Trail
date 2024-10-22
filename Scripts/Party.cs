using Godot;
using System;
using System.Linq.Expressions;

public partial class Party : Node
{
	private TestHero testHero = null;
	[Export] private NodePath distanceTrackerPath;
	private Label distanceTracker = null;
	private Label StaminaTracker1 = null;
	private Label StaminaTracker2 = null;
	private Label StaminaTracker3 = null;
	private Label StaminaTracker4 = null;
	[Export] private NodePath BiomeHandlerPath;
	private BiomeHandler BiomeHandler = null;
	[Export] private NodePath timeTrackerPath;
	private Label timeTracker = null;
	[Export] private StartWalking startWalking = null;

	//Hero Variables
	public TestHero Hero1;
	public TestHero Hero2;
	public TestHero Hero3;
	public TestHero Hero4;

	//Travel Variables
	public int distance;

	public int timeHours = 9;
	public int timeMinutes = 0;
	public bool AM = true;
	public bool walking = false;

	public Timer timer = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GenerateHero1();
		GenerateHero2();
		GenerateHero3();
		GenerateHero4();
		BiomeHandler = GetNodeOrNull<BiomeHandler>(BiomeHandlerPath);
		//testHero = GetNodeOrNull<TestHero>(testHeroPath);
		//testStaminaTracker = GetNodeOrNull<Label>(testStaminaTrackerPath);
		//Godot.Collections.Array<Node> children = GetChildren();
		//for(int x = 0; x < children.Count; x ++ ){
		//    if(children[x].Name.Equals("TestHeroStamina")){
		//        testStaminaTracker = children[x] as Label;
		//    }
		//}
		StaminaTracker1 = GetNode("Hero1/TestHeroStamina") as Label;
		StaminaTracker1.Text = "Hero1 Stamina: " + Hero1.Stamina;

		StaminaTracker2 = GetNode("Hero2/TestHeroStamina") as Label;
		StaminaTracker2.Text = "Hero2 Stamina: " + Hero2.Stamina;

		StaminaTracker3 = GetNode("Hero3/TestHeroStamina") as Label;
		StaminaTracker3.Text = "Hero3 Stamina: " + Hero3.Stamina;

		StaminaTracker4 = GetNode("Hero4/TestHeroStamina") as Label;
		StaminaTracker4.Text = "Hero4 Stamina: " + Hero4.Stamina;


		distanceTracker = GetNodeOrNull<Label>(distanceTrackerPath);
		timeTracker = GetNodeOrNull<Label>(timeTrackerPath);

		timer = new Timer();
		AddChild(timer);
		timer.Timeout += HandleTravelTick;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void ToggleTraveling(bool traveling){
		if(traveling == true){
			timer.Start();
			Hero1.setWalking(traveling);
			Hero2.setWalking(traveling);
			Hero3.setWalking(traveling);
			Hero4.setWalking(traveling);
			startWalking.Text ="Stop walking";
		}
		else if(traveling == false){
			timer.Stop();
			Hero1.setWalking(traveling);
			Hero2.setWalking(traveling);
			Hero3.setWalking(traveling);
			Hero4.setWalking(traveling);
			startWalking.Text ="Start walking";
		}
	}

	public void HandleTravelTick(){
		distance = distance + 1;
		if(distance%10 == 0){
			Hero1.Stamina -= 1;
			StaminaTracker1.Text = "Hero1 Stamina: " + Hero1.Stamina;

			Hero2.Stamina -= 1;
			StaminaTracker2.Text = "Hero2 Stamina: " + Hero2.Stamina;

			Hero3.Stamina -= 1;
			StaminaTracker3.Text = "Hero3 Stamina: " + Hero3.Stamina;

			Hero4.Stamina -= 1;
			StaminaTracker4.Text = "Hero4 Stamina: " + Hero4.Stamina;
		}
		distanceTracker.Text = "Distance: " + distance;

		if(distance%2 == 0){
			timeMinutes += 15;
			if(timeMinutes%60 == 0){
				timeMinutes = 0;
				timeHours += 1;
				if(timeHours%13 == 0){
					AM = !AM;
					timeHours = 1;
				}
			}
			if(AM == true){
				timeTracker.Text = timeHours+":"+timeMinutes+" AM";
			}else{
				timeTracker.Text = timeHours+":"+timeMinutes+" PM";
			}
		}

		if(distance%10 == 0){
			BiomeHandler.EventPicker();
			ToggleTraveling(false);
		}
	}
	
	public void reduceStamina(int value){
			Hero1.Stamina -= value;
			StaminaTracker1.Text = "Hero1 Stamina: " + Hero1.Stamina;

			Hero2.Stamina -= value;
			StaminaTracker2.Text = "Hero2 Stamina: " + Hero2.Stamina;

			Hero3.Stamina -= value;
			StaminaTracker3.Text = "Hero3 Stamina: " + Hero3.Stamina;

			Hero4.Stamina -= value;
			StaminaTracker4.Text = "Hero4 Stamina: " + Hero4.Stamina;
	}

	public void GenerateHero1(){
		var scene = GD.Load<PackedScene>("res://Instances/testHero.tscn");
		Hero1 = scene.Instantiate() as TestHero;
		AddChild(Hero1);
		Hero1.Name = "Hero1";
		Hero1.Position = new Vector2(-200,0);
	}

	public void GenerateHero2(){
		var scene = GD.Load<PackedScene>("res://Instances/testHero.tscn");
		Hero2 = scene.Instantiate() as TestHero;
		AddChild(Hero2);
		Hero2.Name = "Hero2";
		Hero2.Position = new Vector2(-50,0);
	}
	public void GenerateHero3(){
		var scene = GD.Load<PackedScene>("res://Instances/testHero.tscn");
		Hero3 = scene.Instantiate() as TestHero;
		AddChild(Hero3);
		Hero3.Name = "Hero3";
		Hero3.Position = new Vector2(100,0);
	}
	public void GenerateHero4(){
		var scene = GD.Load<PackedScene>("res://Instances/testHero.tscn");
		Hero4 = scene.Instantiate() as TestHero;
		AddChild(Hero4);
		Hero4.Name = "Hero4";
		Hero4.Position = new Vector2(250,0);
	}


}
