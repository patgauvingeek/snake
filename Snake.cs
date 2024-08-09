using Godot;
using System;

public class Snake : Node2D
{
  [Signal]
  public delegate void Ate();
  
  [Signal]
  public delegate void Crashed();
  
#pragma warning disable 649
  // We assign this in the editor, so we don't need the warning about not being assigned.
  [Export]
  public PackedScene BodyScene;
#pragma warning restore 649

  private bool mDead;
  private int mSize;
  private SnakeHead mSnakeHead;
  private LinkedQueue<SnakeBody> mBodyParts;
 
  public override void _Ready()
  {
    mSnakeHead = GetNode<SnakeHead>("SnakeHead");
    mBodyParts = new LinkedQueue<SnakeBody>();
    Start();
  }
  
  public void Start()
  {
    mDead = false;
    Show();
    while (mBodyParts.Count > 0)
    {
      var wBody = mBodyParts.Dequeue();
      wBody.QueueFree();
    }
    var wScreenSize = GetViewportRect().Size;
    var wPosition = Common.GetCenter(wScreenSize);    
    
    mSnakeHead.Start();
    wPosition.x -= 4*Common.cSquareSize;
    AddBody(wPosition, 0.0f, "tail");
    wPosition.x += Common.cSquareSize;
    AddBody(wPosition, 0.0f, "body");
    wPosition.x += Common.cSquareSize;
    AddBody(wPosition, 0.0f, "body");
    wPosition.x += Common.cSquareSize;
    AddBody(wPosition, 0.0f, "body");
    mSize = 4;
  }
  
  public void Pause()
  {
    mSnakeHead.Pause();
  }
  
  public void Die()
  {
    mDead = true;
    mSnakeHead.Die();
    Hide();
  }
      
  public void Resume()
  {
    if (mDead)
    {
      return;  
    }
    mSnakeHead.Resume();
  }

  public void Restart()
  {
    if (mDead)
    {
      return;  
    }
    mSnakeHead.Restart();
  }

  private SnakeBody AddBody(Vector2 position, float rotationDegrees, String type = "body")
  {
    var wBody = (SnakeBody)BodyScene.Instance();
    wBody.Position = position;
    wBody.RotationDegrees = rotationDegrees;
    wBody.Type = type;
    mBodyParts.Enqueue(wBody);            
    AddChild(wBody);
    return wBody;
  }
  
  private void OnSnakeHeadAte()
  {
    mSize++;
    EmitSignal(nameof(Ate));
  }

  private void OnSnakeHeadCrashed()
  {
    EmitSignal(nameof(Crashed));
  }
  
  private String GetType(Turn turn)
  {
    switch(turn)
    {
      case Turn.Left: return "left";
      case Turn.Right: return "right";
      default: return "body";
    }
  }
  
  private void OnSnakeHeadMoved(Vector2 position, float rotationDegrees, Turn turn)
  {
    //
    // Add body parts
    //
    if (mBodyParts.Count < mSize)
    {
      var wBody = (SnakeBody)AddBody(position, rotationDegrees);
      wBody.Type = GetType(turn);
      return;
    }
    //
    // Move the snake bodies
    //
    var wNewFirst = mBodyParts.Dequeue();
    wNewFirst.Position = position;
    wNewFirst.RotationDegrees = rotationDegrees;
    wNewFirst.Type = GetType(turn);
    mBodyParts.Enqueue(wNewFirst);
    //
    // Set new tail
    //
    var wNewLast = mBodyParts.Peek();
    wNewLast.Type = "tail";
    var wBeforeLast = mBodyParts.Peek(1);
    wNewLast.RotationDegrees = wBeforeLast.RotationDegrees;
  }

}
