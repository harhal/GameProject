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
    class ClosesIntro : ItemsGame
    {
        int step = 0;

        List<Item> wear;
        List<Item> selection;

        Effect shader;

        void ColorOn(int i)
        {
            if (!(bool)wear[i].value)
            {
                playItem(wear[i]);
                selection[i].visible = true;
                wear[i].visible = false;
            }
        }

        void ColorOff(int i)
        {
            if (!(bool)wear[i].value)
            {
                selection[i].visible = false;
                wear[i].visible = true;
            }
        }

        void HighLight()
        {
            if (step < 12)
            {
                ColorOn(step++);
                if (step > 1)
                {
                    ColorOff(step - 2);
                }
                AddEvent(3f, HighLight);
            }
            else if (step < 13)
            {
                step++;
                ColorOff(step - 2);
                Sound introEnd = new Sound(TheGame.language + "/Closes/introEnd");
                EngineCore.PlaySound(introEnd);
                AddEvent((int)introEnd.Duration.TotalSeconds, HighLight);
            }
            else
                exit(-1);
        }

        public ClosesIntro(int set, Action<int> exit) : base(set, exit)
        {
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Closes/BGColor", 100, Vector2.Zero));
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Closes/BG", 100, Vector2.Zero));
            wear = new List<Item>();
            wear.Add(new Item(Canvas, "Closes/0", 30, new Vector2(-8, 15f)));
            wear.Add(new Item(Canvas, "Closes/1", 30, new Vector2(6.5f, 5f)));
            wear.Add(new Item(Canvas, "Closes/2", 30, new Vector2(18f, 6.5f)));
            wear.Add(new Item(Canvas, "Closes/3", 30, new Vector2(28.5f, 7.75f)));
            wear.Add(new Item(Canvas, "Closes/4", 30, new Vector2(36f, 7.75f)));
            wear.Add(new Item(Canvas, "Closes/5", 30, new Vector2(47f, 8.2f)));
            wear.Add(new Item(Canvas, "Closes/6", 20, new Vector2(65.3f, 6.3f)));
            wear.Add(new Item(Canvas, "Closes/7", 20, new Vector2(72f, 7f)));
            wear.Add(new Item(Canvas, "Closes/8", 30, new Vector2(74f, 7f)));
            wear.Add(new Item(Canvas, "Closes/9", 21, new Vector2(16f, 34f)));
            wear.Add(new Item(Canvas, "Closes/10", 21, new Vector2(63f, 27f)));
            wear.Add(new Item(Canvas, "Closes/11", 21, new Vector2(82f, 26f)));
            for (int i = 0; i < wear.Count; i++)
                wear[i].value = false;
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Closes/front", 69, new Vector2(15.5f, 9.4f)));
            Sound intro = new Sound(TheGame.language + "/Closes/Intro");
            EngineCore.PlaySound(intro);
            AddEvent((float)intro.Duration.TotalSeconds + 0.5f, delegate ()
            {
                AddEvent(0.5f, HighLight);
            });
            Canvas.active = false;
            selection = new List<Item>();
            for (int i = 0; i < wear.Count; i++)
            {
                Canvas.elements.Add(wear[i]);
                selection.Add(wear[i].Clone() as Item);
                selection[i].pos -= selection[i].size * 0.2f;
                selection[i].scale *= 1.4f;
                selection[i].visible = false;
            }
            shader = EngineCore.content.Load<Effect>("Shaders/BlackWhite");
        }

        public override Scene GetReloadedScene()
        {
            return new ClosesIntro(set, exit);
        }

        public override void Update()
        {
            base.Update();
            if (EngineCore.currentKeyboardState.IsKeyDown(Keys.Escape))
                exit(-1);
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
            for (int i = 0; i < wear.Count; i++)
                if ((bool)wear[i].value)
                    wear[i].Draw();
            for (int i = 0; i < selection.Count; i++)
                selection[i].Draw();
            EngineCore.spriteBatch.End();
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = ORR;
        }
    }
}
