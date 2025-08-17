using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turn_Based_Game
{
    internal class Player
    {
        //Player stats
        public string Username { get; set; }
        public string Weapon { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Gold { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }

        public Player(string nickname , string armory, int health, int coins, int atk, int def, int spd, int lvl, int exp)
        {
            Username = nickname;
            Weapon = armory;
            Health = health;
            MaxHealth = health;
            Gold = coins;
            Attack = atk;
            Defence = def;
            Speed = spd;
            Level = lvl;
            Experience = exp;
        }

        public void Info()
        {
            Console.WriteLine("Well Done on making your character.\nHere is all of your character's information:");
            Console.WriteLine($"Name: {Username}\nLevel: {Level}\nExperience: {Experience}\nWeapon: {Weapon}\nGold: {Gold}\nHealth: {Health}\nAttack: {Attack}\nDefence {Defence}\n" +
                $"Speed: {Speed}");
            Console.WriteLine("Pless any key to continue...");
        }
    }
}
