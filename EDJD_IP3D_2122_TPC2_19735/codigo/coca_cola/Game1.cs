using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace coca_cola
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        ClsCoca coca;
        Vector3 position;
        float yaw = 45.0f;
        int H, W;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            position = new Vector3(0f, 0f, 0f);
            

        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            coca = new ClsCoca(_graphics.GraphicsDevice, Content.Load<Texture2D>("coca"),Content.Load<Texture2D>("tampa"), Content.Load<Texture2D>("xadrez"), 20, 1f, 2.3f);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState kb = Keyboard.GetState();


            Matrix rotaçao;
            rotaçao = Matrix.CreateRotationY(MathHelper.ToRadians(yaw));
            Vector3 direction;
            direction = Vector3.Transform(Vector3.UnitZ, rotaçao);
            Matrix translaçao;
            translaçao = Matrix.CreateTranslation(position);
            float scale = 0.75f;
            Matrix scala = Matrix.CreateScale(scale);
            



            coca.worldMatrix = scala * rotaçao * translaçao;

            if (kb.IsKeyDown(Keys.Left))
            {
                yaw = yaw + 1.5f;
            }
            if (kb.IsKeyDown(Keys.Right))
            {
                yaw = yaw - 1.5f;
            }
            if (kb.IsKeyDown(Keys.Up))
            {
                position = position + direction * 0.07f;
            }
            if (kb.IsKeyDown(Keys.Down))
            {
                position = position - direction * 0.07f;
            }

            float scale_plano = 8f;
            Matrix scala_p = Matrix.CreateScale(scale_plano);
            coca.worldMatrix_plano = scala_p;

            if (position.X >= scale_plano - 1)
               {
                    position.X = scale_plano -1;
               } 
            if (position.X <= -scale_plano+1)
            {
                position.X = -scale_plano + 1;
            }
            if (position.Z >= scale_plano-1)
            {
                position.Z = scale_plano - 1;
            }
            if (position.Z <= -scale_plano+1)
            {
                position.Z = -scale_plano + 1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            coca.Draw(_graphics.GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}
