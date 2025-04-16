using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson26_Platformer;

public class Platformer : Game
{
    private const int _WindowWidth = 550, _WindowHeight = 400;
    internal const int _Gravity = 60;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Rectangle _gameBoundingBox;
    private Player _player;
    private Collider _ground;
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
        _player = new Player(new Vector2(50, 50), _gameBoundingBox);
        _ground = new Collider(new Vector2(30, 300), new Vector2(100, 1), Collider.ColliderType.Top);
        base.Initialize();
        _player.Initialize();
    }
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _player.LoadContent(Content);
        _ground.LoadContent(Content);
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

        _player.Update(gameTime);
        _ground.ProcessCollision(_player, gameTime);
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _player.Draw(_spriteBatch);
        _ground.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
