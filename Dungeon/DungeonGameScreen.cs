


using System.ComponentModel;
using System.Dynamic;
using System.Formats.Asn1;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Utils;

namespace Dungeon
{
    public class Position
    {
        public int row;
        public int column;
    }

    class Attack
    {
        public String Description { get; set; }
        public int Damage { get; set; }
    }



    class Item
    {

        public string symbole { get; }
        public string description { get; }
        public int value { get; }

        public Item(string itemType)
        {

            if (itemType == "$")
            {
                symbole = itemType;
                value = new Random().Next(5, 21);
                description = $"A small bag of loot with the value {ANSICodes.Colors.Yellow}{value}{ANSICodes.Reset}";
                DungeonGame.addToGold = value;
            }
            else if (itemType == "*")
            {
                Hero hero = new Hero();
                symbole = itemType;
                value = new Random().Next(3, 8);
                description = $"A smal vial of {ANSICodes.Colors.Green}poison{ANSICodes.Reset}, doing {value} points of damage!";
                hero.DebuffType = "Poison";
                hero.DebuffDuration = 5;
                hero.DebuffDamage = value;
                hero.HP -= value;
            }
            else if (itemType == "?")
            {
                symbole = itemType;
                description = $"An old scroll that will provide you with riches so grand\nTake you further than you could ever imagine\nBut there seems to be too many enemies around ..";
            }

        }

    }

    public class DungeonGame : GameEngine.IScene
    {

        #region Variable Declaration

        const char HERO_CHAR = 'H';

        static Dictionary<char, Object> GAME_ITEMS = new(){
            {' ', " "},
            {'█', "█"},
            {'X', new Enemy(){Symbole = 'X', Name = "Axe Wielder", Weapon = "Axe", HitPoints = 10, Strength= 12} },
            {'D', new Enemy(){Symbole = 'D', Name = "Ranged attacker", Weapon = "Bow", HitPoints = 6, Strength= 10} },
            {'B', new Enemy(){Symbole = 'B', Name = "Boss", Weapon = "Bow", Boss = true, HitPoints = 20, Strength= 16} },
            {'N', new Npc(){Symbole = 'N', Name = "Npc" } },
            {'$', new Item("$")},
            {'*', new Item("*")},
            {'?', new Item("?")},
            {HERO_CHAR, null}
        };


        Hero hero;
        Position heroPos;
        bool playerMoved = false;
        bool dirty = true;
        int delta_y;
        int delta_x;

        public static int currentDmg;
        public static int addToGold;
        public static int addToXP;
        public static bool EnemyDead = false;

        public static int amountEnemies = 0;


        object[][] levelMap;

        string eventMessage;

        #endregion

        public DungeonGame(Hero hero)
        {
            this.hero = hero;
            GAME_ITEMS[HERO_CHAR] = hero;
            levelMap = LoadLevel("levels/level1.txt");
        }

        #region Dungeon Game Functions ----------------------------------------------------------------------------------

        object[][] LoadLevel(string levelFile)
        {
            string rawFileData = HelperFunctions.ReadFile(levelFile);
            char[][] tempMap = HelperFunctions.Create2DArrayFromMultiLineString(rawFileData);
            object[][] outputMap = new object[tempMap.Length][];



            for (int row = 0; row < tempMap.Length; row++)
            {
                outputMap[row] = new object[tempMap[row].Length];
                for (int column = 0; column < tempMap[row].Length; column++)
                {
                    char key = tempMap[row][column];
                    if (GAME_ITEMS.ContainsKey(key))
                    {
                        Object item = GAME_ITEMS[key];
                        if (item.GetType() == typeof(Enemy))
                        {
                            // Create a new enemy to put into the map.
                            Enemy e = ((Enemy)item).MakeCopy();
                            e.Rename(); // Give ghe enemy a cool name. 
                            e.Hp = RandomValueDecider(e.HitPoints);
                            e.XP = RandomValueDecider(e.HitPoints * 2);
                            e.Position = new Position() { row = row, column = column }; // Save the map position in the enemy
                            amountEnemies++;
                            outputMap[row][column] = e; // Put the enemy into that map position. 
                        }
                        else if (item.GetType() == typeof(Npc))
                        {

                            Npc n = ((Npc)item).MakeCopy();
                            n.Rename();
                            n.GiveType();
                            n.GiveExperience();
                            n.GetDescription();
                            outputMap[row][column] = n;
                        }
                        else if (item.GetType() == typeof(Hero))
                        {
                            heroPos = new() { row = row, column = column };
                            outputMap[row][column] = item;
                        }
                        else
                        {
                            // Item dos not requier or have anny special thing we need to do.
                            outputMap[row][column] = item;
                        }
                    }
                }
            }

