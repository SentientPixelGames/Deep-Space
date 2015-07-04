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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region variable declaration
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Sprite dude;
        Sprite bossman;
        int bossmanimage;
        Save_Load gameStart;
        TileMap border;
        Sprite[] wrench = new Sprite[999];
        Sprite[] bullet = new Sprite[100];
        Random Rnd = new Random(DateTime.Now.Millisecond);
        int bossAttack;
        int ScreenWidth = 1000;
        int ScreenHeight = 700;
        Vector2 playCoor = new Vector2(25, 350);
        int i;
        int bossSpeed;
        int switchcounter;
        int bosspoop;
        int loadValue = 99;
        int musicValue = 5;
        int transitionValue = 0;
        int timer;
        int lvl = 1;
        int numlvl = 40;
        int levelprogress;
        string world = "ENTRANCE";
        Song backgroundMusic;
        float alpha;
        float fadedelay;
        bool arm;
        bool switchy = false;
        bool openingspace;
        enum GameState
        {
            Transition,
            Menu,
            LevelSel,
            Play,
            Boss,
        }


        GameState CurrentGameState = GameState.Transition;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            

            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = ScreenHeight; 
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferMultiSampling = false;

        }
        protected override void Initialize()
        {
             spriteBatch = new SpriteBatch(GraphicsDevice);
            this.IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            
            #region music
            if (musicValue == 0)
            {
                backgroundMusic = Content.Load<Song>("Music/DS Main");
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true;
            }
            if (musicValue == 4)
            {
                backgroundMusic = Content.Load<Song>("Music/Throne Room");
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true;
            }
            if (musicValue == 2)
            {
                backgroundMusic = Content.Load<Song>("Music/Armory");
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true;
            }
            if (musicValue == 5)
            {
                backgroundMusic = Content.Load<Song>("Music/Opening");
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true;
            }
            if (musicValue == 3)
            {
                backgroundMusic = Content.Load<Song>("Music/Cell Block");
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true;
            }
            if (musicValue == 1)
            {
                backgroundMusic = Content.Load<Song>("Music/Deep Space - 1st World");
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true;
            }
            #endregion
            if (transitionValue == 0 || transitionValue == 1 || transitionValue == 2 || transitionValue == 3 || transitionValue == 4 || transitionValue == 5)
            {

                fadedelay = 10;
                alpha = 1;
                timer = 0;
                openingspace = false;
                switchcounter = 0;
                bosspoop = 750;
                bossSpeed = 0;
                bossAttack = 1250;
                font = Content.Load<SpriteFont>("Fonts/CreditText");
            }

            #region loadvalue
            if (loadValue == 0)
            {
                gameStart = new Save_Load();
                gameStart.Load();
                levelprogress = gameStart.LoadProgress;
                dude = new Sprite(Content.Load<Texture2D>("Sprites/TheDude"), new Vector2(700, 500), new Vector2(50, 75), ScreenWidth, ScreenHeight, 0, 1, 0);
                wrench[0] = new Sprite(Content.Load<Texture2D>("Sprites/Thewrench"), new Vector2((dude.position.X + (dude.size.X / 2)), (dude.position.Y + (dude.size.Y / 2))), new Vector2(25, 25), ScreenWidth, ScreenHeight, 0, 2, 0);

            }
            if (loadValue == 1)
            {

                border = new TileMap(Content.Load<Texture2D>("Tilemap/shipblock"), Content.Load<Texture2D>("Menu/levelbox"), Content.Load<Texture2D>("Tilemap/electricityleftrightA"), Content.Load<Texture2D>("Tilemap/electicityupdownA"), Content.Load<Texture2D>("Tilemap/Door"), Content.Load<Texture2D>("Switches/ElectricSwitch"), Content.Load<Texture2D>("Switches/TurretSwitch"), Content.Load<Texture2D>("Switches/DoorSwitch"));
                border.loadpts(lvl);
                border.bulletPosition();
                dude = new Sprite(Content.Load<Texture2D>("Sprites/TheDude"), new Vector2(border.start.X, border.start.Y), new Vector2(50, 75), ScreenWidth, ScreenHeight, (int)border.start.Z, 1, 0);

                font = Content.Load<SpriteFont>("Fonts/GameText");
                for (i = 0; i <= 998; i++)
                    wrench[i] = new Sprite(Content.Load<Texture2D>("Sprites/Thewrench"), new Vector2((dude.position.X + (dude.size.X / 2)), (dude.position.Y + (dude.size.Y / 2))), new Vector2(25, 25), ScreenWidth, ScreenHeight, 0, 2, 0);
                for (int j = 0; j < border.counter; j++)
                    bullet[j] = new Sprite(Content.Load<Texture2D>("Sprites/bullet"), new Vector2(border.bulletP[j].X, border.bulletP[j].Y), new Vector2(10, 10), ScreenWidth, ScreenHeight, 0, 2, 0);
            }
            if (loadValue == 2)
            {
                dude = new Sprite(Content.Load<Texture2D>("Sprites/dude"), new Vector2(50, 500), new Vector2(50, 75), ScreenWidth, ScreenHeight, 2, 1, 0);
                border = new TileMap(Content.Load<Texture2D>("Tilemap/shipblock"), Content.Load<Texture2D>("Menu/levelbox"), Content.Load<Texture2D>("Tilemap/electricityleftrightA"), Content.Load<Texture2D>("Tilemap/electicityupdownA"), Content.Load<Texture2D>("Tilemap/Door"), Content.Load<Texture2D>("Switches/ElectricSwitch"), Content.Load<Texture2D>("Switches/TurretSwitch"), Content.Load<Texture2D>("Switches/DoorSwitch"));
                border.loadpts(0);
                font = Content.Load<SpriteFont>("Fonts/LevelText");

            }
            if (loadValue == 3)
            {
                border = new TileMap(Content.Load<Texture2D>("Tilemap/shipblock"), Content.Load<Texture2D>("Menu/levelbox"), Content.Load<Texture2D>("Tilemap/electricityleftrightA"), Content.Load<Texture2D>("Tilemap/electicityupdownA"), Content.Load<Texture2D>("Tilemap/Door"), Content.Load<Texture2D>("Switches/ElectricSwitch"), Content.Load<Texture2D>("Switches/TurretSwitch"), Content.Load<Texture2D>("Switches/DoorSwitch"));
                border.loadpts(lvl);
                border.bulletPosition();
                bossAttack = 0;
                bossmanimage = 0;
                switchcounter = 0;
                bossSpeed = 1;
                bosspoop = 5;
                alpha = 1;
                fadedelay = 100;
                openingspace = false;
                arm = false;
                dude = new Sprite(Content.Load<Texture2D>("Sprites/TheDude"), new Vector2(border.start.X, border.start.Y), new Vector2(50, 75), ScreenWidth, ScreenHeight, (int)border.start.Z, 1, 0);
                bossman = new Sprite(Content.Load<Texture2D>("Sprites/BossMan"), new Vector2(805, 260), new Vector2(150,150), ScreenWidth, ScreenHeight, 3, 1, 0);
                font = Content.Load<SpriteFont>("Fonts/GameText");
                for (i = 0; i <= 998; i++)
                    wrench[i] = new Sprite(Content.Load<Texture2D>("Sprites/Thewrench"), new Vector2((dude.position.X + (dude.size.X / 2)), (dude.position.Y + (dude.size.Y / 2))), new Vector2(25, 25), ScreenWidth, ScreenHeight, 0, 2, 0);
                for (int j = 0; j < border.counter; j++)
                    bullet[j] = new Sprite(Content.Load<Texture2D>("Sprites/bullet"), new Vector2(border.bulletP[j].X, border.bulletP[j].Y), new Vector2(10, 10), ScreenWidth, ScreenHeight, 0, 2, 0);
            }
            #endregion

        }
        protected override void UnloadContent()
        {
            Content.Unload();
        }
        protected override void Update(GameTime gameTime)
        {
            #region buttons
            int k;
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
            {
                this.Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F) == true)
            {
                if (graphics.IsFullScreen == true)
                {
                    graphics.PreferredBackBufferHeight = ScreenHeight;
                    graphics.PreferredBackBufferWidth = ScreenWidth;
                }
                else if (graphics.IsFullScreen == false)
                {
                    graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
                    graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
                }
                graphics.ToggleFullScreen();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.B) == true)
                k = 5;
            #endregion
            musicValue = 10;
            switch (CurrentGameState)
            {
                case GameState.Transition:
                    #region
                    if (timer == 0)
                        timer = (int)gameTime.TotalGameTime.TotalSeconds;

                    if (transitionValue == 0)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
                        {
                            musicValue = 0;
                            loadValue = 0;
                            transitionValue = 99;
                            LoadContent();
                            CurrentGameState = GameState.Menu;
                        }
                        if (timer < gameTime.TotalGameTime.TotalSeconds && timer + 3 > gameTime.TotalGameTime.TotalSeconds && openingspace == false)
                        {
                            if (alpha > 0)
                            {
                                fadedelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                                if (fadedelay <= 0)
                                {
                                    fadedelay = 10;

                                    alpha -= (float)0.01;
                                }

                            }

                        }
                        if (timer + 3 < gameTime.TotalGameTime.TotalSeconds && timer + 6 > gameTime.TotalGameTime.TotalSeconds && openingspace == false)
                        {
                            if (alpha < 1)
                            {
                                fadedelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                                if (fadedelay <= 0)
                                {
                                    fadedelay = 10;

                                    alpha += (float)0.01;
                                }

                            }
                        }
                        if (timer + 8 < gameTime.TotalGameTime.TotalSeconds && openingspace == false)
                        {
                            if (alpha > 0)
                            {
                                fadedelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                                if (fadedelay <= 0)
                                {
                                    fadedelay = 10;

                                    alpha -= (float)0.01;
                                }

                            }
                            if (Keyboard.GetState().IsKeyDown(Keys.Space) == true)
                            {
                                openingspace = true;
                                alpha = 1;
                                backgroundMusic = Content.Load<Song>("Music/Boom");
                                MediaPlayer.Play(backgroundMusic);
                                MediaPlayer.IsRepeating = false;
                                timer = (int)gameTime.TotalGameTime.TotalSeconds;
                            }
                        }
                        if (openingspace == true)
                        {
                            if (timer < gameTime.TotalGameTime.TotalSeconds && timer + 6 > gameTime.TotalGameTime.TotalSeconds)
                            {
                                if (alpha > 0)
                                {
                                    fadedelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                                    if (fadedelay <= 0)
                                    {
                                        fadedelay = 10;

                                        alpha -= (float)0.01;
                                    }

                                }
                            }
                            if (timer + 6 < gameTime.TotalGameTime.TotalSeconds && timer + 9 > gameTime.TotalGameTime.TotalSeconds)
                            {
                                if (alpha < 1)
                                {
                                    fadedelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                                    if (fadedelay <= 0)
                                    {
                                        fadedelay = 10;

                                        alpha += (float)0.01;
                                    }

                                }

                            }
                            // if (timer + 8 < (int)gameTime.TotalGameTime.TotalSeconds && timer + 10 > (int)gameTime.TotalGameTime.TotalSeconds)
                            if (timer + 9 == (int)gameTime.TotalGameTime.TotalSeconds)
                            {
                                if (transitionValue == 0)
                                {
                                    if (loadValue == 99)
                                    {
                                        backgroundMusic = Content.Load<Song>("Music/Deep Space - Captains Log");
                                        MediaPlayer.Play(backgroundMusic);
                                        loadValue = 98;
                                        MediaPlayer.IsRepeating = false;
                                    }
                                }
                            }
                            if (timer + 9 < gameTime.TotalGameTime.TotalSeconds)
                            {
                                if (alpha > 0)
                                {
                                    fadedelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                                    if (fadedelay <= 0)
                                    {
                                        fadedelay = 10;

                                        alpha -= (float)0.01;
                                    }

                                }
                                if (Keyboard.GetState().IsKeyDown(Keys.Space) == true)
                                {
                                    musicValue = 0;
                                    loadValue = 0;
                                    transitionValue = 99;
                                    LoadContent();
                                    CurrentGameState = GameState.Menu;
                                }
                            }


                        }
                    }
                    if (transitionValue == 1 || transitionValue == 2 || transitionValue == 3 || transitionValue == 4)
                    {
                        if (timer < gameTime.TotalGameTime.TotalSeconds && openingspace == false)
                        {
                            if (loadValue == 99)
                            {
                                backgroundMusic = Content.Load<Song>("Music/Boom");
                                MediaPlayer.Play(backgroundMusic);
                                MediaPlayer.IsRepeating = false;
                                loadValue = 98;
                            }
                            if (alpha > 0)
                            {
                                fadedelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                                if (fadedelay <= 0)
                                {
                                    fadedelay = 10;

                                    alpha -= (float)0.01;
                                }

                            }
                            if (Keyboard.GetState().IsKeyDown(Keys.Space) == true)
                            {
                                if (transitionValue == 1)
                                {
                                    loadValue = 1;
                                    musicValue = 1;
                                    lvl = 1;
                                    transitionValue = 99;
                                    world = "Entrance";
                                    LoadContent();
                                    CurrentGameState = GameState.Play;
                                }
                                if (transitionValue == 2)
                                {
                                    loadValue = 1;
                                    musicValue = 2;
                                    lvl = 11;
                                    transitionValue = 99;
                                    world = "Armory";
                                    LoadContent();
                                    CurrentGameState = GameState.Play;
                                }
                                if (transitionValue == 3)
                                {
                                    loadValue = 1;
                                    musicValue = 3;
                                    lvl = 21;
                                    transitionValue = 99;
                                    world = "Cell Block";
                                    LoadContent();
                                    CurrentGameState = GameState.Play;
                                }
                                if (transitionValue == 4)
                                {
                                    loadValue = 1;
                                    musicValue = 4;
                                    lvl = 31;
                                    transitionValue = 99;
                                    world = "Throne Room";
                                    LoadContent();
                                    CurrentGameState = GameState.Play;
                                }

                            }
                        }
                    }
                    if (transitionValue == 5)
                    {
                        bosspoop = bosspoop - 2;
                        bossAttack = bossAttack - 2;
                        if (bosspoop < -250)
                        {
                            switchcounter++;
                            bosspoop = 850;
                        }
                        if (bossAttack < -250)
                        {
                            bossSpeed++;
                            bossAttack = 850;
                        }
                        if (switchcounter == 5)
                        {
                            musicValue = 0;
                            loadValue = 0;
                            transitionValue = 99;
                            LoadContent();
                            CurrentGameState = GameState.Menu;
                        }
                            
                    }


                    break;
