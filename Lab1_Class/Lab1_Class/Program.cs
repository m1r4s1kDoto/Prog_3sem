using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;

namespace Lab1_Class
{
    //Класс для девайсов
    public class ComputerDevice
    {
        public const int MaxMfrLength = 30; //Ограничение для полей

        //Поля для хранения
        protected string manufacturer; //Производитель
        protected string model; //Модель

        //Свойство для производителя
        public virtual string Manufacturer
        {
            get => manufacturer;
            set
            {
                Validate(value);
                manufacturer = value;
            }
        }

        //Свойство для модели
        public virtual string Model
        {
            get => model;
            set
            {
                Validate(value);
                model = value;
            }
        }

        //Конструктор по умолчанию
        public ComputerDevice()
        {
            Manufacturer = "Unknown";
            Model = "Unknown";
        }

        //Конструктор с параметрами
        public ComputerDevice(string manufacturer, string model)
        {
            Manufacturer = manufacturer;
            Model = model;
        }

        //Метод для проверки строк
        protected void Validate(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException("Ошибка: строка не может быть пустой");
            if (str.Length >= MaxMfrLength)
                throw new ArgumentOutOfRangeException("Ошибка: введенная строка слишком длинная");
        }

        public override string ToString()
        {
            return $"Устройство: производитель: {Manufacturer}, модель {Model}";
        }
    }

    //Перечисление для типов клавиатур
    public enum KeyboardType
    {
        Membrane, //Мембранная
        Mechanical, //Механическая
        Scissor, //Сенсорная
        Optical, //Оптическая
        Undefined //Неизвестно
    }

    //Класс для клавиатур
    class Keyboard : ComputerDevice
    {
        //Ограничения для веса
        public const double MinWeight = 100;
        public const double MaxWeight = 5000;

        //Поля класса
        public KeyboardType Type { get; init; } //Тип клавиатуры
        public bool Backlight { get; init; } //Наличие подсветки
        public double Weight { get; init; } //Вес в граммах

        //Конструктор по умолчанию
        public Keyboard()
            : base()
        {
            Type = KeyboardType.Undefined;
            Backlight = false;
            Weight = MinWeight;
        }

        //Конструктор с параметрами
        public Keyboard(string manufacturer, string model,  KeyboardType type, bool backlight, double weight)
            : base(manufacturer, model)
        {
            ValidateWeight(weight);
            Type = type;
            Backlight = backlight;
            Weight = weight;
        }

        //Метод для проверки веса
        private void ValidateWeight(double w)
        {
            if (w < MinWeight || w > MaxWeight)
                throw new ArgumentOutOfRangeException("Ошибка: недопустимый вес клавиатуры");
        }

        //Метод для преобразования перечисления
        public static string TypeToString(KeyboardType type)
        {
            return type switch
            {
                KeyboardType.Membrane => "Мембранная",
                KeyboardType.Mechanical => "Механическая",
                KeyboardType.Scissor => "Ножничная",
                KeyboardType.Optical => "Оптическая",
                _ => "Не указано"
            };
        }

        public override string ToString()
        {
            return $"Клавиатура:\nПроизводитель: {Manufacturer}, Модель: {Model}, Тип: {Type}, Наличие подсветки: {(Backlight ? "Есть" : "Нет")}, Вес: {Weight} грамм";
        }
    }

    //Перечисление для типов матриц
    public enum PanelType
    {
        IPS, //IPS (In-Plane Switching)
        VA, //VA (Vertical Alignment)
        TN, //TN (Twisted Nematic)
        OLED, //OLED (Organic Light Emitting Diode)
        QLED, //QLED (Quantum Dot LED)
        miniLED, //Mini-LED
        undefined //Неизвестно
    };

    //
    class Monitor : ComputerDevice
    {
        //Ограничения для полей:
        public const double MinDiagonal = 10.0; //Минимальная диагональ
        public const double MaxDiagonal = 60.0; //Максимальная диагональ
        public const int MinRefrate = 60; //Минимальная частота обновления
        public const int MaxRefrate = 360; //Максимальня частота обновления
        public const int MinRes = 640; //Минимальная длина одной стороны в пикселях
        public const int MaxRes = 7680; //Максимальная длина одной стороны в пикселях

        //Поля класса
        public double Diagonal { get; init; } //Диагональ
        public int Width { get; init; } //Ширина в пикселях
        public int Height { get; init; } //Высота в пикселях
        public int RefreshRate { get; init; } //Частота обновления
        PanelType type { get; init; } //Тип матрциы

        //Метод для проверки диагонали
        private void ValidateDiagonal(double d)
        {
            if (d < MinDiagonal || d > MaxDiagonal)
                throw new ArgumentOutOfRangeException("Ошибка: недопустимая диагональ экрана");
        }

        //Метод для проверки разрешения
        private void ValidateResolution(int w, int h)
        {
            if (h < MinRes || w < MinRes || h > MaxRes || w > MaxRes)
                throw new ArgumentOutOfRangeException("Ошибка: недопустимое разрешенме экрана");
        }

        private void ValidateRefreshRate(int r)
        {
            if (r < MinRefrate || r > MaxRefrate)
                throw new ArgumentOutOfRangeException("Ошибка: недопустимая частота обновления экрана");
        }

        //Конструктор по умолчанию
        public Monitor()
            : base()
        {
            Diagonal = MinDiagonal;
            Width = MinRes;
            Height = MinRes;

        }

        //Конструктор с параметрами
        public Monitor(string manufacturer, string model, double diagonal, int width, int height, int refreshRate, PanelType type) 
            : base (manufacturer, model)
        {
            ValidateDiagonal(diagonal);
            ValidateResolution(width, height);
            ValidateRefreshRate(refreshRate);
            Diagonal = diagonal;
            Width = width;
            Height = height;
            RefreshRate = refreshRate;
            Panel
        }
    }

    class Program
    {

    }
}
