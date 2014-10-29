using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using TileEngine;

namespace Pekka_Kana_2_Modern
{
    public class Player : GameObject
    {
        private Vector2 fallSpeed = new Vector2(0, 20);
        private float moveScale = 180f;
        private bool dead = false;

        public bool Dead
        {
            get { return dead; }
        }

        #region Konstruktor
        public Player(ContentManager content)
        {
            animations.Add("idle",
                new AnimationStrip(content.Load<Texture2D>(@"Textures\Sprites\Pekka\Idle"), 48, "idle"));
            animations["idle"].LoopAnimation = true;

            animations.Add("run",
                new AnimationStrip(content.Load<Texture2D>(@"Textures\Sprites\Pekka\Run"), 48, "run"));
            animations["run"].LoopAnimation = true;

            animations.Add("jump",
                new AnimationStrip(content.Load<Texture2D>(@"Textures\Sprites\Pekka\Jump"), 48, "jump"));
            animations["jump"].LoopAnimation = false;
            animations["jump"].FrameLength = 0.08f;
            animations["jump"].NextAnimation = "idle";

            animations.Add("die",
                new AnimationStrip(content.Load<Texture2D>(@"Textures\Sprites\Pekka\Die"),48,"die"));
            animations["die"].LoopAnimation= false;

            frameWidth = 48;
            frameHeight = 59;
            CollisionRectangle = new Rectangle(9,0,30,46);
            drawDepth = 0.825f;
            enabled = true;
            codeBasedBlocks = false;
            PlayAnimation("idle");

        }
        #endregion

        #region Public Meth
        public override void Update(GameTime gameTime)
        {
            if (!Dead)
            {
                string newAnimation = "idle";
                velocity = new Vector2(0, velocity.Y);
                GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
                KeyboardState keyState = Keyboard.GetState();

                if (keyState.IsKeyDown(Keys.Left) || (gamePad.ThumbSticks.Left.X < -0.3f))
                {
                    flipped = false;
                    newAnimation = "run";
                    velocity = new Vector2(-moveScale, velocity.Y);
                }
                if (keyState.IsKeyDown(Keys.Right) || (gamePad.ThumbSticks.Left.X > 0.3f))
                {
                    flipped = true;
                    newAnimation = "run";
                    velocity = new Vector2(moveScale, velocity.Y);
                }
 
                if (keyState.IsKeyDown(Keys.Space) || (gamePad.Buttons.A == ButtonState.Pressed))
                {
                    if (onGround)
                    {
                        Jump();
                        newAnimation = "jump";
                    }
                }
                if (currentAnimation == "jump")
                    newAnimation = "jump";
               if (newAnimation != currentAnimation)
                {
                    PlayAnimation(newAnimation);
                }                
            }
            velocity += fallSpeed;
            repositionCamera();
            base.Update(gameTime);
        }
        public void Jump()
        {
            velocity.Y = -500;
        }
        #endregion

        #region Helper Meth
        private void repositionCamera()
        {
            int screenLocX = (int)Camera.WorldToScreen(worldLocation).X;
            if (screenLocX > 500)
            {
                Camera.Move(new Vector2(screenLocX - 500, 0));
            }
            if (screenLocX < 200)
            {
                Camera.Move(new Vector2(screenLocX - 200, 0));
            }

        }
        #endregion


    }
}
