using System;

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
                for (int i = 0; i < idx && curr == null; i++)
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

        //Конструктор копирования
        public Stack(Stack other)
        {
            if (other == null || other.Top == null) return;

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
        public static Stack operator >(Stack stack)
        {
            if (stack == null || stack.Top == null) return stack;
            //Создаем временный список и переписываем туда стек
            List<int> temp = new List<int>();
            SNode curr = stack.Top;
            while (curr != null)
            {
                temp.Add(curr.Value);
                curr = curr.Next;
            }
            temp.Sort(); //Сортируем список
            temp.Reverse(); //Переворачиваем список
            Stack sortedStack = new Stack();
            foreach (var val in temp)
                sortedStack = sortedStack + val;
            return sortedStack;
        }
    }

    class Program
    {
        static void Main()
        {

        }
    }
}

