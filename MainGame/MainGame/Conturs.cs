using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using UnicornEngine;

namespace MainGame
{
    class Conturs : Scene
    {
        Object2D WorkSpace;
        Object2D FindedObjects;
        public int count;
        public int num = 0;
        public int pos = 0;
        int set;
        char letter;

        public Conturs(int set, Action<int> exit):base(exit)
        {
            colorBG = Color.Bisque;
            this.set = set;
            Canvas.elements.Add(WorkSpace = new Object2D(Canvas, new Vector2(80, 34), new Vector2(4, 4)));
            Canvas.elements.Add(new Sprite(Canvas, "Conturs/Panel", 70, new Vector2(2, 42)));
            Canvas.elements.Add(FindedObjects = new Object2D(Canvas, new Vector2(70, 13), new Vector2(2, 42)));
            Sprite sprite = null;
            switch (set)
            {
                default:
                    letter = 'д';
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/0", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/    sprite.value =  "0дом";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/0");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/1", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/    sprite.value = "1синица";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/1");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/2", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/    sprite.value = "2кот";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/2");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/3", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/    sprite.value = "3ворона";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/3");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/4", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/    sprite.value = "4дерево";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/4");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/5", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/    sprite.value = "5луна";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/5");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/6", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/    sprite.value = "6волк";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/6");
                    WorkSpace.elements.Add(sprite);
                    count = 2;
                    break;
                case (1):
                    letter = 'з';
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/0", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "0заяц";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/0");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/1", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "1синица";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/1");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/2", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "2ворона";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/2");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/3", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "3замок";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/3");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/4", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "4волк";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/4");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/5", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "5зебра";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/5");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/6", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "6кот";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/6");
                    WorkSpace.elements.Add(sprite);
                    count = 3;
                    break;
                case (2):
                    letter = 'г';
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/0", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "0гусь";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/0");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/1", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "1дом";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/1");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/2", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "2грач";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/2");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/3", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "3ворона";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/3");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/4", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "4гиря";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/4");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/5", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "5корона";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/5");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/6", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "6гора";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/6");
                    WorkSpace.elements.Add(sprite);
                    count = 4;
                    break;
                case (3):
                    letter = 'с';
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/0", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "0сова";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/0");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/1", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "1волк";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/1");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/2", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "2страус";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/2");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/3", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "3заяц";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/3");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/4", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "4синица";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/4");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/5", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "5кот";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/5");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/6", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "6сорока";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/6");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/7", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "7луна";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/7");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/8", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "8слон";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/8");
                    WorkSpace.elements.Add(sprite);
                    count = 5;
                    break;
                case (4):
                    letter = 'к';
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/0", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "0крот";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/0");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/1", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "1зебра";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/1");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/2", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "2кит";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/2");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/3", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "3синица";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/3");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/4", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "4кот";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/4");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/5", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "5крокодил";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/5");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/6", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "6сова";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/6");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/7", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "7краб";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/7");
                    WorkSpace.elements.Add(sprite);
                    ////////////////////////////////////////////
                    sprite = new Sprite(WorkSpace, "Conturs/Set" + set.ToString() + "/Contur/8", new int[2] { 1, 1 }, 1, new Vector2(0, 0));
                    /**/
                    sprite.value = "8кран";
                    sprite.SetCollisionMap("Conturs/Set" + set.ToString() + "/Mask/8");
                    WorkSpace.elements.Add(sprite);
                    count = 6;
                    break;
            }
            Random rand = new Random();
            List<int> randomList = new List<int>();
            for (int i = 1; i < WorkSpace.elements.Count + 1; i++) randomList.Add(i);
            randomList.Sort(delegate (int a, int b) { return rand.Next(2) * 2 - 1; });
            float requency = (WorkSpace.size.X - 37) / WorkSpace.elements.Count;
            for (int i = 0; i < WorkSpace.elements.Count; i++)
            {
                WorkSpace.elements[i].pos = new Vector2(requency * randomList[i], - 5 + rand.Next(20));
                ((Sprite)WorkSpace.elements[i]).scale = 25 + rand.Next(5);
            }


            Canvas.elements.Add(new Button(Canvas, "Conturs/Check", 20, new Vector2(73, 48), new int[] { 1, 1, 1 }, "Check", delegate
            {
                if (count > 0 && count == num)
                    exit(set);
                else
                    Reset();
            }));
            Canvas.elements.Add(new Sprite(Canvas, "Conturs/Set" + set.ToString() + "/Task", 20, new Vector2(73, 40)));
            Canvas.active = false;
            AddEvent(1, delegate { Canvas.active = true; });
        }

        public void Reset()
        {
            foreach (Object2D obj in WorkSpace.elements)
                if (obj.GetType() == typeof(Sprite))
                {
                    ((Sprite)obj).visible = true;
                    obj.active = true;
                }
             FindedObjects.elements.Clear();
            num = 0;
            pos = 0;
        }

        public override void Update()
        {
            if (Canvas.active)
            foreach (Object2D obj in WorkSpace.elements)
            {
                if (obj != null ? (obj.GetType() == typeof(Sprite) ? ((Sprite)obj).animation == 1 : false) : false)
                    ((Sprite)obj).animation = 0;

            }
            base.Update();
            Object2D underMouse = WorkSpace.ObjUnderMouse();
            if (Canvas.active)
                if (underMouse != null ? (underMouse.GetType() == typeof(Sprite)) : false)
            {
                if (EngineCore.oldMouseState.LeftButton == ButtonState.Pressed && EngineCore.currentMouseState.LeftButton == ButtonState.Released && pos <= 5)
                {
                    Sprite sprite = new Sprite(FindedObjects, "Conturs/Set" + set.ToString() + "/Color/" + ((string)underMouse.value)[0], 12, new Vector2(10 * pos, 0.5f));
                    pos++;
                    if (((string)underMouse.value)[1] == letter)
                        num++;
                    else
                        num--;
                    underMouse.active = false;
                    ((Sprite)underMouse).visible = false;
                    FindedObjects.elements.Add(sprite);
                }
                if (((Sprite)underMouse).animationsLengths.Length == 2)
                    ((Sprite)underMouse).animation = 1;
            }
        }
    }
}
