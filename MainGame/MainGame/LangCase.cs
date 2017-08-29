using Microsoft.Xna.Framework;
using System;
using UnicornEngine;

namespace MainGame
{
    class LangCase : Scene
    {
        Sprite sun;

        public override Scene GetReloadedScene()
        {
            return this;
        }

        public LangCase(int set, Action<int> exit) : base(exit)
        {
            Canvas.elements.Add(new Sprite(Canvas, "Common/MainMenu/BGColor", 100, Vector2.Zero));
            Canvas.elements.Add(sun = new Sprite(Canvas, "Common/MainMenu/Sun", 200, Vector2.One * -50));
            Canvas.elements.Add(new Sprite(Canvas, "Common/MainMenu/BackGround", 100, Vector2.Zero));
            Canvas.elements.Add(new Sprite(Canvas, "Common/LangCase/Subscribes", 100, Vector2.Zero));
            Canvas.elements.Add(new Button(Canvas, "Common/LangCase/ButtonRus", 30, new Vector2(10, 20), new int[] { 1, 1, 1 }, "rus", 
                delegate 
                {
                    exit.Invoke(0);
                }));
            Canvas.elements.Add(new Button(Canvas, "Common/LangCase/ButtonBel", 30, new Vector2(60, 20), new int[] { 1, 1, 1 }, "bel",
                delegate
                {
                    exit.Invoke(1);
                }));
        }

        public override void Draw()
        {
            sun.rotation += 0.1f;
            Vector2 offset = Vector2.One * sun.scale / 2;
            offset = Vector2.Transform(offset, Matrix.CreateRotationZ((float)Math.PI * (sun.rotation - 180) / 180));
            sun.pos = new Vector2(70, 18) + offset;
            base.Draw();
        }
    }
}
