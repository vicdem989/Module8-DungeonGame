using System.ComponentModel;

namespace Dungeon
{
    public class Hero
    {
        public string name { get; set; } = string.Empty;
        public string choice { get; set; } = string.Empty;

        public int Level { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }


        public int XP { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }

        public string DebuffType { get; set; } = string.Empty;
        public int DebuffDuration { get; set; }
        public int DebuffDamage { get; set; }

        public int AvailablePoints { get; set; }


        public Hero()
        {
            Level = 1;
            AvailablePoints = 20;
            Strength = 5;
            if (choice == "rogue")
            {
                HP = 25;
            }
            else if (choice == "warrior")
            {
                HP = 50;
            }
            else
            {
                HP = 25;
            }

        }

        public override string ToString()
        {
            if (DebuffType != String.Empty)
                return $"XP ({XP})\nHP ({HP})\nGOLD ({Gold})\nDebuff Type ({DebuffType})\nDuration ({DebuffDuration})\nDamage ({DebuffDamage})";

            return $"XP ({XP})\nHP ({HP})\nGOLD ({Gold})";
        }

    }

}