using Godot;
using System;

public class Apple : StaticBody2D
{
  private Vector2 mCenter;
  private Vector2 mGridSize;
  private AudioStreamPlayer mAudioStreamPlayer;
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    GD.Randomize();
    var wScreenSize = GetViewportRect().Size;
    mCenter = Common.GetCenter(wScreenSize);
    mGridSize = Common.GetGrid(wScreenSize);
    mGridSize = new Vector2(
      x: mGridSize.x - 4,
      y: mGridSize.y - 4
    );
    mAudioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    ChangePosition();
  }
  
  public void ChangePosition()
  {
    Position = new Vector2(
      x: mCenter.x + ((GD.Randi() % mGridSize.x) - Mathf.Floor(mGridSize.x / 2) + 1) * Common.cSquareSize,
      y: mCenter.y + ((GD.Randi() % mGridSize.y) - Mathf.Floor(mGridSize.y / 2) + 1) * Common.cSquareSize
    );
  }

  public void Eat()
  { 
    ChangePosition();
    mAudioStreamPlayer.Play();
    // todo while collision change position
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
