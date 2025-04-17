using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson26_Platformer;

public class Platform
{
    private Texture2D _texture;
    private string _textureName;
    private Vector2 _position, _dimensions;
    private Collider _colliderTop, _colliderRight, _colliderBottom, _colliderLeft;

    public Platform(Vector2 position, Vector2 dimensions, string textureName)
    {
        _textureName = textureName;
        _colliderTop = new Collider(new Vector2(position.X + 3, position.Y), new Vector2(dimensions.X - 6, 1), Collider.ColliderType.Top);
        _colliderRight = new Collider(new Vector2(position.X + dimensions.X - 1, position.Y + 1), new Vector2(1, dimensions.Y - 2), Collider.ColliderType.Right);
        _colliderBottom = new Collider(new Vector2(position.X + 3, position.Y + dimensions.Y), new Vector2(dimensions.X - 6, 1), Collider.ColliderType.Bottom);
        _colliderLeft = new Collider(new Vector2(position.X + 1, position.Y + 1), new Vector2(1, dimensions.Y - 2), Collider.ColliderType.Left);
    }
    internal void LoadContent(ContentManager content)
    {
        _colliderTop.LoadContent(content);
        _colliderRight.LoadContent(content);
        _colliderBottom.LoadContent(content);
        _colliderLeft.LoadContent(content);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        _colliderTop.Draw(spriteBatch);
        _colliderRight.Draw(spriteBatch);
        _colliderBottom.Draw(spriteBatch);
        _colliderLeft.Draw(spriteBatch);
    }
    internal void ProcessCollisions(Player player, GameTime gameTime)
    {
        _colliderTop.ProcessCollision(player, gameTime);
        _colliderRight.ProcessCollision(player, gameTime);
        _colliderBottom.ProcessCollision(player, gameTime);
        _colliderLeft.ProcessCollision(player, gameTime);
    }
}