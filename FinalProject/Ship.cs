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
        int radius = 50;
        bool isMoving = false;
        
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

        public bool getIsMoving() 
        {
            return this.isMoving;
        }
        public void updateShip() {
            Vector2 previousPosition = this.position;

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
            speed = state.IsKeyDown(Keys.Space) ? 8 : 4;
            this.isMoving = this.position != previousPosition;
            if (this.position != previousPosition)
            {
                isMoving = true;
            }
        }
    }
}
