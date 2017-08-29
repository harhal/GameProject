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
    class Ballons : Scene
    {
        class Ballon : Sprite
        {
            public Sound sound;

            public Ballon(Object2D parent, int colorNumber) : base(parent, "Common/Ballons/Ballon", new int[] { 8 }, 8, Vector2.Zero)
            {
                sound = new Sound(TheGame.language + "/Ballons/Color" + colorNumber.ToString());
                color = new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.SkyBlue, Color.DarkBlue, Color.Violet }[colorNumber];
                animationLooped = true;
                parent.elements.Add(this);
            }
        }

        class Item : Sprite
        {
            public int number;
            public Sound sound;

            public Item(Object2D parent, int set, int number) : base(parent, "Common/Ballons/Set" + set.ToString() + "/" + number.ToString(), 13, Vector2.One)
            {
                sound = new Sound(TheGame.language + "/Ballons/Set" + set.ToString() + "/" + number.ToString());
                this.number = number;
                parent.elements.Add(this);
            }
        }

        int set = 0;
        List<Ballon> ballons;
        List<Item> items;
        Sprite clouds;
        Sprite car;
        int baloonsCount = 2;
        int blowed = 0;
        int itemsCount;
        int itemsPressed = 0;
        Color mainColor;
        int fase = 0;
        SoundEffect pop;
        SoundEffect nope;
        Object2D lastObj;

        public Ballons(int set, Action<int> exit) : base(exit)
        {
            this.set = set;
            Canvas.elements.Add(new Sprite(Canvas, "Common/Ballons/Sky", 100, Vector2.Zero));
            Canvas.elements.Add(new Sprite(Canvas, "Common/Ballons/Sky", 100, new Vector2(0, 20)));
            clouds = new Sprite(Canvas, "Common/Ballons/Clouds", 200, Vector2.Zero);
            Canvas.elements.Add(clouds);
            Canvas.elements.Add(new Sprite(Canvas, "Common/Ballons/BG", 100, Vector2.Zero));
            car = new Sprite(Canvas, "Common/Ballons/Car", 20, new Vector2(40, 43));
            Canvas.elements.Add(car);
            ballons = new List<Ballon>();
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 2; j++)
                    ballons.Add(new Ballon(Canvas, i));
            ballons.Sort(delegate (Ballon a, Ballon b)
            {
                return TheGame.rand.Next(2) * 2 - 1;
            });
            for (int y = 0; y < 2; y++)
                for (int x = 0; x < 7; x++)
                {
                    ballons[x + y * 7].pos.X = 6 + x * 12.5f;
                    ballons[x + y * 7].pos.Y = 10 + y * 16;
                }
            List<Sprite> places = new List<Sprite>();
            for (int i = 0; i < 5; i++)
            {
                places.Add(new Sprite(Canvas, "Common/Ballons/Place", 15, new Vector2(5 + 19 * i, 58)));
                Canvas.elements.Add(places.Last());
            }
            places.Sort(delegate (Sprite a, Sprite b)
            {
                int res = TheGame.rand.Next(2) * 2 - 1;
                return res;
            });
            items = new List<Item>();
            for (int i = 0; i < 5; i++)
                items.Add(new Item(places[i], set, i));
                switch (set)
            {
                default:        //0
                    itemsCount = 3;
                    mainColor = Color.Red;
                    break;
                case (1):
                    itemsCount = 2;
                    mainColor = Color.Orange;
                    break;
                case (2):
                    itemsCount = 2;
                    mainColor = Color.Yellow;
                    break;
                case (3):
                    itemsCount = 4;
                    mainColor = Color.Green;
                    break;
                case (4):
                    itemsCount = 3;
                    mainColor = Color.SkyBlue;
                    break;
                case (5):
                    itemsCount = 2;
                    mainColor = Color.DarkBlue;
                    break;
                case (6):
                    itemsCount = 1;
                    mainColor = Color.Violet;
                    break;
            }
            TheGame.task = new Sound(TheGame.language + "/Ballons/Task0" + set.ToString());
            EngineCore.PlaySound(TheGame.task);
            Canvas.active = false;
            AddEvent((float)TheGame.task.Duration.TotalSeconds, delegate () { Canvas.active = true; });
            pop = EngineCore.content.Load<SoundEffect>("Sounds/Common/Ballons/Pop");
            nope = EngineCore.content.Load<SoundEffect>("Sounds/Common/Ballons/Nope");
        }

        public override Scene GetReloadedScene()
        {
            return new Ballons(set, exit);
        }

        public override void Update()
        {
            base.Update();
            if (Canvas.active)
            {
                clouds.pos.X = (clouds.pos.X - 0.1f) % 100;
                if (fase == 0)
                {
                    foreach (Ballon ballon in ballons)
                    {
                        if (ballon.UnderMouse() && ballon.active)
                            if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                                EngineCore.oldMouseState.LeftButton == ButtonState.Released)
                                if (ballon.color == mainColor)
                                {
                                    AddEffect(new EffectSprite(Canvas, "Common/Ballons/Boom", 7, 16, ballon.pos - Vector2.One * 4, 1.2f));
                                    EffectSprite blow = new EffectSprite(Canvas, "Common/Ballons/Blow", 7, 16, ballon.pos - Vector2.One * 4, 1.2f);
                                    blow.color = ballon.color;
                                    AddEffect(blow);
                                    AddEvent(0.1f, delegate
                                    {
                                        ballon.visible = false;
                                        ballon.active = false;
                                    });
                                    AddEvent(1.2f, delegate
                                    {
                                        blowed++;
                                    });
                                    EngineCore.PlaySound(pop);
                                }
                                else
                                {
                                    EngineCore.PlaySound(nope);
                                }
                            else if (ballon != lastObj)
                            {
                                lastObj = ballon;
                            }
                    }
                    if (blowed >= baloonsCount)
                        fase++;
                }
                else if (fase == 1)
                {
                    Canvas.pos.Y -= 0.5f;
                    if (Canvas.pos.Y < -16)
                    {
                        fase++;
                        TheGame.task = new Sound(TheGame.language + "/Ballons/Task1" + set.ToString());
                        EngineCore.PlaySound(TheGame.task);
                        Canvas.active = false;
                        AddEvent((float)TheGame.task.Duration.TotalSeconds, delegate () { Canvas.active = true; });
                    }
                }
                else if (fase == 2)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (items[i].UnderMouse() && items[i].active)
                            if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                                EngineCore.oldMouseState.LeftButton == ButtonState.Released)
                            {
                                if ((int)items[i].number < itemsCount)
                                {
                                    Vector2 start = items[i].parent.pos + items[i].pos;
                                    Vector2 end = car.pos;
                                    EffectSprite effect = new EffectSprite(Canvas, "Common/Ballons/Set" + set.ToString() + "/" + ((int)items[i].number).ToString(), 1, 13, start, 1);
                                    int n = i;
                                    effect.move = delegate (float time, float delay)
                                    {
                                        return Vector2.Transform(((end - start) / 50),
                                               Matrix.CreateRotationZ((float)Math.Cos(time / effect.lifeTime * Math.PI) * (n > 2 ? -1 : 1)));
                                    };
                                    AddEffect(effect);
                                    items[i].visible = false;
                                    items[i].active = false;
                                    itemsPressed++;
                                    if (itemsPressed >= itemsCount)
                                        AddEvent(1, delegate
                                        {
                                            exit(set);
                                        });
                                }
                                else
                                    TheGame.PlayWrong();
                            }
                            else if (items[i] != lastObj)
                            {
                                EngineCore.PlaySound(items[i].sound);
                                lastObj = items[i];
                            }
                    }
                }
            }
        }
    }
}
