using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TA_LinkedList_TwoSided;
using System.Collections.Generic;

namespace LinkedList_Test
{
    // Класс для проведения Unit тестов
    [TestClass]
    public class LinkedList_UnitTest1
    {
        public TA_LinkedList_TwoSided.LinkedList<int> list;

        // Вспомогательный метод для создания нового двусвязного списка в 'ручном' режиме, без использования тестируемых методов двусвязного списка
        public TA_LinkedList_TwoSided.LinkedList<int> ManualListCreation()
        {
            return new TA_LinkedList_TwoSided.LinkedList<int>();
        }

        public TA_LinkedList_TwoSided.LinkedList<int> ManualListCreation(int[] collection)
        {
            TA_LinkedList_TwoSided.LinkedList<int> list = new TA_LinkedList_TwoSided.LinkedList<int>();
            for (int i = 0; i < collection.Length; i++)
            {
                Node<int> node = new Node<int>(collection[i]);
                if (list.Length == 0)
                {
                    node.Next = node;
                    node.Prev = node;
                    list.head = node;
                }
                else
                {
                    list.tail.Next = node;
                    node.Prev = list.tail;
                }
                list.tail = node;
                list.tail.Next = list.head;
                list.head.Prev = list.tail;
                list.Length++;
            }

            return list;
        }

        // Для более качественного тестирования поля 'head' и 'tail' временно получили модификатор доступа 'public'
        [TestMethod]
        public void IsEmpty_Test()
        {
            list = ManualListCreation();
            Assert.IsTrue(list.IsEmpty(), "Ошибка в 'IsEmpty()' при пусом списке");

            list = ManualListCreation(new int[1] { 1 });
            Assert.IsFalse(list.IsEmpty(), "Ошибка в 'IsEmpty()' при непустом списке");
        }

        [TestMethod]
        public void Length_Test()
        {
            list = ManualListCreation();
            Assert.IsTrue(list.Length == 0, "Ошибка 'Length' при количестве элементов = 0");

            list = ManualListCreation(new int[2] { 1, 2 });
            Assert.IsTrue(list.Length == 2, "Ошибка 'Length' при количестве элементов = 2");
        }

        [TestMethod]
        public void Clear_Test()
        {
            list = ManualListCreation(new int[2] { 1, 2 });
            list.Clear();
            Assert.IsTrue(list.Length == 0 && list.head == null, "Ошибка в 'Clear()'");
        }

        [TestMethod]
        public void GetByIndex_Test()
        {
            list = ManualListCreation(new int[3] { 1, 2, 3 });
            Assert.IsTrue(list.GetByIndex(1).Data == 2, "Ошибка в 'GetByIndex()' при наличии элемента");
        }

        [TestMethod]
        public void Find_Test()
        {
            list = ManualListCreation(new int[3] { 1, 2, 3 });
            Assert.IsTrue(list.Find(2).Data == 2, "Ошибка в 'Find()' при наличии элемента в списке");
            Assert.IsTrue(list.Find(5) == null, "Ошибка в 'Find()' при отсутствии элемента в списке");
        }

        [TestMethod]
        public void FindMin_Test()
        {
            list = ManualListCreation();
            Assert.IsTrue(list.FindMin() == null, "Ошибка в 'FindMin()' при пустом списке");

            list = ManualListCreation(new int[3] { 3, 1, 2 });
            Assert.IsTrue(list.FindMin().Data == 1, "Ошибка в 'FindMin()' при непустом списке");
        }

        [TestMethod]
        public void FindMax_Test()
        {
            list = ManualListCreation();
            Assert.IsTrue(list.FindMax() == null, "Ошибка в 'FindMax()' при пустом списке");

            list = ManualListCreation(new int[3] { 3, 1, 2 });
            Assert.IsTrue(list.FindMax().Data == 3, "Ошибка в 'FindMax()' при непустом списке");
        }

        [TestMethod]
        public void Contains_Test()
        {
            list = ManualListCreation(new int[3] { 1, 2, 3 });
            Assert.IsTrue(list.Contains(2), "Ошибка в 'Contains()' при наличии элемента");
            Assert.IsFalse(list.Contains(4), "Ошибка в 'Contains()' при отсутствии элемента");
        }

        [TestMethod]
        public void PushFront_Test()
        {
            list = ManualListCreation(new int[3] { 6, 7, 8 });
            list.PushFront(1);
            list.PushFront(2);
            list.PushFront(3);
            Assert.AreEqual(3, list.head.Data, "Ошибка в 'PushFront(T)'");

            list = ManualListCreation(new int[3] { 6, 7, 8 });
            list.PushFront(new int[3] { 1, 2, 3 });
            Assert.AreEqual(1, list.head.Data, "Ошибка в 'PushFront(T[])'");

            list = ManualListCreation(new int[3] { 6, 7, 8 });
            list.PushFront(new List<int> { 1, 2, 3 });
            Assert.AreEqual(1, list.head.Data, "Ошибка в 'PushFront(List<T>)'");
        }

        [TestMethod]
        public void PushBack_Test()
        {
            list = ManualListCreation(new int[3] { 6, 7, 8 });
            list.PushBack(1);
            list.PushBack(2);
            list.PushBack(3);
            Assert.AreEqual(3, list.tail.Data, "Ошибка в 'PushBack(T)'");

            list = ManualListCreation(new int[3] { 6, 7, 8 });
            list.PushBack(new int[3] { 1, 2, 3 });
            Assert.AreEqual(3, list.tail.Data, "Ошибка в 'PushBack(T[])'");

            list = ManualListCreation(new int[3] { 6, 7, 8 });
            list.PushBack(new List<int> { 1, 2, 3 });
            Assert.AreEqual(3, list.tail.Data, "Ошибка в 'PushBack(List<T>)'");
        }

