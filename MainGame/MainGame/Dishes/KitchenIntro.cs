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
    class KitchenIntro : ItemsGame
    {
        int step = 0;

        List<Item> dishes;

        Effect shader;

        void ColorOn(int i)
        {
            if (!(bool)dishes[i].value)
            {
                playItem(dishes[i]);
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
            if (step < 8)
            {
                ColorOn(step++);
                if (step > 1)
                {
                    ColorOff(step - 2);
                }
                AddEvent(3f, HighLight);
            }
            else if (step < 9)
            {
                step++;
                ColorOff(step - 2);
                Sound introEnd = new Sound(TheGame.language + "/Dishes/introEnd");
                EngineCore.PlaySound(introEnd);
                AddEvent((int)introEnd.Duration.TotalSeconds, HighLight);
            }
            else
                exit(2);
        }

        public KitchenIntro(int set, Action<int> exit) : base(set, exit)
        {
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Dishes/Kitchen/BG", 100, Vector2.Zero));
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Dishes/Kitchen/Panel", 68, new Vector2(27, 0)));
            dishes = new List<Item>();
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/0", 21, new Vector2(-3.5f, 21)));
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/1", 19, new Vector2(18, 13)));
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/2", 23, new Vector2(35, -3)));
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/3", 23, new Vector2(40, -1.5f)));
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/4", 23, new Vector2(48, -0.5f)));
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/5", 21, new Vector2(58, 19)));
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/6", 15, new Vector2(86, 16)));
            dishes.Add(new Item(Canvas, "Dishes/Kitchen/7", 21, new Vector2(82, 27)));
            for (int i = 0; i < dishes.Count; i++)
                dishes[i].value = false;
            Sound intro = new Sound(TheGame.language + "/Dishes/Kitchen/Intro");
            EngineCore.PlaySound(intro);
            AddEvent((float)intro.Duration.TotalSeconds + 0.5f, delegate()
            {
                AddEvent(0.5f, HighLight);
            });
            Canvas.active = false;
            effect = shader = EngineCore.content.Load<Effect>("Shaders/BlackWhite");
        }

        public override Scene GetReloadedScene()
        {
            return new KitchenIntro(set, exit);
        }

        public override void Update()
        {
            base.Update();
            if (EngineCore.currentKeyboardState.IsKeyDown(Keys.Escape))
                exit(2);
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
            for (int i = 0; i < dishes.Count; i++)
                if ((bool)dishes[i].value)
                    dishes[i].Draw();
            EngineCore.spriteBatch.End();
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS, effect);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            for (int i = 0; i < dishes.Count; i++)
                if (!(bool)dishes[i].value)
                    dishes[i].Draw();
            EngineCore.spriteBatch.End();
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = ORR;
        }
    }
}
