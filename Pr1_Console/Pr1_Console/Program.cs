using System;
using System.Collections.Generic;
using System.Data;

namespace NortonCommanderInterface
{
    //Класс для файлов и каталогов
    class FileItem
    {
        public string Name { get; set; } //Имя
        public bool IsDirectory { get; set; } //Папка/каталог или нет
        public long Size { get; set; } //Размер в байтах
        public DateTime LastWriteTime { get; set; } //Дата и время последнего изменения файла

        //Конструктор
        public FileItem(string name, bool isDirectory, long size = 0, DateTime? lastWriteTime = null)
        {
            Name = name;
            IsDirectory = isDirectory;
            Size = size;
            LastWriteTime = lastWriteTime ?? DateTime.Now; //Если дата и время неизвестны, вписать текущую
        }
    }


    class Program
    {
        //Символы псевдографики:
        const char SingleHoriz = '\u2500'; // ─
        const char SingleVert = '\u2502'; // │
        const char SingleJoinTop = '\u252C'; // ┬
        const char SingleJoinBottom = '\u2534'; // ┴ 
        const char DoubleHoriz = '\u2550'; // ═
        const char DoubleVert = '\u2551'; // ║
        const char DoubleTL = '\u2554'; // ╔
        const char DoubleTR = '\u2557'; // ╗
        const char DoubleBL = '\u255A'; // ╚
        const char DoubleBR = '\u255D'; // ╝
        const char DoubleJoinLeft = '\u255F'; // ╟
        const char DoubleJoinRight = '\u2562'; // ╢
        const char DoubleJoinTop = '\u2564'; // ╤  
        const char DoubleJoinBottom = '\u2567'; // ╧  

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Norton Commander"; //Устанавливаем имя окна

            //Устанавливаем размеры окна
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);

            Console.CursorVisible = false; //Делаем курсор невидимым

            //Загрузка тестовых данных
            List<FileItem> leftPanelFiles = GetLeftPanelData();
            List<FileItem> rightPanelFiles = GetRightPanelData();

            ClearScreenBlack();
            DrawBackground(0, 167, 160);//Закраска заднего фона

            //Отрисовка элементов интерфейса
            DrawTopMenu();
            DrawFunctionKeys();
            DrawLeftPanel(leftPanelFiles);
            DrawRightPanel(rightPanelFiles);
            DrawBottomCommandLine();

