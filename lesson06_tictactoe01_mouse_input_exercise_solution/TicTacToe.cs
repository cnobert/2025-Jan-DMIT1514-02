using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson06_tictactoe01_mouse_input_exercise_solution;

public class TicTacToe : Game
{
    private const int _WindowWidth = 170, _WindowHeight = 170;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _gameBoard, _xImage, _oImage;
    private MouseState _currentMouseState, _previousMouseState;

    //enums are a way to create constants that have restricted value space 
    //(an enum can only be assigned the values that we choose)
    public enum GameSpaceState
    {
        X,
        O
    }
    private GameSpaceState _nextTokenToBePlayed = GameSpaceState.X;
    
    public TicTacToe()
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

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _gameBoard = Content.Load<Texture2D>("TicTacToeBoard");
        _xImage = Content.Load<Texture2D>("X");
        _oImage = Content.Load<Texture2D>("O");
    }

    protected override void Update(GameTime gameTime)
    {
        _currentMouseState = Mouse.GetState();

        //detect a "mouse up" event
        if(_previousMouseState.LeftButton == ButtonState.Pressed
            && _currentMouseState.LeftButton == ButtonState.Released
        )
        {
            //declare a data member that will remember the next token to be played
            //change Draw() so that it draws the next token to be played
            //when this "if" statement is entered, change the next token to be played
            if(_nextTokenToBePlayed == "x")
            {
                _nextTokenToBePlayed = "o";
            }
            else
            {
                _nextTokenToBePlayed = "x";
            }
        }


        _previousMouseState = _currentMouseState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_gameBoard, Vector2.Zero, Color.White);
        // Vector2 adjustedMousePosition = new Vector2(
        //         _currentMouseState.Position.X - (_xImage.Width / 2),
        //         _currentMouseState.Position.Y - (_xImage.Height / 2)
        // );
        Vector2 adjustedMousePosition =
            _currentMouseState.Position.ToVector2() - _xImage.Bounds.Center.ToVector2();

        if(_nextTokenToBePlayed == "x")
        {
            _spriteBatch.Draw(_xImage, adjustedMousePosition, Color.White);
        }
        else
        {
            _spriteBatch.Draw(_oImage, adjustedMousePosition, Color.White);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
