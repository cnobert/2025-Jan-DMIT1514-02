using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson12_Ball_and_Paddle;

public class Paddle
{
    private const int _Height = 18, _Width = 2, _Speed = 200;
    private Texture2D _texture;
    private Vector2 _position, _direction, _dimensions;
    private float _speed;
    private int _gameScale;
    private Rectangle _playAreaBoundingBox;
    internal Vector2 Direction 
    { 
        set => _direction = value; 
    }
    internal void Initialize(Vector2 initialPosition, int gameScale, Rectangle playAreaBoundingBox)
    {
        _position = initialPosition * gameScale;
        _gameScale = gameScale;
        _playAreaBoundingBox = playAreaBoundingBox;

        _speed = _Speed * _gameScale;
        _dimensions = new Vector2(_Width, _Height) * _gameScale;
    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Paddle");
    }

    internal void Update(GameTime gameTime)
    {
        _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

        //if they are at the top of the screen, the paddle is now one pixel too high
        //so, "pin" the paddle at exactly the top of the screen
        if(_position.Y <= _playAreaBoundingBox.Top)
        {
            _position.Y = _playAreaBoundingBox.Top;
        }
        else if((_position.Y + _dimensions.Y) >= _playAreaBoundingBox.Bottom) 
        {
            _position.Y = _playAreaBoundingBox.Bottom - _dimensions.Y;
        }
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, null, Color.Orange, 0, Vector2.Zero, _gameScale, SpriteEffects.None, 0f);
    }
}