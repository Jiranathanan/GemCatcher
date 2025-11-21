using Godot;
using System;

public partial class Game : Node2D
{
	// private static readonly AudioStream EXPLODE_SOUND = GD.Load<AudioStream>("res://assets/explode.wav");

	const double GEM_MARGIN = 50.0;
	// [Export] private Gem _gem;
	[Export] private PackedScene _gemScene;
	[Export] private Timer _spawnTimer;
	[Export] private Label _scoreLabel;
	[Export] private AudioStreamPlayer _music;
	[Export] private AudioStreamPlayer2D _effects;
	[Export] private AudioStream _explodeSound;

	private int _score = 0;

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
		gem.OnGemOffScreen += GameOver;
    }

	private void OnScored()
    {
        GD.Print("OnScored received !");
		_effects.Play();
		_score += 1;
		_scoreLabel.Text = $"{_score:0000}";
    }

	private void GameOver()
    {
        GD.Print("Game Over!!!");
		foreach (Node node in GetChildren())
        {
            node.SetProcess(false);
        }
		_spawnTimer.Stop();
		_music.Stop();

		_effects.Stop();
		_effects.Stream = _explodeSound;
		// _effects.Stream = EXPLODE_SOUND;
		_effects.Play();
    }
}
