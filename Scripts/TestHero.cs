using Godot;
using System;

public partial class TestHero : CharacterBody2D
{
    // Called when the node enters the scene tree for the first time.
    public bool walking;
    public int Stamina = 50;
    [Export] private AnimatedSprite2D _animatedSprite;
    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _animatedSprite.Play("Idle");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if(walking == true){
            _animatedSprite.Play("Run");
        }
        if(walking == false){
            _animatedSprite.Play("Idle");
        }
    }

    public void setWalking(bool walking){
        this.walking = walking;
    }
}
