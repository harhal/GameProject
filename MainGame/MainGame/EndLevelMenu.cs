using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UnicornEngine;

namespace MainGame
{
    public class EndLevelMenu:Menu
    {
        public EndLevelMenu(Action exit, Action retry, Action nextLevel) : base("Common/EndLevelMenu/Window", 40)
        {
            EngineCore.PlaySound(new Sound(TheGame.language + "/Great" + TheGame.rand.Next(4).ToString()));
            elements.Add(new Sprite(this, TheGame.language + "/EndLevelMenu/Congratulation", 30, new Vector2(5, 15)));
            elements.Add(new Button(this, "Common/EndLevelMenu/Exit", 9, new Vector2(5, 28), new int[] { 1, 1, 1 }, "Exit", exit));
            elements.Add(new Button(this, "Common/EndLevelMenu/Retry", 9, new Vector2(16, 28), new int[] { 1, 1, 1 }, "Retry", retry));
            elements.Add(new Button(this, "Common/EndLevelMenu/NextLevel", 9, new Vector2(27, 28), new int[] { 1, 1, 1 }, "NextLevel", nextLevel));
        }

        public override Menu GetReloadedMenu()
        {
            return new EndLevelMenu((elements[elements.Count - 3] as Button).OnPress, 
                                    (elements[elements.Count - 2] as Button).OnPress, 
                                    (elements[elements.Count - 1] as Button).OnPress);
        }
    }
}
