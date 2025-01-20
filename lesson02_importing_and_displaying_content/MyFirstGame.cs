
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson02_importing_and_displaying_content;

public class MyFirstGame : Game
{
    private const int WindowWidth = 640, WindowHeight = 320;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteFont _arialFont;
    private string _message = "the message";

    public MyFirstGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
            _graphics.PreferredBackBufferWidth = WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowHeight;
            _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _arialFont = Content.Load<SpriteFont>("SystemArialFont");

    }

    protected override void Update(GameTime gameTime)
    {
       

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.DrawString(_arialFont, _message, Vector2.Zero, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
