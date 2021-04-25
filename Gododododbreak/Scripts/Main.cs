using Godot;
using Godododod.Entities;
using Godododod.Dungeon;

public class Main : Node
{
	private Player _Player;
	private Level _Level;

	public override void _Ready()
	{
		_Levels = new Level(new Vector2(100,100));
		_Levels.ConnectLevel(this);

		_Player = new Player();
		_Player.ConnectToNode(_Levels.Walls);

		//_Player.ConnectToNode(_Dung);
	}

}
