using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace day02_zz_exercise_solution;

public class BouncingSprite : Game
{
    private const int _WindowWidth = 640, _WindowHeight = 320;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _backgroundImage, _beetleImage;
    private float _x = 0, _velocityShipX = 5;
    private float _y = 0, _velocityShipY = 5;
    private float _shipRotation = 0;

    public BouncingSprite()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundImage = Content.Load<Texture2D>("Station");
        _beetleImage = Content.Load<Texture2D>("Beetle");
    }

    protected override void Update(GameTime gameTime)
    {
        //just adding "1" will always make x move rightwards
        //_x = _x + 1;
        //so, we need to introduce another variable, _velocityShipX

        _x += _velocityShipX;
        _y += _velocityShipY;
        //_graphics.GraphicsDevice.Viewport.Width (there's a Height property as well)
        if(_x + _beetleImage.Width >= _WindowWidth || _x <= 0)
        {
            _velocityShipX *= -1;
        }
        if(_y + _beetleImage.Height >= _WindowHeight || _y <= 0)
        {
            _velocityShipY *= -1;
        }
        _shipRotation += 0.1f;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        //Watch out! Order matters! More recent draw calls draw over older ones.
        //for example, in this situation the beetle/ship is hidden by the background:
        //_spriteBatch.Draw(_beetleImage, new Vector2(_x, _y), Color.White);
        //_spriteBatch.Draw(_backgroundImage, Vector2.Zero, Color.White);
        
        _spriteBatch.Draw(_backgroundImage, Vector2.Zero, Color.White);

        //_spriteBatch.Draw(_beetleImage, new Vector2(_x, _y), Color.White);
        _spriteBatch.Draw(
            _beetleImage,          // Texture
            new Vector2(_x, _y),         // Position
            null,                   // Source rectangle (null means use the whole texture)
            Color.White,                  // Color tint
            _shipRotation,               // Rotation in radians
            new Vector2(_beetleImage.Width/ 2, _beetleImage.Height / 2),                 // Origin point for rotation
            1,                  // Scale
            SpriteEffects.None,     // Sprite effects (None, FlipHorizontally, etc.)
            0f                      // Layer depth (0 means frontmost)
        );
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
