using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson11_Ball_class;

public class Pong : Game
{
    private const int _Scale = 3;
    private const int _WindowWidth = 250 * _Scale, _WindowHeight = 150 * _Scale;
    private const int _PlayAreaEdgeLineWidth = 4 * _Scale;

    private const int _PaddleHeight = 18 * _Scale, _PaddleWidth = 2 * _Scale;
    private const int _PaddleSpeed = 20 * _Scale;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backgroundTexture, _paddleTexture;
    private Rectangle _playAreaBoundingBox;

    private Ball _ball;

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

                        //initial position, initial direction, scale, game play area
        _ball.Initialize(new Vector2(50, 65),  new Vector2(-1, -1), _Scale, _playAreaBoundingBox);
        
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
        
        _ball.LoadContent(Content);

        _paddleTexture = Content.Load<Texture2D>("Paddle");
    }

    protected override void Update(GameTime gameTime)
    {
        
        _ball.Update(gameTime);
        
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

        KeyboardState kbState = Keyboard.GetState();

        if(kbState.IsKeyDown(Keys.W))
        {
            _paddleDirection = new Vector2(0, -1);
        }
        else if(kbState.IsKeyDown(Keys.S))
        {
            _paddleDirection = new Vector2(0, 1);
        }
        else //come to a stop if neither key is being pressed
        {
            _paddleDirection = new Vector2(0, 0);
        }
        _paddlePosition += _paddleDirection * _paddleSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

        //if they are at the top of the screen, the paddle is now one pixel too high
        //so, "pin" the paddle at exactly the top of the screen
        if(_paddlePosition.Y <= _playAreaBoundingBox.Top)
        {
            _paddlePosition.Y = _playAreaBoundingBox.Top;
        }
        else if((_paddlePosition.Y + _paddleDimensions.Y) >= _playAreaBoundingBox.Bottom) 
        {
            _paddlePosition.Y = _playAreaBoundingBox.Bottom - _paddleDimensions.Y;
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
