using System;
using Microsoft.Xna.Framework;
using UnicornEngine;

namespace MainGame
{
    public class TrainIntro : Scene
    {
        Sprite train;

        public float timer = 0;
        public float maxTime = 3f;

        public TrainIntro() :base(null)
        {
            Canvas.elements.Add(new Sprite(Canvas, "Train/BG", 100, Vector2.Zero));
            train = new Sprite(Canvas, "Train/TrainBG", 100, new Vector2(120, 25));
            train.elements.Add(new Sprite(train, "Train/Train", 100, new Vector2(0, 0)));
            /*for (int i = 0; i < 5; i++)
            {
                Sprite Lamp = new Sprite(train, "Train/Lamp", 5, new Vector2(24.6f + 16.2f * i, 6));
                Lamp.color = Color.Red;
                train.elements.Add(Lamp);
            }*/
            Canvas.elements.Add(train);
        }

        public override void Update()
        {
            base.Update();
            timer += EngineCore.gameTime.ElapsedGameTime.Milliseconds / 1000f;
            train.pos = new Vector2(120, 25) + Vector2.UnitX * (float)(Math.Cos(Math.PI * timer / maxTime / 2 + Math.PI / 2)) * 120;
        }
    }
}
