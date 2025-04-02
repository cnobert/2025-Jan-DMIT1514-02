using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace lesson24_MosquitoAttack_More_Inheritance;

public class Cannon 
{
    private const float _Speed = 250;
    private const int _NumProjectiles = 5;
    private CelAnimationSequence _animationSequence;
    private CelAnimationPlayer _animationPlayer;
    private Vector2 _position, _direction;
    private float _speed;
    private Rectangle _gameBoundingBox;
    public Vector2 Direction { set => _direction = value; }
    public Rectangle BoundingBox 
        {get => new Rectangle((int) _position.X, (int) _position.Y, _animationSequence.CelWidth, _animationSequence.CelHeight);}
    private Projectile[] _projectiles;
    
    public Cannon()
    {
        _projectiles = new Projectile[_NumProjectiles];
        _projectiles[0] = new CannonBall();
        _projectiles[1] = new FireBall();
        _projectiles[2] = new FireBall();
        _projectiles[3] = new CannonBall();
        _projectiles[4] = new FireBall();
    }
    
    internal void Initialize(Vector2 initialPosition, Rectangle gameBoundingBox)
    {
        _position = initialPosition;
        _animationPlayer = new CelAnimationPlayer();
        _animationPlayer.Play(_animationSequence);
        _speed = _Speed; //we have a _speed data member in case we want to add _scale later
        _gameBoundingBox = gameBoundingBox;

        foreach(Projectile p in _projectiles)
        {
            p.Initialize(gameBoundingBox);
        }
    }
    internal void LoadContent(ContentManager content)
    {
        _animationSequence = 
            new CelAnimationSequence(content.Load<Texture2D>("Cannon"), 40, 1 / 8.0f);
        foreach(Projectile p in _projectiles)
        {
            p.LoadContent(content);
        }
    }
    internal void Update(GameTime gameTime)
    {
        _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
        if(BoundingBox.Left < _gameBoundingBox.Left)
        {
            _position.X = _gameBoundingBox.Left;
        }
        else if(BoundingBox.Right > _gameBoundingBox.Right)
        {
            _position.X = _gameBoundingBox.Right - BoundingBox.Width;
        }
        else//if(!_direction.Equals(Vector2.Zero))
        {
            //another way: if(_direction.X != 0)
            //if we're in this "else", the cannon is not on the sides
            if(!_direction.Equals(Vector2.Zero))
            {
                //if we're in this "if", the cannon is moving
                _animationPlayer.Update(gameTime);
            }
        }
        foreach(Projectile p in _projectiles)
        {
            p.Update(gameTime);
        }
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
        foreach(Projectile p in _projectiles)
        {
            p.Draw(spriteBatch);
        }
    }

    internal void Shoot()
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