using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using static System.Formats.Asn1.AsnWriter;
using System.Threading;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Reflection;

namespace MostroDiBiscottiMonoGame
{
    public class Game1 : Game
    {
        public Game1()
        {
            Global._graphics = new GraphicsDeviceManager(this);
            Global._graphics.IsFullScreen = true;
            int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int div = 1;
            Global._graphics.PreferredBackBufferWidth = w / div;
            Global._graphics.PreferredBackBufferHeight = h / div;
            Global._graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Global.GraphicsDevice = GraphicsDevice;
            Global.Verlets = new VElement(2 * GraphicsDevice.Viewport.Width, 
                GraphicsDevice.Viewport.Height);
            Global.cameraMono = new CameraMono(new Vec2(0, 0));
            Global.mousePosition = new Vec2(0,0);
            Global.worldMousePosition = new Vec2(0, 0);
            Levels levels = new Levels();
            Global.map2 = new Map2(new Size(2 * GraphicsDevice.Viewport.Width, 
                GraphicsDevice.Viewport.Height),
                ref Global.caramelo, ref Global.Verlets, ref Global.monstruo, Global.currentLevel);

            Global.StartButton = new Button(new Rectangle((int)(GraphicsDevice.Viewport.Width / 2) - 200,
                (int)(2 * GraphicsDevice.Viewport.Height / 3) - 100, 400, 200));

            Global.start = false;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Global._spriteBatch = new SpriteBatch(GraphicsDevice);
            Global.galleta = Content.Load<Texture2D>("pixil-frame-0 (9)");
            Global.background = Content.Load<Texture2D>("Piso");
            Global.bubbleTexture = Content.Load<Texture2D>("pixil-frame-0 (16)");
            Global.monstruoTexture = Content.Load<Texture2D>("pixil-frame-0 (10)");
            Global.monstruoTextureAbre = Content.Load<Texture2D>("pixil-frame-0 (11)");
            Global.L1 = Content.Load<Texture2D>("Fondo1");
            Global.L2 = Content.Load<Texture2D>("Estufas");
            Global.pin = Content.Load<Texture2D>("pixil-frame-0 (19)");
            Global.avionL = Content.Load<Texture2D>("Galleta reposo izq");
            Global.avionLactive = Content.Load<Texture2D>("Galleta en avion izq");
            Global.avionR = Content.Load<Texture2D>("Avion reposo");
            Global.avionRactive = Content.Load<Texture2D>("Galleta en avion");
            Global.estrella = Content.Load<Texture2D>("pixil-frame-0 (12)");
            Global.puntajetex = Content.Load<Texture2D>("Puntaje");
            Global.unotex = Content.Load<Texture2D>("Uno (1)");
            Global.dostex = Content.Load<Texture2D>("Dos");
            Global.trestex = Content.Load<Texture2D>("tres");
            Global.startButtonTexture = Content.Load<Texture2D>("pixil-frame-0 (20)");
            Global.titleTexture = Content.Load<Texture2D>("New Project (1)");
            Global.font = Content.Load<SpriteFont>("MyFont");
            Global.pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            Global.pixelTexture.SetData(new[] { Color.White });
            Global.spriteCaramelo = new Sprite(Global.galleta, new Rectangle(0,0,40, 40));
            Global.spriteEstrella = new Sprite(Global.estrella, new Rectangle(0, 0, 40, 40));
            Global.spriteMegaEstrella = new Sprite(Global.estrella, new Rectangle(0, 0, 200, 200));
            Global.spritePin = new Sprite(Global.pin, new Rectangle(0, 0, 40, 40));
            Global.spriteBubble = new Sprite(Global.bubbleTexture, new Rectangle(0, 0, 60, 60));
            Global.spriteLBubble = new Sprite(Global.avionL, new Rectangle(0, 0, 60, 60));
            Global.spriteRBubble = new Sprite(Global.avionR, new Rectangle(0, 0, 60, 60));
            Global.spriteLBubbleA = new Sprite(Global.avionLactive, new Rectangle(0, 0, 60, 60));
            Global.spriteRBubbleA = new Sprite(Global.avionRactive, new Rectangle(0, 0, 60, 60));
            Global.StartButton.setSprite(new Sprite(Global.startButtonTexture, Global.StartButton.hitbox));
            Global.titleSprite = new Sprite(Global.titleTexture, new Rectangle((int)(GraphicsDevice.Viewport.Width / 2) - 400,
                (int)(GraphicsDevice.Viewport.Height / 3) - 200, 800, 400));
            SoundManager.song = Content.Load<Song>("SONATINABISCOTTO");
            SoundManager.soundEffect = Content.Load<SoundEffect>("WAVCuerdaRota");
            SoundManager.CuerdaRota = SoundManager.soundEffect.CreateInstance();
            SoundManager.CuerdaRota.Volume = 0.5f;
            SoundManager.soundeEffectEstrella = Content.Load<SoundEffect>("WAVEstrella");
            SoundManager.SonidoEstrella = SoundManager.soundeEffectEstrella.CreateInstance();
            SoundManager.soundeEffectBubble= Content.Load<SoundEffect>("WAVBubble");
            SoundManager.SonidoBubble = SoundManager.soundeEffectBubble.CreateInstance();
            SoundManager.soundeEffectAvion = Content.Load<SoundEffect>("WAVAvion");
            SoundManager.SonidoAvion = SoundManager.soundeEffectAvion.CreateInstance();
            SoundManager.yomiMonstruo = Content.Load<SoundEffect>("WAVYomi");
            SoundManager.yomiMonstruoinst = SoundManager.yomiMonstruo.CreateInstance();
            SoundManager.yomiMonstruoinst.Volume = 0.5f;
            SoundManager.intro = Content.Load<SoundEffect>("WAVIntro");
            SoundManager.introInst = SoundManager.intro.CreateInstance();
            SoundManager.introInst.Volume = 0.5f;
            SoundManager.PlaySong();
        }

