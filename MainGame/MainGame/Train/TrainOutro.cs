using System;
using Microsoft.Xna.Framework;
using UnicornEngine;

namespace MainGame
{
    public class TrainOutro : Scene
    {
        Object2D train;

        public float timer = 0;
        public float maxTime = 5;

        public TrainOutro(Object2D train) : base(null)
        {
            Canvas.elements.Add(new Sprite(Canvas, "Train/BG", 100, Vector2.Zero));
            this.train = train;
            train.parent = Canvas;
            Canvas.elements.Add(train);
        }

        public override void Update()
        {
            base.Update();
            if (timer < maxTime)
            {
                timer += EngineCore.gameTime.ElapsedGameTime.Milliseconds / 1000f;
                train.pos = new Vector2(-120, 25) + Vector2.UnitX * (float)(Math.Sin(Math.PI / 2 * timer / maxTime + Math.PI / 2)) * 120;
            }
        }
    }
}
