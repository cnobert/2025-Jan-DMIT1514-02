using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace lesson22_MosquitoAttack_Inheritance;

public class Cannon 
{
    private const float _Speed = 250;
    private const int _NumCannonBalls = 10;
    private CelAnimationSequence _animationSequence;
    private CelAnimationPlayer _animationPlayer;
    private Vector2 _position, _direction;
    private float _speed;
    private Rectangle _gameBoundingBox;
    public Vector2 Direction { set => _direction = value; }
    public Rectangle BoundingBox 
        {get => new Rectangle((int) _position.X, (int) _position.Y, _animationSequence.CelWidth, _animationSequence.CelHeight);}
    private CannonBall[] _cannonBalls;
    
    public Cannon()
    {
        _cannonBalls = new CannonBall[_NumCannonBalls];
        for(int c = 0; c < _NumCannonBalls; c++)
        {
            _cannonBalls[c] = new CannonBall();
        }
    }
    
    internal void Initialize(Vector2 initialPosition, Rectangle gameBoundingBox)
    {
        _position = initialPosition;
        _animationPlayer = new CelAnimationPlayer();
        _animationPlayer.Play(_animationSequence);
        _speed = _Speed; //we have a _speed data member in case we want to add _scale later
        _gameBoundingBox = gameBoundingBox;

        foreach(CannonBall cBall in _cannonBalls)
        {
            cBall.Initialize(gameBoundingBox);
        }
    }
    internal void LoadContent(ContentManager content)
    {
        _animationSequence = 
            new CelAnimationSequence(content.Load<Texture2D>("Cannon"), 40, 1 / 8.0f);
        foreach(CannonBall cBall in _cannonBalls)
        {
            cBall.LoadContent(content);
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
        foreach(CannonBall cBall in _cannonBalls)
        {
            cBall.Update(gameTime);
        }
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
        foreach(CannonBall cBall in _cannonBalls)
        {
            cBall.Draw(spriteBatch);
        }
    }

    internal void Shoot()
    {
        int cannonBallIndex = 0;
        bool shot = false;
        while(cannonBallIndex < _NumCannonBalls && !shot)
        {
            shot = _cannonBalls[cannonBallIndex].Shoot(new Vector2(BoundingBox.Center.X, BoundingBox.Top), new Vector2(0, -1), 50);
            cannonBallIndex++;
        }
    }
    internal bool ProcessCollision(Rectangle boundingBox)
    {
        bool hit = false;
        int c = 0;
        while(!hit && c < _cannonBalls.Length)
        {
            hit = _cannonBalls[c].ProcessCollision(boundingBox);
            c++;
        }
        return hit;
    }
}