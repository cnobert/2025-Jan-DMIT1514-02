using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson26_Platformer;

public class Platformer : Game
{
    private const int _WindowWidth = 550, _WindowHeight = 400;
    internal const int _Gravity = 80;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Rectangle _gameBoundingBox;
    private Player _player;
    private Collider _ground;

    private List<Platform> _platforms;
    public Platformer()
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
        _gameBoundingBox = new Rectangle(0, 0, _WindowWidth, _WindowHeight);
        _player = new Player(new Vector2(200, 50), _gameBoundingBox);
        _ground = new Collider(new Vector2(0, 300), new Vector2(_WindowWidth, 1), Collider.ColliderType.Top);

        _platforms = new List<Platform>();
        _platforms.Add(new Platform(new Vector2(200, 200), new Vector2(50, 25), ""));
        _platforms.Add(new Platform(new Vector2(150, 150), new Vector2(50, 25), ""));
        _platforms.Add(new Platform(new Vector2(250, 200), new Vector2(50, 25), ""));
        _platforms.Add(new Platform(new Vector2(350, 250), new Vector2(50, 25), ""));
        _platforms.Add(new Platform(new Vector2(450, 300), new Vector2(50, 25), ""));

        base.Initialize();
        _player.Initialize();
    }
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _player.LoadContent(Content);
        _ground.LoadContent(Content);
        foreach(Platform platform in _platforms)
        {
            platform.LoadContent(Content);
        }
    }
    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbState = Keyboard.GetState();
        if(kbState.IsKeyDown(Keys.Left))
        {
            _player.MoveHorizontally(-1);
        }
        else if (kbState.IsKeyDown(Keys.Right))
        {
            _player.MoveHorizontally(1);
        }
        else
        {
            _player.Stop();
        }
        if(kbState.IsKeyDown(Keys.Space))
        {
            _player.Jump();
        }

        
        _ground.ProcessCollision(_player, gameTime);

        foreach(Platform platform in _platforms)
        {
            platform.ProcessCollisions(_player, gameTime);
        }    
        
        _player.Update(gameTime);
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.AntiqueWhite);
        _spriteBatch.Begin();
        _player.Draw(_spriteBatch);
        _ground.Draw(_spriteBatch);
        foreach(Platform platform in _platforms)
        {
            platform.Draw(_spriteBatch);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
