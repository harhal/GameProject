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
    class Table : ItemsGame
    {
        //int set = 0;

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

        public Table(int set, Action<int> exit) : base(set, exit)
        {
            this.set = set;
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Dishes/Table/BG", 100, Vector2.Zero));
            dishes = new List<Item>();
            dishes.Add(new Item(Canvas, "Dishes/Table/0", 25, new Vector2(9, 11)));
            dishes.Last().SetCollisionMap("Common/Dishes/Table/Masks/0");
            dishes.Add(new Item(Canvas, "Dishes/Table/1", 25, new Vector2(35.5f, 13.5f)));
            dishes.Last().SetCollisionMap("Common/Dishes/Table/Masks/1");
            dishes.Add(new Item(Canvas, "Dishes/Table/2", 25, new Vector2(37, 9)));
            dishes.Last().SetCollisionMap("Common/Dishes/Table/Masks/2");
            dishes.Add(new Item(Canvas, "Dishes/Table/3", 25, new Vector2(55, 13)));
            dishes.Last().SetCollisionMap("Common/Dishes/Table/Masks/3");
            dishes.Add(new Item(Canvas, "Dishes/Table/4", 25, new Vector2(65, 10)));
            dishes.Last().SetCollisionMap("Common/Dishes/Table/Masks/4");
            dishes.Add(new Item(Canvas, "Dishes/Table/5", 22, new Vector2(20, 30)));
            dishes.Last().SetCollisionMap("Common/Dishes/Table/Masks/5");
            dishes.Add(new Item(Canvas, "Dishes/Table/6", 22, new Vector2(36, 31)));
            dishes.Last().SetCollisionMap("Common/Dishes/Table/Masks/6");
            dishes.Add(new Item(Canvas, "Dishes/Table/7", 22, new Vector2(36, 28)));
            dishes.Last().SetCollisionMap("Common/Dishes/Table/Masks/7");
            dishes.Add(new Item(Canvas, "Dishes/Table/8", 22, new Vector2(37, 27)));
            dishes.Last().SetCollisionMap("Common/Dishes/Table/Masks/8");
            dishes.Add(new Item(Canvas, "Dishes/Table/9", 23, new Vector2(53, 31)));
            dishes.Last().SetCollisionMap("Common/Dishes/Table/Masks/9");
            dishes.Add(new Item(Canvas, "Dishes/Table/A", 23, new Vector2(61, 30)));
            dishes.Last().SetCollisionMap("Common/Dishes/Table/Masks/10");
            for (int i = 0; i < dishes.Count; i++)
                dishes[i].value = i < set - 13;
            selection = new List<Item>();
            for (int i = 0; i < dishes.Count; i++)
            {
                Canvas.elements.Add(dishes[i]);
                selection.Add(dishes[i].Clone() as Item);
                selection[i].pos -= selection[i].size * 0.2f;
                selection[i].scale *= 1.4f;
                selection[i].visible = false;
            }
            Sound intro = new Sound(TheGame.language + "/Dishes/Table/Task/" + (set - 13).ToString());
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
            return new TableIntro(set, exit);
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
                            if (i == set - 13)
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
