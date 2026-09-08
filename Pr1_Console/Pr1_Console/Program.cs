using System;
using System.Collections.Generic;
using System.Data;

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
        const char SingleJoinTop = '\u252C'; // ┬  (Стыковка сверху)
        const char SingleJoinBottom = '\u2534'; // ┴  (Стыковка снизу)

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Norton Commander"; //Устанавливаем имя окна

            //Устанавливаем размеры окна
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);

            Console.CursorVisible = false; //Делаем курсор невидимым
            DrawBackground(0, 167, 160);//Закраска заднего фона

            //Отрисовка элементов интерфейса
            DrawTopMenu();
            DrawBottomCommandLine();
            DrawFunctionKeys();
            DrawLeftPanel();
            DrawRightPanel();

            Console.ReadLine();
        }

        //
        static void DrawBackground(byte r, byte g, byte b)
        {
            Console.Write($"\u001b[48;2;{r};{g};{b}m\u001b[38;2;0;0;0m");
            string row = new string(' ', 80);
            for (int y = 0; y < 25; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write(row);
            }
        }

        //Цветовые заготовки для выбора:
        //Для текста
        static void SetPanelColors()
        {
            Console.Write("\u001b[48;2;0;0;168m");   // Синий фон
            Console.Write("\u001b[38;2;0;255;255m"); // Голубой текст
        }

        //Для выделения
        static void SetSelectionColors()
        {
            Console.Write("\u001b[48;2;0;255;255m"); // Голубой фон
            Console.Write("\u001b[38;2;0;0;168m");   // Синий текст
        }

        //Запись с выбранной точки
        static void PrintAt(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        //Верхнее меню
        static void DrawTopMenu()
        {
            Console.Write("\u001b[38;2;0;0;0m"); //Черный шрифт
            PrintAt(0, 0, "   Левая     Файл     Диск     Команды     Правая                              ");
            SetSelectionColors();
            PrintAt(75, 0, " 8 30");
        }

        //Командная строка снизу
        static void DrawBottomCommandLine()
        {
            Console.Write("\u001b[48;2;0;0;0m\u001b[38;2;255;255;255m"); //Черный фон, белый шрифт
            PrintAt(0, 23, "C:\\NC>                                                                          ");
        }

        //Цункциональные кнопки снизу
        static void DrawFunctionKeys()
        {
            Console.SetCursorPosition(0, 24);
            string[] keys = { "Помощь", "Вызов ", "Чтение", "Правка", "Копия ", "НовИмя", "НовКат", "Удал-е", "Меню  ", "Выход" }; //Создаем массив строк

            for (int i = 0; i < keys.Length; i++)
            {
                //Печатаем число
                Console.Write("\u001b[48;2;0;0;0m\u001b[38;2;255;255;255m"); //Черный фон, белый шрифт
                Console.Write(i + 1);

                //Печатаем текст
                Console.Write("\u001b[48;2;0;167;160m\u001b[38;2;0;0;0m"); //Бирюзовый фон, черный шрифт
                Console.Write(keys[i]);

                //Чёрный разделительный пробел между кнопками
                if (i < keys.Length - 1)
                    Console.Write("\u001b[48;2;0;0;0m ");
            }
        }

        //Левая панель
        static void DrawLeftPanel()
        {
            DrawDoubleBox(0, 1, 39, 22, " C:\\NC ", false);
            Console.Write("\u001b[48;2;0;0;168m\u001b[38;2;255;255;255m"); //Синий фон, белый шрифт
            PrintAt(1, 2, "C:\u2193  Имя         Имя           Имя");
            SetPanelColors();
            DrawHorizLine(0, 21, 39, DoubleJoinLeft, DoubleJoinRight);

            //PrintAt(13, 1, DoubleJoinTop.ToString());
            //PrintAt(26, 1, DoubleJoinTop.ToString());
            for (int y = 2; y <= 20; y++)
            {
                PrintAt(13, y, SingleVert.ToString());
                PrintAt(26, y, SingleVert.ToString());
            }
        }

        //Правая панель
        static void DrawRightPanel()
        {
            DrawDoubleBox(39, 1, 41, 22, " C:\\NC ", true);
        }

        //Двойные рамки для панелей
        static void DrawDoubleBox(int left, int top, int width, int height, string title, bool status)
        {
            SetPanelColors();
            PrintAt(left, top, DoubleTL + new string(DoubleHoriz, width - 2) + DoubleTR); //Верхняя граница
            //Боковые границы
            for (int y = 1; y < height - 1; y++)
            {
                PrintAt(left, top + y, DoubleVert + new string(' ', width - 2) + DoubleVert);
            }
            PrintAt(left, top + height - 1, DoubleBL + new string(DoubleHoriz, width - 2) + DoubleBR); //Нижняя граница

            //Печать надписи
            if (status)
                SetSelectionColors();
            else
                SetPanelColors();

            PrintAt(left + (width - title.Length) / 2, top, title);
        }

        //Горизонтальная линия
        static void DrawHorizLine(int left, int top, int width, char leftChar, char rightChar)
        {
            PrintAt(left, top, leftChar + new string(SingleHoriz, width - 2) + rightChar);
        }


    }
}