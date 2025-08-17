using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turn_Based_Game
{
    internal class Enemy
    {
        //Enemy Stats
        public string Username { get; set; }
        public string Weapon { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }
        public int Level { get; set; }

        public Enemy(string getEnemyName, string getEnemyWeapon, int hp, int atk, int def, int spd, int lvl)
        {
            Username = getEnemyName;
            Weapon = getEnemyWeapon;
            Health = hp;
            Attack = atk;
            Defence = def;
            Speed = spd;
            Level = lvl;
        }

        public void EnemyAttack()
        {
            Console.WriteLine($"The {Username} is attacking with the {Weapon}.");
        }
    }
}
