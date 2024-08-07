using Godot;
using System;

public class GameOver : Node2D
{
  private Label mScore;

  public override void _Ready()
  {
    mScore = GetNode<Label>("Score");
  }  
  
  [Signal]
  public delegate void Restart();
  
  private void OnRestartPressed()
  {
    EmitSignal(nameof(Restart));
  }

  public String Score
  {
    get
    {
      return mScore.Text;
    }
    set
    {
      mScore.Text = value;
    }
  }

}
