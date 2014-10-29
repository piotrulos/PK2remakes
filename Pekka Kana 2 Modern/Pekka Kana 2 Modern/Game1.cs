using TileEngine;
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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Pekka_Kana_2_Modern
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState
        {
            MainMenu,
            Options,
            Playing,
        }

        GameState CurrentGameState = GameState.MainMenu;
        int screenWidth = 800, screebHeight = 600;

        cButton btnPlay;



        Player pekka;
        //test
        string titlefps;
        int fps = 0;
        float deltaFPSTime = 0;
        float total = 0;
        float total2 = 0;
        //lol
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
            this.graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screebHeight;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            btnPlay = new cButton(Content.Load<Texture2D>(@"Textures\button"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(300, 300));

            TileMap.Initialize(
                Content.Load<Texture2D>(@"Textures\Tiles\tiles"));
          //TileMap.SetTileAtCell(3, 3, 1, 10);
            pekka = new Player(Content);
      //    pekka.WorldLocation = new Vector2(350, 300);
            Camera.WorldRectangle = new Rectangle(0, 0, 160 * 32, 160 * 32);
            Camera.Position = Vector2.Zero;
            Camera.ViewPortWidth = 800;
            Camera.ViewPortHeight = 600;
            LevelManager.Initialize(Content, pekka);
            LevelManager.LoadLevel(0);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Playing;
                    btnPlay.Update(mouse);
                    break;

                case GameState.Playing:




            KeyboardState kState = Keyboard.GetState(); 
            if (kState.IsKeyDown(Keys.K))
            {
               // GraphicsDevice.Clear(Color.Blue);
            }
            // The time since Update was called last
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            total += elapsed;
            float elapsed2 = (float)gameTime.ElapsedGameTime.Milliseconds;
            total2 += elapsed2;

            float fps = 1 / elapsed;
            deltaFPSTime += elapsed;
            if (deltaFPSTime >= 1)

            {
             //  fps = 0;
              //  total = 0;
                
               //     deltaFPSTime -= 1;
            }
            if (total2 >= 0)
            {
    
                Window.Title = "Pekka Kana 2 Modern (0 FPS) " +
                "Cam (X=" + Camera.Position.X.ToString() + " Y=" + Camera.Position.Y.ToString()
                + ") Position= " + pekka.WorldLocation;

            }

            //fps += 1;

            // Let the GameComponents update
           
            // Allows the game to exit
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();
            ////////////////////


            // TODO: Add your game logic here


            // TODO: Add your update logic here
            pekka.Update(gameTime);
            base.Update(gameTime);
            break;
            }
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Textures\menub"), new Rectangle(0, 0, screenWidth, screebHeight), Color.White);
                    btnPlay.Draw(spriteBatch);
                    break;

                case GameState.Playing:
                    //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Textures\scenery\backtest"), new Rectangle(0, 0, screenWidth, screebHeight), Color.White);
                    pekka.Draw(spriteBatch);
                    TileMap.Draw(spriteBatch);
                    //spriteBatch.End();
                    break;
            }
            spriteBatch.End();
            


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
