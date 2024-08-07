using Godot;
using System;
using System.Collections.Generic;

public class Main : Node2D
{
  private Menu mMenu;
  private GameOver mGameOver;
  private Credit mCredit;
  
  private Int32 mScore;
  private Label mScoreLabel;
  private Snake mSnake;
  private Apple mApple;
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    Input.SetMouseMode(Input.MouseMode.Hidden);
    var wScreenSize = GetViewportRect().Size;
    mMenu = GetNode<Menu>("Menu");
    mMenu.Position = new Vector2(
      x: wScreenSize.x / 2,
      y: wScreenSize.y / 2
     );
    mCredit = GetNode<Credit>("Credit");
    mCredit.Position = new Vector2(
      x: wScreenSize.x / 2,
      y: wScreenSize.y / 2
     );
    mGameOver = GetNode<GameOver>("GameOver");
    mGameOver.Position = new Vector2(
      x: wScreenSize.x / 2,
      y: wScreenSize.y / 2
     );
    mScoreLabel = GetNode<Label>("Score");
    mSnake = GetNode<Snake>("Snake");
    mApple = GetNode<Apple>("Apple");
  }
  
  public override void _Process(float delta)
  {
    if ( ! Input.IsActionJustPressed("menu"))
    {
      return;
    }
    if (mCredit.Visible)
    {
      mCredit.Hide();
      return;  
    }
    if (mMenu.Visible)
    {
      OnMenuResumed();
      return;
    }
    Input.SetMouseMode(Input.MouseMode.Visible);
    mSnake.Pause();
    mMenu.Show();
  }

  private void OnMenuResumed()
  {
    Input.SetMouseMode(Input.MouseMode.Hidden);
    mMenu.Hide();
    mCredit.Hide();
    mSnake.Resume();
  }

  private void OnSnakeAte()
  {
    mScore++;
    mScoreLabel.Text = mScore.ToString();
  }
  
  private void OnSnakeCrashed()
  {
    Input.SetMouseMode(Input.MouseMode.Visible);
    mSnake.Die();
    mGameOver.Score = mScoreLabel.Text;
    mGameOver.Show();
  }

  private void OnMenuRestart()
  {
    Input.SetMouseMode(Input.MouseMode.Hidden);
    mSnake.Start();
    mApple.ChangePosition();
    mScore = 0;
    mScoreLabel.Text = mScore.ToString();
    mMenu.Hide();
    mGameOver.Hide();
    mSnake.Resume();
  }
  
  private void OnMenuQuit()
  {
    GetTree().Quit();
  }

  private void OnMenuShowCredit()
  {
    mCredit.Show();
  }

}
