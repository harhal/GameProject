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
        //Воспитание звуковой культуры речи у детей дошкольного возраста
        Scene scene;
        Effect effect1;
        Sprite menuPanel;
        protected Button lang1;
        protected Button lang2;
        public static Sound task;

        public bool showPanel = true;

        protected Menu settingMenu;
        protected Menu menu;

        MainMenu mainMenu;

        public static string language = "rus";

        static Sound[] rwrong;
        static Sound[] bwrong;

        public static Random rand;

        public static void PlayWrong()
        {
            if (language == "rus")
            {
                if (rwrong != null)
                    EngineCore.PlaySound(rwrong[rand.Next(6)]);
            }
            else
            {
                if (bwrong != null)
                    EngineCore.PlaySound(bwrong[rand.Next(6)]);
            }
        }

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
                    if (set < 10)
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
                    if (set < 11)
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

        void ClosesExit(int set)
        {
            if (set == -1)
                scene = new Closes(0, ClosesExit);
            else
                menu = new EndLevelMenu(
                    delegate ()
                    {
                        menu = mainMenu;
                    },
                    delegate ()
                    {
                        scene = new Closes(set, ClosesExit);
                        menu = null;
                    },
                    delegate ()
                    {
                        if (set < 11)
                        {
                            set++;
                            scene = new Closes(set, ClosesExit);
                            menu = null;
                        }
                        else
                            ToMainMenu();
                    });
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
                case (-1):
                    menu = mainMenu;
                    break;
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
                    scene = new DishesIntro(0, DishesExit);
                    break;
                case (4):
                    scene = new Ballons(0, BallonsExit);
                    break;
                case (5):
                    scene = new Houses(0, HousesExit);
                    break;
                case (6):
                    scene = new ClosesIntro(0, ClosesExit);
                    break;
            }
            if (i > -1)
            {
                showPanel = true;
                menu = null;
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            rand = new Random((int)DateTime.Now.Ticks);
            rwrong = new Sound[6];
            for (int i = 0; i < 6; i++)
                rwrong[i] = new Sound("Rus/Wrong" + i);
            bwrong = new Sound[6];
            for (int i = 0; i < 6; i++)
                bwrong[i] = new Sound("Bel/Wrong" + i);
            scene = new LangCase(0, delegate (int lang)
            {
                if (lang == 0)
                    language = "rus";
                else
                    language = "bel";
                menuPanel = new Sprite(null, "MenuPanel", 25, new Vector2(74, 1));
                    menuPanel.elements.Add(lang1 = new Button(menuPanel, language == "bel" ? "RusButton" : "BelButton", 5, new Vector2(1, 0.8f), new int[] { 1, 1, 1 }, "Lang", delegate
                    {
                        if (language == "rus")
                        {
                            lang1.ChangeTexture("RusButton");
                            lang2.ChangeTexture("RusButton");
                            language = "bel";
                        }
                        else
                        {
                            lang1.ChangeTexture("BelButton");
                            lang2.ChangeTexture("BelButton");
                            language = "rus";
                        }
                        if (scene != null)
                            scene = scene.GetReloadedScene();
                        if (menu != null)
                            menu = menu.GetReloadedMenu();
                    }));
                    lang2 = new Button(null, language == "bel" ? "RusButton" : "BelButton", 5, new Vector2(91, 1.8f), new int[] { 1, 1, 1 }, "Lang", delegate
                    {
                        if (language == "rus")
                        {
                            lang1.ChangeTexture("RusButton");
                            lang2.ChangeTexture("RusButton");
                            language = "bel";
                        }
                        else
                        {
                            lang1.ChangeTexture("BelButton");
                            lang2.ChangeTexture("BelButton");
                            language = "rus";
                        }
                        if (scene != null)
                            scene = scene.GetReloadedScene();
                        if (menu != null)
                            menu = menu.GetReloadedMenu();
                    });
                    menuPanel.elements.Add(new Button(menuPanel, "MenuButton", 5, new Vector2(7, 0.8f), new int[] { 1, 1, 1 }, "Menu", delegate { menu = mainMenu; }));
                    menuPanel.elements.Add(new Button(menuPanel, "SettingButton", 5, new Vector2(13, 0.8f), new int[] { 1, 1, 1 }, "Setting", delegate
                    {
                        menu = settingMenu;
                    }));
                    menuPanel.elements.Add(new Button(menuPanel, "RepeatTask", 5, new Vector2(19, 0.8f), new int[] { 1, 1, 1 }, "RepeatTask", delegate
                    {
                        if (TheGame.task != null)
                            EngineCore.PlaySound(TheGame.task);
                    }));
                    effect1 = EngineCore.content.Load<Effect>("Shaders/BlackWhite");
                    mainMenu = new MainMenu(
                        delegate
                        {
                            menu = new GamesMenu(Play);
                        },
                        delegate
                        {
                            menu = new SettingMenu(delegate { menu = mainMenu; });
                        }, 
                        delegate
                        {
                            menu = new Authors(delegate { menu = mainMenu; });
                        },
                        delegate
                        {
                            if (!(scene is VoidScene))
                                menu = null;
                        },
                        this.Exit);
                    ToMainMenu();
                    settingMenu = new SettingMenu(delegate
                    {
                        menu = null;
                    });
                });
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (menu == null)
            {
                scene.Update();
                if (!(scene is LangCase))
                    menuPanel.Update();
            }
            else
            {
                menu.Update();
                lang2.Update();
            }
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
            EngineCore.spriteBatch.Begin();
            if (!(scene is LangCase))
            {
                if (menu == null)
                    menuPanel.Draw();
                else
                    lang2.Draw();
            }
            EngineCore.spriteBatch.End();
            if (menu != null)
                menu.Draw();
            EngineCore.DrawCursor();
        }
    }
}
