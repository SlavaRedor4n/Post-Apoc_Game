using System;

namespace PostApocalypticGame
{
    class Program
    {
        static int playerHealth = 100;
        static int playerDamage = 10;
        static int experiencePoints = 0;
        static int enemyCount = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в постапокалиптичный мир!");

            while (playerHealth > 0)
            {
                Console.WriteLine("\n-— Бой {0} —-", enemyCount + 1);

                bool isEnemyDead = Battle();

                if (isEnemyDead)
                {
                    enemyCount++;
                    experiencePoints++;
                    Console.WriteLine("Вы победили врага! Очков опыта +1");
                    Console.WriteLine("Всего очков опыта: {0}", experiencePoints);

                    Console.WriteLine("Выберите характеристику для обновления: 1) ХП 2) Урон");
                    int upgradeChoice = Convert.ToInt32(Console.ReadLine());
                    if (upgradeChoice == 1)
                    {
                        playerHealth += 10;
                        Console.WriteLine("ХП прокачаны! Текущее здоровье: {0}", playerHealth);
                    }
                    else if (upgradeChoice == 2)
                    {
                        playerDamage += 5;
                        Console.WriteLine("Урон прокачан! Текущий урон: {0}", playerDamage);
                    }
                }
                else
                {
                    Console.WriteLine("Вы проиграли. Игра окончена!");
                    break;
                }
            }

            Console.WriteLine("\nСпасибо за игру!");
            Console.WriteLine("Всего врагов убито: {0}", enemyCount);
            Console.WriteLine("Всего очков опыта: {0}", experiencePoints);
        }

        static bool Battle()
        {
            int enemyHealth = 50 + enemyCount * 10;
            int enemyDamage = 5 + enemyCount;

            while (enemyHealth > 0 && playerHealth > 0)
            {
                Console.WriteLine("\n-— Ход игрока —-");
                DisplayAttackOptions();
                int attackChoice = Convert.ToInt32(Console.ReadLine());

                int playerAttackDamage = 0;
                switch (attackChoice)
                {
                    case 1:
                        playerAttackDamage = playerDamage;
                        Console.WriteLine("Вы бьёте врага руками!");
                        break;
                    case 2:
                        playerAttackDamage = playerDamage + 5;
                        Console.WriteLine("Вы бьёте врага мачете!");
                        break;
                    case 3:
                        playerAttackDamage = playerDamage + 10;
                        Console.WriteLine("Вы бросаете грену во врага!");
                        break;
                    case 4:
                        playerAttackDamage = playerDamage + 15;
                        Console.WriteLine("Вы стреляете в противника из пистолета!");
                        break;
                    case 5:
                        playerAttackDamage = playerDamage + 20;
                        Console.WriteLine("Вы обрушиваете шквал огня из автомата в противника!");
                        break;
                    case 6:
                        playerAttackDamage = playerDamage + 25;
                        Console.WriteLine("Вы жгёте...эээ... жжёте врага из огнемета!");
                        break;
                    case 7:
                        playerAttackDamage = playerDamage - 5;
                        Console.WriteLine("Вы бросаете камень во вражину!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        continue;
                }

                enemyHealth -= playerAttackDamage;
                Console.WriteLine("Вы нанесили {0} единиц урона врагу!", playerAttackDamage);

                if (enemyHealth <= 0)
                {
                    Console.WriteLine("Враг побежден!");
                    return true;
                }

                Console.WriteLine("-— Ход противника —-");
                int enemyAttackDamage = enemyDamage;
                playerHealth -= enemyAttackDamage;
                Console.WriteLine("Враг атаковал вас и нанёс {0} единиц урона!", enemyAttackDamage);

                Console.WriteLine("Ваше здоровье: {0}", playerHealth);
            }

            return false;
        }

        static void DisplayAttackOptions()
        {
            Console.WriteLine("Выберите атаку:");
            Console.WriteLine("1) Окей.. руки");
            Console.WriteLine("2) Мачете");
            Console.WriteLine("3) Осколочная граната");
            Console.WriteLine("4) Пистолет");
            Console.WriteLine("5) Очередь из автомата");
            Console.WriteLine("6) Огнемет");
            Console.WriteLine("7) Камень.. Мать-природа помогает");
        }
    }
}