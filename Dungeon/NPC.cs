

using Utils;

namespace Dungeon
{
    class Npc
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

        public const string TYPEMERCHANT = "Merchant";
        public const string TYPEWARRIOR = "Warrior";
        public const string TYPEWIZARD = "Wise";

        private string[] TypesOfNPC = new string[] { TYPEMERCHANT, TYPEWARRIOR, TYPEWIZARD };
        private const string EXPERIENCEBEGINNER = "beginner";
        private const string EXPERIENCENOTSOBAD = "not so bad";
        private const string EXPERIENCEMAGNIFICENT = "magnificent";
        private string[] ExperienceLevel = new string[] { EXPERIENCEBEGINNER, EXPERIENCENOTSOBAD, EXPERIENCEMAGNIFICENT };

        public Npc MakeCopy()
        {
            return (Npc)this.MemberwiseClone();
        }

        public void Rename()
        {
            Name = $"{adventureGameCharacterNames.RandomElement()} {adventureGameSurnames.RandomElement()}";
        }
        public void GiveType()
        {
            Type = $"{TypesOfNPC.RandomElement()}";
        }

        public void GiveExperience()
        {
            Experience = $"{ExperienceLevel.RandomElement()}";
        }

        public void GetDescription()
        {
            Description = $"{adventureGameSurDescriptions.RandomElement()}";
        }

        public int GetStats(int heroLevel)
        {

            //Percentage change of getting higher experience based on hero level

            Random rnd = new Random();
            int minValue = 0;
            int maxValue = 0;

            if (Experience == EXPERIENCEBEGINNER)
            {
                minValue = rnd.Next(1, 3);
                maxValue = rnd.Next(5, 9);
            }
            else if (Experience == EXPERIENCENOTSOBAD)
            {
                minValue = rnd.Next(5, 9);
                maxValue = rnd.Next(13, 17);
            }
            else if (Experience == EXPERIENCEMAGNIFICENT)
            {
                minValue = rnd.Next(19, 24);
                maxValue = rnd.Next(27, 31);
            }

            return rnd.Next(minValue, maxValue);
        }


    }
}