#endregion
                case GameState.Menu:
                    #region
                    dude.UpdateMovement();

                    if (wrench[0].tempVelocity == new Vector2(0, 0))
                    {
                        wrench[0] = new Sprite(Content.Load<Texture2D>("Sprites/Thewrench"), new Vector2((dude.position.X + (dude.size.X / 2)), (dude.position.Y + (dude.size.Y / 2))), new Vector2(25, 25), ScreenWidth, ScreenHeight, 0, 2, 0);

                    }
                    if (gameTime.TotalGameTime.TotalSeconds > wrench[0].Egt + 4)
                    {
                        wrench[0] = new Sprite(Content.Load<Texture2D>("Sprites/Thewrench"), new Vector2((dude.position.X + (dude.size.X / 2)), (dude.position.Y + (dude.size.Y / 2))), new Vector2(25, 25), ScreenWidth, ScreenHeight, 0, 2, 0);
                        wrench[0].tempWPosition = dude.tempWPosition;
                        wrench[0].MenuWrenchPosition(gameTime);
                    }
                    wrench[0].MenuWrenchMove();

                    if (dude.mousePosition.X > playCoor.X && dude.mousePosition.X < playCoor.X + 115 && dude.mousePosition.Y > playCoor.Y && dude.mousePosition.Y < playCoor.Y + 40 && dude.lastMouse.LeftButton == ButtonState.Pressed && dude.mousePosition.LeftButton == ButtonState.Released)
                    {
                        loadValue = 2;
                        LoadContent();
                        CurrentGameState = GameState.LevelSel;
                    }


                    break;
