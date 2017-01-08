using System;

namespace ECTest
{
    class Program
    {
        /// <summary>
        /// Health, securely stored
        /// </summary>
        static MemoryProtectedInt<float> Health { get; set; }

        /// <summary>
        /// Basic psuedo-random number generator
        /// </summary>
        static Random Random { get; set; }

        static void Main(string[] args)
        {
            Health = new MemoryProtectedInt<float>(100F);
            Random = new Random(Guid.NewGuid().GetHashCode());

            while (true)
            {
                Console.WriteLine($"Health: { Health.GetValue() }");
                Console.WriteLine("Press any key to perform an attack on the health value..");
                Console.ReadLine();
                Attack();
            }
        }

        /// <summary>
        /// Reduct health
        /// </summary>
        static void Attack()
        {
            float damage = (float)Random.Next(1, 3);

            if (Health.GetValue() - damage < 0)
            {
                Health = new MemoryProtectedInt<float>(0.0f);
                Console.WriteLine("You are dead!");
                return;
            }

            Health = new MemoryProtectedInt<float>(Health.GetValue() - damage);

            Console.WriteLine($"Deducted { damage } health!");
            Console.WriteLine();
        }
    }
}
