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
    class DishesIntro : ItemsGame
    {
        Sprite Kitchen;
        Sprite Table;
        Button KitchenButton;
        Button TableButton;
        Effect shader;

        public DishesIntro(int set, Action<int> exit) : base(set, exit)
        {
            Canvas.active = false;
            AddEvent(0.2f, delegate { Canvas.active = true; });
            shader = EngineCore.content.Load<Effect>("Shaders/BlackWhite");
            Canvas.elements.Add(
                new Sprite(Canvas, "Common/Dishes/Intro/BG", 100, Vector2.Zero));
            Kitchen = new Sprite(Canvas, "Common/Dishes/Intro/Kitchen", 76, new Vector2(0, -5));
            Kitchen.value = true;
            Table = new Sprite(Canvas, "Common/Dishes/Intro/Table", 53, new Vector2(44, 29));
            Table.value = true;
            Canvas.elements.Add(
            KitchenButton = new Button(Canvas, "Common/Dishes/Intro/KitchenButton", 39, new Vector2(27, 16), new int[] { 1, 1, 1 }, "", delegate
            {
                exit(1);
            }));
            Canvas.elements.Add(
            TableButton = new Button(Canvas, "Common/Dishes/Intro/TableButton", 39, new Vector2(52, 36), new int[] { 1, 1, 1 }, "", delegate
            {
                exit(12);
            }));
        }

        public override void Update()
        {
            base.Update();
            if (KitchenButton.UnderMouse())
            {
                effect = shader;
                Table.value = false;
                Kitchen.value = true;
                Table.scale = 53;
                Table.pos = new Vector2(44, 29);
                Kitchen.scale = 84;
                Kitchen.pos = new Vector2(-4, -9);
            }
            else if (TableButton.UnderMouse())
            {
                effect = shader;
                Table.value = true;
                Kitchen.value = false;
                Table.scale = 57;
                Table.pos = new Vector2(42, 27);
                Kitchen.scale = 76;
                Kitchen.pos = new Vector2(0, -5);
            }
            else
            {
                effect = null;
                Table.value = true;
                Kitchen.value = true;
                Table.scale = 53;
                Table.pos = new Vector2(44, 29);
                Kitchen.scale = 76;
                Kitchen.pos = new Vector2(0, -5);
            }
        }

        public override Scene GetReloadedScene()
        {
            return new DishesIntro(set, exit);
        }

        public override void Draw()
        {
            base.Draw();
            Rectangle ORR = EngineCore.graphics.GraphicsDevice.ScissorRectangle;
            RasterizerState RS = new RasterizerState();
            RS.ScissorTestEnable = true;
            if (!(bool)Kitchen.value)
            {
                EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS, effect);
                EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
                Kitchen.Draw();
                EngineCore.spriteBatch.End();
            }
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            if ((bool)Kitchen.value)
                Kitchen.Draw();
            if ((bool)Table.value)
                Table.Draw();
            EngineCore.spriteBatch.End();
            if (!(bool)Table.value)
            {
                EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS, effect);
                EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
                Table.Draw();
                EngineCore.spriteBatch.End();
            }
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            KitchenButton.Draw();
            TableButton.Draw();
            EngineCore.spriteBatch.End();
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = ORR;
        }
    }
}
