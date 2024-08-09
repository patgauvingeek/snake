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
    Input.MouseMode = Input.MouseModeEnum.Captured;
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
    // When using the menu button:
    //   1. Closes credit if it's opened.
    if (mCredit.Visible)
    {
      mCredit.Hide();
      return;
    }
    //   2. Otherwise, it closes the menu if it's opened.
    if (mMenu.Visible)
    {
      OnMenuResumed();
      return;
    }
    //   3. Otherwise, it opens the menu.
    Input.MouseMode = Input.MouseModeEnum.Visible;
    mSnake.Pause();
    mMenu.Show();
  }

  private void OnMenuResumed()
  {
    Input.MouseMode = Input.MouseModeEnum.Captured;
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
    Input.MouseMode = Input.MouseModeEnum.Visible;
    mSnake.Die();
    mGameOver.Score = mScoreLabel.Text;
    mGameOver.Show();
  }

  private void OnMenuRestart()
  {
    Input.MouseMode = Input.MouseModeEnum.Captured;
    mSnake.Start();
    mApple.ChangePosition();
    mScore = 0;
    mScoreLabel.Text = mScore.ToString();
    mMenu.Hide();
    mGameOver.Hide();
    mSnake.Restart();
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
