using System;
using System.Security.Cryptography.X509Certificates;

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

    public struct Size
    {
        public double Length; //Длина стебля в сантиметрах

        //Конструктор
        public Size(double length) 
        { 
            Length = length; 
        }

        //Метод для строкового представления
        public override string ToString()
        {
            return $"{Length} см";
        }
    }

    public class Plant
    {
        public FlowerType Type; //Тип цветка
        public Size PlantSize; //Размер цветка

        //Конструктор
        public Plant(FlowerType type, Size size)
        {
            Type = type;
            PlantSize = size;
        }

        //Метод для строкового представления
        public override string ToString()
        {
            return $"Растение: {Type}, размер: {PlantSize}";
        }
    }

    public class Flower : Plant
    {
        public int Quantity; //Количество цветов

        //Конструктор
        public Flower(FlowerType type, Size size, int quantity)
            : base(type, size)
        {
            Quantity = quantity;
        }

        //Метод для строкового представления
        public override string ToString()
        {
            return $"{base.ToString()}, количество: {Quantity} шт.";
        }

        //Метод для поливания цветка
        public void Watering()
        {
            Console.WriteLine($"Вы полили {Type}. Пусть растет он большим и здоровым.");
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
                    PlantSize.Length--;
                    Console.WriteLine($"Вы подрезали {Type}. Размер: {PlantSize.Length}.");
                }
                else
                {
                    PlantSize.Length = 0;
                    Console.WriteLine($"Вы подрезали {Type} полностью. Куст теперь 0 см.");
                }
            }
        }


    class Program
    {
        static void Main()
        {

        }
    }
}
