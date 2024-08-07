using Godot;
using System;

internal static class Common
{  
  public const float cSquareSize = 16.0f;
  
  public static Vector2 GetCenter(Vector2 screenSize)
  {
    return new Vector2(
      x: screenSize.x / 2,
      y: screenSize.y / 2
    );
  }
  
  public static Vector2 GetGrid(Vector2 screenSize)
  {
    return new Vector2(
      x: Mathf.Floor(screenSize.x / Common.cSquareSize),
      y: Mathf.Floor(screenSize.y / Common.cSquareSize)
    );
  }
}
