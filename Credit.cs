using Godot;
using System;

public class Credit : Node2D
{
  private Music mMusic;
  
  public override void _Ready()
  {
    mMusic = GetNode<Music>("Music");
  }

  private void OnVisibilityChanged()
  {
    if (Visible)
    {
      mMusic.Start();
      return;
    }
    mMusic.Pause(); // don't Stop so it won't play the game over bit.
  }
  
  private void OnCloseButtonPressed()
  {
    Hide();
  }

}
