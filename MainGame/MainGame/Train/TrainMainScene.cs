using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;
using UnicornEngine;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;

namespace MainGame

{
    public class TrainMainScene : Scene
    {
        public class Word : Sprite
        {
            public int number;
            public int indexPos;
            public Sound sound;
            Object2D[] cars;

            public Word(Object2D WorkSpace, Object2D[] cars, int set, int number, string name) :
                base(WorkSpace, TheGame.language + "/Train/Words/Set" + set.ToString() + "/" + number.ToString(), 14, Vector2.Zero)
            {
                this.name = name;
                this.number = number;
                this.cars = cars;
                this.sound = new Sound(TheGame.language + "/Train/Set" + set.ToString() + "/" + number.ToString());
            }

            public void Move(int pos)
            {
                indexPos = pos;
                if (number != 0)
                {
                    this.pos = new Vector2(-6.7f + 19 * pos, 40.5f);
                    parent.elements.Add(this);
                }
                else
                {
                    this.pos = (cars[0].size - size) / 2;//new Vector2(21f, -3);
                    parent.elements.Add(this);
                }
            }
        }

        TrainIntro intro;
        TrainOutro outro;
        Scene scene;
        
        public Object2D train;
        Object2D[] cars;
        List<Word> wordSet;
        string currentStr = "";
        float errorTimer = 0;
        int set;
        int coutRight = 0;
        public bool right = false;
        Word lastWord;
        


        public int countSit = 0;