#endregion
                case GameState.LevelSel:
                    #region
                    dude.UpdateMovement();
                    int x;
                    int y;
                    int music_value_counter = 1;
                    int lvl_counter = 1;
                    int world_counter = 1;

                    for (y = 0; y < 500; y = y + 100)
                    {
                        for (x = 0; x < 1000; x = x + 100)
                        {
                            if (lvl_counter <= levelprogress)
                            {
                                if (dude.mousePosition.X > x + 20 && dude.mousePosition.X < x + 70 &&
                                    dude.mousePosition.Y > y + 20 && dude.mousePosition.Y < y + 70 &&
                                    dude.lastMouse.LeftButton == ButtonState.Pressed && dude.mousePosition.LeftButton == ButtonState.Released)
                                {
                                    if (lvl_counter == 1)
                                    {
                                        transitionValue = 1;
                                        loadValue = 99;
                                        MediaPlayer.Stop();
                                        LoadContent();
                                        CurrentGameState = GameState.Transition;

                                    }
                                    else if (lvl_counter == 40)
                                    {
                                        loadValue = 3;
                                        musicValue = music_value_counter;
                                        world = "Throne Room";
                                        lvl = 40;
                                        LoadContent();
                                        CurrentGameState = GameState.Boss;
                                    }
                                    else
                                    {
                                        loadValue = 1;
                                        musicValue = music_value_counter;
                                        lvl = lvl_counter;
                                        if (world_counter == 1)
                                        {
                                            world = "Entrance";
                                        }
                                        else if (world_counter == 2)
                                        {
                                            world = "Armory";
                                        }
                                        else if (world_counter == 3)
                                        {
                                            world = "Cell Block";
                                        }
                                        else if (world_counter == 4)
                                        {
                                            world = "Throne Room";
                                        }
                                        LoadContent();
                                        CurrentGameState = GameState.Play;
                                    }

                                }
                                lvl_counter++;
                            }

                        }

                        music_value_counter++;
                        world_counter++;
                    }


                    break;
#endregion
                case GameState.Play:
                    #region
                    if (Keyboard.GetState().IsKeyDown(Keys.R) == true)
                        LoadContent();

                    if (Keyboard.GetState().IsKeyDown(Keys.Q) == true)
                    {
                        loadValue = 2;
                        musicValue = 0;
                        LoadContent();
                        CurrentGameState = GameState.LevelSel;

                    }
                    dude.UpdateMovement();
                    if (dude.present != 0)
                    {
                        if (dude.lastMouse.LeftButton == ButtonState.Pressed && dude.mousePosition.LeftButton == ButtonState.Released && dude.mousePosition.X >= 0 && dude.mousePosition.Y >= 0 && dude.mousePosition.X <= dude.screenSize.X && dude.mousePosition.Y <= dude.screenSize.Y && dude.wrenchCount > 0)
                        {
                            dude.updateDude();
                            for (int i = 0; i <= dude.k; i++)
                            {
                                wrench[i].tempWPosition = dude.tempWPosition;
                                wrench[i].updateWrench(gameTime);

                            }
                        }
                    }
                    for (int j = 0; j < border.counter; j++)
                    {
                        if (border.TswitchCheck == false)
                        {
                            border.BImpassible(bullet[j]);
                            if (border.impassibleCheck == false)
                            {

                                bullet[j] = new Sprite(Content.Load<Texture2D>("Sprites/bullet"), new Vector2(border.bulletP[j].X, border.bulletP[j].Y), new Vector2(10, 10), ScreenWidth, ScreenHeight, 0, 2, 0);

                                border.impassibleCheck = true;
                            }
                            bullet[j].Collision();
                            if (bullet[j].winner == true)
                            {
                                bullet[j].winner = false;
                                bullet[j] = new Sprite(Content.Load<Texture2D>("Sprites/bullet"), new Vector2(border.bulletP[j].X, border.bulletP[j].Y), new Vector2(10, 10), ScreenWidth, ScreenHeight, 0, 2, 0);
                            }


                            if (bullet[j].present == 2)
                            {
                                border.turretAim(dude, bullet[j]);
                                bullet[j].bulletMove();
                                bullet[j].present = 1;
                            }
                            bullet[j].updatebulletMove();
                            if (border.TswitchCheck == false)
                                border.DeathBullet(dude, bullet[j]);
                            if (border.impassibleCheck == false)
                            {
                                border.impassibleCheck = true;
                                bullet[j].present = 0;
                            }


                        }
                        else if (border.TswitchCheck == true && bullet[j].present == 1)
                        {
                            border.BImpassible(bullet[j]);
                            bullet[j].updatebulletMove();
                            border.DeathBullet(dude, bullet[j]);
                            if (border.impassibleCheck == false)
                            {
                                border.impassibleCheck = true;
                                bullet[j].present = 0;
                            }

                        }
                    }



                    for (i = dude.k; i <= 1 + dude.k; i++)
                        wrench[i] = new Sprite(Content.Load<Texture2D>("Sprites/Thewrench"), new Vector2((dude.position.X + (dude.size.X / 2)),
                            (dude.position.Y + (dude.size.Y / 2))), new Vector2(25, 25), ScreenWidth, ScreenHeight, 0, 2, 0);

                    for (int i = 0; i <= dude.k; i++)
                    {
                        if (wrench[i].present == 3 && wrench[i].Egt + 1 < gameTime.TotalGameTime.TotalSeconds)
                            wrench[i].present = 1;
                        wrench[i].updateWrenchMovement();
                        // wrench[i].Collision();
                        for (int j = i + 1; j < dude.k; j++)
                        {
                            if (wrench[i].wrenchCollideB(wrench[j]) && wrench[i].present != 0 && wrench[j].present != 0)
                            {
                                float Vtemp = wrench[i].tempVelocity.Y;
                                //wrench[i].position.Y = wrench[j].position.Y - wrench[j].size.Y - 1;
                                wrench[i].tempVelocity.Y = wrench[j].tempVelocity.Y;
                                wrench[j].tempVelocity.Y = Vtemp;
                                wrench[i].present = 1;
                                wrench[j].present = 1;
                            }

                            if (wrench[i].wrenchCollideT(wrench[j]) && wrench[i].present != 0 && wrench[j].present != 0)
                            {
                                float Vtemp = wrench[i].tempVelocity.Y;
                                //wrench[i].position.Y = wrench[j].position.Y + wrench[j].size.Y + 1;
                                wrench[i].tempVelocity.Y = wrench[j].tempVelocity.Y;
                                wrench[j].tempVelocity.Y = Vtemp;
                                wrench[i].present = 1;
                                wrench[j].present = 1;
                            }
                            if (wrench[i].wrenchCollideR(wrench[j]) && wrench[i].present != 0 && wrench[j].present != 0)
                            {
                                float Vtemp = wrench[i].tempVelocity.X;
                                //wrench[i].position.X = wrench[j].position.X + wrench[j].size.X + 1;
                                wrench[i].tempVelocity.X = wrench[j].tempVelocity.X;
                                wrench[j].tempVelocity.X = Vtemp;
                                wrench[i].present = 1;
                                wrench[j].present = 1;
                            }

                            if (wrench[i].wrenchCollideL(wrench[j]) && wrench[i].present != 0 && wrench[j].present != 0)
                            {
                                float Vtemp = wrench[i].tempVelocity.X;
                                //wrench[i].position.X = wrench[j].position.X - wrench[j].size.X - 1;
                                wrench[i].tempVelocity.X = wrench[j].tempVelocity.X;
                                wrench[j].tempVelocity.X = Vtemp;
                                wrench[i].present = 1;
                                wrench[j].present = 1;
                            }
                        }
                        if (dude.present != 0)
                        {
                            if (wrench[i].position.X + wrench[i].size.X - 12.5f > dude.position.X && wrench[i].position.X - 12.5f < dude.position.X + dude.size.X && wrench[i].position.Y + wrench[i].size.Y - 12.5f > dude.position.Y && wrench[i].position.Y - 12.5f < dude.position.Y + dude.size.Y && wrench[i].present == 1)
                            {
                                dude.wrenchCount = dude.wrenchCount + 1;
                                wrench[i].present = 0;
                            }

                        }
                        if (wrench[i].present != 0)
                        {
                            border.WImpassible(wrench[i], gameTime);
                            if (border.impassibleCheck == false)
                            {
                                wrench[i].present = 1;
                                wrench[i].omega *= -1;
                                border.impassibleCheck = true;
                            }
                        }



                    }
                    dude.Collision();

                    border.Impassible(dude);
                    if (border.impassibleCheck == true)
                        border.Death(dude);
                    if (border.impassibleCheck == false)
                        border.impassibleCheck = true;

                    if (dude.winner == true && dude.present != 0)
                    {
                        if (lvl == levelprogress)
                        {
                            levelprogress++;
                            if (levelprogress > gameStart.LoadProgress)
                            {
                                gameStart.Saving(levelprogress);
                            }
                        }
                        lvl = lvl + 1;
                        if (lvl == 11)
                        {
                            transitionValue = 2;
                            loadValue = 99;
                            MediaPlayer.Stop();
                            LoadContent();
                            CurrentGameState = GameState.Transition;

                        }
                        if (lvl == 21)
                        {
                            transitionValue = 3;
                            loadValue = 99;
                            MediaPlayer.Stop();
                            LoadContent();
                            CurrentGameState = GameState.Transition;
                        }
                        if (lvl == 31)
                        {
                            transitionValue = 4;
                            loadValue = 99;
                            MediaPlayer.Stop();
                            LoadContent();
                            CurrentGameState = GameState.Transition;
                        }

                        if (lvl > 39)
                        {
                            musicValue = 0;
                            lvl = 1;
                            loadValue = 2;
                            transitionValue = 99;
                            CurrentGameState = GameState.LevelSel;

                        }
                        LoadContent();
                    }


                    base.Update(gameTime);
                    break;

