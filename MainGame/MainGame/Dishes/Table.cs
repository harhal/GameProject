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
    class Table : Dishes
    {
        int set = 0;

        List<Dish> dishes;

        Effect shader;

        void ColorOn(int i)
        {
            if (!(bool)dishes[i].value)
            {
                dishes[i].pos -= dishes[i].size * 0.2f;
                dishes[i].scale *= 1.4f;
                dishes[i].value = true;
            }
        }

        void ColorOff(int i)
        {
            if ((bool)dishes[i].value)
            {
                dishes[i].pos = dishes[i].posOld;
                dishes[i].scale = dishes[i].scaleOld;
                dishes[i].value = false;
            }
        }

        public Table(int set, Action<int> exit) : base(set, exit)
        {
            this.set = set;
            Canvas.elements.Add(
                new Sprite(Canvas, "Dishes/Table/BG", 100, Vector2.Zero));
            dishes = new List<Dish>();
            dishes.Add(new Dish(Canvas, "Dishes/Table/0", 25, new Vector2(9, 11)));
            dishes.Last().SetCollisionMap("Dishes/Table/Masks/0");
            dishes.Add(new Dish(Canvas, "Dishes/Table/1", 25, new Vector2(35.5f, 13.5f)));
            dishes.Last().SetCollisionMap("Dishes/Table/Masks/1");
            dishes.Add(new Dish(Canvas, "Dishes/Table/2", 25, new Vector2(37, 9)));
            dishes.Last().SetCollisionMap("Dishes/Table/Masks/2");
            dishes.Add(new Dish(Canvas, "Dishes/Table/3", 25, new Vector2(55, 13)));
            dishes.Last().SetCollisionMap("Dishes/Table/Masks/3");
            dishes.Add(new Dish(Canvas, "Dishes/Table/4", 25, new Vector2(65, 10)));
            dishes.Last().SetCollisionMap("Dishes/Table/Masks/4");
            dishes.Add(new Dish(Canvas, "Dishes/Table/5", 22, new Vector2(20, 30)));
            dishes.Last().SetCollisionMap("Dishes/Table/Masks/5");
            dishes.Add(new Dish(Canvas, "Dishes/Table/6", 22, new Vector2(36, 31)));
            dishes.Last().SetCollisionMap("Dishes/Table/Masks/6");
            dishes.Add(new Dish(Canvas, "Dishes/Table/7", 22, new Vector2(36, 28)));
            dishes.Last().SetCollisionMap("Dishes/Table/Masks/7");
            dishes.Add(new Dish(Canvas, "Dishes/Table/8", 22, new Vector2(37, 27)));
            dishes.Last().SetCollisionMap("Dishes/Table/Masks/8");
            dishes.Add(new Dish(Canvas, "Dishes/Table/9", 23, new Vector2(53, 31)));
            dishes.Last().SetCollisionMap("Dishes/Table/Masks/9");
            dishes.Add(new Dish(Canvas, "Dishes/Table/A", 23, new Vector2(61, 30)));
            dishes.Last().SetCollisionMap("Dishes/Table/Masks/A");
            for (int i = 0; i < dishes.Count; i++)
                dishes[i].value = i < set - 13;
            /*effect =*/
            shader = EngineCore.content.Load<Effect>("Shaders/BlackWhite");
        }

        public override void Update()
        {
            base.Update();
            for (int i = 0; i < dishes.Count; i++)
                if (dishes[i].UnderMouse())
                {
                    ColorOn(i);
                    if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                        EngineCore.oldMouseState.LeftButton == ButtonState.Released)
                        if (i == set - 13)
                            AddEvent(0.5f, delegate
                            {
                                exit(++set);
                            });
                }
                else
                    ColorOff(i);
        }

        public override void Draw()
        {
            base.Draw();
            Rectangle ORR = EngineCore.graphics.GraphicsDevice.ScissorRectangle;
            RasterizerState RS = new RasterizerState();
            RS.ScissorTestEnable = true;
            int a = 0;
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS, shader);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            for (a = 0; a < dishes.Count ? !(bool)dishes[a].value : false; a++)
                dishes[a].Draw();
            EngineCore.spriteBatch.End();
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            if (a < 11)
            {
                dishes[a].Draw();
                a++;
            }
            EngineCore.spriteBatch.End();
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS, shader);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            for (; a < dishes.Count; a++)
                if (!(bool)dishes[a].value)
                    dishes[a].Draw();
            EngineCore.spriteBatch.End();
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = ORR;
        }
    }
}