        public TrainMainScene(int set, Action<int> exit) : base(exit)
        {
            this.set = set;
            train = new Object2D(Canvas, new Vector2(100, 16f), new Vector2(0, 25f));
            train.elements.Add(new Sprite(train, "Common/Train/TrainBG", 100, new Vector2(0, 0)));
            cars = new Object2D[5];
            for (int i = 0; i < 5; i++)
            {
                cars[i] = new Object2D(train, new Vector2(14, 14), new Vector2(21f + 16 * i, -3));
                train.elements.Add(cars[i]);
            }
            wordSet = new List<Word>();
            if (TheGame.language == "rus")
                #region rus
                switch (set)
                {
                    default:        //0
                        wordSet.Add(new Word(cars[0], cars, set, 0, "карандаш"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "шкаф"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "флажок"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "кот"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "топор"));
                        break;
                    case (1):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "ракета"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "автобус"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "сачок"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "ключ"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "чайник"));
                        break;
                    case (2):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "кашка"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "ананас"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "сом"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "мак"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "крокодил"));
                        break;
                    case (3):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "лук"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "краски"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "иголка"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "астра"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "арбуз"));
                        break;
                    case (4):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "зонт"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "телефон"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "носорог"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "гриб"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "бегемот"));
                        break;
                    case (5):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "телевизор"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "ранец"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "цыплёнок"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "кормушка"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "автобус"));
                        break;
                    case (6):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "альбом"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "мёд"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "диван"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "нож"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "жук"));
                        break;
                    case (7):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "комар"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "рак"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "кактус"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "сноп"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "помидор"));
                        break;
                    case (8):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "рябина"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "аквариум"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "муравьи"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "иволга"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "апельсин"));
                        break;
                    case (9):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "носки"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "индюк"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "колос"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "стол"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "ландыш"));
                        break;
                }
            #endregion
            else
                #region bel
                switch (set)
                {
                    default:        //0
                        wordSet.Add(new Word(cars[0], cars, set, 0, "аловак"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "канапа"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "ананас"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "сабака"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "аўтобус"));
                        break;
                    case (1):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "ракета"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "аўтобус"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "сачок"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "ключ"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "чайнік"));
                        break;
                    case (2):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "каша"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "ананас"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "сом"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "мак"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "кракадзіл"));
                        break;
                    case (3):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "цыбуля"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "яблык"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "кактус"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "сачок"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "камар"));
                        break;
                    case (4):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "парасон"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "насарог"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "грыб"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "бегемот"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "тэлевізар"));
                        break;
                    case (5):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "канапа"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "ананас"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "сом"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "мурашкi"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "івалга"));
                        break;
                    case (6):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "рабіна"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "апельсін"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "нож"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "жук"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "камар"));
                        break;
                    case (7):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "шчупак"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "кактус"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "сноп"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "памідор"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "рабина"));
                        break;
                    case (8):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "рабіна"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "акварыўм"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "мурашкі"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "івалга"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "апельсін"));
                        break;
                    case (9):
                        wordSet.Add(new Word(cars[0], cars, set, 0, "гальштук"));
                        wordSet.Add(new Word(Canvas, cars, set, 1, "камар"));
                        wordSet.Add(new Word(Canvas, cars, set, 2, "рабіна"));
                        wordSet.Add(new Word(Canvas, cars, set, 3, "аўтобус"));
                        wordSet.Add(new Word(Canvas, cars, set, 4, "сачок"));
                        break;
                }
            #endregion
            intro = new TrainIntro();
            scene = intro;

            Canvas.elements.Add(new Sprite(Canvas, "Common/Train/BG", 100, Vector2.Zero));
            for (int i = 0; i < 4; i++)
                Canvas.elements.Add(new Sprite(Canvas, "Common/Train/WordPanel", 15, new Vector2(11.5f + 19 * i, 41)));
            Canvas.elements.Add(train);
            train.elements.Add(new Sprite(train, "Common/Train/Train", 100, new Vector2(0, 0)));

            wordSet.Sort(delegate (Word a, Word b)
            {
                if (a == null ? false : a.number == 0)
                    return -1;
                if (b == null ? false : b.number == 0)
                    return 1;
                return TheGame.rand.Next(2) * 2 - 1;
            });
            for (int i = 0; i < 5; i++)
                wordSet[i].Move(i);
            Canvas.elements.Add(new Button(Canvas, TheGame.language + "/Train/Check", 20, new Vector2(2, 2), new int[] { 1, 1, 1 }, "Check", Check));
            Reset();
            if (set == 0)
            {
                TheGame.task = new Sound(TheGame.language + "/Train/Task");
                Canvas.active = false;
                EngineCore.PlaySound(TheGame.task);
                AddEvent((float)TheGame.task.Duration.TotalSeconds - 3, delegate
                {
                    Canvas.active = true;
                });
            }
        }

        private void Check()
        {
            if (coutRight == 4) right = true;
            else
            {
                TheGame.PlayWrong();
                Reset();
            }
        }

        public override Scene GetReloadedScene()
        {
            return new TrainMainScene(set, exit);
        }

        private void Reset()
        {
            coutRight = 0;
            CleareEffects();
            CleareEvents();
            for (int i = 1; i < 5; i++)
            {
                if (cars[i].elements.Count == 1)
                    cars[i].elements.Clear();
            }
            wordSet[0].Move(0);
            for (int i = 1; i < 5; i++)
            {
                wordSet[i].visible = true;
                wordSet[i].active = true;
            }
            currentStr = wordSet[0].name;
            countSit = 0;
        }

        public override void Update()
        {
            if (outro != null ? outro.timer >= outro.maxTime : false)
            {
                exit(set);
            }
            else if (right)
            {
                if (outro == null)
                {
                    AddEvent(1, delegate ()
                    {
                        outro = new TrainOutro(train, cars);
                    });
                }
                scene = outro;
            }
            else if (intro.timer >= intro.maxTime)
                scene = null;
            if (scene != null)
                scene.Update();
            else if (!Canvas.active)
                base.Update();
            else
            {
                base.Update();
                if (EngineCore.currentKeyboardState.IsKeyDown(Keys.R)) Reset();
                if (errorTimer > 0)
                {
                    errorTimer -= EngineCore.gameTime.ElapsedGameTime.Milliseconds / 1000f;
                }
                else if (errorTimer < 0)
                {
                    errorTimer = 0;
                    bool broken = true;
                    for (int i = 0; i < 4; i++)
                    {
                        if (wordSet[i].visible && currentStr[currentStr.Length - 1] == wordSet[i].name[0]) broken = false;
                        wordSet[i].color = Color.White;
                    }
                    if (broken) Reset();
                }
                if (Draggable == null)
                {
                    if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                        EngineCore.oldMouseState.LeftButton == ButtonState.Released && errorTimer == 0)
                    {
                        for (int i = 1; i < 5; i++)
                            if (wordSet[i] != null ? wordSet[i].UnderMouse() : false)
                            {
                                Object2D curCar = (Object2D)wordSet[i].Clone();
                                wordSet[i].visible = false;
                                wordSet[i].active = false;
                                Vector2 start = wordSet[i].pos + wordSet[i].size / 2;
                                Vector2 end = cars[countSit + 1].pos + cars[countSit + 1].size / 2;
                                EffectSprite effect = new EffectSprite(Canvas, wordSet[i].texture, 1, wordSet[i].scale, start, 1);
                                int n = i;
                                effect.move = delegate (float time, float delay)
                                {
                                    return Vector2.Transform(((end - start) / 75),
                                           Matrix.CreateRotationZ((float)Math.Cos(time / effect.lifeTime * Math.PI) * (n > 2 ? -1 : 1)));
                                };
                                AddEffect(effect);
                                int b = countSit + 1;
                                AddEvent(1, delegate
                                {
                                    curCar.pos = (cars[b].size - curCar.size) / 2;
                                    curCar.parent = cars[b];
                                    cars[b].elements.Add(curCar);
                                    if (currentStr[currentStr.Length - 1] == curCar.name[0]) coutRight++;
                                    currentStr += curCar.name;
                                });
                                countSit++;
                                return;
                            }
                        if (train.UnderMouse())
                        {
                            Reset();
                        }
                    }
                    foreach (Object2D word in Canvas.elements)
                        if (word is Word ? word.UnderMouse() && lastWord != word : false)
                        {
                            lastWord = word as Word;
                            EngineCore.PlaySound((word as Word).sound);
                        }
                    for (int i = 0; i < 5; i++)
                        if (cars[i].elements.Count == 1)
                            if (cars[i].elements[0].UnderMouse() && lastWord != cars[i].elements[0])
                            {
                                lastWord = cars[i].elements[0] as Word;
                                EngineCore.PlaySound((cars[i].elements[0] as Word).sound);
                            }
                }
            }
        }

        public override void Draw()
        {
            if (scene != null)
            {
                scene.effect = effect;
                scene.Draw();
            }
            else
                base.Draw();
        }
    }
}