            return outputMap;
        }

        private int RandomValueDecider(int maxValueCeiling)
        {
            Random rnd = new Random();
            int minValue = maxValueCeiling / 2;
            int maxvalue = maxValueCeiling;
            return rnd.Next(minValue, maxvalue);
        }

        string GetDisplaySymboleFor(object item)
        {

            if (item.GetType() == typeof(Enemy))
            {
                return $"{((Enemy)item).Symbole}";
            }
            else if (item.GetType() == typeof(Hero))
            {
                return $"{HERO_CHAR}";
            }
            else if (item.GetType() == typeof(Npc))
            {
                return $"{((Npc)item).Symbole}";
            }
            else if (item.GetType() == typeof(Item))
            {
                return $"{((Item)item).symbole}";
            }

            return $"{item}";
        }

        bool isThisAEnemy(object mappLocationItem)
        {
            if (mappLocationItem != null)
            {
                return mappLocationItem.GetType() == typeof(Enemy);
            }
            return false;
        }


        #endregion -----------------------------------------------------------------------------------------------------

        #region GameEngine.IScene --------------------------------------------------------------------------------------

        public Action<Type, object[]> OnExitScreen { get; set; }

        public void init()
        {

        }

        public void input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey keyCode = Console.ReadKey(true).Key;
                if (keyCode == ConsoleKey.DownArrow || keyCode == ConsoleKey.S)
                {
                    delta_y = 1;

                }
                else if (keyCode == ConsoleKey.UpArrow || keyCode == ConsoleKey.W)
                {
                    delta_y = -1;
                }

                if (keyCode == ConsoleKey.RightArrow || keyCode == ConsoleKey.D)
                {
                    delta_x = 1;

                }
                else if (keyCode == ConsoleKey.LeftArrow || keyCode == ConsoleKey.A)
                {
                    delta_x = -1;
                }

