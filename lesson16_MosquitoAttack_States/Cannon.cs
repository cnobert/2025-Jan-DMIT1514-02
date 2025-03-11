using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace lesson16_MosquitoAttack_States;

public class Cannon 
{
    private const float _Speed = 250;
    private CelAnimationSequence _animationSequence;
    private CelAnimationPlayer _animationPlayer;
    private Vector2 _position, _direction;
    private float _speed;

    public Vector2 Direction { set => _direction = value; }

    internal void Initialize(Vector2 initialPosition)
    {
        _position = initialPosition;
        _animationPlayer = new CelAnimationPlayer();
        _animationPlayer.Play(_animationSequence);
        _speed = _Speed; //we have a _speed data member in case we want to add _scale later
    }
    internal void LoadContent(ContentManager content)
    {
        _animationSequence = 
            new CelAnimationSequence(content.Load<Texture2D>("Cannon"), 40, 1 / 8.0f);
    }

    internal void Update(GameTime gameTime)
    {
        _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

        _animationPlayer.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
    }
}