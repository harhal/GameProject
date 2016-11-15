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
        //Effect effect1;

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
                        menu = null;
                    }
                    else
                        ToMainMenu();
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
                        menu = null;
                    }
                    else
                        ToMainMenu();
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
                        menu = null;
                    }
                    else
                        ToMainMenu();
                });
        }

        void HousesExit(int set)
        {
            menu = new EndLevelMenu(
                delegate ()
                {
                    menu = mainMenu;
                },
                delegate ()
                {
                    scene = new Houses(set, HousesExit);
                    menu = null;
                },
                delegate ()
                {
                    if (set < 2)
                    {
                        set++;
                        scene = new Houses(set, HousesExit);
                        menu = null;
                    }
                    else
                        ToMainMenu();
                });
        }

        void BallonsExit(int set)
        {
            menu = new EndLevelMenu(
                delegate ()
                {
                    menu = mainMenu;
                },
                delegate ()
                {
                    scene = new Ballons(set, BallonsExit);
                    menu = null;
                },
                delegate ()
                {
                    if (set < 6)
                    {
                        set++;
                        scene = new Ballons(set, BallonsExit);
                        menu = null;
                    }
                    else
                        ToMainMenu();
                });
        }

        void DishesExit(int set)
        {
            switch (set)
            {
                default:
                    if (set > 1 && set < 11)
                        menu = new EndLevelMenu(
                            delegate ()
                            {
                                menu = mainMenu;
                            },
                            delegate ()
                            {
                                scene = new Kitchen(--set, DishesExit);
                                menu = null;
                            },
                            delegate ()
                            {
                                scene = new Kitchen(set, DishesExit);
                                menu = null;
                            });
                    if (set > 11 && set < 24)
                        menu = new EndLevelMenu(
                            delegate ()
                            {
                                menu = mainMenu;
                            },
                            delegate ()
                            {
                                scene = new Table(--set, DishesExit);
                                menu = null;
                            },
                            delegate ()
                            {
                                scene = new Table(set, DishesExit);
                                menu = null;
                            });
                    break;
                case (0):
                    scene = new DishesIntro(0, DishesExit);
                    break;
                case (1):
                    scene = new KitchenIntro(2, DishesExit);
                    break;
                case (2):
                    scene = new Kitchen(2, DishesExit);
                    break;
                case (11):
                    menu = new EndLevelMenu(
                        delegate ()
                        {
                            menu = mainMenu;
                        },
                        delegate ()
                        {
                            scene = new DishesIntro(10, DishesExit);
                            menu = null;
                        },
                        delegate ()
                        {
                            ToMainMenu();
                        });
                    break;
                case (12):
                    scene = new TableIntro(12, DishesExit);
                    break;
                case (13):
                    scene = new Table(13, DishesExit);
                    break;
                case (24):
                    menu = new EndLevelMenu(
                        delegate ()
                        {
                            menu = mainMenu;
                        },
                        delegate ()
                        {
                            scene = new DishesIntro(0, DishesExit);
                            menu = null;
                        },
                        delegate ()
                        {
                            ToMainMenu();
                        });
                    break;
            }
        }

        void ToMainMenu()
        {
            showPanel = false;
            scene = new VoidScene(0, delegate (int exCode) { Exit(); });
            menu = mainMenu;
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
                case (3):
                    scene = new Houses(0, HousesExit);
                    break;
                case (4):
                    scene = new Ballons(0, BallonsExit);
                    break;
                case (5):
                    scene = new DishesIntro(0, DishesExit);
                    break;
            }
            showPanel = true;
            menu = null;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            //effect1 = EngineCore.content.Load<Effect>("Shaders/Blur");
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
            /*if (menu == null || scene is VoidScene)
            {
                scene.effect = null;
            }
            else
            {
                scene.effect = effect1;

            }*/
            scene.Draw();
            DrawMenu();
            EngineCore.DrawCursor();
        }
    }
}
