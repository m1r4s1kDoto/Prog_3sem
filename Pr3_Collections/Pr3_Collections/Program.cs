using System;
using System.Collections;
using System.Collections.Generic;

namespace Pr3_Collections
{
    

    class Program
    {
        static void Main()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;


            while (true)
            {
                Console.Clear();
                Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА #3");
                Console.Write("Выберите задание, которое хотите проверить (1-4, для выхода 0): ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Task1(); break;
                    case "2": Task2(); break;
                    case "3": Task3(); break;
                    case "0": return;
                    default: break;
                }
            }
        }

        static void Task1()
        {
            Console.WriteLine("ЗАДАНИЕ 1: НЕОБОБЩЕННЫЕ КОЛЛЕКЦИИ");

            ArrayList list = new ArrayList(); //Создаем необобщенную коллекцию
            Random rnd = new Random();

            //Генерируем 5 случайных чисел и добавляем их в коллекцию
            Console.WriteLine("Пункт A: заполнение 5 случайными числами");
            for (int i = 0; i < 5; i++)
            {
                int randomNum = rnd.Next(322, 993);
                list.Add(randomNum);
            }
            PrintList(list);

            Console.WriteLine("\nПункт B: добавление строки");
            //Создаем строку и добавляем в коллекцию
            string str = "Sergey";
            list.Add(str);
            PrintList(list);

            Console.WriteLine("\nПункт C: удаление заданного элемента");
            list.Remove("Sergey"); //По значению
            list.RemoveAt(0); //По индексу 
            PrintList(list);

            Console.WriteLine("\nПункт D: вывод количества элементов и содержимого");
            PrintList(list);
            Console.WriteLine($"Количество элементов: {list.Count}"); //4

            Console.WriteLine("\nПункт E: поиск заданного значения");
            PrintList(list);
            var searchVal = list[1]; //Берем второй элемент
            bool isFound = list.Contains(searchVal); //Проверяем его наличие
            if (isFound == true)
            {
                int idx = list.IndexOf(searchVal);
                Console.WriteLine($"Элемент {searchVal} найден. Он находится под индексом {idx}");
            }
            else
                Console.WriteLine($"Элемент {searchVal} не найден");
            Pause();
        }

        //Вспомогательный метод для вывода необобщенной коллекции
        static void PrintList(ArrayList collection)
        {
            Console.Write("Содержимое коллекции: ");
            foreach (var item in collection)
                Console.Write($"{item} ");
            Console.WriteLine();
        }

        static void Task2()
        {
            Console.WriteLine("ЗАДАНИЕ 2: ОБОБЩЕННЫЕ КОЛЛЕКЦИИ");

            //Создаем стек и заполняем его данными
            Stack<double> stack = new Stack<double>();
            stack.Push(6.7);
            stack.Push(2.28);
            stack.Push(3.1415926);
            stack.Push(99.3);
            stack.Push(72.0);

            Console.WriteLine("\nПункт A: вывод стека");
            PrintStackDouble(stack);

            Console.WriteLine("\nПункт B: удаление элементов из стека");
            Console.Write("До\n ");
            PrintStackDouble(stack);
            double deleted;
            int n = 3; //Количество элементов для удаления
            for (int i = 0; i < n && stack.Count() > 0; i++)
            {
                deleted = stack.Pop();
                Console.WriteLine($" Удалено: {deleted}");
            }
            Console.Write("После\n ");
            PrintStackDouble(stack);

            Console.WriteLine("\nПункт C: добавление элементов в стек разными способами");
            stack.Push(14.88);
            stack.Push(123.45);
            PrintStackDouble(stack);

            Console.WriteLine("\nПункт D: создание двусвязного списка и его заполнение");
            //Создаем двусвязный список и заполняем его данными
            LinkedList<double> llist = new LinkedList<double>(stack);
            var item = llist.AddLast(72); //Добавление элемента в конец списка
            llist.AddFirst(99.3); //Добавление элемента в начало списка
            llist.AddAfter(item, 3.14); //Добавление после узла
            llist.AddBefore(item, 19.583); //Добавление до узла

            Console.WriteLine("\nПункт E: вывод двусвязного списка");
            PrintLinkedListDouble(llist);

            Console.WriteLine("\nПункт F: поиск значения в двусвязном списке");
            PrintLinkedListDouble(llist);
            double searchVal = 3.14; //Искоомое значение
            bool isFound = llist.Contains(searchVal); //Проверка наличия
            if (isFound == true)
                Console.WriteLine($"Элемент {searchVal} найден.");
            else
                Console.WriteLine($"Элемент {searchVal} не найден.");
            Pause();
        }

        //Вспомогательный метод для вывода стека
        static void PrintStackDouble(Stack<double> stack)
        {
            Console.Write("Содержимое стека (сверху вниз): ");
            foreach (var item in stack)
                Console.Write($"{item} ");
            Console.WriteLine();
        }

        //Вспомогательный метод для вывода двусвязного списка
        static void PrintLinkedListDouble(LinkedList<double> llist)
        {
            Console.Write("Содержимое двусвязного списка: ");
            foreach (var item in llist)
                Console.Write($"{item} ");
            Console.WriteLine();
        }

        static void Task3()
        {
            Console.WriteLine("ЗАДАНИЕ 3: ОБОБЩЕННЫЕ КОЛЛЕКЦИИ С ПОЛЬЗОВАТЕЛЬСКИМ ТИПОМ ДАННЫХ");

            //Создаем стек и заполняем его данными
            Stack<ComputerDevice> stack = new Stack<ComputerDevice>();
            stack.Push(new ComputerDevice("Razer", "Viper Ultimate"));
            stack.Push(new ComputerDevice("HyperX", "Cloud Alpha S"));
            stack.Push(new Monitor("XIAOMI", "G27QI", 27, 2560, 1440, 200, PanelType.IPS));
            stack.Push(new Keyboard("AULA", "F75", KeyboardType.Mechanical, true, 1023));
            Keyboard k1 = stack.Peek();
            stack.Push((stack.Peek().Clone);

            Console.WriteLine("\nПункт A: вывод стека");
            PrintStack()
            Pause();
        }

        //Вспомогательный метод для вывода стека с пользовательским типом
        static void PrintStackCD(Stack<ComputerDevice> stack)
        {
            foreach (var item in stack)
            {
                Console.WriteLine
                Console.WriteLine($"")
            }
        }


        static void Pause()
        {
            Console.WriteLine("\nДля продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
