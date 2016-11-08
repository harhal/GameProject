using Microsoft.Xna.Framework;
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
        int set = 0;
        List<Sprite> houses;
        List<Sprite> items;
        int itemCount = 6;
        int[] partsCount;
        int right = 0;
        bool isVoid = true;
        Sprite place;

        public Houses(int set, Action<int> exit) : base(exit)
        {
            Canvas.elements.Add(new Sprite(Canvas, "Houses/BG", 100, Vector2.Zero));
            houses = new List<Sprite>();
            items = new List<Sprite>();
            place = new Sprite(Canvas, "Houses/Place", 25, new Vector2(37, 1));
            Canvas.elements.Add(place);
            for (int i = 0; i < 3; i++)
            {
                Sprite house = new Sprite(Canvas, "Houses/House" + i.ToString(), 28, new Vector2(4 + i * 31, 27));
                house.value = 0;
                houses.Add(house);
                Canvas.elements.Add(house);
            }
            this.set = set;
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
            int[] partsCountBuf = (int[])partsCount.Clone();
            int parts = 0;
            for (int i = 0; i < 6; i++)
            {
                Sprite item = new Sprite(place, "Houses/Sets/" + set.ToString() + "/" + i.ToString(), 19, Vector2.One * 3);
                while (partsCountBuf[parts] <= 0)
                    parts++;
                item.value = parts;
                partsCountBuf[parts]--;
                items.Add(item);
            }
            Random rand = new Random();
            items.Sort(delegate (Sprite a, Sprite b)
            {
                int res = rand.Next(2) * 2 - 1;
                return res;
            });

        }

        public override void Update()
        {
            if (isVoid && right < 6)
            {
                place.elements.Add(items[right]);
                isVoid = false;
            }
            if (right < 6)
            {
                if (items[right].UnderMouse() &&
                    EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                    EngineCore.oldMouseState.LeftButton == ButtonState.Released &&
                    Draggable == null)
                {
                    Draggable = (Sprite)items[right].Clone();
                    Draggable.parent = Canvas;
                    Canvas.elements.Add(Draggable);
                    items[right].visible = false;
                }
                if (Draggable != null)
                {
                    if (EngineCore.currentMouseState.LeftButton == ButtonState.Released &&
                        EngineCore.oldMouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (houses[(int)Draggable.value].UnderMouse())
                        {
                            isVoid = true;
                            right++;
                            place.elements.Clear();
                            Sprite item = (Sprite)Draggable;
                            item.scale = 10;
                            item.parent = houses[(int)item.value];
                            item.pos = new Vector2(((int)houses[(int)item.value].value) * 7, 12);
                            houses[(int)item.value].elements.Add(item);
                            if (((int)houses[(int)item.value].value) >= partsCount[(int)item.value])
                                houses[(int)item.value].ChangeTexture("Houses/CompleteHouse" + ((int)item.value).ToString());
                            houses[(int)item.value].value = ((int)houses[(int)item.value].value) + 1;
                            Canvas.elements.Remove(Draggable);
                            Draggable = null;
                        }
                        else
                        {
                            Canvas.elements.Remove(Draggable);
                            Draggable = null;
                            items[right].visible = true;
                            items[right].active = true;
                        }
                    }
                }
            }
            base.Update();
            if (right >= 6)
            {
                for (int i = 0; i < 3; i++)
                    houses[i].ChangeTexture("Houses/CompleteHouse" + i.ToString());
                AddEvent(1, delegate
                {
                    exit(set);
                });
            }
        }
    }
}
