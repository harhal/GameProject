using System;
using UnicornEngine;

namespace MainGame
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            EngineCore.FullScreen = false;
            TheGame theGame = new TheGame();
            theGame.Run();
        }
    }
#endif
}

