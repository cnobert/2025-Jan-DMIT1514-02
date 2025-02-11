using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson09_pong_begin;

public class Pong : Game
{
    private const int _WindowWidth = 250, _WindowHeight = 150, _BallWidthAndHeight = 7;
    private const int _PlayAreaEdgeLineWidth = 4;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backgroundTexture, _ballTexture;

    private Rectangle _playAreaBoundingBox;

    private Vector2 _ballPosition, _ballDirection;
    private float _ballSpeed;

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

        _ballPosition.X = 50;
        _ballPosition.Y = 65;

        _ballSpeed = 20;
        _ballDirection.X = -1;
        _ballDirection.Y = -1;

        _playAreaBoundingBox = new Rectangle(0, 0, _WindowWidth, _WindowHeight);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
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
        _spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_ballTexture, _ballPosition, Color.White);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
