using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson13_TicTacToe_HUD;

public class TicTacToe : Game
{
    private const int _WindowWidth = 170, _WindowHeight = 170;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _gameBoardImage, _xImage, _oImage;
    private MouseState _currentMouseState, _previousMouseState;

    private HUD _hud; //this class should draw the HUD and store certain pieces of game state information

#region data structures
    public enum GameSpaceState
    {
        X, O, Empty
    }
    private GameSpaceState _nextTokenToBePlayed = GameSpaceState.X;
    
    private GameSpaceState[,] _gameBoard = 
        new GameSpaceState[,]
        {
            {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty},//row 0
            {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty},//row 1
            {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty} //row 2
        };

    public enum GameState
    {
        Initialize, WaitForPlayerMove, MakePlayerMove,
        EvaluatePlayerMove, GameOver
    }
    private GameState _currentGameState = GameState.Initialize;
#endregion
#region  setup methods
    public TicTacToe()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight + HUD.Height;
        _graphics.ApplyChanges();

        _hud = new HUD();
        //pass the HUD it's position (where its top left corner begins)
        _hud.Initialize(new Vector2(0, _WindowHeight));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _gameBoardImage = Content.Load<Texture2D>("TicTacToeBoard");
        _xImage = Content.Load<Texture2D>("X");
        _oImage = Content.Load<Texture2D>("O");
        
        _hud.LoadContent(this.Content);
    }
#endregion
    protected override void Update(GameTime gameTime)
    {
        _currentMouseState = Mouse.GetState();

        switch(_currentGameState)
        {
            case GameState.Initialize:
                _nextTokenToBePlayed = GameSpaceState.X;
                //TODO: set all game board spaces to empty
                //Exercise: write a nested for loop that sets all game board spaces to "empty"
                // for(int row = 0; row < _gameBoard.GetLength(0); row++)
                // {
                //     for(int column = 0; column < _gameBoard.GetLength(1); column++)
                //     {
                //         _gameBoard[row, column] = GameSpaceState.Empty;
                //     }
                // }

                _currentGameState = GameState.WaitForPlayerMove;
                break;
            case GameState.WaitForPlayerMove:
                //"mouse up" event
                if(_previousMouseState.LeftButton == ButtonState.Pressed
                    && _currentMouseState.LeftButton == ButtonState.Released
                )
                {
                    //todo: check if this move is valid
                    int x = _currentMouseState.X;
                    int y = _currentMouseState.Y;

                    int correspondingGameBoardRow = y / _xImage.Height;
                    int correspondingGameBoardColumn = x / _oImage.Width;

                    //now, we can use the above two variables to access _gameBoard
                    //and see if the move is valid
                    //if the move is invalid, do nothing

                    //if this is a valid move:
                    _currentGameState = GameState.MakePlayerMove;
                }
                break;
            case GameState.MakePlayerMove:
                //todo: using the technique from above, 
                //convert from the clicked-on pixels to the 2D array
                //and place the token in the game space (in _gameBoard)

                _currentGameState = GameState.EvaluatePlayerMove;
                break;
            case GameState.EvaluatePlayerMove:
                //todo: determine if there is a winner
                //was there a winner? if so, set IsMouseVisible to true, and move to GameOver
                //else, change nextTokenToBePlayed and then go to WaitForPlayerMove
                if(_nextTokenToBePlayed == GameSpaceState.X)
                {
                    _nextTokenToBePlayed = GameSpaceState.O;
                    _hud.XTurnCount++;
                }
                else
                {
                    _nextTokenToBePlayed = GameSpaceState.X;
                    _hud.OTurnCount++;
                }
                _currentGameState = GameState.WaitForPlayerMove;

                //if we detect a winner:
                _hud.Message = "X wins, click anywhere to play again";
                break;
            case GameState.GameOver:
                //wait for a click anywhere, and then change _currentGameState to Initialize


                break;
        }
        
        _previousMouseState = _currentMouseState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightSalmon);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_gameBoardImage, Vector2.Zero, Color.White);

        _hud.Draw(_spriteBatch);

        this.DrawCurrentGameBoard();
        switch(_currentGameState)
        {
            case GameState.Initialize:
                break;
            case GameState.WaitForPlayerMove:
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
                break;
            case GameState.MakePlayerMove:
                break;
            case GameState.EvaluatePlayerMove:
                break;
            case GameState.GameOver:
            //todo:
            //1. draw the "game over" background (you will find this asset and import it
            //2. out the "X has won, click anywhere to play again" message
            //3. for 10/10 marks, somehow make the winning line visible
                break;
        }

        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
    private void DrawCurrentGameBoard()
    {
        //todo: 
        //1. make it draw "O" tokens as well
        //2. make it take into account line widths when drawing
        for(int row = 0; row < _gameBoard.GetLength(0); row++)
        {
            for(int column = 0; column < _gameBoard.GetLength(1); column++)
            {
                if(_gameBoard[row, column] == GameSpaceState.X)
                {
                    _spriteBatch.Draw(_xImage, new Vector2(column * _xImage.Width, row * _xImage.Height), Color.White);
                }
            }
        }
    }
}
