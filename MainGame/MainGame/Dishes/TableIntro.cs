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
    class TableIntro : Dishes
    {
        int step = 0;

        List<Dish> dishes;

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

        void HighLight()
        {
            if (step < 11)
            {
                ColorOn(step++);
                if (step > 1)
                {
                    ColorOff(step - 2);
                }
                AddEvent(3f, HighLight);
            }
            else
                exit(13);
        }

        public TableIntro(int set, Action<int> exit) : base(set, exit)
        {
            Canvas.elements.Add(
                new Sprite(Canvas, "Dishes/Table/BG", 100, Vector2.Zero));
            dishes = new List<Dish>();
            dishes.Add(new Dish(Canvas, "Dishes/Table/0", 25, new Vector2(9, 11)));
            dishes.Add(new Dish(Canvas, "Dishes/Table/1", 25, new Vector2(35.5f, 13.5f)));
            dishes.Add(new Dish(Canvas, "Dishes/Table/2", 25, new Vector2(37, 9)));
            dishes.Add(new Dish(Canvas, "Dishes/Table/3", 25, new Vector2(55, 13)));
            dishes.Add(new Dish(Canvas, "Dishes/Table/4", 25, new Vector2(65, 10)));
            dishes.Add(new Dish(Canvas, "Dishes/Table/5", 22, new Vector2(20, 30)));
            dishes.Add(new Dish(Canvas, "Dishes/Table/6", 22, new Vector2(36, 31)));
            dishes.Add(new Dish(Canvas, "Dishes/Table/7", 22, new Vector2(36, 28)));
            dishes.Add(new Dish(Canvas, "Dishes/Table/8", 22, new Vector2(37, 27)));
            dishes.Add(new Dish(Canvas, "Dishes/Table/9", 23, new Vector2(53, 31)));
            dishes.Add(new Dish(Canvas, "Dishes/Table/A", 23, new Vector2(61, 30)));
            for (int i = 0; i < dishes.Count; i++)
                dishes[i].value = false;
            AddEvent(0.5f, HighLight);
            Canvas.active = false;
            effect = EngineCore.content.Load<Effect>("Shaders/BlackWhite");
        }

        public override void Draw()
        {
            base.Draw();
            Rectangle ORR = EngineCore.graphics.GraphicsDevice.ScissorRectangle;
            RasterizerState RS = new RasterizerState();
            RS.ScissorTestEnable = true;
            int a = 0;
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS, effect);
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
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS, effect);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            for (; a < dishes.Count; a++)
                if (!(bool)dishes[a].value)
                    dishes[a].Draw();
            EngineCore.spriteBatch.End();
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = ORR;
        }
    }
}
