using Godot;
using System;

public class Menu : Node2D
{
  [Signal]
  public delegate void Resumed();
  
  private void OnResumePressed()
  {
	EmitSignal(nameof(Resumed));
  }
  
  [Signal]
  public delegate void Restart();
  
  private void OnRestartPressed()
  {
	EmitSignal(nameof(Restart));
  }
  
  [Signal]
  public delegate void Quit();
  
  private void OnQuitPressed()
  {
	EmitSignal(nameof(Quit));
  }
  
  [Signal]
  public delegate void ShowCredit();
  
  private void OnCreditPressed()
  {
	EmitSignal(nameof(ShowCredit));
  }

}
