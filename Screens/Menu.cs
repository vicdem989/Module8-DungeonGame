using System.ComponentModel;
using Utils;
using static Utils.Output;

namespace Dungeon
{
    public class MenuScreen : GameEngine.IScene
    {
        #region Constants And Variables 
        public static string Option1= "New Game";
        public static string Option2 = "Continue Game";
        public static string Option3 = "Settings";
        public static string Option4 = "Quit";
        const int MENU_ITEM_WIDTH = 50;
        public static string[] menuItems;// = { Option1, Option2, Option3, Option4 };
        int currentMenuIndex = 0;
        int startRow = 0;
        int startColumn = 0;
        public static int menuChange = 0;

        #endregion
        public Action<Type, Object[]> OnExitScreen { get; set; }
        public void init()
        {
            menuItems = new string[] {Option1, Option2, Option3, Option4 };
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

                    if (menuItems[currentMenuIndex] == "Quit")
                    {
                        OnExitScreen(null, null);
                    }
                    else if (menuItems[currentMenuIndex] == "New Game")
                    {
                        //OnExitScreen(typeof(MasterMindGame), new object[] { GAME_TYPE.PLAYER_VS_NPC });
                        OnExitScreen(typeof(CharacterCreationScreen), null);
                    }
                    else if (menuItems[currentMenuIndex] == "Settings")
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
            currentMenuIndex = Math.Clamp(currentMenuIndex, 0, menuItems.Length - 1);
            menuChange = 0;
        }
        public void draw()
        {
            Console.WriteLine(ANSICodes.Positioning.SetCursorPos(startRow, startColumn));
            for (int index = 0; index < menuItems.Length; index++)
            {
                if (index == currentMenuIndex)
                {
                    printActiveMenuItem($"* {menuItems[index]} *");
                }
                else
                {
                    printMenuItem($"  {menuItems[index]}  ");
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