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
    class Authors:Menu
    {
        Action exit;
        Object2D lang2Area;

        public Authors(Action exit) : base(TheGame.language + "/AuthorsMenu/Window", 77, 0)
        {
            this.exit = exit;
            lang2Area = new Object2D(null, new Vector2(5, 5), new Vector2(91, 1.8f));
        }

        public override Menu GetReloadedMenu()
        {
            return new Authors(exit);
        }

        public override void Update()
        {
            base.Update();
            if (EngineCore.currentMouseState.LeftButton == ButtonState.Released &&
                EngineCore.oldMouseState.LeftButton == ButtonState.Pressed && !lang2Area.UnderMouse())
                exit();
        }
    }
}
