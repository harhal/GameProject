using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        class Word : Sprite
        {
            public int number;
            public Sound sound;

            public Word(Object2D parent, int set, int number) :
                base(parent, TheGame.language + "/Wizzard/Set" + set.ToString() + "/" + number.ToString(), number == 0 ? 20 : 16, number == 0 ? new Vector2(40, 6.5f) : Vector2.Zero)
            {
                this.number = number;
                this.sound = new Sound(TheGame.language + "/Wizzard/Set" + set.ToString() + "/" + number.ToString());
            }

            public void Move(int pos)
            {
                if (pos < 0)
                {
                    this.pos = new Vector2(40, 6.5f);
                    this.scale = 20;
                }
                else
                    this.pos = new Vector2(13 + pos * 19.3f, 37);
            }
        }

        int set = 0;
        List<Word> words;
        Word trueWord;
        Word baseWord;
        Sprite mask;
        public bool win = false;

        public Wizzards(int set, Action<int> exit) : base(exit)
        {
            this.set = set;
            int maskPos = 0;
            if (set >= 0 && set <= 5)
                maskPos = 0;
            if ((set >= 6 && set <= 9) || (set == 11))
                maskPos = 2;
            if (set == 10)
                maskPos = 3;
            words = new List<Word>();
            Canvas.elements.Add(new Sprite(Canvas, "Common/Wizzard/BackGround", 100, Vector2.Zero));
            for (int i = 0; i < 4; i++)
            {
                words.Add(new Word(Canvas, set, i + 1));
                Canvas.elements.Add(words[i]);
            }
            words.Sort(delegate (Word a, Word b) { return TheGame.rand.Next(2) * 2 - 1; });
            for (int i = 0; i < 4; i++)
                words[i].Move(i);
            Canvas.elements.Add(baseWord = new Word(Canvas, set, 0));
            trueWord = new Word(Canvas, set, 1);
            trueWord.Move(-1);
            trueWord.visible = false;
            Canvas.elements.Add(trueWord);
            Sprite sprite;
            Canvas.elements.Add(sprite = new Sprite(Canvas, TheGame.language + "/Wizzard/Set" + set.ToString() + "/Subscribes/0", 1, new Vector2(0, 28)));
            sprite.scale = 7 / sprite.size.Y;
            sprite.pos.X = 50 - sprite.size.X / 2;
            Canvas.elements.Add(mask = new Sprite(Canvas, "Common/Wizzard/Mask", 7.5f, new Vector2(50 - sprite.size.X / 2 + maskPos * 7.5f, 28)));
            mask.visible = false;
            mask.color = new Color(255, 0, 0, 128);
            TheGame.task = new Sound(TheGame.language + "/Wizzard/Set" + set.ToString() + "/Task");
            Canvas.active = false;
            EngineCore.PlaySound(TheGame.task);
            AddEvent((float)TheGame.task.Duration.TotalSeconds, delegate
            {
                Canvas.active = true;
            });
            //TheGame.help = new Sound(TheGame.language + "/Wizzard/Set" + set.ToString() + "/Task");
        }

        public override Scene GetReloadedScene()
        {
            return new Wizzards(set, exit);
        }

        public override void Update()
        {
            base.Update();
            foreach (Word word in words)
            {
                if (word.UnderMouse() && Canvas.active)
                {
                    if (!word.UnderMouseBefore())
                        EngineCore.PlaySound(word.sound);
                    if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                        EngineCore.oldMouseState.LeftButton == ButtonState.Released)
                        if (word.number == 1)
                        {
                            trueWord.visible = true;
                            baseWord.visible = false;
                            Sprite sprite;
                            Canvas.elements.Add(sprite = new Sprite(Canvas, TheGame.language + "/Wizzard/Set" + set.ToString() + "/Subscribes/1", 1, new Vector2(0, 28)));
                            sprite.scale = 7 / sprite.size.Y;
                            sprite.pos.X = 50 - sprite.size.X / 2;
                            Canvas.active = false;
                            EngineCore.PlaySound(trueWord.sound);
                            AddEvent(1, delegate
                            {
                                exit(set);
                            });
                        }
                        else
                        {
                            TheGame.PlayWrong();
                            mask.visible = true;
                            AddEvent(0.5f, delegate
                            {
                                mask.visible = false;
                            });
                        }
                }
            }
            if (baseWord.UnderMouse() && !baseWord.UnderMouseBefore() && Canvas.active)
                EngineCore.PlaySound(baseWord.sound);
        }
    }
}
