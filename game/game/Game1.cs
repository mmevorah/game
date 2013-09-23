using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Ball
        Texture2D ballSprite;
        Vector2 ballPosition = Vector2.Zero;
        Vector2 ballSpeed = new Vector2(150, 150);

        //Player 1
        Texture2D paddle1Sprite;
        Vector2 paddle1Position;

        //Player 2
        Texture2D paddle2Sprite;
        Vector2 paddle2Position;




        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            paddle1Position = new Vector2(
                graphics.GraphicsDevice.Viewport.Width - paddle1Sprite.Width,
                graphics.GraphicsDevice.Viewport.Height / 2 - paddle1Sprite.Height / 2);

            paddle2Position = new Vector2(
                0,
                graphics.GraphicsDevice.Viewport.Height / 2 - paddle2Sprite.Height / 2);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ballSprite = Content.Load<Texture2D>("ball");

            paddle1Sprite = Content.Load<Texture2D>("paddle");
            paddle2Sprite = Content.Load<Texture2D>("paddle");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Controls
            KeyboardState keyState = Keyboard.GetState();

            //Player 1
            if (keyState.IsKeyDown(Keys.Up))
                paddle1Position.Y -= 5;
            else if (keyState.IsKeyDown(Keys.Down))
                paddle1Position.Y += 5;

            //Player 2
            if (keyState.IsKeyDown(Keys.W))
                paddle2Position.Y -= 5;
            else if (keyState.IsKeyDown(Keys.S))
                paddle2Position.Y += 5;

            //Move ball
            ballPosition += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            int maxX = GraphicsDevice.Viewport.Width;
            int maxY = GraphicsDevice.Viewport.Height - ballSprite.Height;

            //Check collision
            if (ballPosition.Y > maxY || ballPosition.Y < 0)
                ballSpeed.Y *= -1;

            if (ballPosition.X < 0 && ballPosition.X < paddle2Position.X)
            {
                //Add Scoring
                ballPosition.Y = 0;
                ballPosition.X = maxX / 2;
                ballSpeed.X = 150;
                ballSpeed.Y = 150;
            }
            else if (ballPosition.X > maxX && ballPosition.X > paddle1Position.X)
            {
                //Add Scoring
                ballPosition.Y = 0;
                ballPosition.X = maxX / 2;
                ballSpeed.X = 150;
                ballSpeed.Y = 150;
            }


            //Check Ball Paddle Collision
            Rectangle ballRect =
                new Rectangle((int)ballPosition.X, (int)ballPosition.Y,
                    ballSprite.Width, ballSprite.Height);
           
            Rectangle paddle1Rect =
                new Rectangle((int)paddle1Position.X, (int)paddle1Position.Y,
                    paddle1Sprite.Width, paddle1Sprite.Height);
            Rectangle paddle2Rect =
                new Rectangle((int)paddle2Position.X, (int)paddle2Position.Y,
                    paddle2Sprite.Width, paddle2Sprite.Height);



            if (ballRect.Intersects(paddle1Rect) || ballRect.Intersects(paddle2Rect))
            {
                ballSpeed.X *= -1;
            }

           

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(ballSprite, ballPosition, Color.White);
            spriteBatch.Draw(paddle1Sprite, paddle1Position, Color.White);
            spriteBatch.Draw(paddle2Sprite, paddle2Position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
