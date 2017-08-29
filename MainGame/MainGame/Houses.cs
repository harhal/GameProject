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
    class Houses : Scene
    {
        class House : Sprite
        {
            public bool full
            {
                get
                {
                    return include >= capacity;
                }
            }
            public int include;
            public int capacity;
            public List<Item> items;

            public House(Object2D parent, int set, int size) : base(parent, "Common/Houses/House" + size.ToString(), 28, new Vector2(4 + size * 31, 27))
            {
                int[] partsCount;
                switch (set)
                {
                    default:        //0
                        partsCount = new int[] { 3, 2, 1 };
                        break;
                    case (1):
                        partsCount = new int[] { 0, 3, 3 };
                        break;
                    case (2):
                        partsCount = new int[] { 0, 2, 4 };
                        break;
                }
                this.capacity = partsCount[size];
                parent.elements.Add(this);
                items = new List<Item>();
            }
        }

        class Item : Sprite
        {
            public Sound sound;
            public House house;

            public Item(Object2D parent, int set, int number, House house) : base(parent, /*TheGame.language +*/ "Common/Houses/Set" + set.ToString() + "/" + number.ToString(), 19, Vector2.One * 3)
            {
                this.house = house;
                sound = new Sound(TheGame.language + "/Houses/Set" + set.ToString() + "/" + number.ToString());
            }
        }

        int set = 0;
        List<House> houses;
        List<Item> items;
        int right = 0;
        bool isVoid = true;
        Sprite place;

        public Houses(int set, Action<int> exit) : base(exit)
        {
            this.set = set;
            Canvas.elements.Add(new Sprite(Canvas, "Common/Houses/BG", 100, Vector2.Zero));
            houses = new List<House>();
            items = new List<Item>();
            place = new Sprite(Canvas, "Common/Houses/Place", 25, new Vector2(37, 1));
            Canvas.elements.Add(place);
            for (int i = 0; i < 3; i++)
                houses.Add(new House(Canvas, set, i));
            int curHouse = 0;
            for (int i = 0; i < 6; i++)
            {
                if (!(houses[curHouse].items.Count < houses[curHouse].capacity))
                    curHouse++;
                Item item = new Item(place, set, i, houses[curHouse]);
                items.Add(item);
                houses[curHouse].items.Add(item);
            }
            items.Sort(delegate (Item a, Item b)
            {
                int res = TheGame.rand.Next(2) * 2 - 1;
                return res;
            });
            TheGame.task = new Sound(TheGame.language + "/Houses/Task" + set.ToString());
            EngineCore.PlaySound(TheGame.task);
            Canvas.active = false;
            AddEvent((float)TheGame.task.Duration.TotalSeconds, delegate () { Canvas.active = true; });
        }

        public override Scene GetReloadedScene()
        {
            return new Houses(set, exit);
        }

        public override void Update()
        {
            if (Canvas.active)
            {
                if (isVoid && right < 6)
                {
                    place.elements.Add(items[right]);
                    isVoid = false;
                }
                if (right < 6)
                {
                    if (items[right].UnderMouse() && Draggable == null)
                        if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                            EngineCore.oldMouseState.LeftButton == ButtonState.Released)
                        {
                            Draggable = (Item)items[right].Clone();
                            Draggable.parent = Canvas;
                            Canvas.elements.Add(Draggable);
                            items[right].visible = false;
                        }
                        else if (items[right].UnderMouseBefore())
                            EngineCore.PlaySound(items[right].sound);
                    if (Draggable is Item)
                    {
                        if (EngineCore.currentMouseState.LeftButton == ButtonState.Released &&
                            EngineCore.oldMouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (((Draggable as Item).house).UnderMouse())
                            {
                                isVoid = true;
                                right++;
                                place.elements.Clear();
                                Item item = (Item)Draggable;
                                item.scale = 10;
                                item.parent = item.house;
                                item.pos = new Vector2(item.house.include * 7, 12);
                                item.house.elements.Add(item);
                                if (item.house.full)
                                    item.house.ChangeTexture("Common/Houses/CompleteHouse" + ((int)item.value).ToString());
                                item.house.include++;
                                Canvas.elements.Remove(Draggable);
                                Draggable = null;
                            }
                            else
                            {
                                TheGame.PlayWrong();
                                Canvas.elements.Remove(Draggable);
                                Draggable = null;
                                items[right].visible = true;
                                items[right].active = true;
                            }
                        }
                    }
                }
            }
            base.Update();
            if (right >= 6 && Canvas.active)
            {
                for (int i = 0; i < 3; i++)
                    houses[i].ChangeTexture("Common/Houses/CompleteHouse" + i.ToString());
                AddEvent(1, delegate
                {
                    exit(set);
                });
            }
        }
    }
}