        [TestMethod]
        public void Insert_Test()
        {
            list = ManualListCreation(new int[3] { 6, 7, 8 });
            list.Insert(1, 1);
            Assert.AreEqual(1, list.head.Next.Data, "Ошибка в 'Insert(T, index)'");

            list = ManualListCreation(new int[3] { 6, 7, 8 });
            list.Insert(new int[3] { 1, 2, 3 }, 1);
            Assert.AreEqual(2, list.head.Next.Next.Data, "Ошибка в 'Insert(T[], index)'");

            list = ManualListCreation(new int[3] { 6, 7, 8 });
            list.Insert(new List<int> { 1, 2, 3 }, 1);
            Assert.AreEqual(2, list.head.Next.Next.Data, "Ошибка в 'Insert(List<T>, index)'");
        }

        [TestMethod]
        public void PopFront_Test()
        {
            list = ManualListCreation();
            Assert.IsTrue(list.PopFront() == null, "Ошибка в 'PopFront()' при пустом списке");

            list = ManualListCreation(new int[3] { 6, 7, 8 });
            Assert.IsTrue(list.PopFront().Data == 6, "Ошибка в 'PopFront()' при непустом списке");
        }

        [TestMethod]
        public void PopBack_Test()
        {
            list = ManualListCreation();
            Assert.IsTrue(list.PopBack() == null, "Ошибка в 'PopBack()' при пустом списке");

            list = ManualListCreation(new int[3] { 6, 7, 8 });
            Assert.IsTrue(list.PopBack().Data == 8, "Ошибка в 'PopBack()' при непустом списке");
        }

        [TestMethod]
        public void Extract_Test()
        {
            list = ManualListCreation();
            Assert.IsTrue(list.Extract(1) == null, "Ошибка в 'Extract()' при пустом списке");

            list = ManualListCreation(new int[3] { 6, 7, 8 });
            Assert.IsTrue(list.Extract(1).Data == 7, "Ошибка в 'Extract()' при непустом списке");
        }

        [TestMethod]
        public void Remove_Test()
        {
            list = ManualListCreation();
            list.Remove(1);
            Assert.AreEqual(list, new TA_LinkedList_TwoSided.LinkedList<int>(), "Ошибка в 'Remove()' при пустом списке");

            list = ManualListCreation(new int[3] { 1, 2, 3 });
            list.Remove(1);
            Assert.IsTrue(list.head.Data == 1 && list.head.Next.Data == 3, "Ошибка в 'Remove()' при непустом списке");
        }

        [TestMethod]
        public void RemoveMin_Test()
        {
            list = ManualListCreation();
            list.RemoveMin();
            Assert.IsTrue(list.head == null, "Ошибка в 'RemoveMin()' при пустом списке");

            list = ManualListCreation(new int[1] { 1 });
            list.RemoveMin();
            Assert.IsTrue(list.head == null, "Ошибка в 'RemoveMin()' при списке с одним элементом");

            list = ManualListCreation(new int[3] { 3, 1, 2 });
            list.RemoveMin();
            Assert.IsTrue(list.head.Data == 3 && list.head.Next.Data == 2, "Ошибка в 'RemoveMin()' при списке с несколькими элементами");
        }

        [TestMethod]
        public void RemoveMax_Test()
        {
            list = ManualListCreation();
            list.RemoveMax();
            Assert.IsTrue(list.head == null, "Ошибка в 'RemoveMax()' при пустом списке");

            list = ManualListCreation(new int[1] { 1 });
            list.RemoveMax();
            Assert.IsTrue(list.head == null, "Ошибка в 'RemoveMax()' при списке с одним элементом");

            list = ManualListCreation(new int[3] { 3, 1, 2 });
            list.RemoveMax();
            Assert.IsTrue(list.head.Data == 1 && list.head.Next.Data == 2, "Ошибка в 'RemoveMax()' при списке с несколькими элементами");
        }

        [TestMethod]
        public void InsertionSort_Test()
        {
            list = ManualListCreation(new int[5] { 2, 5, 1, 3, 4 });
            list.InsertionSort();
            Assert.IsTrue(list.head.Data == 1 
                && list.head.Next.Data == 2 
                && list.head.Next.Next.Data == 3 
                && list.head.Next.Next.Next.Data == 4 
                && list.head.Next.Next.Next.Next.Data == 5, "Ошибка в 'InsertionSort()'");
        }

        [TestMethod]
        public void ToString_Test()
        {
            list = ManualListCreation(new int[3] { 1, 2, 3 });
            Assert.IsTrue(list.ToString().Contains("1 2 3"), "Ошибка в 'ToString()'");
        }

        [TestMethod]
        public void ToArray_Test()
        {
            list = ManualListCreation(new int[3] { 1, 2, 3 });
            var listArr = list.ToArray();
            int[] arr = new int[3] { 1, 2, 3 };
            bool check = Equals(listArr.GetType(), arr.GetType());
            if(check)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (!Equals(listArr[i], arr[i]))
                    {
                        check = false;
                    }
                }
            }
            Assert.IsTrue(check, "Ошибка в 'ToArray()'") ;
        }

        [TestMethod]
        public void ToList_Test()
        {
            list = ManualListCreation(new int[3] { 1, 2, 3 });
            var listList = list.ToList();
            List<int> arr = new List<int> { 1, 2, 3 };
            bool check = Equals(listList.GetType(), arr.GetType());
            if (check)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (!Equals(listList[i], arr[i]))
                    {
                        check = false;
                    }
                }
            }
            Assert.IsTrue(check, "Ошибка в 'ToList()'");
        }
    }
}
