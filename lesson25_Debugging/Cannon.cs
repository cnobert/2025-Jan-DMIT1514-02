using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson25_Debugging;

public class Cannon : GameBot
{
    private const int _NumProjectiles = 5;
    internal Vector2 Direction { set => _direction = value; }
    private Projectile[] _projectiles;
    internal Cannon()
    {
        _projectiles = new Projectile[_NumProjectiles];
        _projectiles[0] = new CannonBall();
        _projectiles[1] = new FireBall();
        _projectiles[2] = new FireBall();
        _projectiles[3] = new CannonBall();
        _projectiles[4] = new FireBall();
    }
    
    internal override void Initialize(Vector2 initialPosition, Rectangle gameBoundingBox, float speed)
    {
        base.Initialize(initialPosition, gameBoundingBox, speed);
        foreach(Projectile p in _projectiles)
        {
            p.Initialize(gameBoundingBox);
        }
    }
    internal override void LoadContent(ContentManager content)
    {
        _animationSequenceAlive = 
            new CelAnimationSequence(content.Load<Texture2D>("Cannon"), 40, 1 / 8.0f);
        foreach(Projectile p in _projectiles)
        {
            p.LoadContent(content);
        }
    }
    internal override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        switch(_state)
        {
            case State.Alive:
                if(BoundingBox.Left < _gameBoundingBox.Left)
                {
                    _position.X = _gameBoundingBox.Left;
                }
                else if(BoundingBox.Right > _gameBoundingBox.Right)
                {
                    _position.X = _gameBoundingBox.Right - BoundingBox.Width;
                }
                else
                {
                    //if we're in this "else", the cannon is not on the sides
                    if(!_direction.Equals(Vector2.Zero))
                    {
                        //if we're in this "if", the cannon is moving
                        _animationPlayer.Update(gameTime);
                    }
                }
                break;
            case State.Dying:
                break;
            case State.Dead:
                break;
        }
        foreach(Projectile p in _projectiles)
        {
            p.Update(gameTime);
        }
    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        foreach(Projectile p in _projectiles)
        {
            p.Draw(spriteBatch);
        }
    }

    internal override void Shoot()
    {
        int cannonBallIndex = 0;
        bool shot = false;
        while(cannonBallIndex < _NumProjectiles && !shot)
        {
            Vector2 position = new Vector2(BoundingBox.Center.X - _projectiles[cannonBallIndex].BoundingBox.Width / 2, BoundingBox.Top);
            shot = _projectiles[cannonBallIndex].Shoot(position, new Vector2(0, -1), 50);
            cannonBallIndex++;
        }
    }
    internal bool ProcessCollision(Rectangle boundingBox)
    {
        bool hit = false;
        int c = 0;
        while(!hit && c < _projectiles.Length)
        {
            hit = _projectiles[c].ProcessCollision(boundingBox);
            c++;
        }
        return hit;
    }
}