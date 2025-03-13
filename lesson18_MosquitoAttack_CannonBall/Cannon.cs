using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace lesson18_MosquitoAttack_CannonBall;

public class Cannon 
{
    private const float _Speed = 250;
    private CelAnimationSequence _animationSequence;
    private CelAnimationPlayer _animationPlayer;
    private Vector2 _position, _direction;
    private float _speed;
    private Rectangle _gameBoundingBox;
    public Vector2 Direction { set => _direction = value; }
    public Rectangle BoundingBox 
        {get => new Rectangle((int) _position.X, (int) _position.Y, _animationSequence.CelWidth, _animationSequence.CelHeight);}
    private CannonBall _cannonBall;
    
    public Cannon()
    {
        _cannonBall = new CannonBall();
    }
    
    internal void Initialize(Vector2 initialPosition, Rectangle gameBoundingBox)
    {
        _position = initialPosition;
        _animationPlayer = new CelAnimationPlayer();
        _animationPlayer.Play(_animationSequence);
        _speed = _Speed; //we have a _speed data member in case we want to add _scale later
        _gameBoundingBox = gameBoundingBox;
        _cannonBall.Initialize(new Vector2(25, 300), gameBoundingBox, 50, new Vector2(0, -1));
    }
    internal void LoadContent(ContentManager content)
    {
        _animationSequence = 
            new CelAnimationSequence(content.Load<Texture2D>("Cannon"), 40, 1 / 8.0f);
        _cannonBall.LoadContent(content);
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
        _cannonBall.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
        _cannonBall.Draw(spriteBatch);
    }

    internal void Shoot()
    {
        //we want to shoot the cannonBall upwards, at a speed of 50
        _cannonBall.Shoot(new Vector2(BoundingBox.Center.X, BoundingBox.Top), new Vector2(0, -1), 50);

        //Exercise:
        /*
            1. write the cannonBall's shoot method
            2. have MosquitoAttack call Cannon's shoot method when the space bar is pressed
        */
    }
}