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

namespace Deep_Space
{
    class Movement
    {
        public MouseState mousePosition;
        public float rotation;
        public MouseState lastMouse;
        public Vector2 position;
        public Vector2 size;
        public float velocity = 3;
        public float omega = 0.1f;
        public Vector2 tempPosition;
        public Vector2 tempVelocity = new Vector2(0,0);
        public Vector2 screenSize;
        public Vector2 tempX;
        public int k = 0;
        public int z = 0;
        public int wrenchCount;
        public Vector2 newposition;
        public int present;
        public Vector2 tempWPosition;
        public bool winner = false;
        public double Egt = 0;
        public Vector2 unitVector;

        public void UpdateMovement()
        {
            lastMouse = mousePosition;
            mousePosition = Mouse.GetState();


            //calculates unit vector for the direction of mouse's location compared to sprite character
            tempPosition.X = mousePosition.X - (position.X + (size.X / 2));
            tempPosition.Y = mousePosition.Y - (position.Y + (size.Y / 2));
            tempX.X = tempPosition.X;
            tempPosition.X = tempPosition.X / (float)Math.Sqrt((tempPosition.X * tempPosition.X) + (tempPosition.Y * tempPosition.Y));
            tempPosition.Y = tempPosition.Y / (float)Math.Sqrt((tempPosition.Y * tempPosition.Y) + (tempX.X * tempX.X));
            tempWPosition = tempPosition;
            unitVector = tempPosition;
            newposition.X = tempPosition.X * 60;
            newposition.Y = tempPosition.Y * 60;
            position.X += -tempVelocity.X;
            position.Y += -tempVelocity.Y;
        }
        public void updateDude()
        {
            tempPosition.X = tempPosition.X * velocity;
            tempPosition.Y = tempPosition.Y * velocity;
            tempVelocity.X = tempVelocity.X + tempPosition.X;
            tempVelocity.Y = tempVelocity.Y + tempPosition.Y;
            //five wrench max compound speed
            if (tempVelocity.X > 8)
                tempVelocity.X = 8;
            if (tempVelocity.X < -8)
                tempVelocity.X = -8;
            if (tempVelocity.Y > 8)
                tempVelocity.Y = 8;
            if (tempVelocity.Y < -8)
                tempVelocity.Y = -8;
            k = k + 1;
            if (k == 998)
            {
                k = 0;
            }

            wrenchCount = wrenchCount - 1;
        }

        public void updateWrench(GameTime gametime)
        {
            if (present == 2)
            {
                tempWPosition.X = tempWPosition.X * 2 * velocity;
                tempWPosition.Y = tempWPosition.Y * 2 * velocity;
                tempVelocity.X = tempVelocity.X + tempWPosition.X;
                tempVelocity.Y = tempVelocity.Y + tempWPosition.Y;
                present = 3;
                Egt = gametime.TotalGameTime.TotalSeconds;
            }

        }
        public void updateWrenchMovement()
        {
            position.X += tempVelocity.X;
            position.Y += tempVelocity.Y;
            rotation = rotation + omega;
        }

        public void Collision()
        {
            if (position.X + size.X >= screenSize.X || position.Y + size.Y >= screenSize.Y - 50 || position.X <= 0 || position.Y <= 0)
            {
                winner = true;
            }

        }
        public bool wrenchCollideR(Sprite otherWrench)
        {
            if (this.position.X > otherWrench.position.X &&
                this.position.X < otherWrench.position.X + otherWrench.size.X &&
                this.position.Y > otherWrench.position.Y - otherWrench.size.Y + 1 &&
                this.position.Y < otherWrench.position.Y + otherWrench.size.Y - 1)
            {
                return true;
            }
            else
                return false;
        }
        public bool wrenchCollideL(Sprite otherWrench)
        {
            if (this.position.X + this.size.X > otherWrench.position.X &&
                this.position.X + this.size.X < otherWrench.position.X + otherWrench.size.X &&
                this.position.Y > otherWrench.position.Y - otherWrench.size.Y + 1 &&
                this.position.Y < otherWrench.position.Y + otherWrench.size.Y - 1)
            {
                return true;
            }
            else
                return false;
        }
        public bool wrenchCollideB(Sprite otherWrench)
        {
            if (this.position.Y + this.size.Y > otherWrench.position.Y &&
                this.position.Y + this.size.Y < otherWrench.position.Y + otherWrench.size.Y &&
                this.position.X > otherWrench.position.X - otherWrench.size.X + 1 &&
                this.position.X < otherWrench.position.X + otherWrench.size.X - 1)
            {
                return true;
            }
            else
                return false;
        }

        public bool wrenchCollideT(Sprite otherWrench)
        {
            if (this.position.Y > otherWrench.position.Y &&
                this.position.Y < otherWrench.position.Y + otherWrench.size.Y &&
                this.position.X > otherWrench.position.X - otherWrench.size.X + 1 &&
                this.position.X < otherWrench.position.X + otherWrench.size.X - 1)
            {
                return true;
            }
            else
                return false;
        }
        public void MenuWrenchPosition(GameTime gametime)
        {
                tempWPosition.X = tempWPosition.X * 2 * velocity;
                tempWPosition.Y = tempWPosition.Y * 2 * velocity;
                tempVelocity.X = tempVelocity.X + tempWPosition.X;
                tempVelocity.Y = tempVelocity.Y + tempWPosition.Y;
                Egt = gametime.TotalGameTime.TotalSeconds;

        }
        public void bulletMove()
        {
            tempPosition.X = tempPosition.X * 1.25f * velocity;
            tempPosition.Y = tempPosition.Y * 1.25f * velocity;
            tempVelocity.X = tempVelocity.X + tempPosition.X;
            tempVelocity.Y = tempVelocity.Y + tempPosition.Y;
        }
        public void updatebulletMove()
        {
            position.X += tempVelocity.X;
            position.Y += tempVelocity.Y;
        }

        public void MenuWrenchMove()
        {
            position.X += tempVelocity.X;
            position.Y += tempVelocity.Y;
            rotation += omega;
        }
    
    
    
    

    
        

    }
}
