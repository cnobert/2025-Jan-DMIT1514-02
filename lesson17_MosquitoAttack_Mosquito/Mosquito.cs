using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace lesson17_MosquitoAttack_Mosquito;

public class Mosquito
    {
        private CelAnimationSequence _animationSequence;
        private CelAnimationPlayer _animationPlayer;

        private Vector2 _position;
        private float _speed;
        private Vector2 _direction;

        private Rectangle _gameBoundingBox;
        internal Rectangle BoundingBox
        {
            get
            {
                //return new Rectangle(_position.ToPoint(), new Point(_animationSequence.CelWidth, _animationSequence.CelHeight));
                return new Rectangle((int)_position.X, (int)_position.Y, _animationSequence.CelWidth, _animationSequence.CelHeight);
            }
        }
        internal void Initialize(Vector2 position, float speed, Vector2 direction, Rectangle gameBoundingBox)
        {
            _speed = speed;
            _position = position;
            _direction = direction;

            _gameBoundingBox = gameBoundingBox;

            _animationPlayer = new CelAnimationPlayer();

            //we need to make that the _animationSequence is not null before this code runs
            _animationPlayer.Play(_animationSequence);
        }

        internal void LoadContent(ContentManager content)
        {
            _animationSequence = new CelAnimationSequence(content.Load<Texture2D>("Mosquito"), 46, 1 / 8.0f);
        }
        internal void Update(GameTime gameTime)
        {
            _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if(BoundingBox.Left < _gameBoundingBox.Left || BoundingBox.Right > _gameBoundingBox.Right)
            {
                _direction.X *= -1;
            }

            _animationPlayer.Update(gameTime);
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
        }
    }