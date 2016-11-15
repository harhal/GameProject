using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnicornEngine;

namespace MainGame
{
    abstract class Dishes : Scene
    {
        protected class Dish : Sprite
        {
            public float scaleOld;
            public Vector2 posOld;

            public Dish(Object2D parent, string texture, float size, Vector2 pos) :
                base(parent, texture, size, pos)
            {
                scaleOld = size;
                posOld = pos;
            }
        }

        int set = 0;

        public Dishes(int set, Action<int> exit) : base(exit)
        {
            this.set = set;
        }
    }
}
