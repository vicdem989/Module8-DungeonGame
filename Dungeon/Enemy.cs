

using Utils;

namespace Dungeon
{
    class Enemy
    {
        public Position Position { get; set; }
        public char Symbole { get; set; }
        public string Name { get; set; }
        public bool Boss { get; set; }
        public string Weapon { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Hp { get; set; }
        public int XP { get; set; }

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


        public Attack Attack()
        {
            Hero hero = new Hero();
            string method = attackMethods.RandomElement(); // Note: I am using an extension I added to the array type in utils/array.cs to do this.
            Random rnd = new Random();
            int damage = rnd.Next(0, HitPoints + 1);
            string description = $"{Name} {Hp} {method} {Weapon}, causing {ANSICodes.Colors.Red}{damage}{ANSICodes.Reset} damage to you";
            if (damage == 0) // Epic failure 
            {
                description = $"{Name} {Hp} trys to {method} {Weapon}, but misses fantasticly";
            }
            else if (damage == HitPoints) // Critical hit
            {
                description = $"{Name} {Hp}  lunges and {method} {Weapon}, causing {ANSICodes.Colors.Red}{ANSICodes.Effects.Bold}massive{ANSICodes.Reset} damage to you";
            }
            Hp -= hero.Strength;
            int goldDrop = rnd.Next(1, HitPoints);
            if (Hp <= 0)
            {
                description = $"You {ANSICodes.Colors.Red}killed {Name}{ANSICodes.Reset}!\n{Name} dropped {ANSICodes.Colors.Yellow}{goldDrop}{ANSICodes.Reset} gold!";
                DungeonGame.amountEnemies--;
                DungeonGame.addToGold = goldDrop;
                DungeonGame.addToXP = XP;
                DungeonGame.EnemyDead = true;
            }
            else if (Hp > 0)
            {

                description += $"\nYou manage to damage {Name}, dealing {hero.Strength} damage! {Name} only has {Hp} hp left!";
            }



            DungeonGame.currentDmg = damage;
            return new Dungeon.Attack() { Description = description, Damage = damage };
        }

    }
}