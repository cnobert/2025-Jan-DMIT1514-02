using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace lesson21_MosquitoAttack_Final;

public class HUD
{
#region draw logic
    private const int _Height = 40;
    private SpriteFont _textFont;
    private Texture2D _background;
    private Vector2 _position; //top left corner of the HUD
    
    //the positions below are in local space, it is offset relative to _position
    private Vector2 _livesPosition, _oScorePosition, _messagePosition;
#endregion
#region game data
    private string _message;
    private int _lives, _oTurnCount;

    internal int Lives { get => _lives; set => _lives = value;}
    internal int OTurnCount { get => _oTurnCount; set => _oTurnCount = value; }
    internal string Message { get => _message; set => _message = value; }

    internal static int Height => _Height;

    #endregion
    internal void Initialize(Vector2 position)
    {
        _position = position;
        _livesPosition = new Vector2(5, 5) + position;
        _oScorePosition = new Vector2(5, 18) + position;
        _messagePosition = new Vector2(52, 10) + position;

        _lives = 0;
        OTurnCount = 0;
        Message = "";
    }
    internal void LoadContent(ContentManager content)
    {
        _background = content.Load<Texture2D>("HUDBackground");
        _textFont = content.Load<SpriteFont>("SystemArialFont");
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_background, _position, Color.White);
        spriteBatch.DrawString(_textFont, "X = " + _lives, _livesPosition, Color.Blue, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
        spriteBatch.DrawString(_textFont, "O = " + _oTurnCount, _oScorePosition, Color.Blue, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
        spriteBatch.DrawString(_textFont, _message, _messagePosition, Color.Blue, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
    }
}