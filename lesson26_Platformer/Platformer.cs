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

    private Collider _colliderTop, _colliderRight, _colliderBottom, _colliderLeft;
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

        //the top collider's top left corner is at 160, 270
        //the right collider needs to begin at 250,270 PLUS THE WIDTH OF THE TOP COLLIDER
        _colliderTop = new Collider(new Vector2(160, 270), new Vector2(80, 1), Collider.ColliderType.Top);
        _colliderRight = new Collider(new Vector2(250, 270), new Vector2(1, 20), Collider.ColliderType.Right);
        _colliderBottom = new Collider(new Vector2(160, 290), new Vector2(80, 1), Collider.ColliderType.Bottom);
        _colliderLeft = new Collider(new Vector2(150, 270), new Vector2(1, 20), Collider.ColliderType.Left);

        base.Initialize();
        _player.Initialize();
    }
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _player.LoadContent(Content);
        _ground.LoadContent(Content);
        _colliderTop.LoadContent(Content);
        _colliderRight.LoadContent(Content);
        _colliderBottom.LoadContent(Content);
        _colliderLeft.LoadContent(Content);
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

        
        _ground.ProcessCollision(_player, gameTime);
        _colliderTop.ProcessCollision(_player, gameTime);
        _colliderRight.ProcessCollision(_player, gameTime);
        _colliderBottom.ProcessCollision(_player, gameTime);
        _colliderLeft.ProcessCollision(_player, gameTime);
        
        _player.Update(gameTime);
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.AntiqueWhite);
        _spriteBatch.Begin();
        _player.Draw(_spriteBatch);
        _ground.Draw(_spriteBatch);
        _colliderTop.Draw(_spriteBatch);
        _colliderRight.Draw(_spriteBatch);
        _colliderBottom.Draw(_spriteBatch);
        _colliderLeft.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
