

using Utils;

namespace Dungeon
{
    class ProgressItem
    {
        public Position Position { get; set; }
        public char Symbole { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Type { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public int Visits = 0;


        private string[] adventureGameCharacterNames = new string[] { "Tuk", "Iselda", "Little Foool", "Leg Eater" };
        private string[] adventureGameSurnames = new string[] { "The Great", "The Shy", "Ismerelda" };
        private string[] adventureGameSurDescriptions = new string[] {

            "\nHmmmmm... You seem to have at least some potential...\nHere are some stats so you dont... die...",
            "Just take the stats and leave me alone... God...",
            "Aaaah yeeaas.. Another one has come.. Another one will.. Fall.. \nOooooh.. yessss... Stats you shall recieve"
        };


        public ProgressItem MakeCopy()
        {
            return (ProgressItem)this.MemberwiseClone();
        }

        public void Rename()
        {
            Name = $"{adventureGameCharacterNames.RandomElement()} {adventureGameSurnames.RandomElement()}";
        }

        public void GetDescription()
        {
            Description = $"{adventureGameSurDescriptions.RandomElement()}";
        }

        


    }
}