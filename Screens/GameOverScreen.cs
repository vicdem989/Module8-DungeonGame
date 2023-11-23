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
            Console.WriteLine(Output.Align("Stats:", Alignment.CENTER));
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
                Output.Write(Output.Align($"Level gained: {hero.Level}", Alignment.CENTER));
                Output.Write(Output.Align($"Total gold gained: {hero.Gold}", Alignment.CENTER));
                Output.Write(Output.Align($"Total strength gained: {hero.Strength}", Alignment.CENTER));
            }
        }

        #endregion



    }

}