using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private LinkedList<T> list = new LinkedList<T>();
        private int stackrange;
        public LimitedSizeStack(int stackrange) => this.stackrange = stackrange;


        public void Push(T item)
        {
            list.AddLast(item);
            if (list.Count > stackrange)
                list.RemoveFirst();
        }


        public T Pop()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException();
            }

            else
            {
                var value = list.Last.Value;
                list.RemoveLast();
                return value;
            }
                
        }

        public int Count => list.Count;
    }
}
