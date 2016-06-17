using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PRPG.Voxel;

namespace PRPG
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        VoxelObject v;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            v = new VoxelObject(this, graphics, new Vector3(1, 0, 0));
            
        }
        
        protected override void Initialize()
        {
            IsMouseVisible = true;
            v.Size = new Vector3(5, 5, 5);
            v.CamSpeed = 0.3f;
            v.Initialize();
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font1");
        }
        
        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                v.MoveForward();
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                v.MoveBack();
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                v.MoveLeft();
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                v.MoveRight();
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                v.MoveUp();
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                v.MoveDown();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            v.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, v.Position.ToString(), new Vector2(), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
