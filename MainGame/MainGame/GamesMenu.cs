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

        public GamesMenu(Action<int> play) : base("GamesMenu/Window", 50, 0)
        {
            this.play = play;
            levelPanel = new Object2D(this, new Vector2(44, 42), new Vector2(3, 10));
            levelList = new List<Sprite>();
            levelList.Add(new Sprite(levelPanel, "GamesMenu/Train", 20, Vector2.Zero));
            levelList.Add(new Sprite(levelPanel, "GamesMenu/Conturs", 20, Vector2.Zero));
            levelList.Add(new Sprite(levelPanel, "GamesMenu/Wizzards", 20, Vector2.Zero));
            levelList.Add(new Sprite(levelPanel, "GamesMenu/Houses", 20, Vector2.Zero));
            levelList.Add(new Sprite(levelPanel, "GamesMenu/Ballons", 20, Vector2.Zero));
        }

        public override void Update()
        {
            base.Update();
            for (int i = 0; i < levelList.Count; i++)
                if (levelList[i].UnderMouse() &&
                    EngineCore.oldMouseState.LeftButton == ButtonState.Released &&
                    EngineCore.currentMouseState.LeftButton == ButtonState.Pressed)
                    play(i);
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
                levelList[i].pos.X = 23 * (i % 2);
                levelList[i].pos.Y = 13 * (int)(i / 2) - offset;
                levelList[i].Draw();
            }
            EngineCore.spriteBatch.End();
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = ORR;
        }
    }
}
