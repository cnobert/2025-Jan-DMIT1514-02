using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace lesson20_MosquitoAttack_FireBalls;

public class Mosquito
    {
        private CelAnimationSequence _animationSequence;
        private CelAnimationPlayer _animationPlayer;

        private Vector2 _position, _direction;
        private float _speed;

        private enum State
        {
            Alive, Dying, Dead
        }
        private State _state;

        private Rectangle _gameBoundingBox;
        internal Rectangle BoundingBox
        {   
            get{    return new Rectangle(_position.ToPoint(), new Point(_animationSequence.CelWidth, _animationSequence.CelHeight));}
        }
        // internal bool Alive 
        // {
        //     get 
        //     {
        //         return _state == State.Alive;
        //     }
        // }
        internal bool Alive => _state == State.Alive;
        
        internal void Initialize(Vector2 position, Rectangle gameBoundingBox, float speed, Vector2 direction)
        {
            _direction = direction;
            _position = position;
            _animationPlayer = new CelAnimationPlayer();
            _animationPlayer.Play(_animationSequence);
            _speed = speed;
            _gameBoundingBox = gameBoundingBox;

            _state = State.Alive;
        }

        internal void LoadContent(ContentManager content)
        {
            _animationSequence = new CelAnimationSequence(content.Load<Texture2D>("Mosquito"), 46, 1 / 8.0f);
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
                    break;
                case State.Dying:
                    _state = State.Dead;
                    break;
                case State.Dead:
                    break;
            }
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            switch(_state)
            {
                case State.Alive:
                    _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
                    break;
                case State.Dying:
                    break;
                case State.Dead:
                    break;
            }
        }
        internal void Die()
        {
            //the mosquito has been hit
            _state = State.Dying;
        }
    }