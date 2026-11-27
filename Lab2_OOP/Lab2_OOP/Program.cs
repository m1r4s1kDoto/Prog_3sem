using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Lab2_OOP
{
    //Класс для реализации узла стека
    public class SNode
    {
        public int Value;
        public SNode Next;

        //Конструктор
        public SNode(int value, SNode next = null)
        {
            Value = value; //Содержимое узла
            Next = next; //Указатель на следующий узел
        }
    }

    //Класс для реализации стека
    public class Stack
    {
        private SNode Top; //Вершина стека
        public int Count { get; private set; }

        //Индексатор
        public int this[int idx]
        {
            get
            {
                //Идем к элементу от вершины
                SNode curr = Top;
                for (int i = 0; i < idx && curr != null; i++)
                    curr = curr.Next;
                if (curr == null)
                    throw new IndexOutOfRangeException();
                return curr.Value;
            }
        }

        //Конструктор по умолчанию
        public Stack()
        {
            Top = null;
            Count = 0;
        }

        //Конструктор с узлом
        public Stack(SNode top)
        {
            Top = top;
            while (top != null)
            {
                Count++;
                top = top.Next;
            }
        }

        //Конструктор копирования
        public Stack(Stack other)
        {
            if (other == null || other.Top == null)
                throw new ArgumentNullException("Копируемый стек равен null");

            //Создаем временный список и переписываем туда стек
            List<int> temp = new List<int>();
            SNode curr = other.Top;
            while (curr != null)
            {
                temp.Add(curr.Value);
                curr = curr.Next;
            }
            temp.Reverse(); //Переворачиваем список
            //Заполняем стек
            foreach (var val in temp)
            {
                Top = new SNode(val, Top);
                Count++;
            }
        }

        //Перегрузка арифметической операции +
        public static Stack operator +(Stack stack, int item)
        {
            Stack newStack = new Stack(); //Новый стек
            newStack.Top = new SNode(item, stack.Top); //Прикрепление новой вершины
            newStack.Count++;
            return newStack;
        }

        //Перегрузка арифметической операции -
        public static Stack operator --(Stack stack)
        {
            if (stack.Top == null)
                return stack;
            Stack newStack = new Stack(); //Новый стек
            newStack.Top = stack.Top.Next; //Назначение следующего элемента вершиной
            newStack.Count--;
            return newStack;
        }

        //Перегрузка операции true
        public static bool operator true(Stack stack)
        {
            return stack.Top == null;
        }

        //Перегрузка операции false
        public static bool operator false(Stack stack)
        {
            return stack.Top != null;
        }

        //Перегрузка операции >
        public static bool operator >(Stack stack1, Stack stack2)
        {
            if (stack1 == null || stack2 == null)
                throw new ArgumentNullException("Один из стеков равен null");

            if (stack1.Top == null)
                throw new ArgumentNullException("Нечего копировать");

            //Создаем временный список и переписываем туда стек
            List<int> temp = new List<int>();
            SNode curr = stack1.Top;
            while (curr != null)
            {
                temp.Add(curr.Value);
                curr = curr.Next;
            }
            temp.Sort(); //Сортируем список
            temp.Reverse(); //Переворачиваем список

            //Заполняем стек
            stack2.Count = 0;
            foreach (var val in temp)
            {
                stack2.Top = new SNode(val, stack2.Top);
                stack2.Count++;
            }
            return true;
        }

        //Перегрузка операции <
        public static bool operator <(Stack stack1, Stack stack2)
        {
            if (stack2 == null || stack1 == null)
                throw new ArgumentNullException("Один из стеков равен null");

            if (stack2.Top == null)
                throw new ArgumentNullException("Нечего копировать");

            //Создаем временный список и переписываем туда стек
            List<int> temp = new List<int>();
            SNode curr = stack2.Top;
            while (curr != null)
            {
                temp.Add(curr.Value);
                curr = curr.Next;
            }
            temp.Sort(); //Сортируем список
            temp.Reverse(); //Переворачиваем список

            //Заполняем стек
            stack1.Count = 0;
            foreach (var val in temp)
            {
                stack1.Top = new SNode(val, stack1.Top);
                stack1.Count++;
            }
            return true;
        }

        //Метод для строкового представления
        public override string ToString()
        {
            if (Top == null) return "Стек пуст";
            List<int> items = new List<int>();
            for (SNode curr = Top; curr != null; curr = curr.Next)
                items.Add(curr.Value);
            return string.Join(" -> ", items);
        }
    }

    //Статический класс с методами расширения
    public static class Extensions
    {
        //Метод для подсчета количества предложений в строке
        public static int SentenceCount(this string str)
        {
            //Если пустая строка
            if (string.IsNullOrWhiteSpace(str))
                return 0;

            int count = 0;
            bool trailingText = false; //остаточный текст

            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c == '.' || c == '!' || c == '?')
                {
                    //Многоточие
                    if (c == '.' && i + 1 < str.Length && str[i + 1] == '.')
                        // Пропускаем все остальные точки в многоточии
                        while (i + 1 < str.Length && str[i + 1] == '.')
                            i++;
                    count++;
                    trailingText = false;
                }
                else if (!char.IsWhiteSpace(c))
                    trailingText = true;
            }

            //Засчитываем остаточный тест как предложение
            if (trailingText)
                count++;

            return count;
        }

        //Метод для определения среднего элемента стека
        public static int Middle(this Stack stack)
        {
            if (stack == null)
                throw new ArgumentNullException(nameof(stack));

            if (stack.Count == 0)
                throw new InvalidOperationException("Стек пуст, невозможно определить средний элемент");

            int middleIdx = (stack.Count - 1) / 2; //для четного count будет выбран элемент левее
            return stack[middleIdx];

        }
    }

    class Program
    {
        static void Main()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА #2");
                Console.WriteLine("1. Проверка работы стека");

                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1": Task1(); break;

                    case "0": return;
                    default: break;
                }

            }

        }

        static void Task1()
        {
            Console.Clear();
            Console.WriteLine("ПРОВЕРКА РАБОТЫ СТЕКА");
            Console.WriteLine("Создание стека");
            SNode node = new SNode(10);
            Stack stack1 = new Stack(node);
            Console.WriteLine($"Текущий стек: {stack1}");

            Console.WriteLine("\nДобавление элементов в стек");
            for (int i = 20; i <= 60; i += 10)
            {
                stack1 += i;
                Console.WriteLine($"Добавлено: {i}");
            }
            Console.WriteLine($"Стек после добавления: {stack1}");

            Console.WriteLine("\nИзвлечение элементов из стека");
            int pop = 3;
            for (int i = 0; i < 3; i++)
                stack1--;
            Console.WriteLine($"Извлечено элементов из стека: {pop}");
            Console.WriteLine($"Стек после извлечения: {stack1}");

            Console.WriteLine("\nКопирование стека");
            Stack stack2 = new Stack();
            if (stack1 > stack2)
                Console.WriteLine("Стек успешно скопирован");
            Console.WriteLine($"Скопированный и отсортированный стек: {stack2}");

            Console.WriteLine("\nПроверка, пустой ли стек");
            Console.WriteLine($"Проверяемый стек: {stack2}");
            if (stack2)
                Console.WriteLine("Стек пуст");
            else
                Console.WriteLine("Стек не пуст");
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nДля продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}

