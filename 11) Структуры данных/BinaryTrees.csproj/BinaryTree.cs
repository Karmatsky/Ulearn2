// Вставьте сюда финальное содержимое файла BinaryTree.cs

using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees
{
    public class TreeNode<T>
    {
        public T Value { get; }
        public TreeNode<T> LeftBranch { get; set; }
        public TreeNode<T> RightBranch { get; set; }

        public TreeNode(T value)
        {
            Value = value;
        }

        public int Size = 1;
    }

    public class BinaryTree<T> : IEnumerable<T> where T : IComparable
    {
        private TreeNode<T> treeN;

        public void Add(T key)
        {
            var binaryTree = treeN;
            if (treeN == null)
            {
                treeN = new TreeNode<T>(key);
            }

            else
            {
                while (true)
                {
                    binaryTree.Size++;
                    if (binaryTree.Value.CompareTo(key) > 0)
                    {
                        if (binaryTree.LeftBranch == null)
                        {
                            binaryTree.LeftBranch = new TreeNode<T>(key);
                            break;
                        }

                        binaryTree = binaryTree.LeftBranch;
                    }
                    else
                    {
                        if (binaryTree.RightBranch == null)
                        {
                            binaryTree.RightBranch = new TreeNode<T>(key);
                            break;
                        }

                        binaryTree = binaryTree.RightBranch;
                    }
                }
            }

        }

        public bool Contains(T key)
        {
            var newTreeN = treeN;
            while (newTreeN != null)
            {
                var result = newTreeN.Value.CompareTo(key);
                if (result == 0)
                {
                    return true;
                }
                newTreeN = result > 0
                    ? newTreeN.LeftBranch
                    : newTreeN.RightBranch;
            }
            return false;
        }

        public T this[int i]
        {
            get
            {
                var tree = treeN;
                while (true)
                {
                    if (tree == null) continue;
                    var leftSize = tree.LeftBranch?.Size ?? 0;
                    if (i == leftSize)
                        return tree.Value;
                    if (i < leftSize)
                        tree = tree.LeftBranch;
                    else if (i > leftSize)
                    {
                        tree = tree.RightBranch;
                        i -= leftSize + 1;
                    }
                }
            }
        }
        private static IEnumerable<T> GetValues(TreeNode<T> treeN)
        {
            while (true)
            {
                if (treeN == null) yield break;
                {
                    foreach (var val in GetValues(treeN.LeftBranch))
                    {
                        yield return val;
                    }
                }
                yield return treeN.Value;
                treeN = treeN.RightBranch;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetValues(treeN).GetEnumerator();
        }
    }
}