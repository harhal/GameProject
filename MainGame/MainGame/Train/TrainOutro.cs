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
        int currCar = 0;
        Object2D[] cars;
        Sprite panel;
        TrainMainScene.Word word;

        void ShowCar()
        {
            if (currCar < 5)
            {
                word = cars[currCar].elements[0].Clone() as TrainMainScene.Word;
                word.parent = panel;
                word.scale = 25;
                word.pos = (panel.size - word.size) / 2;
                panel.elements.Clear();
                panel.elements.Add(word);
                EngineCore.PlaySound(word.sound);
                currCar++;
                AddEvent(2, ShowCar);
            }
            else
                panel.visible = false;
        }

        public TrainOutro(Object2D train, Object2D[] cars) : base(null)
        {
            this.cars = cars;
            Canvas.elements.Add(new Sprite(Canvas, "Common/Train/BG", 100, Vector2.Zero));
            this.train = train;
            train.parent = Canvas;
            Canvas.elements.Add(train);
            Canvas.elements.Add(panel = new Sprite(Canvas, "Common/Train/WordPanel", 30, new Vector2(35, 20)));
            ShowCar();
        }

        public override Scene GetReloadedScene()
        {
            return new TrainOutro(train, cars);
        }

        public override void Update()
        {
            base.Update();
            if (currCar >= 5 && timer < maxTime)
            {
                timer += EngineCore.gameTime.ElapsedGameTime.Milliseconds / 1000f;
                train.pos = new Vector2(-120, 25) + Vector2.UnitX * (float)(Math.Sin(Math.PI / 2 * timer / maxTime + Math.PI / 2)) * 120;
            }
        }
    }
}
