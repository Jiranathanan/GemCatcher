using Godot;
using System;

public partial class Game : Node2D
{
	// [Export] private Gem _gem;
	[Export] private PackedScene _gemScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        // Gem gem = GetNode<Gem>("Gem");
		// gem.OnScored += OnScored;
		// _gem.OnScored += OnScored;
		SpawnGem();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SpawnGem()
    {
        Gem gem = (Gem)_gemScene.Instantiate();
		AddChild(gem);
		gem.Position = new Vector2( 400, -100);
		gem.OnScored += OnScored;
    }

	private void OnScored()
    {
        GD.Print("OnScored received !");
    }
}
