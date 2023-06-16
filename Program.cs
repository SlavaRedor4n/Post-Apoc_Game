using System;
using System.ComponentModel;

namespace PostApocalypticGame
{
    class Program
    {
        static int playerHealth = 100;
        static int playerDamage = 20;
        static int experiencePoints = 0;
        static int opponentsKilled = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Что-то вдохновленное первыми двумя Fallout");

            while (playerHealth > 0 && opponentsKilled < 30)
            {
                Console.WriteLine("\n-— Бой {0} —-", opponentsKilled + 1);
                Opponent enemy = GenerateOpponent();
                Console.WriteLine("{0} появился! Его здоровье составляет: {1}", enemy.Name, enemy.Health);

                bool isEnemyDefeated = Battle(enemy);

                if (isEnemyDefeated)
                {
                    Console.WriteLine("Вы разобрались с {0}! Вами было получено {1} очков экспы.", enemy.Name, enemy.ExperiencePoints);
                    experiencePoints += enemy.ExperiencePoints;
                    opponentsKilled++;

                    Console.WriteLine("Имеющиеся очки экспы: {0}", experiencePoints);
                    Console.WriteLine("1) Восстановить жизненные силы");
                    Console.WriteLine("2) Увеличить урон");
                    Console.WriteLine("Выберите, что вам нужно прокачать за имеющуюся экспу:");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            RestoreHealth();
                            break;
                        case 2:
                            IncreaseDamage();
                            break;
                        default:
                            Console.WriteLine("Invalid option. Continuing to the next round.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Пустошь поглотила вас стараниями {0}. Игра завершена!", enemy.Name);
                    break;
                }
            }

            Console.WriteLine("\nСпасибо, что сыграли!");
            Console.WriteLine("Полученных очков опыта: {0}", experiencePoints);
            Console.WriteLine("Нажмите любую клавишу для того, чтобы покинуть игру...");
            Console.ReadKey();
        }

        static bool Battle(Opponent enemy)
        {
            while (playerHealth > 0 && enemy.Health > 0)
            {
                Console.WriteLine("\n-— Ход игрока" +
                    " —-");
                DisplayAttackOptions();
                int attackChoice = Convert.ToInt32(Console.ReadLine());

                int playerAttackDamage = 0;
                switch (attackChoice)
                {
                    case 1:
                        playerAttackDamage = playerDamage;
                        Console.WriteLine("Вы бьёте {0} при помощи холодного оружия!", enemy.Name);
                        break;
                    case 2:
                        playerAttackDamage = playerDamage + 10;
                        Console.WriteLine("Вы стреляете в {0} из автомата!", enemy.Name);
                        break;
                    case 3:
                        playerAttackDamage = playerDamage + 20;
                        Console.WriteLine("Вы используете осколочную гранату против {0}!", enemy.Name);
                        break;
                    default:
                        Console.WriteLine("Invalid attack choice. Try again!");
                        continue;
                }

                enemy.Health -= playerAttackDamage;
                Console.WriteLine("Вы нанесли {0} урона {1}!", playerAttackDamage, enemy.Name);

                if (enemy.Health <= 0)
                {
                    return true;
                }

                Console.WriteLine("\n-— Ход противника —-");
                int enemyAttackDamage = enemy.Attack();
                playerHealth -= enemyAttackDamage;
                Console.WriteLine("{0} атаковал вас и нанес {1} единиц урона!", enemy.Name, enemyAttackDamage);
                Console.WriteLine("Ваше здоровье составляет: {0}", playerHealth);
            }

            return false;
        }

        static void DisplayAttackOptions()
        {
            Console.WriteLine("Выберите атаку:");
            Console.WriteLine("1) Холодное оружие");
            Console.WriteLine("2) Автомат");
            Console.WriteLine("3) Осколочная граната");
        }

        static Opponent GenerateOpponent()
        {
            Random random = new Random();
            int opponentType = random.Next(1, 6);

            switch (opponentType)
            {
                case 1:
                    return new MutatedAnimal("Гигантский огненный геккон", 50, 5);
                case 2:
                    return new MutatedAnimal("Рейдер-легковооружённый", 30, 1);
                case 3:
                    return new MutatedAnimal("Сталкер", 80, 3);
                case 4:
                    return new MutatedAnimal("Гигантская ледяная муха", 40, 2);
                case 5:
                    return new MutatedAnimal("Ядовитый огромный паук", 20, 1);
                default:
                    return new MutatedAnimal("Неизвестное ни науке, ни вам существо", 100, 10);
            }
        }

        static void RestoreHealth()
        {
            int healthToRestore = experiencePoints / 2;
            playerHealth += healthToRestore;
            experiencePoints -= healthToRestore;

            Console.WriteLine("Вы восстановили {0} единиц здоровья.", healthToRestore);
            Console.WriteLine("Текущее здоровье: {0}", playerHealth);
            Console.WriteLine("Текущее количество экспы: {0}", experiencePoints);
        }

        static void IncreaseDamage()
        {
            int damageIncrease = experiencePoints / 2;
            playerDamage += damageIncrease;
            experiencePoints -= damageIncrease;

            Console.WriteLine("Вы повысили дамаг на {0} единиц.", damageIncrease);
            Console.WriteLine("Текущий дамаг: {0}", playerDamage);
            Console.WriteLine("Текущеее количество очков опыта: {0}", experiencePoints);
        }
    }

    class Opponent
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int ExperiencePoints { get; set; }

        public Opponent(string name, int health, int experiencePoints)
        {
            Name = name;
            Health = health;
            ExperiencePoints = experiencePoints;
        }

        public virtual int Attack()
        {
            return 10;
        }
    }

    class MutatedAnimal : Opponent
    {
        public MutatedAnimal(string name, int health, int experiencePoints)
        : base(name, health, experiencePoints)
        {
        }

        public override int Attack()
        {
            Random random = new Random();
            int damage = random.Next(5, 15);
            return damage;
        }
    }
}