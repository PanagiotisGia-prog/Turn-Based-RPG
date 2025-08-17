using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace Turn_Based_Game
{
    internal class Program
    {
        static Random rng = new Random();
        static int availablePoints = 20;
        static string? username = "";
        static string? weapon = "";
        static int life = 20;
        static int gold = 100;
        static int attack = 0;
        static int defend = 0;
        static int speed = 0;
        static int level = 1;
        static int experience = 0;
        static Dictionary<string, int> items = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            Player player = new Player(username = "", weapon = "", life, gold, attack, defend, speed, level, experience);
            CharacterCreation(player);
            Console.Clear();
            SpendPoints(player);
            Console.Clear();
            Summary(player);

            Enemy goblin = new Enemy("Goblin", "club", 10, 6, 2, 8, 1);
            Enemy skeleton = new Enemy("Skeleton", "sword", 13, 10, 7, 8, 1);
            Enemy bandit = new Enemy("Bandit", "crossbow", 20, 8, 8, 10, 1);

            new Program().GameInit(player, goblin, skeleton, bandit);

            Console.Clear();
            Console.WriteLine("Thank you for playing! This is a very early demo of my turn-based RPG project." +
                " I appreciate your interest and feedback. The game is still in its early stages, I made it as an exercise in my free time and there is much more to come!");

            Console.ReadKey();
        }
        public void GameInit(Player player, Enemy goblin, Enemy skeleton, Enemy bandit)
        {
            bool gameState = true;
            int curGoblinHp = goblin.Health;
            int curSkeletonHp = skeleton.Health;
            int curBanditHp = bandit.Health;
            Console.WriteLine("Now that you are all set up it's time to begin your adventure");
            Console.WriteLine("You can choose to either go to an adventure or shop some items or even rest to regain your full health");
            while (gameState)
            {
                Console.WriteLine("What would you like to do:");
                Console.WriteLine("1. Go to an adventure (This choice will initiate the combat)\n2. Buy some items\n3. Rest (Health will fully recover)\n4. Character's Info\n5. Exit Game");
                string? playerChoose = Console.ReadLine()?.ToLower();
                if (playerChoose == "")
                {
                    Console.WriteLine("Ok looks like you need some time to decide...");
                    Thread.Sleep(1000);
                    Console.WriteLine("You ready? Great let's go one more time");
                    continue;
                }
                else
                {
                    switch (playerChoose)
                    {
                        case "1":
                        case "adventure":
                        case "go to an adventure":
                        case "go to a adventure": //In case the player misspels
                            Console.Clear();
                            if (goblin.Health <= 0)
                                goblin.Health = curGoblinHp;
                            if (skeleton.Health <= 0)
                                skeleton.Health = curSkeletonHp;
                            if (bandit.Health <= 0)
                                bandit.Health = curBanditHp;
                            Console.WriteLine("You decide to go to an adventure");
                            Console.WriteLine("You pick a mission from th Adventurers Guild and leave town");
                            Console.WriteLine("On your way you encounter an enemy");
                            int enemyAppear = rng.Next(1, 3 + 1);
                            switch (enemyAppear)
                            {
                                case 1:
                                    Console.WriteLine("A Goblin stands in front of your way. He is armed with a club and is ready to attack.");
                                    while (player.Health > 0 && goblin.Health > 0)
                                    {
                                        if (player.Speed > goblin.Speed)
                                        {
                                            Console.WriteLine($"{username} Health: {player.Health}");
                                            PlayerActionTurn(player, goblin);
                                            EnemyActionTurn(player, goblin);
                                            if (player.Health <= 0)
                                            {
                                                Console.WriteLine("You have been defeated...");
                                                Console.WriteLine("Game Over!");
                                                gameState = false;
                                            }
                                            else if (goblin.Health <= 0)
                                            {
                                                Console.WriteLine($"You've defeated the goblin");
                                                player.Experience += 150;
                                                player.Gold += 50;
                                                if (player.Experience >= 500)
                                                {
                                                    player.Level++;
                                                    Console.WriteLine("You've gained a level. Congratulations!");
                                                    Console.WriteLine($"You've got {player.Gold} gold");
                                                    gameState = false;
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"You've gained {player.Experience} exp and {player.Gold} gold!");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{username} Health: {player.Health}");
                                            EnemyActionTurn(player, goblin);
                                            PlayerActionTurn(player, goblin);
                                            if (player.Health <= 0)
                                            {
                                                Console.WriteLine("You have been defeated...");
                                                Console.WriteLine("Game Over!");
                                                gameState = false;
                                            }
                                            else if (goblin.Health <= 0)
                                            {
                                                Console.WriteLine($"You've defeated the goblin");
                                                player.Experience += 150;
                                                player.Gold += 50;
                                                if (player.Experience >= 500)
                                                {
                                                    player.Level++;
                                                    Console.WriteLine("You've gained a level. Congratulations!");
                                                    Console.WriteLine($"You've got {player.Gold} gold");
                                                    gameState = false;
                                                    return;
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"You've gained {player.Experience} exp and {player.Gold} gold!");
                                                }
                                            }
                                        }
                                    }
                                    break;

                                case 2:
                                    Console.WriteLine("A Skeleton stands in front of your way. He is armed with a sword and shield and is ready to attack.");
                                    while (player.Health > 0 && skeleton.Health > 0)
                                    {
                                        if (player.Speed > skeleton.Speed)
                                        {
                                            Console.WriteLine($"{username} Health: {player.Health}");
                                            PlayerActionTurn(player, skeleton);
                                            EnemyActionTurn(player, skeleton);
                                            if (player.Health <= 0)
                                            {
                                                Console.WriteLine("You have been defeated...");
                                                Console.WriteLine("Game Over!");
                                                gameState = false;
                                            }
                                            else if (skeleton.Health <= 0)
                                            {
                                                Console.WriteLine($"You've defeated the skeleton");
                                                player.Experience += 200;
                                                player.Gold += 70;
                                                if (player.Experience >= 500)
                                                {
                                                    player.Level++;
                                                    Console.WriteLine("You've gained a level. Congratulations!");
                                                    Console.WriteLine($"You've got {player.Gold} gold");
                                                    gameState = false;
                                                    return;
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"You've gained {player.Experience} exp and {player.Gold} gold!");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{username} Health: {player.Health}");
                                            EnemyActionTurn(player, skeleton);
                                            PlayerActionTurn(player, skeleton);
                                            if (player.Health <= 0)
                                            {
                                                Console.WriteLine("You have been defeated...");
                                                Console.WriteLine("Game Over!");
                                                gameState = false;
                                            }
                                            else if (skeleton.Health <= 0)
                                            {
                                                Console.WriteLine($"You've defeated the skeleton");
                                                player.Experience += 200;
                                                experience = player.Experience;
                                                player.Gold += 70;
                                                gold = player.Gold;
                                                if (player.Experience >= 500)
                                                {
                                                    player.Level++;
                                                    Console.WriteLine("You've gained a level. Congratulations!");
                                                    Console.WriteLine($"You've got {player.Gold} gold");
                                                    gameState = false;
                                                    return;
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"You've gained {player.Experience} exp and {player.Gold} gold!");
                                                }
                                            }
                                        }
                                    }
                                    break;

                                case 3:
                                    Console.WriteLine("A Bandit stands in front of your way. He is armed with a crossbow and is ready to attack.");
                                    while (player.Health > 0 && bandit.Health > 0)
                                    {
                                        if (player.Speed > bandit.Speed)
                                        {
                                            Console.WriteLine($"{username} Health: {player.Health}");
                                            PlayerActionTurn(player, bandit);
                                            EnemyActionTurn(player, bandit);
                                            if (player.Health <= 0)
                                            {
                                                Console.WriteLine("You have been defeated...");
                                                Console.WriteLine("Game Over!");
                                                gameState = false;
                                            }
                                            else if (bandit.Health <= 0)
                                            {
                                                Console.WriteLine($"You've defeated the bandit");
                                                player.Experience += 250;
                                                player.Gold += 150;
                                                if (player.Experience >= 500)
                                                {
                                                    player.Level++;
                                                    Console.WriteLine("You've gained a level. Congratulations!");
                                                    Console.WriteLine($"You've got {player.Gold} gold");
                                                    gameState = false;
                                                    return;
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"You've gained {player.Experience} exp and {player.Gold} gold!");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{username} Health: {player.Health}");
                                            EnemyActionTurn(player, bandit);
                                            PlayerActionTurn(player, bandit);
                                            if (player.Health <= 0)
                                            {
                                                Console.WriteLine("You have been defeated...");
                                                Console.WriteLine("Game Over!");
                                                gameState = false;
                                            }
                                            else if (bandit.Health <= 0)
                                            {
                                                Console.WriteLine($"You've defeated the bandit");
                                                player.Experience += 250;
                                                player.Gold += 150;
                                                if (player.Experience >= 500)
                                                {
                                                    player.Level++;
                                                    Console.WriteLine("You've gained a level. Congratulations!");
                                                    Console.WriteLine($"You've got {player.Gold} gold");
                                                    gameState = false;
                                                    return;
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"You've gained {player.Experience} exp and {player.Gold} gold!");
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                            break;

                        case "2":
                        case "items":
                        case "item":
                        case "shop":
                        case "buy some items":
                        case "buy some item":
                            Console.Clear();
                            Shop(player);
                                break;

                        case "3":
                        case "rest":
                            Console.Clear();
                            Console.WriteLine("You find an Inn to rest.");
                            player.Health = player.MaxHealth;
                            break;

                        case "4":
                        case "info":
                        case "character":
                        case "information":
                        case "character information":
                        case "character's information":
                            Console.Clear();
                            Summary(player);
                            break;

                        case "5":
                        case "exit":
                        case "exit game":
                            Console.Clear();
                            Console.WriteLine("Exiting the game!");
                            return;

                        default:
                            Console.WriteLine("Invalid action please choose only the choices that you were given.");
                            continue;
                    }
                }
            }
        }
        public static void CharacterCreation(Player player)
        {
            bool chooseName = true;
            bool chooseWeapon = true;
            Console.WriteLine("What's your username: ");
 
            while (chooseName == true)
            {
                username = Console.ReadLine();
                if (username == "")
                {
                    Console.WriteLine("You need to choose a name!");
                    continue;
                }
                else
                {
                    Console.WriteLine($"{username} please choose your weapon: 1.Sword and Shield 2.Magic Rod 3.Bow");
                    chooseName = false;
                }
            }

            while (chooseWeapon == true)
            {
                weapon = Console.ReadLine()?.ToLower();
                if (weapon == "")
                {
                    Console.WriteLine("Don;t you know that it's dangerous to go alone? Choose a weapon!");
                    continue;
                }
                else
                {
                    switch (weapon)
                    {
                        case "1":
                        case "sword and shield":
                        case "sword & shield":
                        case "sword shield":
                        case "sword":
                        case "shield":
                            Console.WriteLine("You chose the Sword and the Shield. Excellent choice!");
                            player.Attack += 10;
                            player.Defence += 15;
                            weapon = "Sword and Shield";
                            break;

                        case "2":
                        case "magic rod":
                        case "magic":
                        case "rod":
                            Console.WriteLine("You chose the Magic Rod. Excellent choice!");
                            player.Attack += 20;
                            player.Defence += 5;
                            weapon = "Magic Rod";
                            break;

                        case "3":
                        case "bow":
                            Console.WriteLine("You chose the Bow. Excellent choice!");
                            player.Attack += 8;
                            player.Speed += 15;
                            weapon = "Bow";
                            break;

                        default:
                            Console.WriteLine("Choose only one of the weapons that I mention");
                            continue;
                    }
                }
                chooseWeapon = false;
            }
        }
        public static void SpendPoints(Player player) 
        {
            while (availablePoints != 0)
            {
                Console.WriteLine($"You have {availablePoints} points left.");
                Console.WriteLine($"1. Attack: {player.Attack}\n2. Defence: {player.Defence}\n3. Speed {player.Speed}\n4. Health: {player.Health}");
                string? pointsSpent = Console.ReadLine()?.ToLower().Trim();
                if (pointsSpent == "")
                {
                    Console.WriteLine("Please choose a skill to spent your points");
                    continue;
                }
                else
                {
                    switch (pointsSpent)
                    {
                        case "1":
                        case "attack":
                        case "atk":
                            player.Attack++;
                            availablePoints--;
                            Console.Clear();
                            break;

                        case "2":
                        case "defence":
                        case "def":
                            player.Defence++;
                            availablePoints--;
                            Console.Clear();
                            break;

                        case "3":
                        case "speed":
                        case "spd":
                            player.Speed++;
                            availablePoints--;
                            Console.Clear();
                            break;

                        case "4":
                        case "health":
                        case "life":
                            player.MaxHealth = player.Health;
                            player.Health++;
                            availablePoints--;
                            Console.Clear();
                            break;

                        default:
                            Console.WriteLine("Invalid skill. Please select only one of the skills you were given");
                            Console.Clear();
                            continue;
                    }
                }
            }
        } 
        public static void Summary(Player player)
        {
            Console.WriteLine("Well Done on making your character.\nHere is all of your character's information:");
            Console.WriteLine($"Name: {username}\nLevel: {player.Level}\nExperience: {player.Experience}\nWeapon: {weapon}\nGold: {gold}\nHealth: {player.Health}\nAttack: {player.Attack}\nDefence {player.Defence}\n" +
                $"Speed: {player.Speed}");
            Console.WriteLine("Pless any key to continue...");
        }
        public void PlayerActionTurn(Player player, Enemy enemy)
        {
            while (true)
            {
                Console.WriteLine("It's your turn.");
                Console.WriteLine("What would you do?");
                Console.WriteLine("1. Attack\n2. Guard\n3. Items");
                string? choice = Console.ReadLine()?.ToLower();

                if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.WriteLine("You need to choose an action");
                    continue;
                }

                switch (choice)
                {
                    case "1":
                    case "attack":
                        Console.WriteLine($"{username} attacks with the {weapon}");
                        int success = rng.Next(1, 20 + 1 );
                        if (success >= 10)
                        {
                            if (player.Attack >= 30)
                            {
                                if (player.Attack < enemy.Defence)
                                {
                                    int damage = rng.Next(12, 15);
                                    int totalDamage = damage;
                                    Console.WriteLine($"{username} deals {totalDamage} damage");
                                    enemy.Health -= totalDamage;
                                }
                                else
                                {
                                    int damage = rng.Next(15, 20);
                                    int totalDamage = damage;
                                    Console.WriteLine($"{username} deals {totalDamage} damage");
                                    enemy.Health -= totalDamage;
                                }
                            } 
                            else if (player.Attack >= 20)
                            {
                                if (player.Attack < enemy.Defence)
                                {
                                    int damage = rng.Next(9, 12);
                                    int totalDamage = damage;
                                    Console.WriteLine($"{username} deals {totalDamage} damage");
                                    enemy.Health -= totalDamage;
                                }
                                else
                                {
                                    int damage = rng.Next(11, 16);
                                    int totalDamage = damage;
                                    Console.WriteLine($"{username} deals {totalDamage} damage");
                                    enemy.Health -= totalDamage;
                                }
                            } 
                            else if (player.Attack >= 10)
                            {
                                if (player.Attack < enemy.Defence)
                                {
                                    int damage = rng.Next(6, 9);
                                    int totalDamage = damage;
                                    Console.WriteLine($"{username} deals {totalDamage} damage");
                                    enemy.Health -= totalDamage;
                                }
                                else
                                {
                                    int damage = rng.Next(7, 12);
                                    int totalDamage = damage;
                                    Console.WriteLine($"{username} deals {totalDamage} damage");
                                    enemy.Health -= totalDamage;
                                }
                            }
                            else
                            {
                                if (player.Attack < enemy.Defence)
                                {
                                    int damage = rng.Next(2, 5);
                                    int totalDamage = damage;
                                    Console.WriteLine($"{username} deals {totalDamage} damage");
                                    enemy.Health -= totalDamage;
                                }
                                else
                                {
                                    int damage = rng.Next(3, 8);
                                    int totalDamage = damage;
                                    Console.WriteLine($"{username} deals {totalDamage} damage");
                                    enemy.Health -= totalDamage;
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine($"{username} misses!");
                        }
                        return;

                    case "2":
                    case "guard":
                        Console.WriteLine($"{username} guards!");
                        player.Defence += 3;
                        return;

                    case "3":
                    case "item":
                    case "items":
                        Console.WriteLine("Items:");
                        foreach (var item in items)
                        {
                            Console.WriteLine($"{item.Key} x{item.Value}");
                        }
                        Console.WriteLine("To choose an item simply type the name of the item or press \"Enter\" to exit.");
                        string? useItem = Console.ReadLine()?.ToLower().Trim().Replace(" ", "");
                        if (string.IsNullOrWhiteSpace(useItem))
                        {
                            break; //Is treted like the player change his mind and doesn't want to use an item
                        }
                        
                        if (useItem == "smallpotion")
                        {
                            if (items.ContainsKey("Small potion") && items["Small potion"] > 0)
                            {
                                if (player.Health == player.MaxHealth)
                                {
                                    Console.WriteLine("You already at full Hp!");
                                    break;
                                }
                                items["Small potion"]--;
                                if (items["Small potion"] == 0)
                                    items.Remove("Small potion");
                                player.Health = Math.Min(player.Health + 10, player.MaxHealth);
                                Console.WriteLine("You restored 10 Hp!");
                            }
                            else
                            {
                                Console.WriteLine("You have no small potion left in your items");
                            }
                        }
                        else if (useItem == "mediumpotion")
                        {
                            if (items.ContainsKey("Medium potion") && items["Medium potion"] > 0)
                            {
                                if (player.Health == player.MaxHealth)
                                {
                                    Console.WriteLine("You already at full Hp!");
                                    break;
                                }
                                items["Medium potion"]--;
                                if (items["Medium potion"] == 0)
                                    items.Remove("Medium potion");
                                player.Health = Math.Min(player.Health + 30, player.MaxHealth);
                                Console.WriteLine("You restored 30 Hp!");
                            }
                            else
                            {
                                Console.WriteLine("You have no medium potion left in your items");
                            }
                        }
                        else if (useItem == "largepotion")
                        {
                            if (items.ContainsKey("Large potion") && items["Large potion"] > 0)
                            {
                                if (player.Health == player.MaxHealth)
                                {
                                    Console.WriteLine("You already at full Hp!");
                                    break;
                                }
                                items["Large potion"]--;
                                if (items["Large potion"] == 0)
                                    items.Remove("Large potion");
                                player.Health = Math.Min(player.Health + 50, player.MaxHealth);
                                Console.WriteLine("You restored 50 Hp!");
                            }
                            else
                            {
                                Console.WriteLine("You have no large potion left in your items");
                            }
                        } 
                        else if (useItem == "regenerationpotion")
                        {
                            if (items.ContainsKey("Regeneration potion") && items["Regeneration potion"] > 0)
                            {
                                if (player.Health == player.MaxHealth)
                                {
                                    Console.WriteLine("You already at full Hp!");
                                    break;
                                }
                                items["Regeneration potion"]--;
                                if (items["Regeneration potion"] == 0)
                                    items.Remove("Regeneration potion");
                                player.Health = player.MaxHealth;
                                Console.WriteLine("You fully restored your Hp!");
                            }
                            else
                            {
                                Console.WriteLine("You have no regeneration potion left in your items");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This item doesn't exist!");
                        }
                            break;

                    default:
                        Console.WriteLine("Invalid action please choose only one of the given choices");
                        break;
                }

            }    
        }
        public void EnemyActionTurn(Player player, Enemy enemy)
        {
            Console.WriteLine($"It's {enemy.Username}'s turns");
            int enemyTurn = rng.Next(1, 3);
            switch (enemyTurn)
            {
                case 1:
                    Console.WriteLine($"{enemy.Username} attacks with {enemy.Weapon}");
                    int success = rng.Next(1, 20 + 1);
                    if (success >= 10)
                    {
                        if (enemy.Attack >= 30)
                        {
                            if (enemy.Attack < player.Defence)
                            {
                                int damage = rng.Next(12, 15);
                                int totalDamage = damage;
                                Console.WriteLine($"{enemy.Username} deals {totalDamage} damage");
                                player.Health -= totalDamage;
                            }
                            else
                            {
                                int damage = rng.Next(15, 20);
                                int totalDamage = damage;
                                Console.WriteLine($"{enemy.Username} deals {totalDamage} damage");
                                player.Health -= totalDamage;
                            }
                        }
                        else if (player.Attack >= 20)
                        {
                            if (enemy.Attack < player.Defence)
                            {
                                int damage = rng.Next(9, 12);
                                int totalDamage = damage;
                                Console.WriteLine($"{enemy.Username} deals {totalDamage} damage");
                                player.Health -= totalDamage;
                            }
                            else
                            {
                                int damage = rng.Next(11, 16);
                                int totalDamage = damage;
                                Console.WriteLine($"{enemy.Username} deals {totalDamage} damage");
                                player.Health -= totalDamage;
                            }
                        }
                        else if (player.Attack >= 10)
                        {
                            if (enemy.Attack < player.Defence)
                            {
                                int damage = rng.Next(6, 9);
                                int totalDamage = damage;
                                Console.WriteLine($"{enemy.Username} deals {totalDamage} damage");
                                player.Health -= totalDamage;
                            }
                            else
                            {
                                int damage = rng.Next(7, 12);
                                int totalDamage = damage;
                                Console.WriteLine($"{enemy.Username} deals {totalDamage} damage");
                                player.Health -= totalDamage;
                            }
                        }
                        else
                        {
                            if (enemy.Attack < player.Defence)
                            {
                                int damage = rng.Next(2, 5);
                                int totalDamage = damage;
                                Console.WriteLine($"{enemy.Username} deals {totalDamage} damage");
                                player.Health -= totalDamage;
                            }
                            else
                            {
                                int damage = rng.Next(3, 8);
                                int totalDamage = damage;
                                Console.WriteLine($"{enemy.Username} deals {totalDamage} damage");
                                player.Health -= totalDamage;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{enemy.Username} misses!");
                    }
                    break;
                case 2:
                    Console.WriteLine($"{enemy.Username} guards!");
                    enemy.Defence += 3;
                    break;
            }
        }
        public void Shop(Player player)
        {
            Console.WriteLine("You enter shop");
            Console.WriteLine("Shopkeeper: Hello adventurer, what would you like to buy?");
            while (true)
            {
                Console.WriteLine($"You have {player.Gold} gold");
                Console.WriteLine("1. Small potion (Restores 10 hp) - 10 gold\n2. Medium potion (Restores 30 hp) - 30 gold\n3. Large potion (Restores 50 hp) - 50 gold\n" +
                    "4. Regeneration potion (Restores full health) - 100 gold\n5. Leave shop");
                Console.WriteLine("More items will come soon!");
                string? buy = Console.ReadLine()?.ToLower().Trim().Replace(" ", "");
                if (string.IsNullOrEmpty(buy))
                {
                    Console.WriteLine("Please make a choice. If you don't want to buy anything at the moment you can type \"5\" or \"leave\" in order to leave the shop");
                }
                else
                {
                    switch (buy)
                    {
                        case "1":
                        case "smallpotion":
                            if (player.Gold >= 10)
                            {
                                Console.WriteLine("You bought a small potion");
                                player.Gold -= 10;
                                if (items.ContainsKey("Small potion"))
                                    items["Small potion"]++;
                                else
                                    items["Small potion"] = 1;
                            }
                            else
                            {
                                Console.WriteLine("Else you don't have enough gold");
                            }
                            break;

                        case "2":
                        case "mediumpotion":
                            if (player.Gold >= 30)
                            {
                                Console.WriteLine("You bought a medium potion");
                                player.Gold -= 30;
                                if (items.ContainsKey("Medium potion"))
                                    items["Medium potion"]++;
                                else
                                    items["Medium potion"] = 1;
                            }
                            else
                            {
                                Console.WriteLine("Else you don't have enough gold");
                            }
                            break;

                        case "3":
                        case "largepotion":
                            if (player.Gold >= 50)
                            {
                                Console.WriteLine("You bought a large potion");
                                player.Gold -= 50;
                                if (items.ContainsKey("Large potion"))
                                    items["Large potion"]++;
                                else
                                    items["Large potion"] = 1;
                            }
                            else
                            {
                                Console.WriteLine("Else you don't have enough gold");
                            }
                            break;

                        case "4":
                        case "regenerationpotion":
                            if (player.Gold >= 100)
                            {
                                Console.WriteLine("You bought a regeneration potion");
                                player.Gold -= 100;
                                if (items.ContainsKey("Regeneration potion"))
                                    items["Regeneration potion"]++;
                                else
                                    items["Regeneration potion"] = 1;
                            }
                            else
                            {
                                Console.WriteLine("Else you don't have enough gold");
                            }
                            break;

                        case "5":
                        case "leave":
                            Console.WriteLine("You are leaving the shop.");
                            Console.WriteLine("Shopkeeper: Please come again!");
                            Console.Clear();
                            return;

                        default:
                            Console.WriteLine("Sorry, this item is not available right now!");
                            break;
                    }
                    Console.WriteLine("Shopkeeper: Is there anything else you wanna buy? (Y/N)");
                    string? buyAgain = Console.ReadLine()?.ToLower();
                    if (buyAgain is not "y" or "yes")
                    {
                        Console.Clear();
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
}
