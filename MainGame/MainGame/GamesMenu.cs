using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnicornEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MainGame
{
    class GamesMenu:Menu
    {
        List<Sprite> levelList;
        float offset = 0;
        Action<int> play;
        Object2D levelPanel;
        Object2D scrollUp;
        Object2D scrollDown;
        Object2D lang2Area;

        public GamesMenu(Action<int> play) : base(TheGame.language + "/GamesMenu/Window", 77, 0)
        {
            this.play = play;
            levelPanel = new Object2D(this, new Vector2(63, 42), new Vector2(7, 10));
            scrollUp = new Object2D(this, new Vector2(63, 10), new Vector2(7, 0));
            scrollDown = new Object2D(this, new Vector2(63, 10), new Vector2(7, 52));
            levelList = new List<Sprite>();
            levelList.Add(new Sprite(levelPanel, "Common/GamesMenu/Train", 30, Vector2.Zero));
            levelList.Add(new Sprite(levelPanel, "Common/GamesMenu/Conturs", 30, Vector2.Zero));
            levelList.Add(new Sprite(levelPanel, "Common/GamesMenu/Wizzards", 30, Vector2.Zero));
            levelList.Add(new Sprite(levelPanel, "Common/GamesMenu/Dishes", 30, Vector2.Zero));
            levelList.Add(new Sprite(levelPanel, "Common/GamesMenu/Ballons", 30, Vector2.Zero));
            levelList.Add(new Sprite(levelPanel, "Common/GamesMenu/Houses", 30, Vector2.Zero));
            levelList.Add(new Sprite(levelPanel, "Common/GamesMenu/Closes", 30, Vector2.Zero));
            lang2Area = new Object2D(null, new Vector2(5, 5), new Vector2(91, 1.8f));
        }

        public override Menu GetReloadedMenu()
        {
            return new GamesMenu(play);
        }

        public override void Update()
        {
            base.Update();
            offset += (EngineCore.oldMouseState.ScrollWheelValue - EngineCore.currentMouseState.ScrollWheelValue) / 10;
            if (EngineCore.oldMouseState.ScrollWheelValue - EngineCore.currentMouseState.ScrollWheelValue == 0)
            {
                if (scrollUp.UnderMouse()) offset -= 2;
                if (scrollDown.UnderMouse()) offset += 2;
            }
            if (offset < 0) offset = 0;
            if (20 * (int)((levelList.Count + 1) / 2) - offset < levelPanel.size.Y) offset = 20 * (int)((levelList.Count + 1) / 2) - levelPanel.size.Y;
            for (int i = 0; i < levelList.Count; i++)
                if (levelList[i].UnderMouse() &&
                    EngineCore.oldMouseState.LeftButton == ButtonState.Released &&
                    EngineCore.currentMouseState.LeftButton == ButtonState.Pressed)
                    play(i);
            if (EngineCore.currentMouseState.LeftButton == ButtonState.Released &&
                EngineCore.oldMouseState.LeftButton == ButtonState.Pressed && !this.UnderMouse() && !lang2Area.UnderMouse())
                play(-1);
        }

        public override void Draw()
        {
            base.Draw();
            Rectangle ORR = EngineCore.graphics.GraphicsDevice.ScissorRectangle;
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = levelPanel.posRect;
            RasterizerState RS = new RasterizerState();
            RS.ScissorTestEnable = true;
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS);
            for (int i = 0; i < levelList.Count; i++)
            {
                levelList[i].pos.X = 33 * (i % 2);
                levelList[i].pos.Y = 20 * (int)(i / 2) - offset;
                levelList[i].Draw();
            }
            EngineCore.spriteBatch.End();
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = ORR;
        }
    }
}
