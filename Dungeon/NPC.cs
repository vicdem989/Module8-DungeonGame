

using Utils;

namespace Dungeon
{
    class Npc
    {
        public Position Position { get; set; }
        public char Symbole { get; set; }
        public string Name { get; set; }


        private string[] adventureGameCharacterNames = new string[] { "Aria", "Thorin", "Zara", "Jasper", "Elena", "Cedric", "Lyra", "Orion", "Seraphina", "Gareth" };
        private string[] adventureGameSurnames = new string[] { "the Brave", "Ironheart", "Shadowwalker", "the Wise", "Stormchaser", "Dragonbane", "Nightwhisper", "the Fearless", "Oakenshield", "Lightbringer" };
        private string[] attackMethods = { "slashes with", "throws the", "stabs using the" };

        public Enemy MakeCopy()
        {
            return (Enemy)this.MemberwiseClone();
        }

        public void Rename()
        {
            Name = $"{adventureGameCharacterNames.RandomElement()} {adventureGameSurnames.RandomElement()}";
        }


        

    }
}