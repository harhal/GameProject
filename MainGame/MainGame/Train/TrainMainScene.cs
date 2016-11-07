using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;
using UnicornEngine;
using System.Collections.Generic;
using System.Linq;

namespace MainGame

{
    public class TrainMainScene : Scene
    {
        TrainIntro intro;
        TrainOutro outro;
        Scene scene;

        public Object2D train;
        Sprite[] wordsSprites;
        Object2D[] cars;
        List<string> wordSet;
        List<int> selectedWords;
        string firstWord;
        string currentStr = "";
        float errorTimer = 0;
        int set;
        int coutRight = 0;
        public bool right = false;


        public int countSit = 0;

        public TrainMainScene(int set, Action<int> exit) : base(exit)
        {
            this.set = set;
            wordSet = new List<string>();
            switch (set)
            {
                default:        //0
                    firstWord = "карандаш";
                    wordSet.Add("шкаф");
                    wordSet.Add("флажок");
                    wordSet.Add("кот");
                    wordSet.Add("топор");
                    break;
                case (1):
                    firstWord = "ракета";
                    wordSet.Add("автобус");
                    wordSet.Add("сачок");
                    wordSet.Add("ключ");
                    wordSet.Add("чайник");
                    break;
                case (2):
                    firstWord = "кашка";
                    wordSet.Add("ананас");
                    wordSet.Add("сом");
                    wordSet.Add("мак");
                    wordSet.Add("крокодил");
                    break;
                case (3):
                    firstWord = "лук";
                    wordSet.Add("краски");
                    wordSet.Add("иголка");
                    wordSet.Add("астра");
                    wordSet.Add("арбуз");
                    break;
                case (4):
                    firstWord = "зонт";
                    wordSet.Add("телефон");
                    wordSet.Add("носорог");
                    wordSet.Add("гриб");
                    wordSet.Add("бегемот");
                    break;
                case (5):
                    firstWord = "телевизор";
                    wordSet.Add("ранец");
                    wordSet.Add("цыплёнок");
                    wordSet.Add("кормушка");
                    wordSet.Add("автобус");
                    break;
                case (6):
                    firstWord = "альбом";
                    wordSet.Add("мёд");
                    wordSet.Add("диван");
                    wordSet.Add("нож");
                    wordSet.Add("жук");
                    break;
                case (7):
                    firstWord = "комар";
                    wordSet.Add("рак");
                    wordSet.Add("кактус");
                    wordSet.Add("сноп");
                    wordSet.Add("помидор");
                    break;
                case (8):
                    firstWord = "рябина";
                    wordSet.Add("аквариум");
                    wordSet.Add("муравьи");
                    wordSet.Add("иволга");
                    wordSet.Add("апельсин");
                    break;
                case (9):
                    firstWord = "носки";
                    wordSet.Add("индюк");
                    wordSet.Add("колос");
                    wordSet.Add("стол");
                    wordSet.Add("ландыш");
                    break;
                case (10):
                    firstWord = "шар";
                    wordSet.Add("рак");
                    wordSet.Add("круг");
                    wordSet.Add("галстук");
                    wordSet.Add("крокодил");
                    break;
            }
            intro = new TrainIntro();
            scene = intro;
            currentStr += firstWord;
            Random rand = new Random();
            selectedWords = (new int[] { 1, 2, 3, 4 }).ToList<int>();
            selectedWords.Sort(delegate (int a, int b) { return rand.Next(2) * 2 - 1; });
            wordsSprites = new Sprite[4];

            Canvas.elements.Add(new Sprite(Canvas, "Train/BG", 100, Vector2.Zero));
            for (int i = 0; i < 4; i++)
                Canvas.elements.Add(new Sprite(Canvas, "Train/WordPanel", 15, new Vector2(11.5f + 19 * i, 41)));
            train = new Object2D(Canvas, new Vector2(100, 16f), new Vector2(0, 25f));
            Canvas.elements.Add(train);
            train.elements.Add(new Sprite(train, "Train/TrainBG", 100, new Vector2(0, 0)));
            cars = new Object2D[4];
            for (int i = 0; i < 4; i++)
            {
                cars[i] = new Object2D(train, new Vector2(14, 14), new Vector2(38 + 16 * i, -3));
                train.elements.Add(cars[i]);
            }
            Sprite sprite;
            train.elements.Add(sprite = new Sprite(train, "Train/Words/Set" + set.ToString() + "/0", 14, new Vector2(21f, -3)));
            sprite.value = set;
            train.elements.Add(new Sprite(train, "Train/Train", 100, new Vector2(0, 0)));

            for (int i = 0; i < 4; i++)
            {
                wordsSprites[i] = new Sprite(Canvas, "Train/Words/Set" + set.ToString() + '/' + selectedWords[i], 14, new Vector2(12.3f + 19 * i, 41.3f));
                wordsSprites[i].value = selectedWords[i] - 1;
                Canvas.elements.Add(wordsSprites[i]);
            }
            Canvas.elements.Add(new Button(Canvas, "Train/Check", 20, new Vector2(2, 2), new int[] { 1, 1, 1 }, "Check", Check));

        }

        private void Check()
        {
            if (coutRight == 4) right = true;
            else Reset();
        }

        private void Reset()
        {
            coutRight = 0;
            train.elements.Clear();
            for (int i = 0; i < 4; i++)
            {
                Canvas.elements.Remove(cars[i]);
                train.elements.Remove(cars[i]);
                cars[i] = new Object2D(train, new Vector2(14, 14), new Vector2(38 + 15 * i, -5));
                train.elements.Add(cars[i]);
            }
            train.elements.Add(new Sprite(train, "Train/Words/Set" + set.ToString() + "/0", 14, new Vector2(21f, -5f)));
            train.elements.Add(new Sprite(train, "Train/Train", 100, new Vector2(0, 0)));
            for (int i = 0; i < 4; i++)
            {
                wordsSprites[i].visible = true;
                wordsSprites[i].active = true;
            }
            currentStr = firstWord;
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
                    outro = new TrainOutro(train);
                scene = outro;
            }
            else if (intro.timer >= intro.maxTime)
                scene = null;
            if (scene != null)
                scene.Update();
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
                        if (wordsSprites[i].visible && currentStr[currentStr.Length - 1] == wordSet[(int)wordsSprites[i].value][0]) broken = false;
                        wordsSprites[i].color = Color.White;
                    }
                    if (broken) Reset();
                }
                if (Draggable == null)
                {
                    if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                        EngineCore.oldMouseState.LeftButton == ButtonState.Released && errorTimer == 0)
                        for (int i = 0; i < 4; i++)
                            if (wordsSprites[i] != null ? wordsSprites[i].UnderMouse() : false)
                            {
                                if (currentStr[currentStr.Length - 1] == wordSet[(int)wordsSprites[i].value][0]) coutRight++;
                                currentStr += wordSet[(int)wordsSprites[i].value];
                                Object2D curCar = (Object2D)wordsSprites[i].Clone();
                                wordsSprites[i].visible = false;
                                wordsSprites[i].active = false;
                                curCar.pos = cars[countSit].pos;
                                train.elements[train.elements.FindIndex(delegate (Object2D a) { return a == cars[countSit]; })] = curCar;
                                curCar.parent = train;
                                cars[countSit] = curCar;
                                countSit++;
                                return;
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
