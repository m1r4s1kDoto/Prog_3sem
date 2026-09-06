using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NortonCommanderInterface
{
    class Program
    {
        //Символы псевдографики:
        const char SingleHoriz = '\u2500'; // ─

        const char SingleVert = '\u2502'; // │

        const char DoubleHoriz = '\u2550'; // ═

        const char DoubleVert = '\u2551'; // ║

        const char DoubleTL = '\u2554'; // ╔

        const char DoubleTR = '\u2557'; // ╗ 

        const char DoubleBL = '\u255A'; // ╚ 

        const char DoubleBR = '\u255D'; // ╝ 

        const char DoubleJoinLeft = '\u255F'; // ╟ 

        const char DoubleJoinRight = '\u2562'; // ╢ 

        static void DrawBackgroundRGB(byte r, byte g, byte b)
        {
            // Сбрасываем системные цвета .NET, чтобы они не перебивали ANSI
            Console.ResetColor();

            // Отправляем ANSI-код цвета фона (\u001b[48;2;R;G;Bm) и черного текста (\u001b[38;2;0;0;0m)
            Console.Write($"\u001b[48;2;{r};{g};{b}m\u001b[38;2;0;0;0m");

            // Очищаем экран — теперь Windows зальет его ИМЕННО этим RGB оттенком
            Console.Clear();
        }

        //Метод для установки курсора в точку и вывода текста
        static void PrintAt(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        //Метод для отрисовки рамки панели
        static void DrawDoubleBox(int left, int top, int width, int height, string title)
        {
            //Устанавливаем цвет
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            //Верхняя граница
            PrintAt(left, top, DoubleTL + new string(DoubleHoriz, width - 2) + DoubleTR);

            //Боковые стенки
            for (int y = height - 1;  y >= 0;  y--)
                PrintAt(left, top + y, DoubleVert + new string(' ', width - 2) + DoubleVert);



        }

        static void Main()
        {
            //Фиксируем размер окна и даем ему название
            Console.Title = "Norton Commander";
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);

            Console.CursorVisible = false; //Делаем курсор невидимым

            // 1. Включаем UTF-8 и режим поддержки ANSI в системе
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            EnableAnsi();

            // 2. Устанавливаем точный цвет RGB(0, 167, 160)
            DrawBackgroundRGB(0, 167, 160);


            Console.ReadLine();
        }

        static void EnableAnsi()
        {
            var handle = GetStdHandle(-11);
            GetConsoleMode(handle, out uint mode);
            SetConsoleMode(handle, mode | 0x0004); // Включает флаг ENABLE_VIRTUAL_TERMINAL_PROCESSING
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
    }
}
