using Godot;
using System;

public class Walls : Node2D
{
  #pragma warning disable 649
	// We assign this in the editor, so we don't need the warning about not being assigned.
	[Export]
	public PackedScene WallScene;
  #pragma warning restore 649
  
  public override void _Ready()
  {
	var wScreenSize = GetViewportRect().Size;
	var wCenter = Common.GetCenter(wScreenSize);
	var wGridSize = Common.GetGrid(wScreenSize);
	
	var wHalfX = Mathf.Floor(wGridSize.x / 2) * Common.cSquareSize;
	var wHalfY = Mathf.Floor(wGridSize.y / 2) * Common.cSquareSize;
	var wLimitTopLeft = new Vector2(
	  x: wCenter.x - wHalfX,
	  y: wCenter.y - wHalfY
	);
	var wLimitBottomRight = new Vector2(
	  x: wCenter.x + wHalfX,
	  y: wCenter.y + wHalfY
	);
	for(var wX = wLimitTopLeft.x; wX <= wLimitBottomRight.x; wX+=Common.cSquareSize)
	{
	  var wWall = (StaticBody2D)WallScene.Instance();
	  wWall.Position = new Vector2(
		x: wX,
		y: wLimitTopLeft.y
	  );
	  AddChild(wWall);
	}
	for(var wX = wLimitTopLeft.x; wX <= wLimitBottomRight.x; wX+=Common.cSquareSize)
	{
	  var wWall = (StaticBody2D)WallScene.Instance();
	  wWall.Position = new Vector2(
		x: wX,
		y: wLimitBottomRight.y
	  );
	  AddChild(wWall);
	}
	for(var wY = wLimitTopLeft.y + Common.cSquareSize; wY <= wLimitBottomRight.y - Common.cSquareSize; wY+=Common.cSquareSize)
	{
	  var wWall = (StaticBody2D)WallScene.Instance();
	  wWall.Position = new Vector2(
		x: wLimitTopLeft.x,
		y: wY
	  );
	  AddChild(wWall);
	}
	for(var wY = wLimitTopLeft.y + Common.cSquareSize; wY <= wLimitBottomRight.y - Common.cSquareSize; wY+=Common.cSquareSize)
	{
	  var wWall = (StaticBody2D)WallScene.Instance();
	  wWall.Position = new Vector2(
		x: wLimitBottomRight.x,
		y: wY
	  );
	  AddChild(wWall);
	}
  }
  
}
