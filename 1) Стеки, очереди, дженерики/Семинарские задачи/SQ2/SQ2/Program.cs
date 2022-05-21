using System;
using System.Collections.Generic;
using System.Text;


namespace SQ2
{
    class Queue
    {
        private Stack<int> stack = new Stack<int>();
        private Stack<int> temp = new Stack<int>();

        public void Push(int Value)
        {
            stack.Push(Value);
        }

        public int Pop()
        {
            while (stack.Count != 0)
                temp.Push(stack.Pop());

            int Value = temp.Pop();

            while (temp.Count != 0)
                stack.Push(temp.Pop());

            return Value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            while (stack.Count != 0)
                temp.Push(stack.Pop());

            while (temp.Count != 0)
            {
                sb.Append(temp.Peek());
                sb.Append(" ");
                stack.Push(temp.Pop());
            }

            return sb.ToString();
        }
    }

    class Program
    {
        static void Main()
        {
            Queue q = new Queue();
            for (int i = 1; i <3; i++)
            {
                q.Push(i);
            }
            

            Console.WriteLine("{0,-10} - исходная очередь", q);
            Console.WriteLine("{1,-10} - После удаления {0}", q.Pop(), q);
            Console.WriteLine("{1,-10} - После удаления {0}", q.Pop(), q);

            Console.ReadLine();
        }
    }
}
