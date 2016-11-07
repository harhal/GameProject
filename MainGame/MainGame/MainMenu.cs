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
        Object2D closingArea;
        Action close;

        public MainMenu(Action play, Action settings, Action authors, Action close, Action exit) :base ("MainMenu/Panel", 100, 0)
        {
            Button button;
            elements.Add(button = new Button(this, "MainMenu/Button", 40, new Vector2(5, 5), new int[] { 1, 1, 2 }, "Play", play));
            elements.Add(new Sprite(button, "MainMenu/Play", 20, new Vector2(10, 2)));
            elements.Add(button = new Button(this, "MainMenu/Button", 40, new Vector2(5, 18), new int[] { 1, 1, 2 }, "Settings", settings));
            elements.Add(new Sprite(button, "MainMenu/Settings", 30, new Vector2(5, 1)));
            elements.Add(button = new Button(this, "MainMenu/Button", 40, new Vector2(5, 31), new int[] { 1, 1, 2 }, "Authors", authors));
            elements.Add(new Sprite(button, "MainMenu/Authors", 15, new Vector2(12, 2)));
            elements.Add(button = new Button(this, "MainMenu/Button", 40, new Vector2(5, 44), new int[] { 1, 1, 2 }, "Exit", exit));
            elements.Add(new Sprite(button, "MainMenu/Exit", 15, new Vector2(12, 2)));
            this.close = close;
            closingArea = new Object2D(this, new Vector2(50, 50), new Vector2(50, 0));
        }

        public override void Update()
        {
            base.Update();
            if (EngineCore.currentMouseState.LeftButton == ButtonState.Released &&
                EngineCore.oldMouseState.LeftButton == ButtonState.Pressed &&
                closingArea.UnderMouse())
                close();
        }
    }
}
