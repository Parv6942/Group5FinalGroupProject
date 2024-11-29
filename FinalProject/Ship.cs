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
    internal class Ship
    {
        public Vector2 position = new Vector2(100, 100);
        int speed = 4;
        int radius = 0;
        public void setRadius(int radius)
        {
            this.radius = radius;
        }
        public int getRadius() { 
            return this.radius;
        } 
        public void endSpeed()
        {
            this.speed = 0;
        }
        public void updateShip() {
            //3// Move the player along X-axis using Keyboard   
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left))
            {
                this.position.X -= speed;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                this.position.X += speed;
            }
            //4// Move the player along X-axis using Keyboard  
            if (state.IsKeyDown(Keys.Up))
            {
                this.position.Y -=speed ;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                this.position.Y +=speed ;
            }
            if (state.IsKeyDown(Keys.Enter))
            {
                speed = 8;
            }
            if (state.IsKeyUp(Keys.Enter))
            {
                speed = 4;
            }
            
        }
    }
}
