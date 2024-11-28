using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameProject3_Spaceship
{
    internal class Controller
    {
        private TimeSpan elapsedTime; 
        private int secondsElapsed;
        public int playerScore;

        bool gameGoing = true;

        public Controller()
        {
            elapsedTime = TimeSpan.Zero;
            secondsElapsed = 0;
        }

        // Update the timer and return the seconds elapsed
        public int updateTime(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime.TotalSeconds >= 1 && gameGoing)
            {
                secondsElapsed++;
                elapsedTime = TimeSpan.Zero;
            }

            return secondsElapsed; // Return the current seconds count
        }

        public void restartTimer()
        {
            secondsElapsed = 0;
            elapsedTime = TimeSpan.Zero;
        }

        public bool didCollisionHappen(Ship player, Asteroid ast) {
            int playerRadius = player.getRadius();
            int astRadius = Asteroid.radius;
            int distance = playerRadius + astRadius;
            if (Vector2.Distance(player.position, ast.position) < distance) {
                return true;
            }
            return false;
        }
        public bool didShipPast(Ship player, Asteroid ast)
        {
            int playerRadius = player.getRadius();
            int astRadius = Asteroid.radius;
            int distance = playerRadius + astRadius;
            if (player.position.X >= ast.position.X)
            {
                return true;
            }
            return false;
        }

        public String gameEndScript() {

            String gameEndMessage;
            

            if (secondsElapsed >= 15)
            {
                gameEndMessage = $"Game Won, You surpassed {playerScore} asteroids";
                gameGoing = false;
            } else
            {
                gameEndMessage = "Game Lost!! Spaceship hit by Asteroid";
                gameGoing = false;
            }

            
            return gameEndMessage;
        }
}
}
