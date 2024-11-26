using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static MonogameProject3_Spaceship.Game1;

namespace MonogameProject3_Spaceship

{  // this a menu class that handles drawing menu screens
	public class Menu
	{
		// Fields to hold the font and background texture
		private SpriteFont font;
		private Texture2D background;

		// Constructor to initialize the font and background
		public Menu(SpriteFont font, Texture2D background) 
		{
		  this.font = font;
		  this.background = background;
		}

		// Method to draw the menu based on the current game state
		public void Draw (SpriteBatch spriteBatch, GameState gameState)
		{
			// Draw the background first 
			spriteBatch.Draw(background, new Rectangle(0, 0, 1200, 900), Color.White);

			// Draw the title
			spriteBatch.DrawString(font, "SPACE ADVENTURE", new Vector2(500, 100), Color.Gold);

			// Draw different menu options based on the game state
			switch (gameState) 
			{
				case GameState.MainMenu:
				spriteBatch.DrawString(font, "Press Enter to Start", new Vector2(500, 200), Color.White);
				spriteBatch.DrawString(font, "Press Q to Quit", new Vector2(500, 400), Color.White);
				break;

				case GameState.GameOver:
				spriteBatch.DrawString(font, "Game Over! Press Enter to Return to Menu", new Vector2(400, 250), Color.Red);
				spriteBatch.DrawString(font, "Press Q to Quit", new Vector2(500,400), Color.White);
				break;
			
			
			}



		}



	}
}
