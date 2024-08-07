using Godot;
using System;

public class SnakeBody : StaticBody2D
{
  private AnimatedSprite mAnimatedSprite;
    
  public String Type { 
    get
    {
      mAnimatedSprite = mAnimatedSprite ?? GetNode<AnimatedSprite>("AnimatedSprite");
      return mAnimatedSprite.Animation;
    }
    set
    {
      mAnimatedSprite = mAnimatedSprite ?? GetNode<AnimatedSprite>("AnimatedSprite");
      mAnimatedSprite.Animation = value;
    } 
  }
}
