using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnicornEngine;

namespace MainGame
{
    class Wizzards : Scene
    {
        int set = 0;
        List<string> wordSet;
        string firstWord;
        int trueWord;
        List<Sprite> wordsPic;
        Sprite truePic;
        Sprite mask;
        float errorTimer = 0;
        public bool win = false;

        public Wizzards(int set, Action<int> exit) : base(exit)
        {
            this.set = set;
            wordSet = new List<string>();
            int maskPos = 0;
            switch (set)
            {
                default:        //0
                    firstWord = "Сом";
                    wordSet.Add("Кот");
                    wordSet.Add("Дом");
                    wordSet.Add("Торт");
                    wordSet.Add("Мак");
                    trueWord = 2;
                    maskPos = 0;
                    break;
                case (1):
                    firstWord = "Дом";                                                 	 
                    wordSet.Add("Ком");
                    wordSet.Add("Торт");
                    wordSet.Add("Мак");
                    wordSet.Add("Дуб");
                    trueWord = 1;
                    maskPos = 0;
                    break;
                case (2):
                    firstWord = "Торт";                                                 	
                    wordSet.Add("Корт");
                    wordSet.Add("Ком");
                    wordSet.Add("Мак");
                    wordSet.Add("Дуб");
                    trueWord = 1;
                    maskPos = 0;
                    break;
                case (3):
                    firstWord = "Каска";                                               	
                    wordSet.Add("Маска");
                    wordSet.Add("Корт");
                    wordSet.Add("Торт");
                    wordSet.Add("Мак");
                    trueWord = 1;
                    maskPos = 0;
                    break;
                case (4):
                    firstWord = "Мак";                                                 	
                    wordSet.Add("Рак");
                    wordSet.Add("Торт");
                    wordSet.Add("Шар");
                    wordSet.Add("Кот");
                    trueWord = 1;
                    maskPos = 0;
                    break;
                case (5):
                    firstWord = "Зуб";                                                 	
                    wordSet.Add("Дуб");
                    wordSet.Add("Мак");
                    wordSet.Add("Кот");
                    wordSet.Add("Рак");
                    trueWord = 1;
                    maskPos = 0;
                    break;
            }
            List<int> words = (new int[]{ 1, 2, 3, 4 }).ToList<int>();
            Random rand = new Random();
            words.Sort(delegate (int a, int b) { return rand.Next(2) * 2 - 1; });
            wordsPic = new List<Sprite>();
            Canvas.elements.Add(new Sprite(Canvas, "Wizzard/BackGround", 100, Vector2.Zero));
            for (int i = 0; i < 4; i++)
            {
                wordsPic.Add(new Sprite(Canvas, "Wizzard/Set" + set.ToString() + "/" + words[i].ToString(), 16, new Vector2(13 + i * 19.3f, 37)));
                wordsPic[i].value = words[i];
                Canvas.elements.Add(wordsPic[i]);
            }
            Canvas.elements.Add(new Sprite(Canvas, "Wizzard/Set" + set.ToString() + "/0", 20, new Vector2(24, 6.5f)));
            truePic = new Sprite(Canvas, "Wizzard/Set" + set.ToString() + "/" + trueWord.ToString(), 20, new Vector2(55, 6.5f));
            truePic.visible = false;
            Canvas.elements.Add(truePic);
            Sprite sprite;
            Canvas.elements.Add(sprite = new Sprite(Canvas, "Wizzard/Set" + set.ToString() + "/Subscribes/0", 1, new Vector2(0, 28)));
            sprite.scale = 7 / sprite.size.Y;
            sprite.pos.X = 30 - sprite.size.X / 2;
            Canvas.elements.Add(sprite = new Sprite(Canvas, "Wizzard/Set" + set.ToString() + "/Subscribes/1", 1, new Vector2(0, 28)));
            sprite.scale = 7 / sprite.size.Y;
            sprite.pos.X = 70 - sprite.size.X / 2;
            Canvas.elements.Add(mask = new Sprite(Canvas, "Wizzard/Mask", 7.5f, new Vector2(70 - sprite.size.X / 2 + maskPos * 7.5f, 28)));
        }

        public override void Update()
        {
            base.Update();
            if (errorTimer > 0)
            {
                errorTimer -= EngineCore.gameTime.ElapsedGameTime.Milliseconds / 1000f;
            }
            else if (errorTimer < 0)
            {
                errorTimer = 0;
                mask.color = Color.White;
            }
            foreach (Sprite word in wordsPic)
            {
                if (word.UnderMouse() &&
                    EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                    EngineCore.oldMouseState.LeftButton == ButtonState.Released && errorTimer <= 0)
                    if ((int)word.value == trueWord)
                    {
                        truePic.visible = true;
                        mask.visible = false;
                        AddEffect(new EffectSprite(Canvas, "Wizzard/Mask", 1, 1, Vector2.Zero, 1));
                        AddEvent(1, delegate
                        {
                            win = true;
                        });
                    }
                    else
                    {
                        mask.color = Color.Red;
                        errorTimer = 0.5f;
                    }
            }
            if (win) exit(set);
        }
    }
}
