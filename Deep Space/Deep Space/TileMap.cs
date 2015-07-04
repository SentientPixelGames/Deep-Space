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
using System.IO;

namespace Deep_Space
{
    class TileMap : Movement

    {
        public int arrayWidth = 100;
        public int arrayHeight = 65;
        public int[,] pts = new int[100,66];
        public Vector2[] bulletP = new Vector2[100];
        public int counter = 0;
        public Texture2D texture;
        public Texture2D texture1;
        public Texture2D texture2;
        public Texture2D texture3;
        public Texture2D door;
        public Texture2D ElectricSwitch;
        public Texture2D TurretSwitch;
        public Texture2D DoorSwitch;
        public Vector3 start = new Vector3();
        public bool impassibleCheck = true;
        public bool EdyingCheck = false;
        public bool TdyingCheck = false;
        public bool EswitchCheck = false;
        public bool TswitchCheck = false;
        public bool DswitchCheck = false;

        Rectangle rectangle;
        Vector2 origin;
        int BorderFrame;
        int DeathFrame;
        int frameHeight;
        int frameWidth;
        int electricframe;
        int turretframe;
        int doorframe;
        int doorAframe;
        float doortimer;

        float timer;
        float deathtimer;
        float interval;
       
        public void loadpts(int level)
        {
            using (StreamReader reader = new StreamReader("levels/level" + level.ToString() + ".txt"))
            {
                string line;
                int y = 0;
                while ((line = reader.ReadLine()) != null)
                {

                    string[] tile = line.Split(',');

                    for (int x = 0; x < arrayWidth; x++)
                    {
                        pts[x, y] = Convert.ToInt32(tile[x]);
                    }

                    y++;

                    start.X = pts[0, 65];
                    start.Y = pts[1, 65];
                    start.Z = pts[2, 65];
                    if (pts[3, 65] == 0)
                        EswitchCheck = false;
                    else EswitchCheck = true;
                    if (pts[4, 65] == 0)
                        TswitchCheck = false;
                    else TswitchCheck = true;
                    if (pts[5, 65] == 0)
                        DswitchCheck = false;
                    else DswitchCheck = true;


                }
                reader.Close();
            }
            
        } 
        public TileMap(Texture2D newTexture, Texture2D newTexture1, Texture2D newTexture2, Texture2D newTexture3, Texture2D newDoor, Texture2D newElectricSwitch, Texture2D newTurretSwitch, Texture2D newDoorSwitch)
        {
            texture = newTexture;
            texture1 = newTexture1;
            texture2 = newTexture2;
            texture3 = newTexture3;
            door = newDoor;
            ElectricSwitch = newElectricSwitch;
            TurretSwitch = newTurretSwitch;
            DoorSwitch = newDoorSwitch;
        }
        public void tilePosition(SpriteBatch spriteBatch, GameTime gameTime)
        {
            frameHeight = 10;
            frameWidth = 10;
            interval = 25;
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                BorderFrame++;
                timer = 0;
                if (BorderFrame >= 5)
                    BorderFrame = 0;
               

            }
            if (DswitchCheck == false)
            {
                doortimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                if (doortimer > interval && doorAframe > 0)
                {
                    doorAframe--;
                    doortimer = 0; 
                }
            }
            if (DswitchCheck == true)
            {
                doortimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                if (doortimer > interval && doorAframe < 5)
                {
                    doorAframe++;
                    doortimer = 0;
                }
            }
                
