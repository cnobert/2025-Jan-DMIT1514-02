using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson14_Pong_Final;

public class Pong : Game
{
    private const int _Scale = 3;
    private const int _WindowWidth = 250 * _Scale, _WindowHeight = 150 * _Scale;
    private const int _PlayAreaEdgeLineWidth = 4 * _Scale;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backgroundTexture;
    private Rectangle _playAreaBoundingBox;
    private Ball _ball;
    private Paddle _paddle;
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

        _playAreaBoundingBox = new Rectangle(0, _PlayAreaEdgeLineWidth, _WindowWidth, _WindowHeight - 2 * _PlayAreaEdgeLineWidth);
        
        _ball = new Ball();
        _ball.Initialize(new Vector2(150, 130),  new Vector2(1, -1), _Scale, _playAreaBoundingBox);
        _paddle = new Paddle();
        _paddle.Initialize(new Vector2(210, 75), _Scale, _playAreaBoundingBox);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        
        _ball.LoadContent(Content);
        _paddle.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        
        
        #region keybard input
        KeyboardState kbState = Keyboard.GetState();
        if(kbState.IsKeyDown(Keys.W))
        {
            _paddle.Direction = new Vector2(0, -1);
        }
        else if(kbState.IsKeyDown(Keys.S))
        {
            _paddle.Direction = new Vector2(0, 1);
        }
        else //come to a stop if neither key is being pressed
        {
            _paddle.Direction = new Vector2(0, 0);
        }
        #endregion
        

        _ball.Update(gameTime);
        _paddle.Update(gameTime);
        //_paddle01.Update(gameTime);
        //_paddle02.Update(gameTime);
        //_blocker.Update(gameTime);

        if(_ball.ProcessCollision(_paddle.BoundingBox))
        {
            ;
        }
        /*
            if(_ball.ProcessCollision(_paddle01.BoundingBox))
            {
                _hud.Paddle01Score++;
                _paddle01.Glow();
            }

        */
        //_ball.ProcessCollision(_paddle01.BoundingBox);
        //_ball.ProcessCollision(_paddle02.BoundingBox);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_backgroundTexture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, _Scale, SpriteEffects.None, 0f);

        _ball.Draw(_spriteBatch);
        _paddle.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
