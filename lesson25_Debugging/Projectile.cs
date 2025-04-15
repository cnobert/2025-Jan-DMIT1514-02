using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson25_Debugging;

public abstract class Projectile
{
    protected Vector2 _position, _direction, _dimensions;
    protected float _speed;
    protected Rectangle _gameBoundingBox;
    protected enum State 
    {
        Flying,
        NotFlying
    }
    protected State _state;

    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(_position.ToPoint(), _dimensions.ToPoint());
        }
    }
    //"virtual" means "my children may override this method, but it's not required
    internal virtual void Initialize(Rectangle gameBoundingBox)
    {
        _gameBoundingBox = gameBoundingBox;
        _state = State.NotFlying;
    }

    internal abstract void LoadContent(ContentManager content);

    internal virtual void Update(GameTime gameTime)
    {
        switch(_state)
        {
            case State.Flying:
                _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
                if(!BoundingBox.Intersects(_gameBoundingBox))
                {
                    _state = State.NotFlying;
                }
                break;
            case State.NotFlying:
                break;
        }
    }
    /*
        "abstract foces the child class to define a method with this signature
        allows this:
            Projectile projectile01 = new FireBall();
            Projectile projectile02 = new CannonBall();
        but does not allow:
            Projectile projectile = new Projectile();
    */
    internal abstract void Draw(SpriteBatch spriteBatch);

    internal bool Shoot(Vector2 position, Vector2 direction, float speed)
    {
        bool shot = false;
        if(_state == State.NotFlying)
        {
            _position = position;
            _direction = direction;
            _speed = speed;
            _state = State.Flying;
            shot = true;
        }
        return shot;
    }
    internal bool ProcessCollision(Rectangle boundingBox)
    {
        bool didHit = false;
        if(_state == State.Flying && BoundingBox.Intersects(boundingBox))
        {
            didHit = true;
            //take myself out of play
            _state = State.NotFlying;
        }
        return didHit;
    }
}