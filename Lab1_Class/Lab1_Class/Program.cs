using System;

namespace Lab1_Class
{
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

    class Monitor
    {
        //Ограничения для полей:
        public const double MinDiagonal = 10.0; //Минимальная диагональ
        public const double MaxDiagonal = 60.0; //Максимальная диагональ
        public const int MinRefrate = 60; //Минимальная частота обновления
        public const int MaxRefrate = 360; //Максимальня частота обновления
        public const int MinRes = 640; //Минимальная длина одной стороны в пикселях
        public const int MaxRes = 7680; //Максимальная длина одной стороны в пикселях

        //Поля класса:
        double diagonal; //Диагональ
        int width; //Ширина в пикселях
        int height; //Высота в пикселях
        int refreshRate; //Частота обновления

        //Конструктор с параметрами
        public Monitor(double Diagonal, int Width, int Height, int RefreshRate) 
        {
            diagonal = Diagonal;
            width = Width;
            height = Height;
            refreshRate = RefreshRate;
        }
    }

    class Program
    {

    }
}
