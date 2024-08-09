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
    mMusic.Stop();
  }
  
  private void OnCloseButtonPressed()
  {
    Hide();
  }

}
