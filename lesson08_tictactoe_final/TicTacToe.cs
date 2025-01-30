using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson08_tictactoe_final;

public class TicTacToe : Game
{
    private const int _WindowWidth = 170, _WindowHeight = 170;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _gameBoard, _xImage, _oImage;
    private MouseState _currentMouseState, _previousMouseState;

    public enum GameSpaceState
    {
        X, O, Empty
    }
    private GameSpaceState _nextTokenToBePlayed = GameSpaceState.X;
    
    public enum GameState
    {
        Initialize, WaitForPlayerMove, MakePlayerMove,
        EvaluatePlayerMove, GameOver
    }
    private GameState _currentGameState = GameState.Initialize;

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

        switch(_currentGameState)
        {
            case GameState.Initialize:
                _nextTokenToBePlayed = GameSpaceState.X;
                //TODO: set all game board spaces to empty
                _currentGameState = GameState.WaitForPlayerMove;
                break;
            case GameState.WaitForPlayerMove:
                //"mouse up" event
                if(_previousMouseState.LeftButton == ButtonState.Pressed
                    && _currentMouseState.LeftButton == ButtonState.Released
                )
                {
                    //todo: check if this move is valid
                    //if this is a valid move:
                    _currentGameState = GameState.MakePlayerMove;
                }
                break;
            case GameState.MakePlayerMove:
                //todo: place the token in the game space
                _currentGameState = GameState.EvaluatePlayerMove;
                break;
            case GameState.EvaluatePlayerMove:
                //todo: determine if there is a winner
                //was there a winner? if so, move to gameover
                //otherwise, change nextTokenToBePlayed
                if(_nextTokenToBePlayed == GameSpaceState.X)
                {
                    _nextTokenToBePlayed = GameSpaceState.O;
                }
                else
                {
                    _nextTokenToBePlayed = GameSpaceState.X;
                }
                _currentGameState = GameState.WaitForPlayerMove;
                break;
            case GameState.GameOver:
                break;
        }
        
        _previousMouseState = _currentMouseState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightSalmon);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_gameBoard, Vector2.Zero, Color.White);

        switch(_currentGameState)
        {
            case GameState.Initialize:
                break;
            case GameState.WaitForPlayerMove:
                break;
            case GameState.MakePlayerMove:
                break;
            case GameState.EvaluatePlayerMove:
                break;
            case GameState.GameOver:
                break;
        }

        Vector2 adjustedMousePosition =
            _currentMouseState.Position.ToVector2() - _xImage.Bounds.Center.ToVector2();

        if(_nextTokenToBePlayed == GameSpaceState.X)
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
