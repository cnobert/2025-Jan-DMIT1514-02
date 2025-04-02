using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson24_MosquitoAttack_More_Inheritance;

public abstract class GameBot
{
    protected CelAnimationSequence _animationSequenceAlive;
    protected CelAnimationSequence _animationSequenceDying;
    protected CelAnimationPlayer _animationPlayer;
    protected Vector2 _position, _direction;
    protected float _speed;
    protected Rectangle _gameBoundingBox;
    internal Rectangle BoundingBox 
        {get => new Rectangle((int) _position.X, (int) _position.Y, _animationSequenceAlive.CelWidth, _animationSequenceAlive.CelHeight);}

    protected enum State
        {
            Alive, Dying, Dead
        }
    protected State _state;
    internal bool Alive => _state == State.Alive;
    internal virtual void Initialize(Vector2 initialPosition, Rectangle gameBoundingBox, float speed)
    {
        _position = initialPosition;
        _animationPlayer = new CelAnimationPlayer();
        _animationPlayer.Play(_animationSequenceAlive);
        _speed = speed;
        _gameBoundingBox = gameBoundingBox;
        _state = State.Alive;
    }
    internal abstract void LoadContent(ContentManager content);

    internal virtual void Update(GameTime gameTime)
    {
        switch(_state)
        {
            case State.Alive:
                _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
                break;
            case State.Dying:
                break;
            case State.Dead:
                break;
        }
    }

    internal virtual void Draw(SpriteBatch spriteBatch)
    {
        switch(_state)
        {
            case State.Alive:
            case State.Dying:
                _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
                break;
            case State.Dead:
                break;
        }
    }

    internal abstract void Shoot();
}