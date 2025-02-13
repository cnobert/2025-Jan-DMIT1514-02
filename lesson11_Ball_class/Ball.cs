using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson11_Ball_class;

public class Ball
{
    private const int _WidthAndHeight = 7, _Speed = 175;
    private Texture2D _texture;
    private Vector2 _position, _direction, _dimensions;
    private float _speed;
    private int _gameScale;
    private Rectangle _playAreaBoundingBox;

    internal void Initialize(Vector2 initialPosition, Vector2 initialDirection, int gameScale, Rectangle playAreaBoundingBox)
    {
        _position = initialPosition;
        _direction = initialDirection;
        _gameScale = gameScale;
        _speed = _Speed * _gameScale;
        _dimensions = new Vector2(_WidthAndHeight) * _gameScale;
    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Ball");
    }

    internal void Update(GameTime gameTime)
    {
        _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

        //bounce ball off left and right sides
        if(_position.X <= _playAreaBoundingBox.Left || (_position.X + _dimensions.X) >= _playAreaBoundingBox.Right)
        {
            _direction.X *= -1;
        }
        //bounce ball of top and bottom
        if  (_position.Y <= (_playAreaBoundingBox.Top) || 
                (_position.Y + _dimensions.Y) >= (_playAreaBoundingBox.Bottom)
            )
        {
            _direction.Y *= -1;
        }
    }

}