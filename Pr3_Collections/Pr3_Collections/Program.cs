using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
                Console.WriteLine("1. Необобщенные коллекции");
                Console.WriteLine("2. Обобщенные коллекции");
                Console.WriteLine("3. Обобщенные коллекции с пользовательским типом данных");
                Console.WriteLine("4. Наблюдаемые коллекции");



                Console.Write("Выберите задание, которое хотите проверить (1-4, для выхода 0): ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Task1(); break;
                    case "2": Task2(); break;
                    case "3": Task3(); break;
                    case "4": Task4(); break;
                    case "0": return;
                    default: break;
                }
            }
        }

        static void Task1()
        {
            Console.Clear();
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
            Console.WriteLine($" Добавлено {str}");
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
            Console.Clear();
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
            Console.WriteLine("До ");
            PrintStackDouble(stack);
            double deleted;
            int n = 3; //Количество элементов для удаления
            for (int i = 0; i < n && stack.Count() > 0; i++)
            {
                deleted = stack.Pop();
                Console.WriteLine($" Удалено: {deleted}");
            }
            Console.WriteLine("После");
            PrintStackDouble(stack);

            Console.WriteLine("\nПункт C: добавление элементов в стек разными способами");
            stack.Push(14.88);
            Console.WriteLine($" Добавлено: {stack.Peek()}");
            stack.Push(123.45);
            Console.WriteLine($" Добавлено: {stack.Peek()}");

            Console.WriteLine("\nПункт D: создание двусвязного списка и его заполнение");
            //Создаем двусвязный список и заполняем его данными
            LinkedList<double> llist = new LinkedList<double>(stack);
            var item = llist.AddLast(72); //Добавление элемента в конец списка
            Console.WriteLine($" Добавлено: {llist.AddFirst(99.3).Value}"); //Добавление элемента в начало списка
            Console.WriteLine($" Добавлено: {llist.AddAfter(item, 3.14).Value}"); //Добавление после узла
            Console.WriteLine($" Добавлено: {llist.AddBefore(item, 19.583).Value}"); //Добавление до узла

            Console.WriteLine("\nПункт E: вывод двусвязного списка");
            PrintLinkedListDouble(llist);

            Console.WriteLine("\nПункт F: поиск значения в двусвязном списке");
            PrintLinkedListDouble(llist);
            double searchVal = 3.14; //Искомое значение
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
            Console.Clear();
            Console.WriteLine("ЗАДАНИЕ 3: ОБОБЩЕННЫЕ КОЛЛЕКЦИИ С ПОЛЬЗОВАТЕЛЬСКИМ ТИПОМ ДАННЫХ");

            //Создаем стек и заполняем его данными
            Stack<ComputerDevice> stack = new Stack<ComputerDevice>();
            stack.Push(new ComputerDevice("Razer", "Viper Ultimate"));
            stack.Push(new ComputerDevice("HyperX", "Cloud Alpha S"));
            stack.Push((ComputerDevice)stack.Peek().Clone()); //Демонстрация работы метода клонирования
            stack.Push(new Monitor("XIAOMI", "G27QI", 27, 2560, 1440, 200, PanelType.IPS));
            stack.Push(new Keyboard("AULA", "F75", KeyboardType.Mechanical, true, 1023));

            Console.WriteLine("\nПункт A: вывод стека");
            PrintStackCD(stack);

            Console.WriteLine("\nПункт B: удаление элементов из стека");
            Console.WriteLine("До");
            PrintStackCD(stack);
            Console.WriteLine();
            ComputerDevice deleted;
            int n = 3; //количество элементов для удаления
            for (int i = 0; i < n; i++)
            {
                deleted = stack.Pop();
                Console.WriteLine($" Удалено: {deleted.GetName()}");
            }
            Console.WriteLine("После");
            PrintStackCD(stack);

            Console.WriteLine("\nПункт C: добавление элементов в стек разными способами");
            stack.Push(new Monitor("XIAOMI", "G27QI", 27, 2560, 1440, 200, PanelType.IPS));
            Console.WriteLine($" Добавлено: {stack.Peek().GetName()}");
            stack.Push(new Keyboard("AULA", "F75", KeyboardType.Mechanical, true, 1023));
            Console.WriteLine($" Добавлено: {stack.Peek().GetName()}");

            Console.WriteLine("\nПункт D: создание двусвязного списка и его заполнение");
            //Создаем двусвязный список и заполняем его данными
            LinkedList<ComputerDevice> llist = new LinkedList<ComputerDevice>(stack);
            var item = llist.Last; //Взятие последнего узла
            Console.WriteLine($" Добавлено: {llist.AddFirst(new ComputerDevice("Lamzu", "Atlantis")).Value.GetName()}"); //Добавление элемента в начало списка
            Console.WriteLine($" Добавлено: {llist.AddLast(new ComputerDevice("Sergey", "Semenov")).Value.GetName()}"); //Добавление элемента в конец списка
            Console.WriteLine($" Добавлено: {llist.AddAfter(item, new Monitor("LG", "UltraGear OLED", 27.0, 3840, 2160, 240, PanelType.OLED)).Value.GetName()}"); //Добавление элемента после узла
            Console.WriteLine($" Добавлено: {llist.AddBefore(item, new Monitor("Samsung", "Odyssey G7", 32.0, 2560, 1440, 240, PanelType.VA)).Value.GetName()}");//Добавление элемента до узла

            Console.WriteLine("\nПункт E: вывод двусвязного списка");
            PrintLinkedListCD(llist);

            Console.WriteLine("\nПункт F: поиск значения в двусвязном списке");
            ComputerDevice searchDev = new ComputerDevice("Razer", "Viper Ultimate"); //Искомый элемент
            Console.WriteLine($"Искомый элемент: {searchDev.GetName()}");
            ComputerDevice foundItem = null;
            ComputerDeviceModelComparer modelComparer = new ComputerDeviceModelComparer();

            foreach (var dev in llist)
                if (dev.CompareTo(searchDev) == 0 && modelComparer.Compare(dev, searchDev) == 0) //Демонстрация сравнения по умолчанию и альтернативного сравнения
                {
                    foundItem = dev;
                    break;
                }

            if (foundItem != null)
                Console.WriteLine($"Элемент найден: {foundItem.GetName()}");
            else
                Console.WriteLine("Элемент не найден в списке.");


            Pause();
        }

        //Вспомогательный метод для вывода стека с пользовательским типом
        static void PrintStackCD(Stack<ComputerDevice> stack)
        {
            Console.WriteLine("Содержимое стека (сверху вниз):");
            foreach (var item in stack)
                Console.WriteLine($"{item.GetName()}");
        }

        //Вспомогательный метод для двусвязного списка с пользовательским типом
        static void PrintLinkedListCD(LinkedList<ComputerDevice> llist)
        {
            Console.WriteLine("Содержимое стека (сверху вниз):");
            foreach (var item in llist)
                Console.WriteLine($"{item.GetName()}");
        }

        static void Task4()
        {
            Console.Clear();
            Console.WriteLine("ЗАДАНИЕ 4: НАБЛЮДАЕМЫЕ КОЛЛЕКЦИИ");

            ObservableCollection<ComputerDevice> observableDevices = new ObservableCollection<ComputerDevice>(); //Создаем наблюдаемыую коллекцию 
            observableDevices.CollectionChanged += CDCollection_CollectionChanged; //Регистрируем метод-обработчик на событие CollectionChanged

            //Добавляем элементы
            Console.WriteLine("\nДобавление элементов");
            observableDevices.Add(new ComputerDevice("Lamzu", "Atlantis"));
            observableDevices.Add(new Monitor("LG", "UltraGear OLED", 27.0, 3840, 2160, 240, PanelType.OLED));
            observableDevices.Add(new Keyboard("AULA", "F75", KeyboardType.Mechanical, true, 1023));

            //Удаляем элемент
            Console.WriteLine("\nУдаление элементов");
            var toRemove = observableDevices.FirstOrDefault(d => d.Manufacturer == "Lamzu" && d.Model == "Atlantis");
            observableDevices.Remove(toRemove); //По указателю
            observableDevices.RemoveAt(1); //По индексу

            //Очищаем коллекцию
            Console.WriteLine("\nОчистка коллекции");
            observableDevices.Clear();

            observableDevices.CollectionChanged -= CDCollection_CollectionChanged; //Отписываемся от события

            Pause();
        }

        //Произвольный метод-обработчик CollectionChange                  контейнер с событиями
        private static void CDCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //Свойство e.Action показывает, какое именно действие произошло с коллекцией 
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add: //Добавление
                    Console.WriteLine("[СОБЫТИЕ] в коллекцию добавлен новый элемент(ы):");
                    if (e.NewItems != null)
                        foreach (ComputerDevice newItem in e.NewItems)
                            Console.WriteLine($" +{newItem}");
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove: //Удаление
                    Console.WriteLine("[СОБЫТИЕ] из коллекции удален элемент(ы):");
                    if (e.OldItems != null)
                        foreach (ComputerDevice oldItem in e.OldItems)
                            Console.WriteLine($" -{oldItem}");
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: //Очистка
                    Console.WriteLine("[СОБЫТИЕ] коллекция была полностью очищена");
                    break;

                default:
                    Console.WriteLine($"[СОБЫТИЕ] Произошло действие: {e.Action}");
                    break;
            }
        }


        static void Pause()
        {
            Console.WriteLine("\nДля продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
