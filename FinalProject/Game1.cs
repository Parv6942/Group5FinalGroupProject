using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace MonogameProject3_Spaceship
{
    public class Game1 : Game
    {
        // Add difficulty levels
        public enum Difficulty { Easy, Medium, Hard }
        private Difficulty selectedDifficulty = Difficulty.Easy; // Default difficulty
        private int playerLives; // Store player lives
        public int menuIndex = 0; // Index for difficulty selection


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Texture asserts for the game
        Texture2D shipSprite;
        Texture2D asteroidSprite1;
        Texture2D asteroidSprite2;
        Texture2D asteroidSprite3;
        Texture2D spaceSprite;

        // Font assets for displaying text
        SpriteFont gameFont;
        SpriteFont timerFont;
        SpriteFont scoreFont;
        
        // Background music for the game
        Song song;

        // variables for the menu
        // use a enum to represent the different states of the game
        public enum GameState { MainMenu, InGame, GameOver, Exit}
        // Initializes the current game state to the main menu at the start of the game
        private GameState currentGameState = GameState.MainMenu;

        // instance for handling Main Menu and Game Over screens
        private Menu _menu; 

        //  Player-controlled spaceship
        Ship player = new Ship();

        // Asteroids with different speeds
        Asteroid ast1 = new Asteroid(4);
        Asteroid ast2 = new Asteroid(6);
        Asteroid ast3 = new Asteroid(8);

        // Flag for resetting asteroid positions when the game ends
        bool setBack = true;


        // Timer variables
        private TimeSpan elapsedTime;
        private int secondsElapsed;

        //Controller object
        Controller controller = new Controller();

        //Game State 
        public bool inGame = true;

        private KeyboardState previousKeyboardState;

        private TimeSpan collisionCooldown; // Tracks cooldown period
        private TimeSpan lastCollisionTime; // Tracks the last collision time




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // handles initialize  logic 
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            //timer related
            elapsedTime = TimeSpan.Zero;
            secondsElapsed = 0;

            collisionCooldown = TimeSpan.FromSeconds(2); // 2 seconds cooldown
            lastCollisionTime = TimeSpan.Zero; // No collision yet


            //Menu related 


            base.Initialize();
        }

        // handles loading assets 
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

            // Load thse gamefont and spaceSprite into the menu
            _menu = new Menu(gameFont, spaceSprite);

            // Play background music
            MediaPlayer.Play(song);

        }

        // handles the menu logic
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here
            // Get the current keyboard state
            KeyboardState keyboardState = Keyboard.GetState();

			//Switch case to handle the current game state
			switch (currentGameState) 
            {
                case GameState.MainMenu:
                    // Navigate menu with Up and Down keys
                    if (keyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))
                    {
                        menuIndex = (menuIndex + 1) % 3; // Cycle through 0, 1, 2
                    }
                    if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
                    {
                        menuIndex = (menuIndex - 1 + 3) % 3; // Wrap around to 2 if at 0
                    }

                    // Set difficulty on Enter key
                    if (keyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
                    {
                        selectedDifficulty = (Difficulty)menuIndex;

                        // Set lives based on difficulty
                        playerLives = selectedDifficulty switch
                        {
                            Difficulty.Easy => 3,
                            Difficulty.Medium => 2,
                            Difficulty.Hard => 1,
                            _ => 3 // Default to Easy if something goes wrong
                        };

                        currentGameState = GameState.InGame; // Start the game
                    }

                    // Exit the game if Q is pressed
                    if (keyboardState.IsKeyDown(Keys.Q))
                    {
                        Exit();
                    }
                    break;


                case GameState.InGame:
               // Update gameplay logic
              UpdateGamplay(gameTime);
              if (!inGame)
              {
                currentGameState = GameState.GameOver;
              }
              break;

              case GameState.GameOver:
              // Reset the game and return to mainmenu if R is pressed
              if (keyboardState.IsKeyDown(Keys.R))
              {
                ResetGame();
                currentGameState = GameState.MainMenu;
              }
					// Exit the game if Q is pressed
			  else if (keyboardState.IsKeyDown(Keys.Q))
              {
                Exit();
              }
              break;
            }

            previousKeyboardState = keyboardState;
            base.Update(gameTime);
        }


        //UpdateGameplay handles the ship, asteroid, points gameplay and mechaincs
        private void UpdateGamplay(GameTime gameTime)
        {
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
                // Only decrement lives if the cooldown period has elapsed
                if (gameTime.TotalGameTime - lastCollisionTime > collisionCooldown)
                {
                    playerLives--; // Go down a life
                    lastCollisionTime = gameTime.TotalGameTime; // Update the last collision time

                    if (playerLives <= 0) // End game if lives zero
                    {
                        inGame = false;
                    }
                }
            }


   //         // End the game if the timer exceeds 15 seconds or the score drops below 0
   //         if (secondsElapsed >= 15 || controller.playerScore < 0)
			//{
			//	inGame = false;
			//}

            // Increment score if the player successfully dodges an asteroid
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
			if (controller.didShipPast(player, ast3) && ast3.currentOne)
			{
				controller.playerScore++;
				ast3.currentOne = false;
			}
		}

        // Draw() hands the menu and display it
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            // Draw based on the current game state
            switch (currentGameState)
            {
                case GameState.MainMenu:
                _menu.Draw(_spriteBatch, GameState.MainMenu, menuIndex); 
                break;

                case GameState.InGame:
                DrawGameplay();
                break;

                case GameState.GameOver:
                _menu.Draw(_spriteBatch, GameState.GameOver, menuIndex);
                break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // DrawGameplay() handles the background and game objects, and displaying timer and score, etc
        private void DrawGameplay()
        {
            // Draw the background and game objects
			_spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
			//_spriteBatch.Draw(shipSprite, player.position, Color.White);//without centering the sprite
			_spriteBatch.Draw(shipSprite, new Vector2(player.position.X - shipSprite.Width / 2, player.position.Y - shipSprite.Height / 2), Color.White);//Using Offset and With Centering the sprite
			_spriteBatch.Draw(asteroidSprite1, new Vector2(ast1.position.X - Asteroid.radius, ast1.position.Y - Asteroid.radius), Color.White);
			_spriteBatch.Draw(asteroidSprite2, new Vector2(ast2.position.X - Asteroid.radius, ast2.position.Y - Asteroid.radius), Color.White);
			_spriteBatch.Draw(asteroidSprite3, new Vector2(ast3.position.X - Asteroid.radius, ast3.position.Y - Asteroid.radius), Color.White);

			// Displaying Timer and Score
			_spriteBatch.DrawString(timerFont, "Time: " + secondsElapsed, new Vector2(_graphics.PreferredBackBufferWidth / 2, 30), Color.White);
			_spriteBatch.DrawString(scoreFont, "Score: " + controller.playerScore, new Vector2(_graphics.PreferredBackBufferWidth / 4, 30), Color.White);
            // Display remaining lives
            _spriteBatch.DrawString(scoreFont, "Lives: " + playerLives, new Vector2(_graphics.PreferredBackBufferWidth / 1.5f, 30), Color.White);


            // Display game-over text if the game ends
            if (!inGame)
			{
				_spriteBatch.DrawString(gameFont, controller.gameEndScript(), new Vector2(_graphics.PreferredBackBufferWidth / 4 - 100, _graphics.PreferredBackBufferHeight / 2), Color.White);

			}


		}

        // ResetGame() resets the game
        private void ResetGame()
        {
            // Reset game variables
            inGame = true;
            controller.playerScore = 0;
            secondsElapsed = 0;

			// Reset asteroids
			ast1 = new Asteroid(4);
            ast2 = new Asteroid(6);
            ast3 = new Asteroid(8);

            // Reset lives based on difficulty
            playerLives = selectedDifficulty switch
            {
                Difficulty.Easy => 3,
                Difficulty.Medium => 2,
                Difficulty.Hard => 1
            };


            setBack = true;
        }





    }
}
