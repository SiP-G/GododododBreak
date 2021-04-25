using System;
using Godot;
using Godododod.Utilities;

namespace Godododod.Entities
{
	public class Player : Entity
	{
		private Camera2D _Camera;

		public Player()
		{ 
			Data.Sprite.Texture = ImageLoader.LoadTexture("res://Sprites/Entities/Player/Player.png", true);
			Data.InitBody(4, 3, new Vector2(0, -16));
			Data.InitCollider(3.5f, 3);

			_Camera = new Camera2D();
			_Camera.Current = true;
			AddChild(_Camera);

			Body.PhysicsProcess += Control;
		}

		private void Control(float delta)
		{
			GetInputDirection();
		}

		private void GetInputDirection()
		{
			Data.Velocity.x = Godot.Input.GetActionStrength("right") - Godot.Input.GetActionStrength("left");
			Data.Velocity.y = Godot.Input.GetActionStrength("down") - Godot.Input.GetActionStrength("up");
		}
	}
}
