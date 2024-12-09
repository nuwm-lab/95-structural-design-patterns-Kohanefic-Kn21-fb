using System;
using System.Collections.Generic;

namespace StructuralPatternsExample
{
    // ===== Базовий функціонал =====
    public interface INumberGenerator
    {
        List<int> GenerateNumbers(int count);
        void DisplayNumbers();
    }

    public class DivisibleBySixGenerator : INumberGenerator
    {
        private List<int> numbers = new List<int>();

        public List<int> GenerateNumbers(int count)
        {
            numbers.Clear();
            int current = 6;
            for (int i = 0; i < count; i++)
            {
                numbers.Add(current);
                current += 6;
            }
            return numbers;
        }

        public void DisplayNumbers()
        {
            Console.WriteLine("Числа, що діляться на 6:");
            foreach (var num in numbers)
            {
                Console.WriteLine(num);
            }
        }
    }

    // ===== Адаптер =====
    public class DivisibleByTwoAdapter : INumberGenerator
    {
        private DivisibleBySixGenerator generator;

        public DivisibleByTwoAdapter(DivisibleBySixGenerator gen)
        {
            generator = gen;
        }

        public List<int> GenerateNumbers(int count)
        {
            var numbers = generator.GenerateNumbers(count);
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] /= 3; // Перетворюємо числа, що діляться на 6, у числа, що діляться на 2
            }
            return numbers;
        }

        public void DisplayNumbers()
        {
            Console.WriteLine("Числа, що діляться на 2 (через адаптер):");
            foreach (var num in GenerateNumbers(5))
            {
                Console.WriteLine(num);
            }
        }
    }

    // ===== Компонувальник =====
    public class CompositeNumberGenerator : INumberGenerator
    {
        private List<INumberGenerator> generators = new List<INumberGenerator>();

        public void AddGenerator(INumberGenerator generator)
        {
            generators.Add(generator);
        }

        public List<int> GenerateNumbers(int count)
        {
            List<int> allNumbers = new List<int>();
            foreach (var generator in generators)
            {
                allNumbers.AddRange(generator.GenerateNumbers(count));
            }
            return allNumbers;
        }

        public void DisplayNumbers()
        {
            Console.WriteLine("Компонований список чисел:");
            foreach (var generator in generators)
            {
                generator.DisplayNumbers();
            }
        }
    }

    // ===== Декоратор =====
    public class DecoratedNumberGenerator : INumberGenerator
    {
        private INumberGenerator _generator;

        public DecoratedNumberGenerator(INumberGenerator generator)
        {
            _generator = generator;
        }

        public List<int> GenerateNumbers(int count)
        {
            var numbers = _generator.GenerateNumbers(count);
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] += 1; // Додаємо 1 до кожного числа
            }
            return numbers;
        }

        public void DisplayNumbers()
        {
            Console.WriteLine("Числа з модифікацією (декоратор):");
            foreach (var num in GenerateNumbers(5))
            {
                Console.WriteLine(num);
            }
        }
    }
      


    class Program
    {
        static void Main(string[] args)
        {
            // ===== Основний генератор чисел =====
            var divisibleBySix = new DivisibleBySixGenerator();
            divisibleBySix.GenerateNumbers(5);
            divisibleBySix.DisplayNumbers();

            // ===== Адаптер =====
            var divisibleByTwoAdapter = new DivisibleByTwoAdapter(divisibleBySix);
            divisibleByTwoAdapter.DisplayNumbers();
            // ===== Компонувальник =====
            var compositeGenerator = new CompositeNumberGenerator();
            compositeGenerator.AddGenerator(divisibleBySix);
            compositeGenerator.AddGenerator(divisibleByTwoAdapter);
            compositeGenerator.DisplayNumbers();

            // ===== Декоратор =====
            var decoratedGenerator = new DecoratedNumberGenerator(divisibleBySix);
            decoratedGenerator.DisplayNumbers();

            Console.WriteLine("Програма завершила роботу.");
        }
    }
}