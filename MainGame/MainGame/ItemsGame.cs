using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnicornEngine;

namespace MainGame
{
    abstract class ItemsGame : Scene
    {
        protected class Item : Sprite
        {
            public float scaleOld;
            public Vector2 posOld;
            public Sound sound;

            public Item(Object2D parent, string texture, float size, Vector2 pos) :
                base(parent, "Common/" + texture, size, pos)
            {
                this.sound = new Sound(TheGame.language + '/' +  texture);
                scaleOld = size;
                posOld = pos;
            }
        }

        protected int set = 0;

        Item lastItem;

        protected void playItem(Item item)
        {
            if (lastItem != item)
            {
                EngineCore.PlaySound(item.sound);
                lastItem = item;
            }
        }

        public ItemsGame(int set, Action<int> exit) : base(exit)
        {
            this.set = set;
        }
    }
}
