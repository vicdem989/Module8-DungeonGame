using System.ComponentModel;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using Utils;
using static Utils.Output;

namespace Dungeon
{


    public class CharacterCreationScreen : GameEngine.IScene
    {
        #region Constants And Variables 
        Hero hero;

        #endregion


        #region GameEngine.IScene ------------------------------------------------------------------------------------------

        public Action<Type, Object[]> OnExitScreen { get; set; }

        public void init()
        {
            hero = new Hero();
            Console.Clear();
        }

        public void input()
        {

            Output.Write("Create your character!", true);


            while (hero.name == string.Empty)
            {
                Output.Write("Enter character name: ");
                hero.name = Console.ReadLine();
                Console.Clear();
            }

            Output.Write("Choose class: (Either Rogue or Warrior)");
            hero.choice = "rogue";



            //MenuScreen.Option1 = "Quit";
            //OnExitScreen(typeof(MenuScreen), null);


            OnExitScreen(typeof(DungeonGame), new object[] { hero });
        }

        public void update()
        {

        }

        public void draw()
        {

        }

        #endregion



    }

}