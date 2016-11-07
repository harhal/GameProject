using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using UnicornEngine;

namespace MainGame
{
    public class TheGame : UnicornGame
    {
        Scene scene;
        Effect effect1;
        //VoidScene voidScene;

        MainMenu mainMenu;

        void ContursExit(int set)
        {
            menu = new EndLevelMenu(
                delegate ()
                {
                    menu = mainMenu;
                },
                delegate ()
                {
                    scene = new Conturs(set, ContursExit);
                    menu = null;
                },
                delegate ()
                {
                    if (set < 4)
                    {
                        set++;
                        scene = new Conturs(set, ContursExit);
                    }
                    else
                        ToMainMenu();
                    menu = null;
                });
        }

        void TrainExit(int set)
        {
            menu = new EndLevelMenu(
                delegate()
                {
                    menu = mainMenu;
                },
                delegate ()
                {
                    scene = new TrainMainScene(set, TrainExit);
                    menu = null;
                },
                delegate ()
                {
                    if (set < 11)
                    {
                        set++;
                        scene = new TrainMainScene(set, TrainExit);
                    }
                    else
                        ToMainMenu();
                    menu = null;
                });
        }

        void WizzardsExit(int set)
        {
            menu = new EndLevelMenu(
                delegate ()
                {
                    menu = mainMenu;
                },
                delegate ()
                {
                    scene = new Wizzards(set, WizzardsExit);
                    menu = null;
                },
                delegate ()
                {
                    if (set < 6)
                    {
                        set++;
                        scene = new Wizzards(set, WizzardsExit);
                    }
                    else
                        ToMainMenu();
                    menu = null;
                });
        }

        void ToMainMenu()
        {
            showPanel = false;
            scene = new VoidScene(0, delegate (int exCode) { Exit(); });
        }

        void Play(int i)
        {
            switch (i)
            {
                case (0):
                    scene = new TrainMainScene(0, TrainExit);
                    break;
                case (1):
                    scene = new Conturs(0, ContursExit);
                    break;
                case (2):
                    scene = new Wizzards(0, WizzardsExit);
                    break;
            }
            showPanel = true;
            menu = null;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            effect1 = EngineCore.content.Load<Effect>("Shaders/Blur");
            SetBack(delegate () { menu = mainMenu; });
            mainMenu = new MainMenu(
                delegate
                {
                    menu = new GamesMenu(Play);
                },
                delegate
                {
                    menu = new SettingMenu(delegate { menu = mainMenu; });
                }, 
                null, 
                delegate
                {
                    if (!(scene is VoidScene))
                        menu = null;
                },
                this.Exit);
            ToMainMenu();
            menu = mainMenu;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (menu == null)
                scene.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (menu == null || scene is VoidScene)
            {
                scene.effect = null;
            }
            else
            {
                scene.effect = effect1;

            }
            scene.Draw();
            DrawMenu();
            EngineCore.DrawCursor();
        }
    }
}
