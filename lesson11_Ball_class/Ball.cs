using Microsoft.Xna.Framework;
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
        _dimen
    }

}