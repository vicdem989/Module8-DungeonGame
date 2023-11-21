using System.ComponentModel;
using Utils;
using static Utils.Output;

namespace Dungeon
{
    public class MenuScreen : GameEngine.IScene
    {
        #region Constants And Variables 
        const String NEW_GAME = "Start new Game";
        const String CONTINUE_GAME = "Continue Game";
        const string SETTINGS = "Settings";
        const string QUIT = "Quit";
        const int MENU_ITEM_WIDTH = 50;
        public static string[] mainMenuItems = { NEW_GAME, CONTINUE_GAME, SETTINGS, QUIT };
        int currentMenuIndex = 0;
        int startRow = 0;
        int startColumn = 0;
        public static int menuChange = 0;

        #endregion
        public Action<Type, Object[]> OnExitScreen { get; set; }
        public void init()
        {
            startRow = Console.WindowHeight / 2; ///TODO: fix, should not be static. 
            startColumn = 0;
        }
        public void input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey keyCode = Console.ReadKey(true).Key;
                if (keyCode == ConsoleKey.DownArrow)
                {
                    menuChange = 1;
                }
                else if (keyCode == ConsoleKey.UpArrow)
                {
                    menuChange = -1;
                }
                else if (keyCode == ConsoleKey.Enter)
                {

                    if (mainMenuItems[currentMenuIndex] == QUIT)
                    {
                        OnExitScreen(null, null);
                    }
                    else if (mainMenuItems[currentMenuIndex] == NEW_GAME)
                    {
                        //OnExitScreen(typeof(MasterMindGame), new object[] { GAME_TYPE.PLAYER_VS_NPC });
                        OnExitScreen(typeof(CharacterCreationScreen), null);
                    }
                    else if (mainMenuItems[currentMenuIndex] == SETTINGS)
                    {
                        OnExitScreen(typeof(Settings), null);
                    }
                }
            }
            else
            {
                menuChange = 0;
            }

        }
        public void update()
        {
            currentMenuIndex += menuChange;
            currentMenuIndex = Math.Clamp(currentMenuIndex, 0, mainMenuItems.Length - 1);
            menuChange = 0;
        }
        public void draw()
        {
            Console.WriteLine(ANSICodes.Positioning.SetCursorPos(startRow, startColumn));
            for (int index = 0; index < mainMenuItems.Length; index++)
            {
                if (index == currentMenuIndex)
                {
                    printActiveMenuItem($"* {mainMenuItems[index]} *");
                }
                else
                {
                    printMenuItem($"  {mainMenuItems[index]}  ");
                }
            }
        }
        void printActiveMenuItem(string item)
        {
            Output.Write(Reset(Bold(Align(item, Alignment.CENTER))), newLine: true);
        }
        void printMenuItem(string item)
        {
            Output.Write(Reset(Align(item, Alignment.CENTER)), newLine: true);
        }

    }
}