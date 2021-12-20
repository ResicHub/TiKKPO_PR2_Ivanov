using System;
using System.Collections.Generic;

namespace TA_LinkedList_TwoSided
{
    // Реализация двусвязного списка
    public struct LinkedList<T> where T : IComparable<T>
    {
        // Указатели на первый и последний элементы двусвязного списка
        public Node<T> head;
        public Node<T> tail;

        // Возвращает количесво элементов в двусвязном списке
        public int Length;

        // Возвращает 'true' если список пуст 
        public bool IsEmpty()
        {
            if (head == null && Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Метод для добавления элемента или коллекции элементов в начало двусвязного списка
        public void PushFront(T item)
        {
            Node<T> node = new Node<T>(item);

            if (IsEmpty())
            {
                node.Next = node;
                node.Prev = node;
                tail = node;
            }
            else
            {
                node.Next = head;
                node.Prev = tail;
                head.Prev = node;
                tail.Next = node;
            }
            head = node;

            Length++;
        }

        public void PushFront(T[] collection)
        {
            for (int i = collection.Length-1; i >= 0; i--)
            {
                PushFront(collection[i]);
            }
        }

        public void PushFront(List<T> collection)
        {
            T[] newCollection = collection.ToArray();
            PushFront(newCollection);
        }

        // Метод для добавления элемента или коллекции элементов в конец двусвязного списка
        public void PushBack(T item)
        {
            Node<T> node = new Node<T>(item);

            if (IsEmpty())
            {
                node.Next = node;
                node.Prev = node;
                head = node;
            }
            else
            {
                tail.Next = node;
                node.Prev = tail;
            }
            tail = node;
            tail.Next = head;
            head.Prev = tail;

            Length++;
        }

        public void PushBack(T[] collection)
        {
            foreach (T item in collection)
            {
                PushBack(item);
            }
        }

        public void PushBack(List<T> collection)
        {
            T[] newCollection = collection.ToArray();
            PushBack(newCollection);
        }

        // Метод для добавления элемента или коллекции элементов на место с индексом 'index'
        public void Insert(T item, int index)
        {
            if (index == 0)
            {
                PushFront(item);
            }
            else if (index == Length)
            {
                PushBack(item);
            }
            else
            {
                Node<T> current = head;

                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }

                Node<T> newNode = new Node<T>(item) 
                { 
                    Prev = current, 
                    Next = current.Next 
                };
                current.Next.Prev = newNode;
                current.Next = newNode;

                Length++;
            }
        }

        public void Insert(T[] collection, int index)
        {
            foreach (T item in collection)
            {
                Insert(item, index);
                index++;
            }
        }

        public void Insert(List<T> collection, int index)
        {
            T[] newCollection = collection.ToArray();
            Insert(newCollection, index);
        }

        // Метод, возвращающий узел с индексом 'index'
        public Node<T> GetByIndex(int index)
        {
            Node<T> current = head;

            if (index > 0)
            {
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
            }

            return current;
        }

        // Метод для нахождения первого узла с элементом 'item', возвращает 'null', если элемент не найден
        public Node<T> Find(T item)
        {
            if (!IsEmpty())
            {
                Node<T> current = head;

                for (int i = 0; i < Length; i++)
                {
                    if (Equals(current.Data, item))
                    {
                        return current;
                    }
                    current = current.Next;
                }
            }

            return null;
        }

        // Метод для поиска наименьшего элемента двусвязного списка
        public Node<T> FindMin()
        {
            if (IsEmpty())
            {
                return null;
            }

            Node<T> current = head;
            Node<T> minNode = current;

            for (int i = 0; i < Length; i++)
            {
                current = current.Next;
                if (minNode.Data.CompareTo(current.Data) > 0)
                {
                    minNode = current;
                }
            }

            return minNode;
        }

        // Метод для поиска наибольшего элемента двусвязного списка
        public Node<T> FindMax()
        {
            if (IsEmpty())
            {
                return null;
            }

            Node<T> current = head;
            Node<T> maxNode = current;

            for (int i = 0; i < Length; i++)
            {
                current = current.Next;
                if (maxNode.Data.CompareTo(current.Data) < 0)
                {
                    maxNode = current;
                }
            }

            return maxNode;
        }

        // Метод, проверяющий вхождение элемента 'item' в двусвязный список
        public bool Contains(T item)
        {
            if (Find(item) != null)
            {
                return true;
            }

            return false;
        }

        // Метод, который извлекает и возвращает начальный узел двусвязного списка
        public Node<T> PopFront()
        {
            if (IsEmpty())
            {
                return null;
            }

            Node<T> nodeForReturn = head;

            if (Length == 1)
            {
                head = null;
                tail = null;
            }
            else
            {
                head = head.Next;
                head.Prev = tail;
                tail.Next = head;
            }
            Length--;

            return nodeForReturn;
        }

        // Метод, который извлекает и возвращает последний узел двусвязного списка
        public Node<T> PopBack()
        {
            if (IsEmpty())
            {
                return null;
            }

            Node<T> nodeForReturn = tail;

            if (Length == 1)
            {
                head = null;
                tail = null;
            }
            else
            {
                tail = tail.Prev;
                head.Prev = tail;
                tail.Next = head;
            }
            Length--;

            return nodeForReturn;
        }

        // Метод, извлекающий узел с индексом 'index'
        public Node<T> Extract(int index)
        {
            if (IsEmpty())
            {
                return null;
            }
            if (index == 0)
            {
                return PopFront();
            }
            else if (index == Length -1)
            {
                return PopBack();
            }
            else
            {
                Node<T> current = GetByIndex(index);

                current.Prev.Next = current.Next;
                current.Next.Prev = current.Prev;
                Length--;

                return current;
            }
        }

        // Метод для удаления узла с индексом 'index'
        public void Remove(int index)
        {
            Extract(index);
        }

        // Метод для удаления наименьшего элемента двусвязного списка
        public void RemoveMin()
        {
            if (!IsEmpty())
            {
                if (Length == 1)
                {
                    Clear();
                }
                else
                {
                    Node<T> minNode = FindMin();
                    if (Equals(minNode, head))
                    {
                        head = head.Next;
                        head.Prev = tail;
                        tail.Next = head;
                    }
                    else if (Equals(minNode, tail))
                    {
                        tail = tail.Prev;
                        tail.Next = head;
                        head.Prev = tail;
                    }
                    else
                    {
                        minNode.Prev.Next = minNode.Next;
                        minNode.Next.Prev = minNode.Prev;
                    }
                    Length--;
                }
            }
        }

        // Метод для удаления наименьшего элемента двусвязного списка
        public void RemoveMax()
        {
            if (!IsEmpty())
            {
                if (Length == 1)
                {
                    Clear();
                }
                else
                {
                    Node<T> maxNode = FindMax();
                    if (Equals(maxNode, head))
                    {
                        head = head.Next;
                        head.Prev = tail;
                        tail.Next = head;
                    }
                    else if (Equals(maxNode, tail))
                    {
                        tail = tail.Prev;
                        tail.Next = head;
                        head.Prev = tail;
                    }
                    else
                    {
                        maxNode.Prev.Next = maxNode.Next;
                        maxNode.Next.Prev = maxNode.Prev;
                    }
                    Length--;
                }
            }
        }

        // Метод для сортировки вставками двусвязного списка
        public void InsertionSort()
        {
            for (int i = 1; i < Length; i++)
            {
                T tempData = GetByIndex(i).Data;
                Node<T> current = GetByIndex(i);
                int j = i;
                while (j > 0 && tempData.CompareTo(GetByIndex(j-1).Data) == -1)
                {
                    current = current.Prev;
                    j--;
                }
                Insert(tempData, j);
                Remove(i+1);
            }
        }

        // Метод, формирующий строку всех элементов двусвязного списка по порядку
        public override string ToString()
        {
            Node<T> current = head;
            string list = "";

            for (int i = 0; i < Length; i++)
            {
                if (i > 0)
                {
                    current = current.Next;
                }

                list += current.ToString();
                list += " ";
            }

            return list;
        }

        // Метод для перевода двусвязного списка в массив
        public T[] ToArray()
        {
            T[] array = new T[Length];
            Node<T> current = head;

            for (int i = 0; i < Length; i++)
            {
                array[i] = current.Data;
                current = current.Next;
            }

            return array;
        }

        // Метод для перевода двусвязного списка в коллекцию типа 'List<T>'
        public List<T> ToList()
        {
            List<T> list = new List<T>();
            Node<T> current = head;

            for (int i = 0; i < Length; i++)
            {
                list.Add(current.Data);
                current = current.Next;
            }

            return list;
        }

        // Метод, отчищающий двусвязный список
        public void Clear()
        {
            head = null;
            tail = null;
            Length = 0;
        }
    }
}
