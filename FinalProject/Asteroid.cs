using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonogameProject3_Spaceship
{
    internal class Asteroid
    {
        public Vector2 position = new Vector2(1300, 300);
        public int speed;
        static public int radius = 60;
        public bool currentOne = true;
        public Asteroid(int speed)
        {

            this.speed = speed;
        }

        public void updateAsteroid()
        {
            this.position.X -= this.speed;
            if (this.position.X < -100)
            {
                this.position.X = 1300;
                Random random = new Random();
                this.position.Y = random.Next(100, 900);
                this.currentOne = true;
            }

        }
    }
}
