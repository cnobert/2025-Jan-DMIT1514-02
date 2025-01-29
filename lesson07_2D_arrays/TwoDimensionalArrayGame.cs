using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson07_2D_arrays;

public class TwoDimensionalArrayGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _arialFont;
    private string _message = "the message";

    public enum GameSpaceState
    {
        X, O, Empty
    }
    //using your coding toolkit from CPSC1012 combined with enums, the code below
    //is the most scalable and efficient code data structure that you can create
    //however, the end goal is a 2D array (an array of arrays)
    //Next class, we will set this up.
    private GameSpaceState[] _rowTop;
    private GameSpaceState[] _rowMiddle;
    private GameSpaceState[] _rowBottom;
    

    public TwoDimensionalArrayGame()
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

        //playing around with arrays

        int[] myNumbers = new int[10];
        for(int c = 0; c < 10; c++)
        {
            myNumbers[c] = c + 3;
        }

        // for(int c = 0; c < 10; c++)
        // {
        //     _spriteBatch.DrawString(_arialFont, myNumbers[c] + "", new Vector2(c * 30, 0), Color.White);
        // }
        // int xLocation = 0;
        // for(int c = myNumbers.Length - 1; c >= 0; c--)
        // {
        //     _spriteBatch.DrawString(_arialFont, myNumbers[c] + "", new Vector2(xLocation++ * 30, 0), Color.White);
        // }

        int[,] numArray = 
        {
            {1, 2, 3, 4 },
            {5, 6, 7, 8 },
            {9, 10, 11, 12 }
        };
        //in a 2D array, the first index is the rows, the second is the columns
        //so, to output the number "6", we use row index 1 and column index 1
        _spriteBatch.DrawString(_arialFont, numArray[1, 1] + "", Vector2.Zero, Color.White);

        //Exercise: with different DrawString calls, output the number 12, then 8, then 1

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
