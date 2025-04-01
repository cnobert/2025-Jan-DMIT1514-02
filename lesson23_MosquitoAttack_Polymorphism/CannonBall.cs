using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson23_MosquitoAttack_Polymorphism;

public class CannonBall : Projectile
{
    private Texture2D _texture;
    
    internal override void Initialize(Rectangle gameBoundingBox)
    {
        base.Initialize(gameBoundingBox);
        _dimensions = new Vector2(_texture.Width, _texture.Height);
    }
    internal override void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("CannonBall");
    }
    
    internal override void Draw(SpriteBatch spriteBatch)
    {
        switch(_state)
        {
            case State.Flying:
                spriteBatch.Draw(_texture, _position, Color.White);
                break;
            case State.NotFlying:
                break;
        }
    }
    
}