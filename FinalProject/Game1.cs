﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace MonogameProject3_Spaceship
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D shipSprite;
        Texture2D asteroidSprite1;
        Texture2D asteroidSprite2;
        Texture2D asteroidSprite3;
        Texture2D spaceSprite;
        SpriteFont gameFont;
        SpriteFont timerFont;
        SpriteFont scoreFont;
        Song song;
        
        Ship player = new Ship();
        Asteroid ast1 = new Asteroid(4);
        Asteroid ast2 = new Asteroid(6);
        Asteroid ast3 = new Asteroid(8);


        bool setBack = true;


        // Timer variables
        private TimeSpan elapsedTime;
        private int secondsElapsed;

        //Controller object
        Controller controller = new Controller();

        //Game State
        public bool inGame = true;
          
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            //timer related
            elapsedTime = TimeSpan.Zero;
            secondsElapsed = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            shipSprite = Content.Load<Texture2D>("ship");
            asteroidSprite1 = Content.Load<Texture2D>("asteroid");
            asteroidSprite2 = Content.Load<Texture2D>("asteroid");
            asteroidSprite3 = Content.Load<Texture2D>("asteroid");
            spaceSprite = Content.Load<Texture2D>("space");
            gameFont = Content.Load<SpriteFont>("spaceFont");
            timerFont = Content.Load<SpriteFont>("timerFont");
            scoreFont = Content.Load<SpriteFont>("timerFont");
            song = Content.Load<Song>("amalgam-2170071");

            MediaPlayer.Play(song);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //1// Auto Move the player along X-axis
            //player.position.X++;

            //2// Auto Move the player along Y-axis
            //player.position.Y++;

            //3// Move the player along X-axis and Y-axis using Keyboard   
            player.setRadius(shipSprite.Width);
            player.updateShip();
            ast1.updateAsteroid();
            ast2.updateAsteroid();
            ast3.updateAsteroid();

            // Get updated seconds count from Controller
            secondsElapsed = controller.updateTime(gameTime);

            // Check for collision
            if ((controller.didCollisionHappen(player, ast1) || controller.didCollisionHappen(player, ast2) || controller.didCollisionHappen(player, ast3)) && inGame)
            {
                controller.playerScore -= 3;
            }
            if (secondsElapsed >= 15 || controller.playerScore < 0)
            {
                inGame = false;
            }

            if (controller.didShipPast(player, ast1) && ast1.currentOne)
            {
                controller.playerScore++;
                ast1.currentOne = false;
            }
            if (controller.didShipPast(player, ast2) && ast2.currentOne)
            {
                controller.playerScore++;
                ast2.currentOne = false;
            }
            if  (controller.didShipPast(player, ast3) && ast3.currentOne)
            {
                controller.playerScore++;
                ast3.currentOne = false;
            }


            base.Update(gameTime);

           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
            //_spriteBatch.Draw(shipSprite, player.position, Color.White);//without centering the sprite
            _spriteBatch.Draw(shipSprite, new Vector2(player.position.X-shipSprite.Width/2, player.position.Y-shipSprite.Height/2), Color.White);//Using Offset and With Centering the sprite
            _spriteBatch.Draw(asteroidSprite1, new Vector2(ast1.position.X-Asteroid.radius , ast1.position.Y-Asteroid.radius), Color.White);
            _spriteBatch.Draw(asteroidSprite2, new Vector2(ast2.position.X - Asteroid.radius, ast2.position.Y - Asteroid.radius), Color.White);
            _spriteBatch.Draw(asteroidSprite3, new Vector2(ast3.position.X - Asteroid.radius, ast3.position.Y - Asteroid.radius), Color.White);
            // Displaying Timer
            _spriteBatch.DrawString(timerFont, "Time: " + secondsElapsed, new Vector2(_graphics.PreferredBackBufferWidth / 2, 30), Color.White);
            _spriteBatch.DrawString(scoreFont, "Score: " + controller.playerScore, new Vector2(_graphics.PreferredBackBufferWidth / 4, 30), Color.White);

            // Displaying Game Over Message

            
            if (!inGame)
            {
                _spriteBatch.DrawString(gameFont, controller.gameEndScript(), new Vector2(_graphics.PreferredBackBufferWidth / 4 - 100, _graphics.PreferredBackBufferHeight / 2), Color.White);
                ast1.speed = 0;
                ast2.speed = 0;
                ast3.speed = 0;
                if (setBack)
                {
                    ast1.position.X += 5;
                    ast2.position.X += 5;
                    ast3.position.X += 5;
                    setBack = false;
                }
                player.endSpeed();

            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
