using Godot;
using System;

public partial class Game : Node2D
{
	const double GEM_MARGIN = 50.0;
	// [Export] private Gem _gem;
	[Export] private PackedScene _gemScene;
	[Export] private Timer _spawnTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        // Gem gem = GetNode<Gem>("Gem");
		// gem.OnScored += OnScored;
		// _gem.OnScored += OnScored;
		_spawnTimer.Timeout += SpawnGem;
		SpawnGem();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SpawnGem()
    {
		Rect2 vpr = GetViewportRect();
        Gem gem = (Gem)_gemScene.Instantiate();
		AddChild(gem);

		float rx = (float)GD.RandRange(
			vpr.Position.X + GEM_MARGIN, vpr.End.X - GEM_MARGIN
		);

		gem.Position = new Vector2( rx, -100);
		gem.OnScored += OnScored;
    }

	private void OnScored()
    {
        GD.Print("OnScored received !");
    }
}
