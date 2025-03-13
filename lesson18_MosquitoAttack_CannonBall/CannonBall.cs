using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson18_MosquitoAttack_CannonBall;

public class CannonBall 
{
    private Vector2 _position, _direction;
    private float _speed;
    private Rectangle _gameBoundingBox;
    private Texture2D _texture;

    private enum State 
    {
        Flying,
        NotFlying
    }
    private State _state;
    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(_position.ToPoint(), new Point(_texture.Width, _texture.Height));
        }
    }
    internal void Initialize(Vector2 position, Rectangle gameBoundingBox, float speed, Vector2 direction)
    {
        _direction = direction;
        _position = position;
        _speed = speed;
        _gameBoundingBox = gameBoundingBox;
        _state = State.NotFlying;
    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("CannonBall");
    }
    internal void Update(GameTime gameTime)
    {
        switch(_state)
        {
            case State.Flying:
                _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
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
                spriteBatch.Draw(_texture, _position, Color.White);
                break;
            case State.NotFlying:
                break;
        }
    }
    internal void Shoot(Vector2 position, Vector2 direction, float speed)
    {
        if(_state == State.NotFlying)
        {
            //assuming that the position passed down is where the centre of the cannonBall should be
            _position = new Vector2(position.X - _texture.Width / 2, position.Y);
            _direction = direction;
            _speed = speed;
            _state = State.Flying;
        }
    }
}