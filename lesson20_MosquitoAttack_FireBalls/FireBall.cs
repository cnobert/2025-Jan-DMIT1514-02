using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson20_MosquitoAttack_FireBalls;

public class FireBall
{
    private const float Speed = 150;
    private Vector2 _position, _direction;
    private float _speed;
    private Rectangle _gameBoundingBox;

    private enum State 
    {
        Flying,
        NotFlying
    }
    private State _state;
    private CelAnimationSequence _animationSequence;
    private CelAnimationPlayer _animationPlayer;
    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(_position.ToPoint(), new Point(_animationSequence.CelWidth, _animationSequence.CelHeight));
        }
    }

    public FireBall()
    {
        _animationPlayer = new CelAnimationPlayer();
    }

    internal void Initialize(Rectangle gameBoundingBox)
    {
        _gameBoundingBox = gameBoundingBox;
        _state = State.NotFlying;
        _animationPlayer.Play(_animationSequence);
    }
    internal void LoadContent(ContentManager content)
    {
        _animationSequence = new CelAnimationSequence(content.Load<Texture2D>("FireBall"), 5, 1 / 8f);
    }
    internal void Update(GameTime gameTime)
    {
        switch(_state)
        {
            case State.Flying:
                _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
                if(!BoundingBox.Intersects(_gameBoundingBox))
                {
                    _state = State.NotFlying;
                }
                _animationPlayer.Update(gameTime);
                break;
            case State.NotFlying:
                break;
        }
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        switch(_state)
        {
            case State.Flying:
                _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
                break;
            case State.NotFlying:
                break;
        }
    }
}