            // Установка курсора в командную строку
            Console.SetCursorPosition(7, 23);
            Console.CursorVisible = true;
            Console.ReadLine();
        }

        static void ClearScreenBlack()
        {
            Console.Write("\u001b[48;2;0;0;0m\u001b[2J\u001b[H");
        }

        //Закраска заднего фона
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
            Console.Write("\u001b[38;2;0;255;255m"); // Голубой шрифт
        }

        //Для выделения
        static void SetSelectionColors()
        {
            Console.Write("\u001b[48;2;0;255;255m"); // Голубой фон
            Console.Write("\u001b[38;2;0;0;168m");   // Синий шрифт
        }

        // Вспомогательный метод для печати слова с жёлтой первой буквой
        static void PrintItemWithHotkey(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            // 1. Первая буква жёлтая
            Console.Write("\u001b[38;2;255;255;0m" + text[0]);
            // 2. Остальной текст чёрный
            Console.Write("\u001b[38;2;0;0;0m" + text.Substring(1));
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
                                                 // Печатаем пункты меню с выделением первых букв
            PrintAt(0, 0, "     ");

            PrintItemWithHotkey("Левая"); Console.Write("    ");
            PrintItemWithHotkey("Файл"); Console.Write("    ");
            PrintItemWithHotkey("Диск"); Console.Write("    ");
            PrintItemWithHotkey("Команды"); Console.Write("    ");
            PrintItemWithHotkey("Правая");

            // Часы справа (синий текст на бирюзовом фоне)
            SetSelectionColors();
            PrintAt(75, 0, " 8 30");
        }

        //Командная строка снизу
        static void DrawBottomCommandLine()
        {
            Console.Write("\u001b[48;2;0;0;0m\u001b[38;2;255;255;255m"); //Черный фон, белый шрифт
            PrintAt(0, 23, "C:\\NC>                                                                          ");
        }

        //Функциональные кнопки снизу
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
        static void DrawLeftPanel(List<FileItem> items)
        {
            DrawDoubleBox(0, 1, 39, 22, " C:\\NC ", false); //Левая двойная рамка
            Console.Write("\u001b[48;2;0;0;168m\u001b[38;2;255;255;255m"); //Синий фон, белый шрифт
            PrintAt(1, 2, "C:\u2193  Имя         Имя          Имя");

            //Разделения внутри левой панели
            SetPanelColors();
            DrawHorizLine(0, 20, 39, DoubleJoinLeft, DoubleJoinRight);
            PrintAt(13, 1, DoubleJoinTop.ToString());
            PrintAt(26, 1, DoubleJoinTop.ToString());
            for (int y = 2; y <= 19; y++)
            {
                PrintAt(13, y, SingleVert.ToString());
                PrintAt(26, y, SingleVert.ToString());
            }
            PrintAt(13, 20, SingleJoinBottom.ToString());
            PrintAt(26, 20, SingleJoinBottom.ToString());

            int maxItemsCol = 17; //Максимальное число файлов в колонке
            for (int i = 0; i < items.Count && i < maxItemsCol * 3; i++)
            {
                int col = i / maxItemsCol; //Текущая колонка
                int row = i % maxItemsCol; //Текущая строка

                int x = 1 + col * 13;
                int y = 3 + row;

                var item = items[i];
                string formattedName = FormatFileName(item.Name, 12); //Форматируем имя

                PrintAt(x, y, formattedName);
            }

            PrintAt(1, 21, "..           ►КАТАЛОГ◄ 11.10.02 19:48");
        }

        //Правая панель
        static void DrawRightPanel(List<FileItem> items)
        {
            DrawDoubleBox(39, 1, 41, 22, " C:\\NC ", true); //Правая двойная рамка
            Console.Write("\u001b[48;2;0;0;168m\u001b[38;2;255;255;255m"); //Синий фон, белый шрифт
            PrintAt(40, 2, $"C:\u2193 Имя         Размер    Дата   Время");

            //Разделения внутри левой панели
            SetPanelColors();
            DrawHorizLine(39, 20, 41, DoubleJoinLeft, DoubleJoinRight);
            PrintAt(53, 1, DoubleJoinTop.ToString());
            PrintAt(63, 1, DoubleJoinTop.ToString());
            PrintAt(72, 1, DoubleJoinTop.ToString());
            for (int y = 2; y <= 19; y++)
            {
                PrintAt(53, y, SingleVert.ToString());
                PrintAt(63, y, SingleVert.ToString());
                PrintAt(72, y, SingleVert.ToString());
            }
            PrintAt(53, 20, SingleJoinBottom.ToString());
            PrintAt(63, 20, SingleJoinBottom.ToString());
            PrintAt(72, 20, SingleJoinBottom.ToString());

            int maxItems = 17; //Максимальное число файлов
            int namePos = 40;
            int sizePos = 54;
            int datePos = 64;
            int timePos = 73;
            for (int i = 0; i < items.Count && i <= maxItems; i++)
            {
                int y = 3 + i;

                var item = items[i];
                string name = FormatFileName(item.Name, 13).PadRight(12);
                string size = item.IsDirectory ? "►КАТАЛОГ◄" : item.Size.ToString();
                string date = item.LastWriteTime.ToString("dd.MM.yy");
                string time = item.LastWriteTime.ToString("H:mm").PadLeft(5);

                //Если файл выделен
                if (i == 0)
                {
                    SetSelectionColors();
                    PrintAt(53, y, SingleVert.ToString());
                    PrintAt(63, y, SingleVert.ToString());
                    PrintAt(72, y, SingleVert.ToString());
                }
                else
                    SetPanelColors();

                PrintAt(namePos, y, name);
                PrintAt(sizePos, y, size);
                PrintAt(datePos, y, date);
                PrintAt(timePos, y, time);
            }

            PrintAt(40, 21, "..            ►КАТАЛОГ◄  11.10.02 19:48");
        }

        //Двойные рамки для панелей
        static void DrawDoubleBox(int left, int top, int width, int height, string title, bool status)
        {
            SetPanelColors();
            PrintAt(left, top, DoubleTL + new string(DoubleHoriz, width - 2) + DoubleTR); //Верхняя граница
            //Боковые границы
            for (int y = 1; y < height - 1; y++)
                PrintAt(left, top + y, DoubleVert + new string(' ', width - 2) + DoubleVert);
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

        //Форматирование имен файлов
        static string FormatFileName(string name, int maxLength)
        {
            int dotIndex = name.LastIndexOf('.'); //Находим последнюю точку

            // Случай 1: Файл имеет расширение (и точка не первая)
            if (dotIndex > 0)
            {
                string baseName = name.Substring(0, dotIndex);
                string ext = name.Substring(dotIndex + 1); // Расширение БЕЗ точки

                // Если имя с расширением целиком влезает в отведённую ширину
                if (name.Length <= maxLength)
                {
                    // Сколько пробелов нужно вставить между именем и расширением
                    int spacesCount = maxLength - baseName.Length - ext.Length;
                    if (spacesCount >= 0)
                    {
                        return baseName + new string(' ', spacesCount) + ext;
                    }
                }

                // Если файл слишком длинный — обрезаем baseName, ставим '~' и прижимаем ext вправо
                int allowedBaseLength = maxLength - ext.Length - 1; // -1 для тильды '~'
                if (allowedBaseLength > 0)
                {
                    string truncatedBase = baseName.Length > allowedBaseLength
                        ? baseName.Substring(0, allowedBaseLength)
                        : baseName;

                    int spacesCount = maxLength - truncatedBase.Length - 1 - ext.Length;
                    return truncatedBase + "~" + new string(' ', Math.Max(0, spacesCount)) + ext;
                }
            }

            // Случай 2: Файл/папка без расширения
            if (name.Length <= maxLength)
                return name.PadRight(maxLength);

            // Если длинное имя без расширения — обрезаем и ставим '~' в конце
            return name.Substring(0, maxLength - 1) + "~";
        }

        //Тестовые данные:
        //Файлы в левой панели
        static List<FileItem> GetLeftPanelData()
        {
            return new List<FileItem>
            {
                new FileItem("..", true),
                new FileItem("GAMES", true),
                new FileItem("DOCUMENTS", true),
                new FileItem("WINDOWS", true),
                new FileItem("SYSTEM32", true),
                new FileItem("Ajaccgdo", false, 417392),
                new FileItem("nc.cfg", false, 255),
                new FileItem("nc_exit.com", false, 255),
                new FileItem("telemax.dat", false, 255),
                new FileItem("nc_exit.doc", false, 255),
                new FileItem("123view.exe", false, 128380),
                new FileItem("arcview.exe", false, 81738),
                new FileItem("MyLongFileNameExample.txt", false, 1024),
                new FileItem("ncdd.exe", false, 255),
                new FileItem("ncedit.exe", false, 255),
                new FileItem("telemax.exe", false, 255),
                new FileItem("vector.exe", false, 255),
                new FileItem("command.com", false, 54619),
                new FileItem("autoexec.bat", false, 128),
                new FileItem("config.sys", false, 210),
                new FileItem("doom.exe", false, 1245088),
                new FileItem("setup.exe", false, 345000),
                new FileItem("readme.txt", false, 4096),
                new FileItem("notes.doc", false, 12288),
                new FileItem("picture.bmp", false, 1440054),
                new FileItem("music.mid", false, 32000),
                new FileItem("database.dbf", false, 512000),
                new FileItem("archive.zip", false, 2048000),
                new FileItem("keyrus.com", false, 12000),
                new FileItem("mouse.com", false, 34000)
            };
        }

        //Файлы в правой панели
        static List<FileItem> GetRightPanelData()
        {
            return new List<FileItem>
            {
                new FileItem("..", true, 0, new DateTime(2002, 10, 11, 19, 48, 0)),
                new FileItem("WORK", true, 0, new DateTime(2026, 01, 15, 10, 30, 0)),
                new FileItem("PROJECTS", true, 0, new DateTime(2026, 03, 01, 14, 20, 0)),
                new FileItem("123view.exe", false, 128380, new DateTime(1995, 05, 25, 5, 00, 0)),
                new FileItem("4372ansi.set", false, 255, new DateTime(1995, 05, 25, 5, 00, 0)),
                new FileItem("8502ansi.set", false, 255, new DateTime(1995, 05, 25, 5, 00, 0)),
                new FileItem("Ajaccgdo", false, 417392, new DateTime(2002, 10, 12, 9, 02, 0)),
                new FileItem("arcview.exe", false, 81738, new DateTime(1995, 05, 25, 5, 00, 0)),
                new FileItem("bitmap.exe", false, 54805, new DateTime(1995, 05, 25, 5, 00, 0)),
                new FileItem("dn.exe", false, 230400, new DateTime(1998, 11, 04, 12, 00, 0)),
                new FileItem("format.com", false, 22500, new DateTime(1993, 08, 11, 6, 00, 0)),
                new FileItem("himem.sys", false, 14200, new DateTime(1993, 08, 11, 6, 00, 0)),
                new FileItem("editor.exe", false, 95000, new DateTime(2001, 04, 19, 18, 15, 0)),
                new FileItem("report_2026.pdf", false, 1048576, new DateTime(2026, 09, 10, 8, 45, 0)),
                new FileItem("very_long_name_test.txt", false, 512, new DateTime(2026, 02, 28, 23, 59, 0))
            };
        }
    }
}