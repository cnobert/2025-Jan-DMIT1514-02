using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson20_MosquitoAttack_FireBalls;

public class MosquitoAttack : Game
{
    private const int _WindowWidth = 550, _WindowHeight = 400, _NumMosquitoes = 20;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _background;
    private SpriteFont _arial;

    private Cannon _cannon;

    private Mosquito [] _mosquitoes;

    private enum GameState { Playing, Paused, Over }
    private GameState _gameState;

    private KeyboardState _kbPreviousState;
    private string _status = "";

    public MosquitoAttack()
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

        _cannon = new Cannon();

        _mosquitoes = new Mosquito[_NumMosquitoes];
        for(int c = 0; c < _NumMosquitoes; c++)
        {
            _mosquitoes[c] = new Mosquito(); 
        }
        base.Initialize(); //this method call invokes LoadContent, thereby making cannon._animationSequence exist

        Rectangle gameBoundingBox = new Rectangle(0, 0, _WindowWidth, _WindowHeight);
        _cannon.Initialize(new Vector2(50, 325), gameBoundingBox);

        Random random = new Random();
        foreach(Mosquito mosquito in _mosquitoes)
        {
            int direction = random.Next(1, 3);
            if(direction == 2)
            {
                direction = -1;
            }
            int xPosition = random.Next(1, _WindowWidth - mosquito.BoundingBox.Width);
            int speed = random.Next(50, 251);
            mosquito.Initialize(new Vector2(xPosition, 25), gameBoundingBox, speed, new Vector2(direction, 0));
        }
        _gameState = GameState.Playing;
        _kbPreviousState = Keyboard.GetState();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _background = Content.Load<Texture2D>("Background");
        _arial = Content.Load<SpriteFont>("SystemArialFont");
        _cannon.LoadContent(Content);
        
        foreach(Mosquito mosquito in _mosquitoes)
        {
            mosquito.LoadContent(Content);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbState = Keyboard.GetState();

        switch(_gameState)
        {
            case GameState.Playing:
                if(kbState.IsKeyDown(Keys.Left))
                {
                    _cannon.Direction = new Vector2(-1, 0);
                }
                else if(kbState.IsKeyDown(Keys.Right))
                {
                    _cannon.Direction = new Vector2(1, 0);
                }
                else //come to a stop if neither key is being pressed
                {
                    _cannon.Direction = new Vector2(0, 0);
                }
                if(kbState.IsKeyDown(Keys.Space) && _kbPreviousState.IsKeyUp(Keys.Space))
                {
                    _cannon.Shoot();
                }
                _cannon.Update(gameTime);
                
                foreach(Mosquito mosquito in _mosquitoes)
                {
                    mosquito.Update(gameTime);
                    if(mosquito.Alive && _cannon.ProcessCollision(mosquito.BoundingBox))
                    {
                        mosquito.Die();
                    }
                }
                //is this a new key down event?
                if(kbState.IsKeyDown(Keys.P) && _kbPreviousState.IsKeyUp(Keys.P))
                {
                    _gameState = GameState.Paused;
                    _status = "Game paused. Press P to start playing again.";
                }
                break;
            case GameState.Paused:
                if(kbState.IsKeyDown(Keys.P) && _kbPreviousState.IsKeyUp(Keys.P))
                {
                    _gameState = GameState.Playing;
                    _status = "";
                }
                break;
            case GameState.Over:
                break;
        }
        _kbPreviousState = kbState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);

        switch(_gameState)
        {
            case GameState.Playing:
                _cannon.Draw(_spriteBatch);
                
                foreach(Mosquito mosquito in _mosquitoes)
                {
                    mosquito.Draw(_spriteBatch);
                }
                break;
            case GameState.Paused:
                _spriteBatch.DrawString(_arial, _status, new Vector2(20, 50), Color.White);
                break;
            case GameState.Over:
                break;
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
