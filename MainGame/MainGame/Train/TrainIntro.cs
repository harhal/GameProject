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
            Canvas.elements.Add(new Sprite(Canvas, "Common/Train/BG", 100, Vector2.Zero));
            train = new Sprite(Canvas, "Common/Train/TrainBG", 100, new Vector2(120, 25));
            train.elements.Add(new Sprite(train, "Common/Train/Train", 100, new Vector2(0, 0)));
            Canvas.elements.Add(train);
        }

        public override Scene GetReloadedScene()
        {
            return new TrainIntro();
        }

        public override void Update()
        {
            base.Update();
            timer += EngineCore.gameTime.ElapsedGameTime.Milliseconds / 1000f;
            train.pos = new Vector2(120, 25) + Vector2.UnitX * (float)(Math.Cos(Math.PI * timer / maxTime / 2 + Math.PI / 2)) * 120;
        }
    }
}
