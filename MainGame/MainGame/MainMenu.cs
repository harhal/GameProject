using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using UnicornEngine;

namespace MainGame
{
    public class MainMenu : Menu
    {
        Object2D windowArea;
        Object2D lang2Area;
        Action close;

        public MainMenu(Action play, Action settings, Action authors, Action close, Action exit) :base ("Common/MainMenu/Panel", 100, 0)
        {
            Button button;
            elements.Add(button = new Button(this, "Common/MainMenu/Button", 40, new Vector2(5, 5), new int[] { 1, 1, 2 }, "Play", play));
            elements.Add(new Sprite(button, TheGame.language + "/MainMenu/Play", 20, new Vector2(10, 2)));
            elements.Add(button = new Button(this, "Common/MainMenu/Button", 40, new Vector2(5, 18), new int[] { 1, 1, 2 }, "Settings", settings));
            elements.Add(new Sprite(button, TheGame.language + "/MainMenu/Settings", 30, new Vector2(5, 1)));
            elements.Add(button = new Button(this, "Common/MainMenu/Button", 40, new Vector2(5, 31), new int[] { 1, 1, 2 }, "Authors", authors));
            elements.Add(new Sprite(button, TheGame.language + "/MainMenu/Authors", 20, new Vector2(10, 2)));
            elements.Add(button = new Button(this, "Common/MainMenu/Button", 40, new Vector2(5, 44), new int[] { 1, 1, 2 }, "Exit", exit));
            elements.Add(new Sprite(button, TheGame.language + "/MainMenu/Exit", 15, new Vector2(12, 2)));
            this.close = close;
            windowArea = new Object2D(this, new Vector2(50, 50), Vector2.Zero);
            lang2Area = new Object2D(null, new Vector2(5, 5), new Vector2(91, 1.8f));
        }

        public override void Update()
        {
            base.Update();
            if (EngineCore.currentMouseState.LeftButton == ButtonState.Released &&
                EngineCore.oldMouseState.LeftButton == ButtonState.Pressed &&
                !windowArea.UnderMouse() && !lang2Area.UnderMouse())
                close();
        }

        public override Menu GetReloadedMenu()
        {
            return new MainMenu((elements[elements.Count - 8] as Button).OnPress,
                                (elements[elements.Count - 6] as Button).OnPress, 
                                (elements[elements.Count - 4] as Button).OnPress, close, 
                                (elements[elements.Count - 2] as Button).OnPress);
        }
    }
}
