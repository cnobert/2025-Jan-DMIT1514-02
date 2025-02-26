using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//the HUD's job will be to keep track of # of turns for X and O, and draw itself and
//its current status 
public class HUD
{
#region draw logic
    private const int _Height = 40;
    private SpriteFont _textFont;
    private Texture2D _background;
    private Vector2 _position; //top left corner of the HUD
    
    //the positions below are in local space, it is offset relative to _position
    private Vector2 _xScorePosition, _oScorePosition, _messagePosition;
#endregion
#region game data
    private string _message;
    private int _xTurnCount, _oTurnCount;

    internal int XTurnCount { get => _xTurnCount; set => _xTurnCount = value;}
    internal int OTurnCount { get => _oTurnCount; set => _oTurnCount = value; }
    internal string Message { get => _message; set => _message = value; }

    internal static int Height => _Height;

    #endregion
    internal void Initialize(Vector2 position)
    {
        _position = position;
        _xScorePosition = new Vector2(_position.X + 5, _position.Y + 5);
        _oScorePosition = new Vector2(_position.X + 5, _position.Y + 18);
        _messagePosition = new Vector2(_position.X + 52, _position.Y + 10);

        _xTurnCount = 0;
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
        spriteBatch.DrawString(_textFont, "X = " + _xTurnCount, _xScorePosition, Color.Blue, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
        spriteBatch.DrawString(_textFont, "O = " + _oTurnCount, _oScorePosition, Color.Blue, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
        spriteBatch.DrawString(_textFont, _message, _messagePosition, Color.Blue, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
    }
}