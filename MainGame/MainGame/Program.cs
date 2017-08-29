using System;
using UnicornEngine;

namespace MainGame
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (args[0] == "-rus")
                    Settings.Default.Language = "rus";
                if (args[0] == "-bel")
                    Settings.Default.Language = "bel";
            }
            TheGame.language = Settings.Default.Language;
            EngineCore.FullScreen = Settings.Default.IsFullScreen;
            EngineCore.musicVolume = Settings.Default.MusicVolume;
            EngineCore.soundVolume = Settings.Default.SoundsVolume;
            EngineCore.mute = Settings.Default.Mute;
            TheGame theGame = new TheGame();
            theGame.Run();
            Settings.Default.Language = TheGame.language;
            Settings.Default.IsFullScreen = EngineCore.FullScreen; 
            Settings.Default.MusicVolume  = EngineCore.musicVolume;
            Settings.Default.SoundsVolume = EngineCore.soundVolume;
            Settings.Default.Mute = EngineCore.mute;
            Settings.Default.Save();
        }
    }
#endif
}

