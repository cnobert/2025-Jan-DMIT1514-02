using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson14_Pong_Final;

public class Ball
{
    private const int _WidthAndHeight = 7, _Speed = 170, _CollisionTimerIntervalMillis = 400;
    private Texture2D _texture;
    private Vector2 _position, _direction, _dimensions;
    private float _speed;
    private int _gameScale;

    private int _collisionTimerMillis;
    private Rectangle _playAreaBoundingBox;
    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(_position.ToPoint(), _dimensions.ToPoint());
        }
    }
    internal void Initialize(Vector2 initialPosition, Vector2 initialDirection, int gameScale, Rectangle playAreaBoundingBox)
    {
        _position = initialPosition * gameScale;
        _direction = initialDirection;
        _gameScale = gameScale;
        _speed = _Speed * _gameScale;
        _dimensions = new Vector2(_WidthAndHeight) * _gameScale;
        _playAreaBoundingBox = playAreaBoundingBox;
    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Ball");
    }

    internal void Update(GameTime gameTime)
    {
        _collisionTimerMillis += gameTime.ElapsedGameTime.Milliseconds;
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

    internal void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, null, Color.Red, 0, Vector2.Zero, _gameScale, SpriteEffects.None, 0f);

    }
    
    internal void ProcessCollision(Rectangle otherBoundingBox)
    {
        if(_collisionTimerMillis >= _CollisionTimerIntervalMillis && BoundingBox.Intersects(otherBoundingBox))
        {
            _collisionTimerMillis = 0;
            //collision!
            Rectangle intersection = Rectangle.Intersect(BoundingBox, otherBoundingBox);
            if(intersection.Width > intersection.Height)
            {
                //this is a horizontal rectangle, therefore it's a top or bottom collision
                _direction.Y *= -1;
            }
            else
            {
                //this is a vertical rectangle, therefore it's a side collision
                _direction.X *= -1;
            }
        }
    }
}