using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using UnicornEngine;

namespace MainGame
{
    class Conturs : Scene
    {
        class Word : Sprite
        {
            public int number;
            public bool right;
            public Sound sound;
            Sprite icon;

            public Word(Object2D WorkSpace, Object2D FindedObjects, int set, bool right, int number) :
                base(WorkSpace, TheGame.language + "/Conturs/Set" + set.ToString() + "/Contur/" + number.ToString(), new int[2] { 1, 1 }, 1, Vector2.Zero)
            {
                this.number = number;
                this.right = right;
                this.sound = new Sound(TheGame.language + "/Conturs/Set" + set.ToString() + "/" + number.ToString());
                SetCollisionMap(TheGame.language + "/Conturs/Set" + set.ToString() + "/Mask/" + number.ToString());
                icon = new Sprite(FindedObjects, TheGame.language + "/Conturs/Set" + set.ToString() + "/Color/" + number.ToString(), 12, Vector2.Zero);
                icon.value = this;
            }

            public void Move(int pos, float requency)
            {
                this.pos = new Vector2(requency * pos, -5 + TheGame.rand.Next(20));
                if (this.pos.X > 60 && this.pos.Y < 5)
                    this.pos.Y = 6 + TheGame.rand.Next(10);
                scale = 20 + TheGame.rand.Next(5);
            }

            public Sprite getIcon(int pos)
            {
                icon.pos = new Vector2(10 * pos, 0.5f);
                return icon;
            }
        }

        Object2D WorkSpace;
        Object2D FindedObjects;
        Word underMouseBefore;
        public int count;
        public int num = 0;
        public int pos = 0;
        int set;

        public Conturs(int set, Action<int> exit):base(exit)
        {
            colorBG = Color.Bisque;
            this.set = set;
            Canvas.elements.Add(WorkSpace = new Object2D(Canvas, new Vector2(100, 34), new Vector2(0, 4)));
            Canvas.elements.Add(new Sprite(Canvas, "Common/Conturs/Panel", 70, new Vector2(2, 42)));
            Canvas.elements.Add(FindedObjects = new Object2D(Canvas, new Vector2(70, 13), new Vector2(2, 42)));
            if (set <= 4)
                count = set + 2;
            else if (set <= 7)
                count = 2;
            else
                count = 3;
            int listSize;
            if (TheGame.language == "rus")
            #region rus
                if (set <= 2)
                    listSize = 7;
                else if (set <= 4)
                    listSize = 9;
                else if (set == 5)
                    listSize = 5;
                else
                    listSize = 6;
            #endregion
            #region bel
            else
                if (set == 0 || set == 5)
                    listSize = 5;
                else if (set == 1 || set >= 6)
                    listSize = 6;
                else if (set == 2)
                    listSize = 7;
                else if (set == 3)
                    listSize = 8;
                else
                    listSize = 10;
            #endregion
            for (int i = 0; i < listSize; i++)
                WorkSpace.elements.Add(new Word(WorkSpace, FindedObjects, set, i < count, i));
            WorkSpace.elements.Sort(delegate (Object2D a, Object2D b) { return TheGame.rand.Next(2) * 2 - 1; });
            float requency = (WorkSpace.size.X - 22) / WorkSpace.elements.Count;
            for (int i = 0; i < WorkSpace.elements.Count; i++)
                ((Word)WorkSpace.elements[i]).Move(i, requency);


            Canvas.elements.Add(new Button(Canvas, TheGame.language + "/Conturs/Check", 20, new Vector2(73, 48), new int[] { 1, 1, 1 }, "Check", delegate
            {
                if (count > 0 && count == num)
                    exit(set);
                else
                {
                    TheGame.PlayWrong();
                    Reset();
                }
            }));
            Canvas.elements.Add(new Sprite(Canvas, TheGame.language + "/Conturs/Set" + set.ToString() + "/Task", 20, new Vector2(73, 40)));
            TheGame.task = new Sound(TheGame.language + "/Conturs/Set" + set.ToString() + "/Task");
            Canvas.active = false;
            EngineCore.PlaySound(TheGame.task);
            AddEvent((float)TheGame.task.Duration.TotalSeconds, delegate
            {
                Canvas.active = true;
            });
        }

        public override Scene GetReloadedScene()
        {
            return new Conturs(set, exit);
        }

        public void Reset()
        {
            foreach (Sprite obj in FindedObjects.elements)
            {
                (obj.value as Word).visible = true;
                (obj.value as Word).active = true;
            }
            FindedObjects.elements.Clear();
            num = 0;
            pos = 0;
        }

        public override void Update()
        {
            base.Update();
            if (Canvas.active)
            {
                if (underMouseBefore != null)
                    underMouseBefore.animation = 0;
                Word underMouse = WorkSpace.ObjUnderMouse() as Word;
                if (underMouse != null)
                {
                    if (EngineCore.oldMouseState.LeftButton == ButtonState.Pressed && EngineCore.currentMouseState.LeftButton == ButtonState.Released && pos <= 5)
                    {
                        FindedObjects.elements.Add(underMouse.getIcon(pos));
                        pos++;
                        if (underMouse.right)
                            num++;
                        else
                            num--;
                        underMouse.active = false;
                        underMouse.visible = false;
                    }
                    if (underMouse.animation == 0)
                    {
                        if (underMouseBefore != underMouse)
                            EngineCore.PlaySound(underMouse.sound);
                        underMouse.animation = 1;
                    }
                    underMouseBefore = underMouse;
                }
                Sprite findedUnderMouse = FindedObjects.ObjUnderMouse() as Sprite;
                if (findedUnderMouse != null)
                {
                    if (EngineCore.oldMouseState.LeftButton == ButtonState.Pressed && EngineCore.currentMouseState.LeftButton == ButtonState.Released)
                    {
                        FindedObjects.elements.Remove(findedUnderMouse);
                        for (int i = 0; i < FindedObjects.elements.Count; i++)
                            FindedObjects.elements[i].pos = new Vector2(10 * i, 0.5f);
                        pos--;
                        if ((findedUnderMouse.value as Word).right)
                            num--;
                        else
                            num++;
                        (findedUnderMouse.value as Word).visible = true;
                        (findedUnderMouse.value as Word).active = true;
                    }
                }
            }
        }
    }
}