                playerMoved = delta_y != 0 || delta_x != 0;
            }
        }

        public void update()
        {
            
            if (playerMoved)
            {
                
                Console.WriteLine(amountEnemies);
                playerMoved = false;

                int newRow = heroPos.row + delta_y;
                int newCol = heroPos.column + delta_x;

                char newLocationDisplayChar = GetDisplaySymboleFor(levelMap[newRow][newCol])[0];
                object locationItem = levelMap[newRow][newCol];

                // Now we ditermin what happens if we enter the new location.
                if (newLocationDisplayChar == ' ')
                {
                    levelMap[heroPos.row][heroPos.column] = ' ';
                    levelMap[newRow][newCol] = hero;
                    heroPos.row = newRow;
                    heroPos.column = newCol;
                }
                else if (isThisAEnemy(locationItem) == true)
                {
                    if (((Enemy)locationItem).Boss)
                    {
                        
                    }
                    else
                    {
                        eventMessage = ((Enemy)locationItem).Attack().Description;
                    }
                    hero.HP -= currentDmg;



                    if (EnemyDead)
                    {
                        levelMap[heroPos.row][heroPos.column] = ' ';
                        heroPos.row = newRow;
                        heroPos.column = newCol;
                        hero.Gold += addToGold;
                        hero.XP += addToXP;
                        levelMap[newRow][newCol] = hero;
                        EnemyDead = false;
                    }

                }
                else if (newLocationDisplayChar == '$')
                {
                    levelMap[heroPos.row][heroPos.column] = ' ';
                    heroPos.row = newRow;
                    heroPos.column = newCol;
                    eventMessage = ((Item)levelMap[newRow][newCol]).description;
                    hero.Gold += addToGold;
                    levelMap[newRow][newCol] = hero;
                }
                else if (newLocationDisplayChar == '*')
                {
                    levelMap[heroPos.row][heroPos.column] = ' ';
                    heroPos.row = newRow;
                    heroPos.column = newCol;
                    eventMessage = ((Item)levelMap[newRow][newCol]).description;
                    hero.Gold += addToGold;
                    levelMap[newRow][newCol] = hero;
                }
                else if (newLocationDisplayChar == '?')
                {
                    levelMap[heroPos.row][heroPos.column] = ' ';
                    heroPos.row = newRow;
                    heroPos.column = newCol;
                    eventMessage = ((Item)levelMap[newRow][newCol]).description;
                    levelMap[newRow][newCol] = hero;
                }
                else if (newLocationDisplayChar == 'N')
                {
                    ((Npc)levelMap[newRow][newCol]).Visits++;
                    eventMessage = $"{ANSICodes.Effects.Bold}" + ((Npc)levelMap[newRow][newCol]).Name + " a " + ((Npc)levelMap[newRow][newCol]).Experience + " " + ((Npc)levelMap[newRow][newCol]).Type + $"{ANSICodes.Reset}\n" + ((Npc)levelMap[newRow][newCol]).Description;
                    if (((Npc)levelMap[newRow][newCol]).Visits == 1)
                    {
                        string type = String.Empty;
                        int stats = ((Npc)levelMap[newRow][newCol]).GetStats(hero.Level);
                        if (((Npc)levelMap[newRow][newCol]).Type == Npc.TYPEMERCHANT)
                        {
                            hero.Gold += stats;
                            type = "gold";
                        }
                        else if (((Npc)levelMap[newRow][newCol]).Type == Npc.TYPEWIZARD)
                        {

                            type = "error. type not yet implemented";
                        }
                        else if (((Npc)levelMap[newRow][newCol]).Type == Npc.TYPEWARRIOR)
                        {
                            hero.Strength += stats;
                            type = "strength";
                        }
                        eventMessage += $"\n{ANSICodes.Colors.Magenta}+{stats}{ANSICodes.Reset} {type}!";
                    }
                    else if (((Npc)levelMap[newRow][newCol]).Visits > 1)
                    {
                        eventMessage = "Screw this, I AM OUT!";
                        levelMap[heroPos.row][heroPos.column] = ' ';
                        heroPos.row = newRow;
                        heroPos.column = newCol;
                        levelMap[newRow][newCol] = hero;
                    }
                }

                if (hero.HP <= 0)
                {
                    Console.Clear();
                    OnExitScreen(typeof(GameOverScreen), new object[] { hero });
                }

                if (amountEnemies == 0)
                {
                    levelMap = LoadLevel("levels/level2.txt");
                    eventMessage = $"{ANSICodes.Effects.Bold}{ANSICodes.Colors.Red}The scroll has summoned you! Defeat the boss..\nAnd the riches are yours!{ANSICodes.Reset}";
                }
                delta_y = 0;
                delta_x = 0;
                dirty = true;
            }


        }

        public void draw()
        {


            if (dirty)
            {
                dirty = false;
                string output = "";

                Console.Clear();
                int sRow = 1;
                int sCol = 0;

                for (int row = 0; row < levelMap.Length; row++)
                {
                    output += ANSICodes.Positioning.SetCursorPos(sRow + row, sCol);
                    for (int col = 0; col < levelMap[row].Length; col++)
                    {
                        output += GetDisplaySymboleFor(levelMap[row][col]);
                    }
                }


                Console.Write(output);

                if (eventMessage != "")
                {
                    Console.WriteLine($"\n{eventMessage}");
                }

                Console.Write(ANSICodes.Positioning.SetCursorPos(sRow, sCol));
                Console.Write(ANSICodes.Colors.Yellow);
                Console.Write(hero);
                Console.Write(ANSICodes.Reset);

            }


        }

        #endregion ---------------------------------------------------------------------------------------------------------


    }


}