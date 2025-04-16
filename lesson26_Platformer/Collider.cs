using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson26_Platformer;

public class Collider
{
    public enum ColliderType { Left, Right, Top, Bottom }
    private ColliderType _type;
    private Vector2 _position, _dimensions;
    private Texture2D _texture;
    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)_position.X, (int)_position.Y, (int)_dimensions.X, (int)_dimensions.Y);
        }
    }
    public Collider(Vector2 position, Vector2 dimensions, ColliderType colliderType)
    {
        _position = position;
        _dimensions = dimensions;
        _type = colliderType;
    }
    internal void LoadContent(ContentManager contentManager) 
    {
        string textureName = "";
        switch(_type)
        {
            case ColliderType.Left:
                textureName = "ColliderLeft";
                break;
            case ColliderType.Right:
                textureName = "ColliderRight";
                break;
            case ColliderType.Top:
                textureName = "ColliderTop";
                break;
            case ColliderType.Bottom:
                textureName = "ColliderBottom";
                break;
        }
        _texture = contentManager.Load<Texture2D>(textureName);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, BoundingBox, new Rectangle(0, 0, 1, 1), Color.White);
    }
    internal bool ProcessCollision(Player player, GameTime gameTime)
    {
        bool didCollide = false;
        if(BoundingBox.Intersects(player.BoundingBox))
        {
            didCollide = true;
            switch(_type)
            {
                case ColliderType.Left:
                    break;
                case ColliderType.Right:
                    break;
                case ColliderType.Top:
                    player.Land(BoundingBox);
                    player.StandOn(gameTime);
                    break;
                case ColliderType.Bottom:
                    break;
            }
        }
        return didCollide;
    }
}