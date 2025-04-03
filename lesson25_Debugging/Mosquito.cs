using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson25_Debugging;

public class Mosquito
    {
        private const int _UpperRandomFiringRange = 100;
        private System.Random _randomNumberGenerator = new System.Random();
        private CelAnimationSequence _animationSequenceAlive;
        private CelAnimationSequence _animationSequenceDying;
        private CelAnimationPlayer _animationPlayer;
        private Vector2 _position, _direction;
        private float _speed;

        private enum State
        {
            Alive, Dying, Dead
        }
        private State _state;

        private Rectangle _gameBoundingBox;
        internal Rectangle BoundingBox =>
            new Rectangle(_position.ToPoint(), new Point(_animationSequenceAlive.CelWidth, _animationSequenceAlive.CelHeight));
        
        internal bool Alive => _state == State.Alive;

        private FireBall _fireBall;
        public Mosquito()
        {
            _fireBall = new FireBall();
        }
        internal void Initialize(Vector2 position, Rectangle gameBoundingBox, float speed, Vector2 direction)
        {
            _direction = direction;
            _position = position;
            _animationPlayer = new CelAnimationPlayer();
            _animationPlayer.Play(_animationSequenceAlive);
            _speed = speed;
            _gameBoundingBox = gameBoundingBox;

            _fireBall.Initialize(gameBoundingBox);

            _state = State.Alive;
        }

        internal void LoadContent(ContentManager content)
        {
            _animationSequenceAlive = new CelAnimationSequence(content.Load<Texture2D>("Mosquito"), 46, 1 / 8.0f);
            _animationSequenceDying = new CelAnimationSequence(content.Load<Texture2D>("Poof"), 16, 1 / 8.0f);
            _fireBall.LoadContent(content);
        }
        internal void Update(GameTime gameTime)
        {
            switch(_state)
            {
                case State.Alive:
                    _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
                    if(BoundingBox.Left < _gameBoundingBox.Left || BoundingBox.Right > _gameBoundingBox.Right)
                    {
                        _direction.X *= -1;
                    }
                    _animationPlayer.Update(gameTime);
                    //"deciding" if we should Shoot() or not
                    if(_randomNumberGenerator.Next(1, _UpperRandomFiringRange) == 1)
                    {
                        this.Shoot();
                    }
                    break;
                case State.Dying:
                    _animationPlayer.Update(gameTime);
                    if(_animationPlayer.PlayedOnce)
                    {
                        _state = State.Dead;
                    }
                    break;
                case State.Dead:
                    break;
            }
            _fireBall.Update(gameTime);
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            switch(_state)
            {
                case State.Alive:
                case State.Dying:
                    _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
                    break;
                case State.Dead:
                    break;
            }
            _fireBall.Draw(spriteBatch);
        }
        internal void Die()
        {
            //the mosquito has been hit
            _animationPlayer.PlayOnce(_animationSequenceDying);
            _state = State.Dying;
        }
        internal void Shoot()
        {
            Vector2 shootingPosition = new Vector2(BoundingBox.Center.X, BoundingBox.Bottom);
            _fireBall.Shoot(shootingPosition, new Vector2(0, 1), 150);
        }
    }