using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace Pr2_Class
{
    //Перечисление типов цветов
    public enum FlowerType
    {
        Rose, //Роза
        Tulip, //Тюльпан
        Daisy, //Ромашка
        Orchid, //Орхидея
        Hazel, //Орешник
        Pion, //Пион
        Sunflower, //Подсолнух
        Lily //Лилия
    }

    //Структура для размера
    public struct Size
    {
        private double _length; //Длина стебля в сантиметрах

        public double Length
        {
            get => _length;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Ошибка: длина не может быть отрицательной.");
                _length = value;
            }
        }

        //Конструктор
        public Size(double length) 
        {
            if (length < 0)
                throw new ArgumentException("Ошибка: длина не может быть отрицательной.");
            _length = length;
        }

        //Метод для строкового представления
        public override string ToString()
        {
            return $"{Length} см";
        }
    }

    public class Plant
    {
        public FlowerType Type { get; set; } //Тип цветка
        public Size PlantSize { get; set; } //Размер цветка
        public readonly DateTime PlantedAt; //Время посадки (только для чтения)

        //Конструктор
        public Plant(FlowerType type, Size size)
        {
            Type = type;
            PlantSize = size;
            PlantedAt = DateTime.Now;
        }

        //Метод для поливания
        public void Watering()
        {
            Console.WriteLine($"Вы полили {Type}. Пусть растет он большим и здоровым.");
        }

        //Метод для строкового представления
        public override string ToString()
        {
            return $"Растение: {Type}, размер: {PlantSize}, дата и время посадки: {PlantedAt:dd.MM.yyyy HH:mm:ss}";
        }
    }

    public class Flower : Plant
    {
        public int Quantity { get; set; } //Количество цветов

        const int MaxQuantity = 1000; //Константа для ограничения        

        //Конструкторы:
        //Без количества
        public Flower(FlowerType type, Size size)
            : base(type, size) //Вызов конструктора родителя
        {
            Quantity = 1;
        }

        //Полный
        public Flower(FlowerType type, Size size, int quantity)
            : base(type, size) //Вызов конструктора родителя
        {
            if (quantity > MaxQuantity || quantity < 0)
            {
                throw new ArgumentException($"Ошибка: количество должно быть от 0 до {MaxQuantity}");
            }

            Quantity = quantity;
        }

        //Копирования
        public Flower(Flower other)
            : base(other.Type, other.PlantSize)
        {
            Quantity = other.Quantity;
        }
        
        //Метод для строкового представления
        public override string ToString()
        {
            return $"{base.ToString()}, количество: {Quantity} шт.";
        }

        //Метод для вдыхания аромата
        public void Smelling()
        {
            Console.WriteLine($"Вы вдохнули аромат {Type}. Надеемся, у вас нет никакой аллергии.");
        }

        //Метод для среза
        public void Cut()
        {
            if (Quantity > 0)
            {
                Quantity--;
                Console.WriteLine($"Вы срезали один {Type}. Осталось: {Quantity}.");
            }
            else
                Console.WriteLine($"Нечего срезать.");
        }

        //Метод с инструкцией по уходу
        public static void GetTips(FlowerType type)
        {
            string tips = type switch
            {
                FlowerType.Rose => "Поливайте 2 раза в неделю, любит солнце, обрезайте увядшие бутоны.",
                FlowerType.Tulip => "Поливайте умеренно, держите в прохладе, не любит застой воды.",
                FlowerType.Daisy => "Поливайте часто, любит свет, неприхотлива.",
                FlowerType.Orchid => "Поливайте раз в неделю погружением, высокая влажность, непрямой свет.",
                FlowerType.Hazel => "Поливайте умеренно, любит полутень, обрезайте весной.",
                FlowerType.Pion => "Поливайте обильно в жару, любит солнце, подкармливайте весной.",
                FlowerType.Sunflower => "Поливайте регулярно, нужно много солнца, высокий рост — подвязывайте.",
                FlowerType.Lily => "Поливайте умеренно, любит свет, не переносит застой воды.",
                _ => "Общие советы: поливайте умеренно, держите на свету."
            };

            Console.WriteLine($"Инструкция по уходу за {type} : {tips}");
        }

        //Перегрузка арифметической оперции +
        public static Flower operator + (Flower A, Flower B)
        {
            if (A.Type != B.Type)
                throw new InvalidOperationException("Ошибка: нельзя складывать цветы разных типов");
            return new Flower(A.Type, A.PlantSize, A.Quantity + B.Quantity);
        }
    }

        public class Bush : Plant
        {

            //Конструктор
            public Bush(FlowerType type, Size size)
                : base(type, size)
            {
            }

            //Метод для строкового представления
            public override string ToString()
            {
                return $"Кустарник {Type}, размер: {PlantSize}";
            }

            public void Cut()
            {
                if (PlantSize.Length > 10)
                {
                    PlantSize  = new Size(PlantSize.Length-10);
                    Console.WriteLine($"Вы подрезали {Type}. Размер: {PlantSize.Length}.");
                }
                else
                {
                    PlantSize = new Size(0);
                    Console.WriteLine($"Вы подрезали {Type} полностью. Куст теперь 0 см.");
                }
            }
        }

    public interface IGarden
    {
        void ShowPlants();

        void AddPlant(Plant plant);

        bool RemovePlant(Plant plant);

        int PlantCount { get; }
    }


    public class Garden : IGarden
    {
        private List<Plant> Plants; //Лист для растений
        public string Name { get; set; } //Название сада

        public int PlantCount => Plants.Count; //Количество растений

        //Конструкторы
        //Пустой сад
        public Garden()
        {
            Plants = new List<Plant>();
            Name = "Мой сад";
        }

        //Пустой сад с именем
        public Garden(string name)
        {
            Plants = new List<Plant>();
            Name = name;
        }

        //Сад с растениями
        public Garden(string name, List<Plant> plants)
        {
            Plants = new List<Plant>(plants); //копирование листа
            Name = name;
        }

        //
        public Plant GetPlant(int index)
        {
            if (index < 0 || index >= Plants.Count)
                return null;
            return Plants[index];
        }

        //Метод для добавления растения
        public void AddPlant(Plant plant)
        {
            if (plant == null)
            {
                Console.WriteLine("Нельзя добавить несуществующее растение.");
                return;
            }

            Plants.Add(plant); //Дабавление растения в лист
        }

        //Метод для удаления растения
        public bool RemovePlant(Plant plant)
        {
            bool removed = Plants.Remove(plant);
            return removed;
        }

        public bool RemovePlantAt(int index)
        {
            if (index < 0 || index >= Plants.Count)
                return false;

            Plant removed = Plants[index];
            Plants.RemoveAt(index);
            return true;
        }

        //Метод для вывода информации о саде
        public void ShowPlants()
        {
            Console.WriteLine($"Растения в саду {Name}:");

            if (Plants.Count == 0)
            {
                Console.WriteLine("Сад пуст");
                return;
            }

            for (int i = 0; i < Plants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Plants[i]}");
            }

            Console.WriteLine($"Всего растений: {Plants.Count}");
        }

        //Метод для полива сада
        public void WaterAll()
        {
            Console.WriteLine($"Производится полив всех растений в саду {Name}");
            foreach (Plant plant in Plants)
                plant.Watering();
        }

        //Метод для очистки сада
        public void Clear()
        {
            Plants.Clear();
            Console.WriteLine($"Сад {Name} был очищен.");
        }
    }

    class Program
    {
        static Garden garden = new Garden("Сад Сереги");
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                MainMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddMenu(); break;
                    case "2": RemoveMenu(); break;
                    case "3": ActionMenu(); break;
                    case "4": Clear(); break;
                    case "5": Rename(); break;
                    case "6": GardenInfo(); break;
                    case "0": return;
                    default: break;
                }
            }
        }

        static void MainMenu()
        {
            Console.WriteLine("ГЛАВНОЕ МЕНЮ");
            Console.WriteLine("1. Добавить растение");
            Console.WriteLine("2. Удалить растение");
            Console.WriteLine("3. Действие с растением");
            Console.WriteLine("4. Очистить сад");
            Console.WriteLine("5. Переименовать сад");
            Console.WriteLine("6. Информация о саде");
            Console.WriteLine("0. Выход");
            Console.Write("Ваш выбор: ");
        }

        //Добавление
        static void AddMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ДОБАВЛЕНИЕ РАСТЕНИЯ");
                Console.WriteLine("1. Добавить цветок");
                Console.WriteLine("2. Добавить куст");
                Console.WriteLine("0. Назад (главное меню)");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var flower = CreateFlower();
                        if (flower != null) garden.AddPlant(flower);
                        Pause();
                        break;

                    case "2":
                        var bush = CreateBush();
                        if (bush != null) garden.AddPlant(bush);
                        Pause();
                        break;

                    case "0": return;

                    default: break;
                }
            }
        }

        //Удаление
        static void RemoveMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("УДАЛЕНИЕ РАСТЕНИЯ");

                // Если сад пуст — нечего удалять
                if (garden.PlantCount == 0)
                {
                    Console.WriteLine("Сад пуст, удалять нечего.");
                    Pause();
                    return;
                }

                garden.ShowPlants();
                Console.Write("\nВведите номер удаляемого растения (0 - назад): ");

                // Безопасное чтение числа
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Неверный ввод.");
                    Pause();
                    continue;
                }

                // Назад
                if (choice == 0)
                    return;

                // Проверка диапазона
                if (choice < 1 || choice > garden.PlantCount)
                {
                    Console.WriteLine($"Неверный номер. Введите от 1 до {garden.PlantCount}.");
                    Pause();
                    continue;
                }

                // Удаляем (переводим в 0-based индекс)
                bool removed = garden.RemovePlantAt(choice - 1);

                if (removed == true)
                    Console.WriteLine($"\nРастение удалено.");
                else
                    Console.WriteLine("\nНе удалось удалить.");

                Pause();
            }
        }

        //Действия с растениями
        static void ActionMenu()
        {
            while (true)
            {
                Console.Clear();
                if (garden.PlantCount == 0)
                {
                    Console.WriteLine("Сад пуст — действия с растениями недоступны.");
                    Pause();
                    return;
                }
                Console.WriteLine("ДЕЙСТВИЯ С РАСТЕНИЯМИ");
                Console.WriteLine("1. Полить все растения");
                Console.WriteLine("2. Выбрать растение для действия");
                Console.WriteLine("0. Назад");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": garden.WaterAll(); Pause(); break;
                    case "2": ChooseMenu(); break;
                    case "0": return;
                    default: break;
                }
            }
        }

        //Меню выбора растения
        static void ChooseMenu()
        {
            while (true)
            {
                Console.Clear();
                garden.ShowPlants();
                Console.WriteLine("\nВыберите растение (0 - выход)");

                //Безопасное чтение числа
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Неверный ввод.");
                    Pause();
                    continue;
                }

                //Назад
                if (choice == 0)
                    return;

                //Проверка диапазона
                if (choice < 1 || choice > garden.PlantCount)
                {
                    Console.WriteLine($"Неверный номер. Введите от 1 до {garden.PlantCount}.");
                    Pause();
                    continue;
                }

                Plant selected = garden.GetPlant(choice - 1);

                if (selected == null)
                {
                    Console.WriteLine("Растение не найдено.");
                    Pause();
                    continue;
                }

                if (selected is Flower flower)
                    FlowerAction(flower);
                else if (selected is Bush bush)
                    BushAction(bush);
            }
        }

        //Действия с цветком
        static void FlowerAction(Flower flower)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{flower}. Действия");
                Console.WriteLine("1. Полить");
                Console.WriteLine("2. Вдохнуть аромат");
                Console.WriteLine("3. Срезать");
                Console.WriteLine("4. Добавить такое же");
                Console.WriteLine("5. Получить инструкции по уходу");
                Console.WriteLine("0. Назад");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": flower.Watering(); Pause(); break;
                    case "2": flower.Smelling(); Pause(); break;
                    case "3": flower.Cut(); Pause(); break;
                    case "4":
                        {
                            Flower other = new Flower(flower);
                            garden.AddPlant(other);
                            Pause();
                            break;
                        }
                    case "5": Flower.GetTips(flower.Type); Pause(); break;
                    case "0": return;
                    default: break;
                }    
            }
        }

        //Действия с кустом
        static void BushAction(Bush bush)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{bush}. Действия");
                Console.WriteLine("1. Полить");
                Console.WriteLine("2. Подрезать");
                Console.WriteLine("0. Назад");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": bush.Watering(); Pause(); break;
                    case "2": bush.Cut(); Pause(); break;
                    case "0": return;
                    default: break;
                }
            }
        }

        //Очистка сада
        static void Clear()
        {
            if (garden.PlantCount == 0)
            {
                Console.WriteLine("\nСад уже пуст.");
                Pause();
                return;
            }

            Console.WriteLine("Вы уверены, что хотите очистить сад(Y/N)?");
            string answer = Console.ReadLine();

            if (answer == "Y" || answer == "y")
            {
                garden.Clear();
                Pause();
            }
            else
            {
                Console.WriteLine("\nОчистка отменена.");
                Pause();
            }
        }
        
        //Переименование
        static void Rename()
        {
            Console.Write("Введите новое название сада: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("\nНазвание не может быть пустым.");
                Pause();
                return;
            }

            garden.Name = name.Trim();
            Console.WriteLine($"Сад переименован в «{garden.Name}»");
        }

        //Информация о саде
        static void GardenInfo()
        {
            Console.Clear();
            garden.ShowPlants();
            Pause();
        }

        //Метод для добавления цветка
        static Flower CreateFlower()
        {
            Console.WriteLine("ДОБАВЛЕНИЕ РАСТЕНИЯ");
            Console.WriteLine("Доступные виды растений");
            var types = Enum.GetValues<FlowerType>();
            for (int i = 0; i < types.Length; i++)
                Console.WriteLine($"{i + 1}. {types[i]}");
            Console.Write("Ваш выбор: ");

            //Вид
            int typeIdx = int.Parse(Console.ReadLine()) - 1;
            FlowerType type = types[typeIdx];

            //Длина стебля
            Console.Write("Длина стебля (см): ");
            double length = double.Parse(Console.ReadLine());

            //Количество
            Console.Write("Количество: ");
            int quantity = int.Parse(Console.ReadLine());

            var flower = new Flower(type, new Size(length), quantity);
            Console.WriteLine($"Добавлено {flower}");
            return flower;
        }

        //Метод для
        static Bush CreateBush()
        {
            Console.WriteLine("ДОБАВЛЕНИЕ КУСТА");
            Console.WriteLine("Доступные виды растений");
            var types = Enum.GetValues<FlowerType>();
            for (int i = 0; i < types.Length; i++)
                Console.WriteLine($"{i + 1}. {types[i]}");
            Console.Write("Ваш выбор: ");

            //Вид
            int typeIdx = int.Parse(Console.ReadLine()) - 1;
            FlowerType type = types[typeIdx];

            //Длина
            Console.Write("Длина (см): ");
            double length = double.Parse(Console.ReadLine());

            var bush = new Bush(type, new Size(length));
            Console.WriteLine($"Добавлен {bush}");
            return bush;
        }

        static void Pause()
        {
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }
}