#endregion
                case GameState.Boss:
                    #region
                    if (Keyboard.GetState().IsKeyDown(Keys.R) == true)
                        LoadContent();

                    if (Keyboard.GetState().IsKeyDown(Keys.Q) == true)
                    {
                        loadValue = 2;
                        musicValue = 0;
                        LoadContent();
                        CurrentGameState = GameState.LevelSel;

                    }
                    dude.UpdateMovement();
#region bossfight code
                    if (bossman.present != 0)
                    {
                        if (switchcounter < bosspoop)
                        {
                            if (bossAttack == 0)
                            {
                                bossAttack = Rnd.Next(1, 3);
                                switchcounter++;
                                switchy = false;
                                arm = false;
                            }
                            if (bossAttack == 1)
                            {
                                if (bossman.position.Y >= 35 && arm == false)
                                    bossman.position.Y = bossman.position.Y - bossSpeed;
                                else
                                {
                                    if (arm == false)
                                    {
                                        bossman.Egt += gameTime.ElapsedGameTime.TotalMilliseconds;

                                        if (bossman.Egt > 500 && bossmanimage < 2)
                                        {
                                            bossmanimage++;
                                            bossman.Egt = 0;
                                            if (bossmanimage == 2)
                                                arm = true;
                                        }
                                    }


                                    if (arm == true && switchy == false)
                                    {
                                        switchy = true;
                                        if (border.EswitchCheck == true)
                                            border.EswitchCheck = false;
                                        else border.EswitchCheck = true;

                                    }
                                    if (arm == true && switchy == true)
                                    {
                                        bossman.Egt += gameTime.ElapsedGameTime.TotalMilliseconds;

                                        if (bossman.Egt > 500 && bossmanimage >= 1)
                                        {
                                            bossmanimage--;
                                            bossman.Egt = 0;

                                        }
                                        if (bossman.position.Y <= 260)
                                        {
                                            bossman.position.Y = bossman.position.Y + bossSpeed;
                                        }
                                        else
                                        {
                                            arm = false;
                                            switchy = false;
                                            bossAttack = 0;
                                        }
                                    }
                                }
                            }

                            if (bossAttack == 2)
                            {
                                if (bossman.position.Y < 450 && arm == false)
                                    bossman.position.Y = bossman.position.Y + bossSpeed;
                                else
                                {
                                    if (arm == false)
                                    {
                                        bossman.Egt += gameTime.ElapsedGameTime.TotalMilliseconds;

                                        if (bossman.Egt > 500 && bossmanimage < 2)
                                        {
                                            bossmanimage++;
                                            bossman.Egt = 0;
                                            if (bossmanimage == 2)
                                                arm = true;
                                        }
                                    }


                                    if (arm == true && switchy == false)
                                    {

                                        if (border.TswitchCheck == true)
                                            border.TswitchCheck = false;
                                        else if (border.TswitchCheck == false)
                                            border.TswitchCheck = true;
                                        switchy = true;
                                    }
                                    if (arm == true)
                                    {
                                        bossman.Egt += gameTime.ElapsedGameTime.TotalMilliseconds;

                                        if (bossman.Egt > 500 && bossmanimage >= 1)
                                        {
                                            bossmanimage--;
                                            bossman.Egt = 0;

                                        }
                                        if (bossman.position.Y >= 260)
                                        {
                                            bossman.position.Y = bossman.position.Y - bossSpeed;
                                        }
                                        else
                                        {
                                            arm = false;
                                            switchy = false;
                                            bossAttack = 0;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (arm == false)
                            {
                                bossman.Egt += gameTime.ElapsedGameTime.TotalMilliseconds;

                                if (bossman.Egt > 500 && bossmanimage < 2)
                                {
                                    bossmanimage++;
                                    bossman.Egt = 0;
                                }
                            }


                            if (bossmanimage == 2 && switchy == false)
                            {
                                border.DswitchCheck = true;
                                switchy = true;
                            }
                            if (border.DswitchCheck == true)
                            {
                                bossman.Egt += gameTime.ElapsedGameTime.TotalMilliseconds;
                                if (bossman.Egt > 10000)
                                {
                                    border.DswitchCheck = false;
                                    arm = true;

                                }
                            }

                            if (arm == true && border.DswitchCheck == false)
                            {
                                bossman.Egt += gameTime.ElapsedGameTime.TotalMilliseconds;

                                if (bossman.Egt > 500 && bossmanimage >= 1)
                                {
                                    bossmanimage--;
                                    bossman.Egt = 0;

                                }
                                if (bossmanimage == 0)
                                {
                                    arm = false;
                                    switchy = false;
                                    bossAttack = 0;
                                    switchcounter = 0;
                                }
                            }

                        }
                    }
            






#endregion
                    if (dude.present != 0)
                    {
                        if (dude.lastMouse.LeftButton == ButtonState.Pressed && dude.mousePosition.LeftButton == ButtonState.Released && dude.mousePosition.X >=0 && dude.mousePosition.Y >= 0 && dude.mousePosition.X <= dude.screenSize.X && dude.mousePosition.Y <= dude.screenSize.Y && dude.wrenchCount > 0)
                        {
                            dude.updateDude();
                            for (int i = 0; i <= dude.k; i++)
                            {
                                wrench[i].tempWPosition = dude.tempWPosition;
                                wrench[i].updateWrench(gameTime);

                            }
                        }
                    }
                    for (int j = 0; j < border.counter; j++)
                    {
                        if (border.TswitchCheck == false)
                        {
                            border.BImpassible(bullet[j]);
                            if (border.impassibleCheck == false)
                            {

                                bullet[j] = new Sprite(Content.Load<Texture2D>("Sprites/bullet"), new Vector2(border.bulletP[j].X, border.bulletP[j].Y), new Vector2(10, 10), ScreenWidth, ScreenHeight, 0, 2, 0);

                                border.impassibleCheck = true;
                            }
                            bullet[j].Collision();
                            if (bullet[j].winner == true)
                            {
                                bullet[j].winner = false;
                                bullet[j] = new Sprite(Content.Load<Texture2D>("Sprites/bullet"), new Vector2(border.bulletP[j].X, border.bulletP[j].Y), new Vector2(10, 10), ScreenWidth, ScreenHeight, 0, 2, 0);
                            }


                            if (bullet[j].present == 2)
                            {
                                border.turretAim(dude, bullet[j]);
                                bullet[j].bulletMove();
                                bullet[j].present = 1;
                            }
                            bullet[j].updatebulletMove();
                            if (border.TswitchCheck == false)
                                if(bossman.present != 0)
                                    border.DeathBullet(dude, bullet[j]);
                            if (border.impassibleCheck == false)
                            {
                                border.impassibleCheck = true;
                                bullet[j].present = 0;
                            }


                        }
                        else if (border.TswitchCheck == true && bullet[j].present == 1)
                        {
                            
                            border.BImpassible(bullet[j]);
                            bullet[j].updatebulletMove();
                            if(bossman.present != 0)
                                border.DeathBullet(dude, bullet[j]);
                            if (border.impassibleCheck == false)
                            {
                                border.impassibleCheck = true;
                                bullet[j].present = 0;
                            }
                        }
                    }



                    for (i = dude.k; i <= 1 + dude.k; i++)
                        wrench[i] = new Sprite(Content.Load<Texture2D>("Sprites/Thewrench"), new Vector2((dude.position.X + (dude.size.X / 2)),
                            (dude.position.Y + (dude.size.Y / 2))), new Vector2(25, 25), ScreenWidth, ScreenHeight, 0, 2, 0);

                    for (int i = 0; i <= dude.k; i++)
                    {
                        if (wrench[i].present == 3 && wrench[i].Egt + 1 < gameTime.TotalGameTime.TotalSeconds)
                            wrench[i].present = 1;
                        wrench[i].updateWrenchMovement();
                        // wrench[i].Collision();
                        for (int j = i + 1; j < dude.k; j++)
                        {
                            if (wrench[i].wrenchCollideB(wrench[j]) && wrench[i].present != 0 && wrench[j].present != 0)
                            {
                                float Vtemp = wrench[i].tempVelocity.Y;
                                //wrench[i].position.Y = wrench[j].position.Y - wrench[j].size.Y - 1;
                                wrench[i].tempVelocity.Y = wrench[j].tempVelocity.Y;
                                wrench[j].tempVelocity.Y = Vtemp;
                                wrench[i].present = 1;
                                wrench[j].present = 1;
                            }

                            if (wrench[i].wrenchCollideT(wrench[j]) && wrench[i].present != 0 && wrench[j].present != 0)
                            {
                                float Vtemp = wrench[i].tempVelocity.Y;
                                //wrench[i].position.Y = wrench[j].position.Y + wrench[j].size.Y + 1;
                                wrench[i].tempVelocity.Y = wrench[j].tempVelocity.Y;
                                wrench[j].tempVelocity.Y = Vtemp;
                                wrench[i].present = 1;
                                wrench[j].present = 1;
                            }
                            if (wrench[i].wrenchCollideR(wrench[j]) && wrench[i].present != 0 && wrench[j].present != 0)
                            {
                                float Vtemp = wrench[i].tempVelocity.X;
                                //wrench[i].position.X = wrench[j].position.X + wrench[j].size.X + 1;
                                wrench[i].tempVelocity.X = wrench[j].tempVelocity.X;
                                wrench[j].tempVelocity.X = Vtemp;
                                wrench[i].present = 1;
                                wrench[j].present = 1;
                            }

                            if (wrench[i].wrenchCollideL(wrench[j]) && wrench[i].present != 0 && wrench[j].present != 0)
                            {
                                float Vtemp = wrench[i].tempVelocity.X;
                                //wrench[i].position.X = wrench[j].position.X - wrench[j].size.X - 1;
                                wrench[i].tempVelocity.X = wrench[j].tempVelocity.X;
                                wrench[j].tempVelocity.X = Vtemp;
                                wrench[i].present = 1;
                                wrench[j].present = 1;
                            }
                        }
                        if (wrench[i].position.X + wrench[i].size.X - 12.5f > bossman.position.X && wrench[i].position.X - 12.5f < bossman.position.X + bossman.size.X && wrench[i].position.Y + wrench[i].size.Y - 12.5f > bossman.position.Y && wrench[i].position.Y - 12.5f < bossman.position.Y + bossman.size.Y)
                        {
                            if (wrench[i].present == 1 || wrench[i].present == 3 || wrench[i].present == 4)
                            {
                                bossman.wrenchCount = bossman.wrenchCount - 1;
                                wrench[i].present = 0;
                                border.DswitchCheck = false;
                                bossAttack = 0;
                                switchcounter = 0;
                                arm = false;
                                bossmanimage = 0;
                                switchy = false;
                                if (bossman.wrenchCount == 2)
                                {
                                    bosspoop = 8;
                                    bossSpeed = 2;
                                }
                                if (bossman.wrenchCount == 1)
                                {
                                    bosspoop = 11;
                                    bossSpeed = 3;
                                }
                                if (bossman.wrenchCount == 0)
                                {
                                    bossman.present = 0;

                                }
                            }
                        }
                        if (dude.present != 0)
                        {
                            if (wrench[i].position.X + wrench[i].size.X - 12.5f > dude.position.X && wrench[i].position.X - 12.5f < dude.position.X + dude.size.X && wrench[i].position.Y + wrench[i].size.Y - 12.5f > dude.position.Y && wrench[i].position.Y - 12.5f < dude.position.Y + dude.size.Y && wrench[i].present == 1)
                            {
                                dude.wrenchCount = dude.wrenchCount + 1;
                                wrench[i].present = 0;

                            }

                        }
                        if (wrench[i].present != 0)
                        {
                            border.WImpassible(wrench[i], gameTime);
                            if (border.impassibleCheck == false)
                            {
                                wrench[i].present = 1;
                                wrench[i].omega *= -1;
                                border.impassibleCheck = true;
                            }
                        }



                    }

                    border.Impassible(dude);
                    if (border.impassibleCheck == true && bossman.present != 0)
                        border.Death(dude);
                    if (border.impassibleCheck == false)
                        border.impassibleCheck = true;

                    if (dude.winner == true && dude.present != 0)
                    {
                        loadValue = 99;
                        musicValue = 5;
                        transitionValue = 5;
                        LoadContent();
                        CurrentGameState = GameState.Transition;
                    }


                    base.Update(gameTime);


                    break;
                    #endregion
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (CurrentGameState)
            {

                case GameState.Transition:
                    #region
                    if (transitionValue == 0)
                    {
                        if (timer < gameTime.TotalGameTime.TotalSeconds && timer + 6 > gameTime.TotalGameTime.TotalSeconds && openingspace == false)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/SPixelGames"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.Lerp(Color.White, Color.Transparent, alpha));
                        if (timer + 8 < gameTime.TotalGameTime.TotalSeconds && openingspace == false)
                            spriteBatch.Draw(Content.Load<Texture2D>("other/GameText"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.Lerp(Color.White, Color.Transparent, alpha));
                        if (timer < gameTime.TotalGameTime.TotalSeconds && timer + 9 > gameTime.TotalGameTime.TotalSeconds && openingspace == true)
                            spriteBatch.Draw(Content.Load<Texture2D>("other/DEEP SPACE"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.Lerp(Color.White, Color.Transparent, alpha));
                        if (timer + 9 < gameTime.TotalGameTime.TotalSeconds && openingspace == true)
                            spriteBatch.Draw(Content.Load<Texture2D>("other/Captains Log"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.Lerp(Color.White, Color.Transparent, alpha));
                    }
                    if (transitionValue == 1)
                    {
                        if (timer < gameTime.TotalGameTime.TotalSeconds && openingspace == false)
                            spriteBatch.Draw(Content.Load<Texture2D>("other/Entrance"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.Lerp(Color.White, Color.Transparent, alpha));
                    }
                    if (transitionValue == 2)
                    {
                        if (timer < gameTime.TotalGameTime.TotalSeconds && openingspace == false)
                            spriteBatch.Draw(Content.Load<Texture2D>("other/Armory"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.Lerp(Color.White, Color.Transparent, alpha));
                    }
                    if (transitionValue == 3)
                    {
                        if (timer < gameTime.TotalGameTime.TotalSeconds && openingspace == false)
                            spriteBatch.Draw(Content.Load<Texture2D>("other/Cell Block"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.Lerp(Color.White, Color.Transparent, alpha));
                    }
                    if (transitionValue == 4)
                    {
                        if (timer < gameTime.TotalGameTime.TotalSeconds && openingspace == false)
                            spriteBatch.Draw(Content.Load<Texture2D>("other/Throne Room"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.Lerp(Color.White, Color.Transparent, alpha));
                    }
                    if (transitionValue == 5)
                    {
                        if (switchcounter == 0)
                        {
                            spriteBatch.DrawString(font, "GAME DESIGNERS", new Vector2(50, bosspoop), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Paul Seifert", new Vector2(50, bosspoop + 75), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Michael Kemmerer", new Vector2(50, bosspoop + 125), Color.LimeGreen);
                        }
                        if (bossSpeed == 0)
                        {
                            spriteBatch.DrawString(font, "GRAPHICAL ARTISTS", new Vector2(50, bossAttack), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Paul Seifert", new Vector2(50, bossAttack + 75), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Michael Kemmerer", new Vector2(50, bossAttack + 125), Color.LimeGreen);
                        }
                        if (switchcounter == 1)
                        {
                            spriteBatch.DrawString(font, "WRENCH DESIGN", new Vector2(50, bosspoop), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Mary Seifert", new Vector2(50, bosspoop + 75), Color.LimeGreen);
                        }
                        if (bossSpeed == 1)
                        {
                            spriteBatch.DrawString(font, "ANIMATION DESIGN", new Vector2(50, bossAttack), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Paul Seifert", new Vector2(50, bossAttack + 75), Color.LimeGreen);
                        }
                        if (switchcounter == 2)
                        {
                            spriteBatch.DrawString(font, "MUSIC BY", new Vector2(50, bosspoop), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Michael Kemmerer", new Vector2(50, bosspoop + 75), Color.LimeGreen);
                        }
                        if (bossSpeed == 2)
                        {
                            spriteBatch.DrawString(font, "PROGRAMMERS", new Vector2(50, bossAttack), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Paul Seifert", new Vector2(50, bossAttack + 125), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Michael Kemmerer", new Vector2(50, bossAttack + 75), Color.LimeGreen);
                        }
                        if (switchcounter == 3)
                        {
                            spriteBatch.DrawString(font, "GAMETESTERS", new Vector2(50, bosspoop), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Eric Christensen", new Vector2(50, bosspoop + 125), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Connor Barniskis", new Vector2(50, bosspoop + 75), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Valay Shah", new Vector2(50, bosspoop + 175), Color.LimeGreen);
                        }

                        if (switchcounter == 4)
                        {
                            spriteBatch.DrawString(font, "SOFTWARE USED", new Vector2(50, bosspoop), Color.LimeGreen);
                            spriteBatch.DrawString(font, "XNA", new Vector2(50, bosspoop + 125), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Garage Band", new Vector2(50, bosspoop + 75), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Gimp 2", new Vector2(50, bosspoop + 175), Color.LimeGreen);
                        }
                        if (bossSpeed == 3)
                        {
                            spriteBatch.DrawString(font, "SPECIAL THANKS TO", new Vector2(50, bossAttack), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Mr. Kannel for introducing us to the C# language", new Vector2(50, bossAttack + 75), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Sir Isaac Newton for giving us inspiration for the game concept", new Vector2(50, bossAttack + 125), Color.LimeGreen);
                            spriteBatch.DrawString(font, "to the people who taught us to use curse words in frustration", new Vector2(50, bossAttack + 175), Color.LimeGreen);
                            spriteBatch.DrawString(font, "Michael Rutz for designing the now extinct website", new Vector2(50, bossAttack + 225), Color.LimeGreen);
                        }
                    }
                      


                    break;
#endregion
                case GameState.Menu:
                    #region
                    wrench[0].Draw(spriteBatch);
                    //dude.Draw(spriteBatch);
                    if (dude.unitVector.X >= 0 && dude.unitVector.Y <= 0.5 && dude.unitVector.Y >= -0.5)
                        spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDude"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                    else if (dude.unitVector.X >= 0 && dude.unitVector.Y < 0.5)
                        spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDuderightup"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                    else if (dude.unitVector.X >= 0 && dude.unitVector.Y > -0.5)
                        spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDuderightdown"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                    else if (dude.unitVector.X < 0 && dude.unitVector.Y <= 0.5 && dude.unitVector.Y >= -0.5)
                        spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDudeleft"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                    else if (dude.unitVector.X < 0 && dude.unitVector.Y < 0.5)
                        spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDudeleftup"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                    else //if(dude.mousePosition.X < dude.position.X + (dude.size.X / 2) && dude.mousePosition.Y > dude.position.Y + dude.size.Y + 50)
                        spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDudeleftdown"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);

                    spriteBatch.Draw(Content.Load<Texture2D>("Menu/Deep Space Menu2"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                    if (dude.mousePosition.X > playCoor.X && dude.mousePosition.X < playCoor.X + 115 && dude.mousePosition.Y > playCoor.Y && dude.mousePosition.Y < playCoor.Y + 40)
                        spriteBatch.DrawString(Content.Load<SpriteFont>("Fonts/MenuText"), "PLAY", new Vector2(playCoor.X, playCoor.Y), Color.DarkOrange);
                    else
                        spriteBatch.DrawString(Content.Load<SpriteFont>("Fonts/MenuText"), "PLAY", new Vector2(playCoor.X, playCoor.Y), Color.Gold);
                    spriteBatch.Draw(Content.Load<Texture2D>("other/arrow"), new Rectangle((int)((dude.position.X + (dude.size.X / 2)) + dude.newposition.X), (int)((dude.position.Y + (dude.size.Y / 2)) + dude.newposition.Y), 50, 35), null, Color.White, (float)Math.Atan2(dude.tempWPosition.Y, dude.tempWPosition.X), new Vector2(25, 17.5f), SpriteEffects.None, 0f);
                    break;
#endregion
                case GameState.Play:
                    #region
                    for (int j = 0; j < border.counter; j++)
                        if (bullet[j].present == 1)
                            bullet[j].Draw(spriteBatch);


                    border.tilePosition(spriteBatch, gameTime);
                    for (int i = 0; i <= dude.k; i++)
                    {

                        if (wrench[i].present == 1)
                            wrench[i].Draw(spriteBatch);
                        if (wrench[i].present == 3)
                            wrench[i].Draw(spriteBatch);
                        if (wrench[i].present == 4)
                            wrench[i].Draw(spriteBatch);
                    }


                    if (dude.present != 0)
                    {
                        if (dude.unitVector.X >= 0 && dude.unitVector.Y <= 0.5 && dude.unitVector.Y >= -0.5)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDude"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        else if (dude.unitVector.X >= 0 && dude.unitVector.Y < 0.5)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDuderightup"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        else if (dude.unitVector.X >= 0 && dude.unitVector.Y > -0.5)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDuderightdown"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        else if (dude.unitVector.X < 0 && dude.unitVector.Y <= 0.5 && dude.unitVector.Y >= -0.5)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDudeleft"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        else if (dude.unitVector.X < 0 && dude.unitVector.Y < 0.5)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDudeleftup"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        else //if(dude.mousePosition.X < dude.position.X + (dude.size.X / 2) && dude.mousePosition.Y > dude.position.Y + dude.size.Y + 50)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDudeleftdown"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        spriteBatch.Draw(Content.Load<Texture2D>("other/arrow"), new Rectangle((int)((dude.position.X + (dude.size.X / 2)) + dude.newposition.X), (int)((dude.position.Y + (dude.size.Y / 2)) + dude.newposition.Y), 50, 35), null, Color.White, (float)Math.Atan2(dude.tempWPosition.Y, dude.tempWPosition.X), new Vector2(25, 17.5f), SpriteEffects.None, 0f);

                    }
                    if (dude.present == 0)
                    {
                        border.deathAnimation(dude, gameTime, spriteBatch, Content.Load<Texture2D>("other/ElectricDying"), Content.Load<Texture2D>("other/FireDying"));
                        spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDudeElectro"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        spriteBatch.DrawString(font, "GAME OVER", new Vector2(425, 250), Color.LimeGreen);
                        spriteBatch.DrawString(font, "PRESS (R) FOR RESTART OR (Q) FOR LEVEL SELECTION SCREEN", new Vector2(175, 350), Color.LimeGreen);
                    }
                    spriteBatch.DrawString(font, "x" + dude.wrenchCount.ToString(), new Vector2(40, 665), Color.LimeGreen);

                    spriteBatch.Draw(Content.Load<Texture2D>("Sprites/Thewrench"), new Rectangle(10, 665, 25, 25), Color.White);
                    spriteBatch.DrawString(font, world + " - " + lvl.ToString(), new Vector2(800, 665), Color.LimeGreen);
                    if (dude.tempVelocity.X >= 0)
                        spriteBatch.Draw(Content.Load<Texture2D>("Menu/levelbox"), new Rectangle(175, 660, (int)(10 * dude.tempVelocity.X), 30), Color.LimeGreen);
                    if (dude.tempVelocity.X < 0)
                        spriteBatch.Draw(Content.Load<Texture2D>("Menu/levelbox"), new Rectangle(175, 660, (int)(10 * -dude.tempVelocity.X), 30), Color.LimeGreen);
                    if (dude.tempVelocity.Y >= 0)
                        spriteBatch.Draw(Content.Load<Texture2D>("Menu/levelbox"), new Rectangle(350, 660, (int)(10 * dude.tempVelocity.Y), 30), Color.LimeGreen);
                    if (dude.tempVelocity.Y < 0)
                        spriteBatch.Draw(Content.Load<Texture2D>("Menu/levelbox"), new Rectangle(350, 660, (int)(10 * -dude.tempVelocity.Y), 30), Color.LimeGreen);
                    spriteBatch.Draw(Content.Load<Texture2D>("other/speedGageLR"), new Rectangle(175, 660, 80, 30), Color.White);
                    spriteBatch.DrawString(font, "Speed-X", new Vector2(93, 665), Color.LimeGreen);


                    spriteBatch.Draw(Content.Load<Texture2D>("other/speedGageLR"), new Rectangle(350, 660, 80, 30), Color.White);
                    spriteBatch.DrawString(font, "Speed-Y", new Vector2(268, 665), Color.LimeGreen);
                    break;
#endregion
                case GameState.LevelSel:
                    #region
                    border.tilePosition(spriteBatch, gameTime);
                    spriteBatch.DrawString(font, "1", new Vector2(35, 21), Color.Black);
                    spriteBatch.DrawString(font, "2", new Vector2(135, 21), Color.Black);
                    spriteBatch.DrawString(font, "3", new Vector2(235, 21), Color.Black);
                    spriteBatch.DrawString(font, "4", new Vector2(335, 21), Color.Black);
                    spriteBatch.DrawString(font, "5", new Vector2(435, 21), Color.Black);
                    spriteBatch.DrawString(font, "6", new Vector2(535, 21), Color.Black);
                    spriteBatch.DrawString(font, "7", new Vector2(635, 21), Color.Black);
                    spriteBatch.DrawString(font, "8", new Vector2(735, 21), Color.Black);
                    spriteBatch.DrawString(font, "9", new Vector2(835, 21), Color.Black);
                    spriteBatch.DrawString(font, "10", new Vector2(922, 21), Color.Black);
                    spriteBatch.DrawString(font, "11", new Vector2(23, 121), Color.Black);
                    spriteBatch.DrawString(font, "12", new Vector2(123, 121), Color.Black);
                    spriteBatch.DrawString(font, "13", new Vector2(223, 121), Color.Black);
                    spriteBatch.DrawString(font, "14", new Vector2(323, 121), Color.Black);
                    spriteBatch.DrawString(font, "15", new Vector2(423, 121), Color.Black);
                    spriteBatch.DrawString(font, "15", new Vector2(423, 121), Color.Black);
                    spriteBatch.DrawString(font, "16", new Vector2(523, 121), Color.Black);
                    spriteBatch.DrawString(font, "17", new Vector2(623, 121), Color.Black);
                    spriteBatch.DrawString(font, "18", new Vector2(723, 121), Color.Black);
                    spriteBatch.DrawString(font, "19", new Vector2(823, 121), Color.Black);
                    spriteBatch.DrawString(font, "20", new Vector2(923, 121), Color.Black);
                    spriteBatch.DrawString(font, "21", new Vector2(23, 221), Color.Black);
                    spriteBatch.DrawString(font, "22", new Vector2(123, 221), Color.Black);
                    spriteBatch.DrawString(font, "23", new Vector2(223, 221), Color.Black);
                    spriteBatch.DrawString(font, "24", new Vector2(323, 221), Color.Black);
                    spriteBatch.DrawString(font, "25", new Vector2(423, 221), Color.Black);
                    spriteBatch.DrawString(font, "26", new Vector2(523, 221), Color.Black);
                    spriteBatch.DrawString(font, "27", new Vector2(623, 221), Color.Black);
                    spriteBatch.DrawString(font, "28", new Vector2(723, 221), Color.Black);
                    spriteBatch.DrawString(font, "29", new Vector2(823, 221), Color.Black);
                    spriteBatch.DrawString(font, "30", new Vector2(923, 221), Color.Black);
                    spriteBatch.DrawString(font, "31", new Vector2(23, 321), Color.Black);
                    spriteBatch.DrawString(font, "32", new Vector2(123, 321), Color.Black);
                    spriteBatch.DrawString(font, "33", new Vector2(223, 321), Color.Black);
                    spriteBatch.DrawString(font, "34", new Vector2(323, 321), Color.Black);
                    spriteBatch.DrawString(font, "35", new Vector2(423, 321), Color.Black);
                    spriteBatch.DrawString(font, "36", new Vector2(523, 321), Color.Black);
                    spriteBatch.DrawString(font, "37", new Vector2(623, 321), Color.Black);
                    spriteBatch.DrawString(font, "38", new Vector2(723, 321), Color.Black);
                    spriteBatch.DrawString(font, "39", new Vector2(823, 321), Color.Black);
                    spriteBatch.Draw(Content.Load<Texture2D>("other/BossLevel"), new Rectangle(920, 320, 50, 50), Color.White);

                    int x;
                    int y;
                    int lvl_counter = 1;

                    for (y = 20; y < 500; y = y + 100)
                    {
                        for (x = 20; x < 1000; x = x + 100)
                        {
                            if (lvl_counter > levelprogress)
                            {
                                spriteBatch.Draw(Content.Load<Texture2D>("Menu/levelbox"), new Rectangle(x, y, 50, 50), Color.Black);
                            }
                            lvl_counter++;

                        }
                    }
                    break;
                #endregion
                case GameState.Boss:
                    #region
                    for (int j = 0; j < border.counter; j++)
                        if (bullet[j].present == 1)
                            bullet[j].Draw(spriteBatch);


                    border.tilePosition(spriteBatch, gameTime);
                    for (int i = 0; i <= dude.k; i++)
                    {

                        if (wrench[i].present == 1)
                            wrench[i].Draw(spriteBatch);
                        if (wrench[i].present == 3)
                            wrench[i].Draw(spriteBatch);
                        if (wrench[i].present == 4)
                            wrench[i].Draw(spriteBatch);
                    }


                    if (dude.present != 0)
                    {
                        if (dude.unitVector.X >= 0 && dude.unitVector.Y <= 0.5 && dude.unitVector.Y >= -0.5)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDude"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        else if (dude.unitVector.X >= 0 && dude.unitVector.Y < 0.5)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDuderightup"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        else if (dude.unitVector.X >= 0 && dude.unitVector.Y > -0.5)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDuderightdown"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        else if (dude.unitVector.X < 0 && dude.unitVector.Y <= 0.5 && dude.unitVector.Y >= -0.5)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDudeleft"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        else if (dude.unitVector.X < 0 && dude.unitVector.Y < 0.5)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDudeleftup"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        else //if(dude.mousePosition.X < dude.position.X + (dude.size.X / 2) && dude.mousePosition.Y > dude.position.Y + dude.size.Y + 50)
                            spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDudeleftdown"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        spriteBatch.Draw(Content.Load<Texture2D>("other/arrow"), new Rectangle((int)((dude.position.X + (dude.size.X / 2)) + dude.newposition.X), (int)((dude.position.Y + (dude.size.Y / 2)) + dude.newposition.Y), 50, 35), null, Color.White, (float)Math.Atan2(dude.tempWPosition.Y, dude.tempWPosition.X), new Vector2(25, 17.5f), SpriteEffects.None, 0f);

                    }
                    if (bossman.present != 0)
                        spriteBatch.Draw(bossman.textures, new Rectangle((int)bossman.position.X,(int)bossman.position.Y,(int)bossman.size.X,(int)bossman.size.Y), new Rectangle(150*bossmanimage,0,150,150), Color.White);
                    if (bossman.present == 0)
                        spriteBatch.Draw(Content.Load<Texture2D>("Sprites/BossManD"), new Rectangle((int)bossman.position.X, (int)bossman.position.Y, (int)bossman.size.X, (int)bossman.size.Y), Color.White);

                    if (dude.present == 0)
                    {
                        border.deathAnimation(dude, gameTime, spriteBatch, Content.Load<Texture2D>("other/ElectricDying"), Content.Load<Texture2D>("other/FireDying"));
                        spriteBatch.Draw(Content.Load<Texture2D>("Sprites/TheDudeElectro"), new Rectangle((int)dude.position.X, (int)dude.position.Y, (int)dude.size.X, (int)dude.size.Y), Color.White);
                        spriteBatch.DrawString(font, "GAME OVER", new Vector2(425, 250), Color.LimeGreen);
                        spriteBatch.DrawString(font, "PRESS (R) FOR RESTART OR (Q) FOR LEVEL SELECTION SCREEN", new Vector2(250, 350), Color.LimeGreen);
                    }
                    spriteBatch.DrawString(font, "x" + dude.wrenchCount.ToString(), new Vector2(40, 665), Color.LimeGreen);
                    spriteBatch.DrawString(font, "Quaazar's Health = " + bossman.wrenchCount.ToString(), new Vector2(500, 665), Color.Red);
                    spriteBatch.Draw(Content.Load<Texture2D>("Sprites/Thewrench"), new Rectangle(10, 665, 25, 25), Color.White);
                    spriteBatch.DrawString(font, world + " - Final", new Vector2(800, 665), Color.LimeGreen);
                    if (dude.tempVelocity.X >= 0)
                        spriteBatch.Draw(Content.Load<Texture2D>("Menu/levelbox"), new Rectangle(175, 660, (int)(10 * dude.tempVelocity.X), 30), Color.LimeGreen);
                    if (dude.tempVelocity.X < 0)
                        spriteBatch.Draw(Content.Load<Texture2D>("Menu/levelbox"), new Rectangle(175, 660, (int)(10 * -dude.tempVelocity.X), 30), Color.LimeGreen);
                    if (dude.tempVelocity.Y >= 0)
                        spriteBatch.Draw(Content.Load<Texture2D>("Menu/levelbox"), new Rectangle(350, 660, (int)(10 * dude.tempVelocity.Y), 30), Color.LimeGreen);
                    if (dude.tempVelocity.Y < 0)
                        spriteBatch.Draw(Content.Load<Texture2D>("Menu/levelbox"), new Rectangle(350, 660, (int)(10 * -dude.tempVelocity.Y), 30), Color.LimeGreen);
                    spriteBatch.Draw(Content.Load<Texture2D>("other/speedGageLR"), new Rectangle(175, 660, 80, 30), Color.White);
                    spriteBatch.DrawString(font, "Speed-X", new Vector2(93, 665), Color.LimeGreen);


                    spriteBatch.Draw(Content.Load<Texture2D>("other/speedGageLR"), new Rectangle(350, 660, 80, 30), Color.White);
                    spriteBatch.DrawString(font, "Speed-Y", new Vector2(268, 665), Color.LimeGreen);
                    if (bossman.present == 0 && dude.present != 0)
                    {

                        if (alpha > 0)
                        {
                            fadedelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (fadedelay <= 0)
                            {
                                fadedelay = 100;

                                alpha -= (float)0.01;
                                if (alpha < 0)
                                    dude.winner = true;
                            }

                        }

                        spriteBatch.Draw(Content.Load<Texture2D>("Menu/levelbox"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.Lerp(Color.Black, Color.Transparent, alpha));
                    }

                    break;
                    #endregion

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
