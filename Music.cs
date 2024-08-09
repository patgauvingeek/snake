using Godot;
using System;

public class Music : Node2D
{
  private AudioStreamPlayer mMusic1;
  private AudioStreamPlayer mMusic2;
  private AudioStreamPlayer mMusic3;
  private AudioStreamPlayer mMusic4;
  private AudioStreamPlayer mMusic5;
  private AudioStreamPlayer mMusicLoop;
  private AudioStreamPlayer mMusicEnd;
  
  private AudioStreamPlayer mCurrent;

  public override void _Ready()
  {
    mMusic1 = GetNode<AudioStreamPlayer>("Music1");
    mMusic2 = GetNode<AudioStreamPlayer>("Music2");
    mMusic3 = GetNode<AudioStreamPlayer>("Music3");
    mMusic4 = GetNode<AudioStreamPlayer>("Music4");
    mMusic5 = GetNode<AudioStreamPlayer>("Music5");
    mMusicLoop = GetNode<AudioStreamPlayer>("MusicLoop");
    mMusicEnd = GetNode<AudioStreamPlayer>("MusicEnd");
    mCurrent = mMusic1;
  }
  
  public void Start()
  {
    Pause();
    mCurrent = mMusic1;
    PlayFromBeginning();
  }
    
  public void Play()
  {
    mCurrent.StreamPaused = false;
  }
  
  public void Pause()
  {
    mCurrent.StreamPaused = true;
  }

  public void Stop()
  {
    Pause();
    mCurrent = mMusic1;
  }
  
  private void PlayFromBeginning()
  {
    mCurrent.StreamPaused = false;
    mCurrent.Play(0);
  }

  private void OnMusic1Finished()
  {
    mCurrent = mMusic2;
    PlayFromBeginning();
  }

  private void OnMusic2Finished()
  {
    mCurrent = mMusic3;
    PlayFromBeginning();
  }

  private void OnMusic3Finished()
  {
    mCurrent = mMusic4;
    PlayFromBeginning();
  }

  private void OnMusic4Finished()
  {
    mCurrent = mMusic5;
    PlayFromBeginning();
  }

  private void OnMusic5Finished()
  {
    mCurrent = mMusicLoop;
    PlayFromBeginning();
  }

  private void OnMusicLoopFinished()
  {
    mCurrent = mMusic1;
    // if crash musicEnd else restart music loop ?
  }

}
