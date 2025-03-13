using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace lesson18_MosquitoAttack_CannonBall;

public class Mosquito
    {
        private CelAnimationSequence _animationSequence;
        private CelAnimationPlayer _animationPlayer;

        private Vector2 _position, _direction;
        private float _speed;

        private Rectangle _gameBoundingBox;
        internal Rectangle BoundingBox
        {   
            get{    return new Rectangle(_position.ToPoint(), new Point(_animationSequence.CelWidth, _animationSequence.CelHeight));}
        }
        internal void Initialize(Vector2 position, Rectangle gameBoundingBox, float speed, Vector2 direction)
        {
            _direction = direction;
            _position = position;
            _animationPlayer = new CelAnimationPlayer();
            _animationPlayer.Play(_animationSequence);
            _speed = speed;
            _gameBoundingBox = gameBoundingBox;
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