            int y = 0;
            int i = 0;
            if (i < arrayHeight * 10)
            {
                for (i = 0; i < arrayHeight * 10; i = i + 10)
                {

                    int x = 0;
                    for (int j = 0; j < arrayWidth * 10; j = j + 10)
                    {
                        if (pts[x, y] == 1)
                        {
                            spriteBatch.Draw(texture, new Rectangle(j, i, 10, 10), Color.White);

                        } 
                        if (pts[x, y] == 2)
                        {
                            spriteBatch.Draw(texture1, new Rectangle(j, i, 50, 50), Color.White);
                        }
                        if (EswitchCheck == false)
                        {
                            if (pts[x, y] == 3)
                            {
                                rectangle = new Rectangle(BorderFrame * frameWidth, 0, frameWidth, frameHeight);
                                origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
                                spriteBatch.Draw(texture2, new Rectangle(j + (int)origin.X, i + (int)origin.Y, 10, 10), rectangle, Color.White, 0f, origin, SpriteEffects.None, 0);

                            }
                            if (pts[x, y] == 4)
                            {
                                rectangle = new Rectangle(BorderFrame * frameWidth, 0, frameWidth, frameHeight);
                                origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
                                spriteBatch.Draw(texture3, new Rectangle(j + (int)origin.X, i + (int)origin.Y, 10, 10), rectangle, Color.White, 0f, origin, SpriteEffects.None, 0);
                            }
                        }
                        if (pts[x, y] == 5)
                        {
                            spriteBatch.Draw(texture1, new Rectangle(j, i, 10, 10), Color.PapayaWhip);
                        }
                        if (pts[x, y] == 6)
                        {
                            spriteBatch.Draw(door, new Rectangle(j, i, 10, 100), new Rectangle(0, 0, 10, 100), Color.White);
                        }
                        if (pts[x, y] == 8)
                        {
                            spriteBatch.Draw(door, new Rectangle(j, i, 10, 100), new Rectangle(0, 0, 10, 100), Color.White, -1.57f, new Vector2(10,0), SpriteEffects.None,0);
                        }
                        if (pts[x, y] == 14)
                        {   
                            frameHeight = 100;
                            frameWidth = 10;
                            rectangle = new Rectangle(doorAframe * frameWidth, 0, frameWidth, frameHeight);                          
                            spriteBatch.Draw(door, new Rectangle(j, i, 10, 100), rectangle, Color.White);
                            frameHeight = 10;
                            frameWidth = 10;
                            
                        }
                        if (pts[x, y] == 15)
                        {   
                            frameHeight = 100;
                            frameWidth = 10;
                            rectangle = new Rectangle(doorAframe * frameWidth, 0, frameWidth, frameHeight);
                            origin = new Vector2(rectangle.Width, 0);
                            spriteBatch.Draw(door, new Rectangle(j, i, 10, 100), rectangle, Color.White, -1.57f, origin, SpriteEffects.None, 0);
                            frameHeight = 10;
                            frameWidth = 10;
                            
                        }
                        if (pts[x, y] == 17)
                        {
                            frameHeight = 100;
                            frameWidth = 10;
                            rectangle = new Rectangle((5-doorAframe) * frameWidth, 0, frameWidth, frameHeight);
                            spriteBatch.Draw(door, new Rectangle(j, i, 10, 100), rectangle, Color.White);
                            frameHeight = 10;
                            frameWidth = 10;

                        }
                        if (pts[x, y] == 18)
                        {
                            frameHeight = 100;
                            frameWidth = 10;
                            rectangle = new Rectangle((5-doorAframe) * frameWidth, 0, frameWidth, frameHeight);
                            origin = new Vector2(rectangle.Width, 0);
                            spriteBatch.Draw(door, new Rectangle(j, i, 10, 100), rectangle, Color.White, -1.57f, origin, SpriteEffects.None, 0);
                            frameHeight = 10;
                            frameWidth = 10;

                        }
                        if (pts[x, y] == 9)
                        {
                            frameHeight = 30;
                            frameWidth = 30;
                            if (EswitchCheck == false)
                                electricframe = 1;
                            else electricframe = 0;
                            rectangle = new Rectangle(electricframe * frameWidth, 0, frameWidth, frameHeight);
                            spriteBatch.Draw(ElectricSwitch, new Rectangle(j, i, 30, 30), rectangle, Color.White);
                            frameHeight = 10;
                            frameWidth = 10;
                        }
                        if (pts[x, y] == 10)
                        {
                            frameHeight = 30;
                            frameWidth = 30;
                            if (TswitchCheck == false)
                                turretframe = 1;
                            else turretframe = 0;
                            rectangle = new Rectangle(turretframe * frameWidth, 0, frameWidth, frameHeight);
                            spriteBatch.Draw(TurretSwitch, new Rectangle(j, i, 30, 30), rectangle, Color.White);
                            frameHeight = 10;
                            frameWidth = 10;
                        }
                        if (pts[x, y] == 11)
                        {
                            frameHeight = 30;
                            frameWidth = 30;
                            if (DswitchCheck == false)
                                doorframe = 1;
                            else doorframe = 0;
                            rectangle = new Rectangle(doorframe * frameWidth, 0, frameWidth, frameHeight);
                            spriteBatch.Draw(DoorSwitch, new Rectangle(j, i, 30, 30), rectangle, Color.White);
                            frameHeight = 10;
                            frameWidth = 10;
                        }
                        if (EswitchCheck == true)
                        {
                            if (pts[x, y] == 12)
                            {
                                rectangle = new Rectangle(BorderFrame * frameWidth, 0, frameWidth, frameHeight);
                                origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
                                spriteBatch.Draw(texture2, new Rectangle(j + (int)origin.X, i + (int)origin.Y, 10, 10), rectangle, Color.White, 0f, origin, SpriteEffects.None, 0);

                            }
                            if (pts[x, y] == 13)
                            {
                                rectangle = new Rectangle(BorderFrame * frameWidth, 0, frameWidth, frameHeight);
                                origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
                                spriteBatch.Draw(texture3, new Rectangle(j + (int)origin.X, i + (int)origin.Y, 10, 10), rectangle, Color.White, 0f, origin, SpriteEffects.None, 0);
                            }
                        }

                       

                        x++;
                    }

                    y++;
                }
            }
        }
        public void bulletPosition()
        {
            int y = 0;
            int i = 0;
            int p = 0;
            if (i < arrayHeight * 10)
            {
                for (i = 0; i < arrayHeight * 10; i = i + 10)
                {

                    int x = 0;
                    for (int j = 0; j < arrayWidth * 10; j = j + 10)
                    {
                        if (pts[x, y] == 5)
                        {
                            bulletP[0 + p] = new Vector2(j, i);
                            p++;
                            counter++;
                        }
                        x++;
                    }

                    y++;
                }
            }
        }
        public void WImpassible(Sprite othersprite, GameTime gametime)
        {
            int y = 0;
            for (int i = 0; i < arrayHeight * 10; i = i + 10)
            {
                int x = 0;
                for (int j = 0; j < arrayWidth * 10; j = j + 10)
                {
                    if (DswitchCheck == false)
                    {
                        if (pts[x, y] == 16 || pts[x, y] == 15 || pts[x, y] == 14)
                        {
                            if (j + 10 >= othersprite.position.X - (othersprite.size.X / 2) && j <= othersprite.position.X - (othersprite.size.X / 2) && i > othersprite.position.Y - (othersprite.size.Y / 2) && i + 10 < othersprite.position.Y + (othersprite.size.Y / 2))
                            {///checks for right side of tile collision
                                othersprite.position.X = j + 11 + (othersprite.size.X / 2);
                                othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);
                                impassibleCheck = false;

                            }
                            if (j + 10 >= othersprite.position.X + (othersprite.size.X / 2) && j <= othersprite.position.X + (othersprite.size.X / 2) && i > othersprite.position.Y - (othersprite.size.Y / 2) && i + 10 < othersprite.position.Y + (othersprite.size.Y / 2))
                            {//checks for left side of tile collision
                                othersprite.position.X = j - (othersprite.size.X / 2) - 1;
                                othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);
                                impassibleCheck = false;

                            }
                            if (i + 10 >= othersprite.position.Y - (othersprite.size.Y / 2) && i <= othersprite.position.Y - (othersprite.size.Y / 2) && j > othersprite.position.X - (othersprite.size.X / 2) && j + 10 < othersprite.position.X + (othersprite.size.X / 2))
                            {//checks for bottom of tile collision
                                othersprite.position.Y = i + 11 + (othersprite.size.Y / 2);
                                othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                                impassibleCheck = false;

                            }
                            if (i + 10 >= othersprite.position.Y + (othersprite.size.Y / 2) && i <= othersprite.position.Y + (othersprite.size.Y / 2) && j > othersprite.position.X - (othersprite.size.X / 2) && j + 10 < othersprite.position.X + (othersprite.size.X / 2))
                            {//checks for top side of tile collision
                                othersprite.position.Y = i - (othersprite.size.X / 2) - 1;
                                othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                                impassibleCheck = false;

                            }
                        }
                    }
                    if (DswitchCheck == true)
                    {
                        if (pts[x, y] == 17 || pts[x, y] == 18 || pts[x, y] == 19)
                        {
                            if (j + 10 >= othersprite.position.X - (othersprite.size.X / 2) && j <= othersprite.position.X - (othersprite.size.X / 2) && i > othersprite.position.Y - (othersprite.size.Y / 2) && i + 10 < othersprite.position.Y + (othersprite.size.Y / 2))
                            {///checks for right side of tile collision
                                othersprite.position.X = j + 11 + (othersprite.size.X / 2);
                                othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);
                                impassibleCheck = false;

                            }
                            if (j + 10 >= othersprite.position.X + (othersprite.size.X / 2) && j <= othersprite.position.X + (othersprite.size.X / 2) && i > othersprite.position.Y - (othersprite.size.Y / 2) && i + 10 < othersprite.position.Y + (othersprite.size.Y / 2))
                            {//checks for left side of tile collision
                                othersprite.position.X = j - (othersprite.size.X / 2) - 1;
                                othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);
                                impassibleCheck = false;

                            }
                            if (i + 10 >= othersprite.position.Y - (othersprite.size.Y / 2) && i <= othersprite.position.Y - (othersprite.size.Y / 2) && j > othersprite.position.X - (othersprite.size.X / 2) && j + 10 < othersprite.position.X + (othersprite.size.X / 2))
                            {//checks for bottom of tile collision
                                othersprite.position.Y = i + 11 + (othersprite.size.Y / 2);
                                othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                                impassibleCheck = false;

                            }
                            if (i + 10 >= othersprite.position.Y + (othersprite.size.Y / 2) && i <= othersprite.position.Y + (othersprite.size.Y / 2) && j > othersprite.position.X - (othersprite.size.X / 2) && j + 10 < othersprite.position.X + (othersprite.size.X / 2))
                            {//checks for top side of tile collision
                                othersprite.position.Y = i - (othersprite.size.X / 2) - 1;
                                othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                                impassibleCheck = false;

                            }
                        }
                    }
                    if (pts[x, y] == 1 || pts[x, y] == 6 || pts[x, y] == 7)
                    {
                        if (j + 10 >= othersprite.position.X - (othersprite.size.X / 2) && j <= othersprite.position.X - (othersprite.size.X / 2) && i > othersprite.position.Y - (othersprite.size.Y / 2) && i + 10 < othersprite.position.Y + (othersprite.size.Y / 2))
                        {///checks for right side of tile collision
                            othersprite.position.X = j + 11 + (othersprite.size.X / 2);
                            othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);
                            impassibleCheck = false;

                        }
                        if (j + 10 >= othersprite.position.X + (othersprite.size.X / 2) && j <= othersprite.position.X + (othersprite.size.X / 2) && i > othersprite.position.Y - (othersprite.size.Y / 2) && i + 10 < othersprite.position.Y + (othersprite.size.Y / 2))
                        {//checks for left side of tile collision
                            othersprite.position.X = j - (othersprite.size.X / 2) - 1;
                            othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);
                            impassibleCheck = false;

                        }
                        if (i + 10 >= othersprite.position.Y - (othersprite.size.Y / 2) && i <= othersprite.position.Y - (othersprite.size.Y / 2) && j > othersprite.position.X - (othersprite.size.X / 2) && j + 10 < othersprite.position.X + (othersprite.size.X / 2))
                        {//checks for bottom of tile collision
                            othersprite.position.Y = i + 11 + (othersprite.size.Y / 2);
                            othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                            impassibleCheck = false;

                        }
                        if (i + 10 >= othersprite.position.Y + (othersprite.size.Y / 2) && i <= othersprite.position.Y + (othersprite.size.Y / 2) && j > othersprite.position.X - (othersprite.size.X / 2) && j + 10 < othersprite.position.X + (othersprite.size.X / 2))
                        {//checks for top side of tile collision
                            othersprite.position.Y = i - (othersprite.size.X / 2) - 1;
                            othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                            impassibleCheck = false;

                        }
                    }
                    if (pts[x, y] == 9 || pts[x, y] == 10 || pts[x, y] == 11)
                    {


                        if (othersprite.present != 2 && j + 30 > othersprite.position.X - (othersprite.size.X / 2) && j < othersprite.position.X + (othersprite.size.X / 2) && i + 30 > othersprite.position.Y - (othersprite.size.Y / 2) && i < othersprite.position.Y + (othersprite.size.Y / 2))
                        {
                            if (othersprite.present != 4)
                            {
                                if (pts[x, y] == 9)
                                {
                                    if (EswitchCheck == true)
                                        EswitchCheck = false;
                                    else if (EswitchCheck == false)
                                        EswitchCheck = true;
                                    othersprite.present = 4;
                                }
                                if (pts[x, y] == 10)
                                {
                                    if (TswitchCheck == true)
                                        TswitchCheck = false;
                                    else if (TswitchCheck == false)
                                        TswitchCheck = true;
                                    othersprite.present = 4;
                                }
                                if (pts[x, y] == 11)
                                {
                                    if (DswitchCheck == true)
                                        DswitchCheck = false;
                                    else if (DswitchCheck == false)
                                        DswitchCheck = true;
                                    othersprite.present = 4;
                                }
                                othersprite.Egt = (float)gametime.TotalGameTime.TotalSeconds;


                            }

                        }
                        if (othersprite.present == 4 && othersprite.Egt + 0.5f < gametime.TotalGameTime.TotalSeconds)
                        {
                            othersprite.present = 1;
                        }

                    }
                
                    x++;
                }
                y++;
            }
        }
        public void Impassible(Sprite othersprite)
        {
            int y = 0;
            for (int i = 0; i < arrayHeight * 10; i = i + 10)
            {
                int x = 0;
                for (int j = 0; j < arrayWidth * 10; j = j + 10)
                {
                    if (DswitchCheck == false)
                    {
                        if (pts[x, y] == 16 || pts[x, y] == 15 || pts[x, y] == 14)
                        {
                            if (j + 10 >= othersprite.position.X && j <= othersprite.position.X && i > othersprite.position.Y && i + 10 < othersprite.position.Y + othersprite.size.Y)
                            {///checks for right side of tile collision
                                othersprite.position.X = j + 11;
                                othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);

                                impassibleCheck = false;

                            }
                            if (j + 10 >= othersprite.position.X + othersprite.size.X && j <= othersprite.position.X + othersprite.size.X && i > othersprite.position.Y && i + 10 < othersprite.position.Y + othersprite.size.Y)
                            {//checks for left side of tile collision
                                othersprite.position.X = j - othersprite.size.X - 1;
                                othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);
                                impassibleCheck = false;

                            }
                            if (i + 10 >= othersprite.position.Y && i <= othersprite.position.Y && j > othersprite.position.X && j + 10 < othersprite.position.X + othersprite.size.X)
                            {//checks for bottom of tile collision
                                othersprite.position.Y = i + 11;
                                othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                                impassibleCheck = false;

                            }
                            if (i + 10 >= othersprite.position.Y + othersprite.size.Y && i <= othersprite.position.Y + othersprite.size.Y && j > othersprite.position.X && j + 10 < othersprite.position.X + othersprite.size.X)
                            {//checks for top side of tile collision
                                othersprite.position.Y = i - othersprite.size.Y - 1;
                                othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                                impassibleCheck = false;

                            }
                        }
                    }
                    if (DswitchCheck == true)
                    {
                        if (pts[x, y] == 17 || pts[x, y] == 18 || pts[x, y] == 19)
                        {
                            if (j + 10 >= othersprite.position.X && j <= othersprite.position.X && i > othersprite.position.Y && i + 10 < othersprite.position.Y + othersprite.size.Y)
                            {///checks for right side of tile collision
                                othersprite.position.X = j + 11;
                                othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);

                                impassibleCheck = false;

                            }
                            if (j + 10 >= othersprite.position.X + othersprite.size.X && j <= othersprite.position.X + othersprite.size.X && i > othersprite.position.Y && i + 10 < othersprite.position.Y + othersprite.size.Y)
                            {//checks for left side of tile collision
                                othersprite.position.X = j - othersprite.size.X - 1;
                                othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);
                                impassibleCheck = false;

                            }
                            if (i + 10 >= othersprite.position.Y && i <= othersprite.position.Y && j > othersprite.position.X && j + 10 < othersprite.position.X + othersprite.size.X)
                            {//checks for bottom of tile collision
                                othersprite.position.Y = i + 11;
                                othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                                impassibleCheck = false;

                            }
                            if (i + 10 >= othersprite.position.Y + othersprite.size.Y && i <= othersprite.position.Y + othersprite.size.Y && j > othersprite.position.X && j + 10 < othersprite.position.X + othersprite.size.X)
                            {//checks for top side of tile collision
                                othersprite.position.Y = i - othersprite.size.Y - 1;
                                othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                                impassibleCheck = false;

                            }
                        }
                    }
                    if (pts[x, y] == 1 || pts[x, y] == 6 || pts[x, y] == 7)
                    {
                        if (j + 10 >= othersprite.position.X && j <= othersprite.position.X && i > othersprite.position.Y && i + 10 < othersprite.position.Y + othersprite.size.Y)
                        {///checks for right side of tile collision
                            othersprite.position.X = j + 11;
                            othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);

                            impassibleCheck = false;

                        }
                        if (j + 10 >= othersprite.position.X + othersprite.size.X && j <= othersprite.position.X + othersprite.size.X && i > othersprite.position.Y && i + 10 < othersprite.position.Y + othersprite.size.Y)
                        {//checks for left side of tile collision
                            othersprite.position.X = j - othersprite.size.X - 1;
                            othersprite.tempVelocity = new Vector2(-othersprite.tempVelocity.X / 2, othersprite.tempVelocity.Y);
                            impassibleCheck = false;

                        }
                        if (i + 10 >= othersprite.position.Y && i <= othersprite.position.Y && j > othersprite.position.X && j + 10 < othersprite.position.X + othersprite.size.X)
                        {//checks for bottom of tile collision
                            othersprite.position.Y = i + 11;
                            othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                            impassibleCheck = false;

                        }
                        if (i + 10 >= othersprite.position.Y + othersprite.size.Y && i <= othersprite.position.Y + othersprite.size.Y && j > othersprite.position.X && j + 10 < othersprite.position.X + othersprite.size.X)
                        {//checks for top side of tile collision
                            othersprite.position.Y = i - othersprite.size.Y - 1;
                            othersprite.tempVelocity = new Vector2(othersprite.tempVelocity.X, -othersprite.tempVelocity.Y / 2);
                            impassibleCheck = false;

                        }
                    }
                    x++;
                }
                y++;
            }
        }
            public void Death(Sprite othersprite)
        {
            int y = 0;
            for (int i = 0; i < arrayHeight*10; i = i + 10)
            {
                int x = 0;
                for (int j = 0; j < arrayWidth*10; j = j + 10)
                {
                    if (EswitchCheck == false)
                    {
                        if (pts[x, y] == 3 || pts[x, y] == 4)
                        {
                            if (j + 10 >= othersprite.position.X && j <= othersprite.position.X && i > othersprite.position.Y && i + 10 < othersprite.position.Y + othersprite.size.Y)
                            {///checks for right side of tile collision
                                othersprite.present = 0;
                                EdyingCheck = true;
                            }
                            if (j + 10 >= othersprite.position.X + othersprite.size.X && j <= othersprite.position.X + othersprite.size.X && i > othersprite.position.Y && i + 10 < othersprite.position.Y + othersprite.size.Y)
                            {//checks for left side of tile collision
                                othersprite.present = 0;
                                EdyingCheck = true;
                            }
                            if (i + 10 >= othersprite.position.Y && i <= othersprite.position.Y && j > othersprite.position.X && j + 10 < othersprite.position.X + othersprite.size.X)
                            {//checks for bottom of tile collision
                                othersprite.present = 0;
                                EdyingCheck = true;
                            }
                            if (i + 10 >= othersprite.position.Y + othersprite.size.Y && i <= othersprite.position.Y + othersprite.size.Y && j > othersprite.position.X && j + 10 < othersprite.position.X + othersprite.size.X)
                            {//checks for top side of tile collision
                                othersprite.present = 0;
                                EdyingCheck = true;
                            }
                        }
                    }
                    else if (EswitchCheck == true)
                    {
                        if (pts[x, y] == 12 || pts[x, y] == 13)
                        {
                            if (j + 10 >= othersprite.position.X && j <= othersprite.position.X && i > othersprite.position.Y && i + 10 < othersprite.position.Y + othersprite.size.Y)
                            {///checks for right side of tile collision
                                othersprite.present = 0;
                                EdyingCheck = true;
                            }
                            if (j + 10 >= othersprite.position.X + othersprite.size.X && j <= othersprite.position.X + othersprite.size.X && i > othersprite.position.Y && i + 10 < othersprite.position.Y + othersprite.size.Y)
                            {//checks for left side of tile collision
                                othersprite.present = 0;
                                EdyingCheck = true;
                            }
                            if (i + 10 >= othersprite.position.Y && i <= othersprite.position.Y && j > othersprite.position.X && j + 10 < othersprite.position.X + othersprite.size.X)
                            {//checks for bottom of tile collision
                                othersprite.present = 0;
                                EdyingCheck = true;
                            }
                            if (i + 10 >= othersprite.position.Y + othersprite.size.Y && i <= othersprite.position.Y + othersprite.size.Y && j > othersprite.position.X && j + 10 < othersprite.position.X + othersprite.size.X)
                            {//checks for top side of tile collision
                                othersprite.present = 0;
                                EdyingCheck = true;
                            }
                        }
                    }
                    x++;
                }
                y++;
            }

        }
            public void deathAnimation(Sprite othersprite, GameTime gametime, SpriteBatch spriteBatch, Texture2D deathElectric, Texture2D deathFire)
            {
                frameHeight = 75;
                frameWidth = 50;
                interval = 25;
                deathtimer += (float)gametime.ElapsedGameTime.TotalMilliseconds / 2;
                if (deathtimer > interval)
                {
                    DeathFrame++;
                    deathtimer = 0;
                    if (DeathFrame >= 5)
                        DeathFrame = 0;
                }
                rectangle = new Rectangle(DeathFrame * frameWidth, 0, frameWidth, frameHeight);
                origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
                if (EdyingCheck == true)
                    spriteBatch.Draw(deathElectric, new Rectangle((int)othersprite.position.X + (int)origin.X, (int)othersprite.position.Y + (int)origin.Y, (int)othersprite.size.X, (int)othersprite.size.Y), rectangle, Color.White, 0f, origin, SpriteEffects.None, 0);
                if (TdyingCheck == true)
                    spriteBatch.Draw(deathFire, new Rectangle((int)othersprite.position.X + (int)origin.X, (int)othersprite.position.Y + (int)origin.Y, (int)othersprite.size.X, (int)othersprite.size.Y), rectangle, Color.White, 0f, origin, SpriteEffects.None, 0);

                EdyingCheck = false;
                TdyingCheck = false;
            }

            public void turretAim(Sprite othersprite, Sprite bullet)
            {
                int y = 0;
                for (int i = 0; i < arrayHeight * 10; i = i + 10)
                {
                    int x = 0;
                    for (int j = 0; j < arrayWidth * 10; j = j + 10)
                    {
                        if (pts[x,y] == 5)
                        {
                            bullet.tempPosition.X = (othersprite.position.X + (othersprite.size.X / 2)) - (bullet.position.X + (bullet.size.X));
                            bullet.tempPosition.Y = (othersprite.position.Y + (othersprite.size.Y / 2)) - (bullet.position.Y + (bullet.size.Y));


                            bullet.tempX.X = bullet.tempPosition.X;
                            bullet.tempPosition.X = bullet.tempPosition.X / (float)Math.Sqrt((bullet.tempPosition.X * bullet.tempPosition.X) + (bullet.tempPosition.Y * bullet.tempPosition.Y));
                            bullet.tempPosition.Y = bullet.tempPosition.Y / (float)Math.Sqrt((bullet.tempPosition.Y * bullet.tempPosition.Y) + (bullet.tempX.X * bullet.tempX.X));
                            unitVector = tempPosition;
                        }
                        x++;
                    }
                    y++;
                }

            }
            public void BImpassible(Sprite othersprite)
            {
                int y = 0;
                for (int i = 0; i < arrayHeight * 10; i = i + 10)
                {
                    int x = 0;
                    for (int j = 0; j < arrayWidth * 10; j = j + 10)
                    {
                        if (pts[x, y] == 1 || pts[x, y] == 6 || pts[x, y] == 7)
                        {
                            if (j + 10 > othersprite.position.X && j < othersprite.position.X + othersprite.size.X && i + 10 > othersprite.position.Y && i < othersprite.position.Y + othersprite.size.Y)
                            {//checks for top side of tile collision
                                impassibleCheck = false;

                            }
                        }
                        if (DswitchCheck == false)
                        {
                            if (pts[x, y] == 14 || pts[x, y] == 15 || pts[x, y] == 16)
                            {
                                if (j + 10 > othersprite.position.X && j < othersprite.position.X + othersprite.size.X && i + 10 > othersprite.position.Y && i < othersprite.position.Y + othersprite.size.Y)
                                {//checks for top side of tile collision
                                    impassibleCheck = false;

                                }
                            }
                        }
                        if (DswitchCheck == true)
                        {
                            if (pts[x, y] == 17 || pts[x, y] == 18 || pts[x, y] == 19)
                            {
                                if (j + 10 > othersprite.position.X && j < othersprite.position.X + othersprite.size.X && i + 10 > othersprite.position.Y && i < othersprite.position.Y + othersprite.size.Y)
                                {//checks for top side of tile collision
                                    impassibleCheck = false;

                                }
                            }
                        }

                        x++;
                    }
                    y++;
                }
            }
            public void DeathBullet(Sprite othersprite, Sprite bullet)
            {
                if (bullet.position.X + bullet.size.X > othersprite.position.X && bullet.position.X < othersprite.position.X + othersprite.size.X && bullet.position.Y + bullet.size.Y > othersprite.position.Y && bullet.position.Y < othersprite.position.Y + othersprite.size.Y)
                {//checks for top side of tile collision
                    impassibleCheck = false;
                    othersprite.present = 0;
                    TdyingCheck = true;

                }
            }

    }
}
