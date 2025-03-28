using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    internal class Character
    {
        private int currentHp;
        private readonly int maxHp;
        private readonly int attackPower;
        private readonly int healPower;
        private int healPotions;
        private readonly string name;
        private int mapPos;
        private readonly Random random;

        internal int CurrentHp { get { return this.currentHp; } }
        internal string Name { get { return this.name; } }
        internal int HealPotions { get { return this.healPotions; } set { this.healPotions = value; } }
        internal bool IsDead { get { return this.currentHp <= 0; } }
        internal int MapPos { get { return this.mapPos; } set { this.mapPos = value; } }

        internal Character(int maxHp, int attackPower, int healPower, int healPotions, string unitName, int mapPos)
        {
            this.maxHp = maxHp;
            this.currentHp = this.maxHp;
            this.attackPower = attackPower;
            this.healPower = healPower;
            this.healPotions = healPotions;
            this.name = unitName;
            this.random = new Random();
            this.mapPos = mapPos;
        }

        internal void Attack(ref Character opponent)
        {
            double rng = this.random.NextDouble();
            rng = rng / 2 + 0.82f;
            int randDamage = (int)(rng * this.attackPower);
            Console.WriteLine($"\n {this.name} deals {randDamage} damage.");
            opponent.currentHp -= randDamage;
            Console.WriteLine($" {opponent.name}'s health: {opponent.currentHp}.");
        }

        internal int Heal()
        {
            if (this.healPotions > 0)
            {
                double rng = this.random.NextDouble();
                rng = rng / 2 + 0.75f;
                int randHeal = (int)(rng * this.healPower);
                Console.WriteLine($"\n {this.name} heals {randHeal} health.");

                this.currentHp += randHeal;
                if (this.currentHp > this.maxHp) { this.currentHp = this.maxHp; }
                this.healPotions--;

                Console.WriteLine($" {this.name}'s health: {this.currentHp}. ({this.healPotions} heal potions left)");
                return 1;
            }
            Console.WriteLine($"\n {this.name} has no more heal potions to use.");
            return 0;
        }
    }
}
