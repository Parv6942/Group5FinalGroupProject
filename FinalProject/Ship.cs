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
        bool isMoiving = false;
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
            return this.isMoiving;
        }
        public void updateShip() {
            //3// Move the player along X-axis using Keyboard   
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left))
            {
                this.position.X -= speed;
                isMoiving = true;
            }
            if (state.IsKeyUp(Keys.Left))
            {
                isMoiving = false;
            }

            if (state.IsKeyDown(Keys.Right))
            {
                this.position.X += speed;
                isMoiving = true;
            }
            if (state.IsKeyUp(Keys.Right))
            {
                isMoiving = false;
            }

            //4// Move the player along X-axis using Keyboard  
            if (state.IsKeyDown(Keys.Up))
            {
                this.position.Y -=speed ;
                isMoiving = true;
            }
            if (state.IsKeyUp(Keys.Up))
            {
                isMoiving = false;
            }

            if (state.IsKeyDown(Keys.Down))
            {
                this.position.Y +=speed ;
                isMoiving = true;
            }
            if (state.IsKeyUp(Keys.Down))
            {
                isMoiving = false;
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
