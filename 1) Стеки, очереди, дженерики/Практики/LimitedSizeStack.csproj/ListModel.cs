using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {

        public enum Action
        {
            AddItem, RemoveItem
        }

        public List<TItem> Items { get; }
        public int Limit { get; }
        private readonly LimitedSizeStack<Tuple<Action, int, TItem>> stack;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            stack = new LimitedSizeStack<Tuple<Action, int, TItem>>(limit);
        }

        public void AddItem(TItem item)
        {
            stack.Push(Tuple.Create(Action.AddItem, Items.Count, item));
            Items.Add(item);
        }

        public void RemoveItem(int index)
        {
            stack.Push(Tuple.Create(Action.RemoveItem, index, Items[index]));
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            if (stack.Count > 0)
            {
                return stack.Count > 0;
            }
            else
            {
                return false;
            }
        }

        public void Undo()
        {
            var (firstItem, secondItem, thirdItem) = stack.Pop();
            if (firstItem == Action.AddItem) Items.RemoveAt(stack.Count);
            else if (firstItem == Action.RemoveItem)
            {
                if (stack.Count == 1) Items.Insert(secondItem - 1, thirdItem);
                else Items.Insert(secondItem, thirdItem);
            }
        }
    }
}
