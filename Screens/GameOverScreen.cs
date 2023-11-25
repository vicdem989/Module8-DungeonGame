using System.ComponentModel;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using Utils;
using static Utils.Output;

namespace Dungeon
{
    public class GameOverScreen : GameEngine.IScene
    {

        #region Variables 

        private bool back = false;
        private bool dirty = true;
        Hero hero;
        public static bool won = false;

        #endregion


        #region GameEngine.IScene ------------------------------------------------------------------------------------------

        public Action<Type, Object[]> OnExitScreen { get; set; }

        public GameOverScreen(Hero hero)
        {
            this.hero = hero;
        }


        public void init()
        {
            Console.Clear();
            this.hero = hero;
            if(won) {
                Output.Write($"{ANSICodes.Colors.Green}{ANSICodes.Effects.Bold}You Won!{ANSICodes.Reset}", true);
            } else {
                Output.Write($"{ANSICodes.Colors.Red}{ANSICodes.Effects.Bold}You Lost!{ANSICodes.Reset}", true);
            }
            Console.WriteLine("Stats:", true);
        }

        public void input()
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey();
                back = true;
            }
        }

        public void update()
        {
            if (back)
            {
                Console.Clear();
                OnExitScreen(typeof(MenuScreen), null);
            }
        }

        public void draw()
        {
            if (dirty)
            {
                dirty = false;
                Output.Write($"Level gained: {hero.Level}", true);
                Output.Write($"Total gold gained: {hero.Gold}", true);
                Output.Write($"Total strength gained: {hero.Strength}", true);
            }
        }

        #endregion



    }

}