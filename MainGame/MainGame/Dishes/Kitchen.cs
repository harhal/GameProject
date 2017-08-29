using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnicornEngine;

namespace MainGame
{
    class Kitchen : ItemsGame
    {
        List<Item> dishes;
        List<Item> selection;

        Effect shader;

        void ColorOn(int i)
        {
            selection[i].visible = true;
            dishes[i].visible = false;
        }

        void ColorOff(int i)
        {
            selection[i].visible = false;
            dishes[i].visible = true;
        }

        public Kitchen(int set, Action<int> exit) : base(set, exit)
        {
            this.set = set;
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Dishes/Kitchen/BG", 100, Vector2.Zero));
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Dishes/Kitchen/Panel", 68, new Vector2(27, 0)));
            dishes = new List<Item>();
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/0", 21, new Vector2(-3.5f, 21)));
            dishes.Last().SetCollisionMap("Common/Dishes/Kitchen/Masks/0");
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/1", 19, new Vector2(18, 13)));
            dishes.Last().SetCollisionMap("Common/Dishes/Kitchen/Masks/1");
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/2", 23, new Vector2(35, -3)));
            dishes.Last().SetCollisionMap("Common/Dishes/Kitchen/Masks/2");
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/3", 23, new Vector2(40, -1.5f)));
            dishes.Last().SetCollisionMap("Common/Dishes/Kitchen/Masks/3");
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/4", 23, new Vector2(48, -0.5f)));
            dishes.Last().SetCollisionMap("Common/Dishes/Kitchen/Masks/4");
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/5", 21, new Vector2(58, 19)));
            dishes.Last().SetCollisionMap("Common/Dishes/Kitchen/Masks/5");
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/6", 15, new Vector2(86, 16)));
            dishes.Last().SetCollisionMap("Common/Dishes/Kitchen/Masks/6");
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/7", 21, new Vector2(82, 27)));
            dishes.Last().SetCollisionMap("Common/Dishes/Kitchen/Masks/7");
            for (int i = 0; i < dishes.Count; i++)
                dishes[i].value = i < set - 2;
            selection = new List<Item>();
            for (int i = 0; i < dishes.Count; i++)
            {
                Canvas.elements.Add(dishes[i]);
                selection.Add(dishes[i].Clone() as Item);
                selection[i].pos -= selection[i].size * 0.2f;
                selection[i].scale *= 1.4f;
                selection[i].visible = false;
            }
            Sound intro = new Sound(TheGame.language + "/Dishes/Kitchen/Task/" + (set - 2).ToString());
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
            return new KitchenIntro(set, exit);
        }

        public override void Update()
        {
            base.Update();
            if (Canvas.active)
            {
                for (int i = 0; i < dishes.Count; i++)
                    if (dishes[i].UnderMouse())
                    {
                        ColorOn(i);
                        if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                            EngineCore.oldMouseState.LeftButton == ButtonState.Released)
                        {
                            playItem(dishes[i]);
                            if (i == set - 2)
                            {
                                Canvas.active = false;
                                shader = null;
                                AddEvent(2f, delegate
                                {
                                    exit(++set);
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
