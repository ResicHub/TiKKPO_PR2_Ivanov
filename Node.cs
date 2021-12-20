using System;

namespace TA_LinkedList_TwoSided
{
    // Реализация узла двусвязного списка
    public class Node<T> where T : IComparable<T>
    {
        // Конструктор узла
        public Node(T data)
        {
            Data = data;
        }

        // Переменная, хранящаяся в узле
        public T Data { get; set; }

        // Ссылка на предыдущий узел
        public Node<T> Prev { get; set; }

        // Ссылка на следующий узел
        public Node<T> Next { get; set; }

        // Переопределение стандартного метода 'ToStripg()' 
        public override string ToString()
        {
            return $"{Data}";
        }
    }
}
