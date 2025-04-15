using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace lesson26_Platformer;

public class CelAnimationPlayer
{
    private CelAnimationSequence _celAnimationSequence;
    private int _celIndex;
    private int _celCountPlayingOnce;
    private enum State {PlayingContinuous, PlayingOnce}
    private State _state = State.PlayingContinuous;
    private float _celTimeElapsed;
    private Rectangle _celSourceRectangle;
    internal bool PlayedOnce => _state == State.PlayingOnce && _celCountPlayingOnce >= _celAnimationSequence.CelCount;
    /// <summary>
    /// Begins or continues playback of a CelAnimationSequence.
    /// </summary>
    internal void Play(CelAnimationSequence celAnimationSequence)
    {
        if (celAnimationSequence == null)
            throw new Exception("CelAnimationPlayer.PlayAnimation received null CelAnimationSequence");

        // If this animation is already running, do not restart it...
        if (celAnimationSequence != this._celAnimationSequence)
        {
            this._celAnimationSequence = celAnimationSequence;
            _celIndex = 0;
            _celTimeElapsed = 0.0f;

            _celSourceRectangle.X = 0;
            _celSourceRectangle.Y = 0;
            _celSourceRectangle.Width = this._celAnimationSequence.CelWidth;
            _celSourceRectangle.Height = this._celAnimationSequence.CelHeight;
        }
    }
    internal void PlayOnce(CelAnimationSequence celAnimationSequence)
    {
        _state = State.PlayingOnce;
        _celCountPlayingOnce = 0;
        Play(celAnimationSequence);
    }
    /// <summary>
    /// Update the state of the CelAnimationPlayer.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public void Update(GameTime GameTime)
    {
        if (_celAnimationSequence != null)
        {
            switch(_state)
            {
                case State.PlayingContinuous:
                    _celTimeElapsed += (float)GameTime.ElapsedGameTime.TotalSeconds;
                    if (_celTimeElapsed >= _celAnimationSequence.CelTime)
                    {
                        _celTimeElapsed -= _celAnimationSequence.CelTime;
                        // Advance the frame index looping as appropriate...
                        _celIndex = (_celIndex + 1) % _celAnimationSequence.CelCount;
                        _celSourceRectangle.X = _celIndex * _celSourceRectangle.Width;
                    }
                    break;
                case State.PlayingOnce:
                    if(_celCountPlayingOnce < _celAnimationSequence.CelCount)
                    {
                        _celTimeElapsed += (float)GameTime.ElapsedGameTime.TotalSeconds;
                        if (_celTimeElapsed >= _celAnimationSequence.CelTime)
                        {
                            _celTimeElapsed -= _celAnimationSequence.CelTime;
                            // Advance the frame index looping as appropriate...
                            _celIndex = (_celIndex + 1) % _celAnimationSequence.CelCount;
                            _celSourceRectangle.X = _celIndex * _celSourceRectangle.Width;
                            _celCountPlayingOnce++;  
                        }
                    }
                    break;
            }
        }
    }
    /// <summary>
    /// Draws the current cel of the animation.
    /// </summary>
    public void Draw(SpriteBatch SpriteBatch, Vector2 Position, SpriteEffects SpriteEffects)
    {
        switch(_state)
        {
            case State.PlayingContinuous:
            case State.PlayingOnce:
                if (_celAnimationSequence != null)
                {
                    SpriteBatch.Draw(_celAnimationSequence.Texture, Position, _celSourceRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects, 0.0f);
                }
                break;
        }
        
    }
}

