using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson10_scale_and_paddle;

public class Pong : Game
{
    private const int _Scale = 3;
    private const int _WindowWidth = 250 * _Scale, _WindowHeight = 150 * _Scale;
    private const int _PlayAreaEdgeLineWidth = 4 * _Scale;

    private const int _BallWidthAndHeight = 7 * _Scale, _BallSpeed = 175 * _Scale;
    private const int _PaddleHeight = 18 * _Scale, _PaddleWidth = 2 * _Scale, _PaddleSpeed = 200 * _Scale;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backgroundTexture, _ballTexture, _paddleTexture;

    private Rectangle _playAreaBoundingBox;

    private Vector2 _ballPosition, _ballDirection;
    private float _ballSpeed;

    private Vector2 _paddlePosition, _paddleDirection, _paddleDimensions;
    private float _paddleSpeed;

    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        _ballPosition = new Vector2(50 * _Scale, 65 * _Scale);
        _ballDirection = new Vector2(-1, -1);
        _ballSpeed = _BallSpeed;
        
        _paddlePosition = new Vector2(215 * _Scale, 75 * _Scale);
        _paddleSpeed = _PaddleSpeed;
        _paddleDimensions = new Vector2(_PaddleWidth, _PaddleHeight);

        _playAreaBoundingBox = new Rectangle(0, 0, _WindowWidth, _WindowHeight);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
        _paddleTexture = Content.Load<Texture2D>("Paddle");
    }

    protected override void Update(GameTime gameTime)
    {
        //in-class exercise #1:
        //make the ball move, according to its speed and direction
        _ballPosition += _ballDirection * _ballSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

        //bounce ball off left and right sides
        if(_ballPosition.X <= _playAreaBoundingBox.Left || (_ballPosition.X + _BallWidthAndHeight) >= _playAreaBoundingBox.Right)
        {
            _ballDirection.X *= -1;
        }
        //bounce ball of top and bottom
        if  (_ballPosition.Y <= (_playAreaBoundingBox.Top + _PlayAreaEdgeLineWidth) || 
                (_ballPosition.Y + _BallWidthAndHeight) >= (_playAreaBoundingBox.Bottom - _PlayAreaEdgeLineWidth)
            )
        {
            _ballDirection.Y *= -1;
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_backgroundTexture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, _Scale, SpriteEffects.None, 0f);

        //params: texture to draw, position, sourceRectangle, color, rotation, origin, SCALE, SpriteEffects, layer depth
        _spriteBatch.Draw(_ballTexture, _ballPosition, null, Color.White, 0, Vector2.Zero, _Scale, SpriteEffects.None, 0f);

        _spriteBatch.Draw(_paddleTexture, _paddlePosition, null, Color.White, 0, Vector2.Zero, _Scale, SpriteEffects.None, 0f);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
