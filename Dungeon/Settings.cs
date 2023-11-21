using System.ComponentModel;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using Utils;
using static Utils.Output;

namespace Dungeon
{
    public class Settings : GameEngine.IScene
    {
      

        #region GameEngine.IScene ------------------------------------------------------------------------------------------

        public Action<Type, Object[]> OnExitScreen { get; set; }

        public void init()
        {
        }

        public void input()
        {
            
        }

        public void update()
        {
           
            Console.WriteLine("LMAO");
        }

        public void draw()
        {
            
        }

        #endregion



    }

}