using Godot;
using System;

public enum Turn
{
  None,
  Left,
  Right
}

public class SnakeHead : Area2D
{  
  private const float cSpeed = 4.0f; // How fast the player will move (square/sec).[Signal]
  private const float cInvertedSpeed = 1 / cSpeed;
  
  [Signal]
  public delegate void Moved(Vector2 position, float rotationDegrees, Turn turn);
  
  [Signal]
  public delegate void Ate();
  
  [Signal]
  public delegate void Crashed();
  
  private float mDelta;
  private Vector2 mScreenSize; // Size of the game window.
  
  private enum Direction
  {
    None,
    Left,
    Right,
    Up,
    Down
  }
  
  private bool mPause;
  private Direction mCommand;
  private Direction mLastDirection;
  private Direction mDirection;
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    mScreenSize = GetViewportRect().Size;
    var wAnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    wAnimatedSprite.Play();
  }
  
  public void Start()
  {
    Position = Common.GetCenter(mScreenSize);    
    RotationDegrees = 0.0f;
    mCommand = Direction.None;
    mLastDirection = Direction.Right;
    mDirection = Direction.None;
  }
  
  public void Pause()
  {
    mPause = true;  
  }
  
  public void Resume()
  {
    mPause = false;
  }
    
  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    if (mPause)
    {
      return;
    }
    if (Input.IsActionPressed("move_left") && mDirection != Direction.Right && mDirection != Direction.None)
    {
      mCommand = Direction.Left;
    }    
    if (Input.IsActionPressed("move_right") && mDirection != Direction.Left)
    {
      mCommand = Direction.Right;
    }
    if (Input.IsActionPressed("move_down") && mDirection != Direction.Up)
    {
      mCommand = Direction.Down;
    }
    if (Input.IsActionPressed("move_up") && mDirection != Direction.Down)
    {
      mCommand = Direction.Up;
    }
    
    mDelta += delta;
    if (mDelta <= cInvertedSpeed)
    {
      return;
    }
    mDelta = mDelta - cInvertedSpeed;
    
    //
    // Update Position
    //    
    mDirection = mCommand;
    var wVelocity = Vector2.Zero;
    var wNewRotationDegrees = 0.0f;
    switch(mDirection)
    {
      case Direction.None: return;
      case Direction.Left:
      {
        wNewRotationDegrees = 180.0f;
        wVelocity.x = -Common.cSquareSize;
      } break;
      case Direction.Right:
      {
        wNewRotationDegrees = 0.0f;
        wVelocity.x = Common.cSquareSize;
      } break;
      case Direction.Up:
      {
        wNewRotationDegrees = -90.0f;
        wVelocity.y = -Common.cSquareSize;
      } break;
      case Direction.Down:
      {
        wNewRotationDegrees = 90.0f;
        wVelocity.y = Common.cSquareSize;
      } break;
    }
    
    EmitSignal(nameof(Moved), Position, RotationDegrees, GetTurn());
    Position += wVelocity;
    RotationDegrees = wNewRotationDegrees;
    mLastDirection = mDirection;
    //clamp ? https://docs.godotengine.org/en/stable/getting_started/first_2d_game/03.coding_the_player.html
  }
  
  private Turn GetTurn()
  {
    if (mLastDirection == Direction.None && mDirection == Direction.Right ||
        mLastDirection == mDirection)
    {
      return Turn.None;
    }
    if (mLastDirection == Direction.Right && mDirection == Direction.Up ||
        mLastDirection == Direction.Up && mDirection == Direction.Left ||
        mLastDirection == Direction.Left && mDirection == Direction.Down ||
        mLastDirection == Direction.Down && mDirection == Direction.Right)
    {
      return Turn.Left;
    }
    return Turn.Right;
  }
      
  private void OnSnakeHeadBodyEntered(object body)
  {
    var wApple = body as Apple;
    if (wApple != null)
    {
      EmitSignal(nameof(Ate));
      wApple.Eat(); // move the apple.
      return; 
    }
    EmitSignal(nameof(Crashed));
  }

}
