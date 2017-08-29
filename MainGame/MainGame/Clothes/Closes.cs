using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnicornEngine;

namespace MainGame
{
    class Closes : ItemsGame
    {
        List<Item> wear;
        List<Item> selection;

        Effect shader;

        void ColorOn(int i)
        {
                selection[i].visible = true;
                wear[i].visible = false;
        }

        void ColorOff(int i)
        {
                selection[i].visible = false;
                wear[i].visible = true;
        }

        public Closes(int set, Action<int> exit) : base(set, exit)
        {
            this.set = set;
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Closes/BGColor", 100, Vector2.Zero));
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Closes/BG", 100, Vector2.Zero));
            wear = new List<Item>();
            wear.Add(new Item(Canvas, "Closes/0", 30, new Vector2(-8, 15f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/0");
            wear.Add(new Item(Canvas, "Closes/1", 30, new Vector2(6.5f, 5f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/1");
            wear.Add(new Item(Canvas, "Closes/2", 30, new Vector2(18f, 6.5f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/2");
            wear.Add(new Item(Canvas, "Closes/3", 30, new Vector2(28.5f, 7.75f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/3");
            wear.Add(new Item(Canvas, "Closes/4", 30, new Vector2(36f, 7.75f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/4");
            wear.Add(new Item(Canvas, "Closes/5", 30, new Vector2(47f, 8.2f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/5");
            wear.Add(new Item(Canvas, "Closes/6", 20, new Vector2(65.3f, 6.3f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/6");
            wear.Add(new Item(Canvas, "Closes/7", 20, new Vector2(72f, 7f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/7");
            wear.Add(new Item(Canvas, "Closes/8", 30, new Vector2(74f, 7f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/8");
            wear.Add(new Item(Canvas, "Closes/9", 21, new Vector2(16f, 34f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/9");
            wear.Add(new Item(Canvas, "Closes/10", 21, new Vector2(63f, 27f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/10");
            wear.Add(new Item(Canvas, "Closes/11", 21, new Vector2(82f, 26f)));
            wear.Last().SetCollisionMap("Common/Closes/Masks/11");
            for (int i = 0; i < wear.Count; i++)
                wear[i].value = i < set - 2;
            selection = new List<Item>();
            for (int i = 0; i < wear.Count; i++)
            {
                Canvas.elements.Add(wear[i]);
                selection.Add(wear[i].Clone() as Item);
                selection[i].pos -= selection[i].size * 0.2f;
                selection[i].scale *= 1.4f;
                selection[i].visible = false;
            }
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Closes/front", 69, new Vector2(15.5f, 9.4f)));
            /*effect =*/
            Sound intro = new Sound(TheGame.language + "/Closes/Task/" + (set).ToString());
            EngineCore.PlaySound(intro);
            Canvas.active = false;
            AddEvent((float)intro.Duration.TotalSeconds, delegate ()
            {
                shader = EngineCore.content.Load<Effect>("Shaders/BlackWhite");
                Canvas.active = true;
            });
        }

        public override Scene GetReloadedScene()
        {
            return new ClosesIntro(set, exit);
        }

        public override void Update()
        {
            base.Update();
            if (Canvas.active)
            {
                for (int i = 0; i < wear.Count; i++)
                    if (wear[i].UnderMouse())
                    {
                        ColorOn(i);
                        if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                            EngineCore.oldMouseState.LeftButton == ButtonState.Released)
                        {
                            playItem(wear[i]);
                            if (i == set)
                            {
                                Canvas.active = false;
                                shader = null;
                                AddEvent(2f, delegate
                                {
                                    exit(set);
                                });
                            }
                        }
                    }
                    else
                        ColorOff(i);
            }
        }

        public override void Draw()
        {
            effect = shader;
            base.Draw();
            Rectangle ORR = EngineCore.graphics.GraphicsDevice.ScissorRectangle;
            RasterizerState RS = new RasterizerState();
            RS.ScissorTestEnable = true;
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            for (int i = 0; i < selection.Count; i++)
                selection[i].Draw();
            EngineCore.spriteBatch.End();
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = ORR;
        }
    }
}
