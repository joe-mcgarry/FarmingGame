using Godot;
using System;

public partial class PlayerCharacterMove : CharacterBody2D
{
	AnimationTree animTree;
	const float speed = 175f;
	bool isMoving;
	Vector2 facingDirection;

	public override void _Ready()
	{
		animTree = GetNode<AnimationTree>("AnimationTree");
	}

	public override void _Process(double delta)
	{
		ProcessAnimation();
	}
	
	void ProcessAnimation()
	{
		animTree.Set("parameters/conditions/isIdle", !isMoving);
		animTree.Set("parameters/conditions/isMoving", isMoving);
		animTree.Set("parameters/Idle/blend_position", facingDirection);
		animTree.Set("parameters/Move/blend_position", facingDirection);
	}

	public override void _PhysicsProcess(double delta) 
	{
		Vector2 velocity = Velocity;
		Vector2 directionInput = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		isMoving = directionInput != Vector2.Zero;

		if(directionInput != Vector2.Zero)
		{
			facingDirection = directionInput;
		}
		velocity = directionInput * speed;
		Velocity = velocity;
		MoveAndSlide();
	}
}