        protected void Reset() {
            Global.Verlets.Clear();
            Global.map2 = new Map2(new Size(2 * GraphicsDevice.Viewport.Width, 
                GraphicsDevice.Viewport.Height),
                ref Global.caramelo, ref Global.Verlets, ref Global.monstruo,Global.currentLevel);
            Global.won = false;
            Global.lost = false;
            Global.puntaje = 0;
            Global.delayTime = 0;
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            Global.MouseState = mouseState;
            Global.mousePosition.X = mouseState.X;
            Global.mousePosition.Y = mouseState.Y;
            Global.worldMousePosition.X = Global.mousePosition.X + Global.cameraMono.Position.X;
            Global.worldMousePosition.Y = Global.mousePosition.Y;
            if (Global.start)
            {
                if (Global.won)
                {
                    Global.passingLevel = true;
                    Global.lost = false;
                    Global.delayTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (Global.delayTime >= 4.0f)
                    {
                        Global.currentLevel++;
                        if (Levels.list.Count == Global.currentLevel)
                        {
                            Global.currentLevel = 0;
                            Reset();
                            Global.start = false;
                            Global.passingLevel = false;
                        }
                        else
                        {
                            Reset();
                            Global.passingLevel = false;
                        }
                    }
                }
                if (!Global.passingLevel)
                {
                    if (Global.lost) Reset();
                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                        Reset();
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();
                    Global.Verlets.Update();
                    Global.cameraMono.Follow(Global.caramelo.punto.pos, GraphicsDevice.Viewport.Width,
                        GraphicsDevice.Viewport.Height);
                    Global.cameraMono.ClampToArea(2 * GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height,
                        GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        BubbleManager.ExplotarBurbuja();
                        RopeManager.Cut();
                    }
                }
            }
            else
            {
                Global.Verlets.Update();
                Global.cameraMono.Follow(Global.caramelo.punto.pos, GraphicsDevice.Viewport.Width,
                    GraphicsDevice.Viewport.Height);
                Global.cameraMono.ClampToArea(2 * GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height,
                    GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (Global.MouseState.LeftButton == ButtonState.Pressed)
                {
                    if (Global.mousePosition.X >= Global.StartButton.hitbox.X && Global.mousePosition.X <= (Global.StartButton.hitbox.X +
                        Global.StartButton.hitbox.Width)
                        && Global.mousePosition.Y >= Global.StartButton.hitbox.Y && Global.mousePosition.Y <= (Global.StartButton.hitbox.Y +
                        Global.StartButton.hitbox.Height))
                    {
                       Global.start = true;
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);
            if (Global.start) 
            {
                Global._spriteBatch.Begin();
                //PARALLAX
                Global._spriteBatch.Draw(Global.L1, new Rectangle(-(int)Global.cameraMono.Position.X / 20,
                    0, 2 * GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                Global._spriteBatch.Draw(Global.L2, new Rectangle(-(int)Global.cameraMono.Position.X / 10,
                    0, 2 * GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                Global._spriteBatch.Draw(Global.background, new Rectangle(-(int)Global.cameraMono.Position.X,
                    0, 2 * GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                //PUNTAJES
                Global._spriteBatch.Draw(Global.puntajetex, new Rectangle(GraphicsDevice.Viewport.Width / 50,
                    GraphicsDevice.Viewport.Height / 30,
                    GraphicsDevice.Viewport.Width / 5, GraphicsDevice.Viewport.Height / 10), Color.White);
                if (Global.puntaje == 1) Global._spriteBatch.Draw(Global.unotex,
                    new Rectangle((int)(GraphicsDevice.Viewport.Width / 4.5f),
                    GraphicsDevice.Viewport.Height / 30,
                    GraphicsDevice.Viewport.Width / 40, GraphicsDevice.Viewport.Height / 11), Color.White);
                if (Global.puntaje == 2) Global._spriteBatch.Draw(Global.dostex,
                    new Rectangle((int)(GraphicsDevice.Viewport.Width / 4.5f),
                    GraphicsDevice.Viewport.Height / 30,
                    GraphicsDevice.Viewport.Width / 40, GraphicsDevice.Viewport.Height / 11), Color.White);

                Global.Verlets.Render(Global._spriteBatch);

                if (Global.won)
                {
                    Global.felicidades = Content.Load<Texture2D>("Felicidades-Nivel");
                    Global._spriteBatch.Draw(Global.felicidades, new Rectangle(GraphicsDevice.Viewport.Width / 4,
                        GraphicsDevice.Viewport.Height / 3,
                        GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 5), Color.White);
                }
                Global._spriteBatch.End();

            }else 
            {
                Title.DrawSreenTitle(Global._spriteBatch);
            }

            base.Draw(gameTime);
        }

    }

}
