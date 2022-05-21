using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        private readonly List<T> list = new List<T>();
        private readonly object objecT = new object();
        /// <summary>
        /// Возвращает элемент по индексу или null, если такого элемента нет.
        /// При присвоении удаляет все элементы после.
        /// Если индекс в точности равен размеру коллекции, работает как Append.
        /// </summary>
        public T this[int index]
        {
            get
            {
                lock (objecT)
                    return Count <= index ? null : list[index];
            }
            set
            {
                lock (objecT)
                {
                    if (Count > index)
                    {
                        list[index] = value;
                        list.RemoveRange(index + 1, list.Count - 1 - index);
                    }

                    else if (index == list.Count)
                    {
                        list.Add(value);
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает последний элемент или null, если такого элемента нет
        /// </summary>
        public T LastItem()
        {
            lock (objecT)
                if (list.Count == 0)
                {
                    return null;
                }
                else
                {
                    return list[list.Count - 1];
                }
        }

        /// <summary>
        /// Добавляет item в конец только если lastItem является последним элементом
        /// </summary>
        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (objecT)
            {
                if (LastItem() == knownLastItem)
                    list.Add(item);
            }
        }

        /// <summary>
        /// Возвращает количество элементов в коллекции
        /// </summary>
        public int Count
        {
            get
            {
                lock (objecT)
                {
                    return list.Count;
                }
            }
        }
    }
}