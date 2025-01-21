using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson05_keyboard_input;

public class KeyboardInputGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteFont _arial;
    private string _message = "Hi. It's warm(er) out now.";

    private KeyboardState _kbPreviousState;

    public KeyboardInputGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _arial = Content.Load<SpriteFont>("SystemArialFont");
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbCurrentState = Keyboard.GetState();

        _message = "";

        #region arrow keys
        if(kbCurrentState.IsKeyDown(Keys.Down)) //down arrow
        {
            _message += "Down ";
        }
        if(kbCurrentState.IsKeyDown(Keys.Up)) //up arrow
        {
            _message += "Up ";
        }
        if(kbCurrentState.IsKeyDown(Keys.Left)) //left arrow
        {
            _message += "Left ";
        }
        if(kbCurrentState.IsKeyDown(Keys.Right)) //right arrow
        {
            _message += "Right ";
        }
        #endregion

        #region "key down" event
        if(_kbPreviousState.IsKeyUp(Keys.Space) && kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "---------------------------------------------------------------------------";
            _message += "-----------------------------------------\n";
            _message += "---------------------------------------------------------------------------";
            _message += "-----------------------------------------\n";
            _message += "---------------------------------------------------------------------------";
            _message += "-----------------------------------------\n";
            _message += "---------------------------------------------------------------------------";
            _message += "-----------------------------------------\n";
        }
        #endregion
        //"key hold event
        else if(kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "Space ";
        }
        #region "key up" event
        else if (_kbPreviousState.IsKeyDown(Keys.Space))
        {
            //the space key is not being held down right now
            //but it was being held down on the last call to Update()
            //so, this is a "key up" event
            _message += "#########################################################################\n";
            _message += "#########################################################################\n";
            _message += "#########################################################################\n";
            _message += "#########################################################################\n";
            _message += "#########################################################################\n";
        }
        #endregion

        _kbPreviousState = kbCurrentState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();
        _spriteBatch.DrawString(_arial, _message, Vector2.Zero, Color.DarkSalmon);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
