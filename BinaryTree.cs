using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTrees;

public class TreeNode<T> where T : IComparable
{
    public T Value;
    public TreeNode<T> Left, Right;
    public int Weight;
}
public class BinaryTree<T> : IEnumerable<T> where T : IComparable
{
    private TreeNode<T> Tree;
    private int Count = 0;
    public void Add(T key)
    {
        if (Tree == null)
        {
            Tree = new TreeNode<T>() { Value = key, Weight =  1};
            Count++;
        }
        else TreeValueCompareAndAdd(key, Tree);
    }

    private void TreeValueCompareAndAdd(T key, TreeNode<T> tree)
    {
        if (key.CompareTo(tree.Value) < 0)
            if (tree.Left == null)
            {
                tree.Left = new TreeNode<T>() { Value = key, Weight = 1 };
                Count++;
                tree.Weight++;
            }
            else
            {
                TreeValueCompareAndAdd(key, tree.Left);
                tree.Weight++;
            }
        else if (tree.Right == null)
        {
            tree.Right = new TreeNode<T>() { Value = key, Weight = 1 };
            Count++;
            tree.Weight++;
        }
        else
        {
            TreeValueCompareAndAdd(key, tree.Right);
            tree.Weight++;
        }
    }

    public bool Contains(T key)
    {
        if (Tree == null) return false;
        return TreeValueCompareAndGet(key, Tree);
    }

    private bool TreeValueCompareAndGet(T key, TreeNode<T> tree)
    {
        if (key.CompareTo(tree.Value) == 0)
            return true;
        if (key.CompareTo(tree.Value) < 0)
            if (tree.Left == null)
                return false;
            else return TreeValueCompareAndGet(key, tree.Left);
        else if (tree.Right == null)
            return false;
        else return TreeValueCompareAndGet(key, tree.Right);
    }

    private IEnumerator<T> TreeValueCompareAndReturn(TreeNode<T> tree)
    {
        if (tree == null) yield break;
        var enumeratorForTreeNode = TreeValueCompareAndReturn(tree.Left);
        while (enumeratorForTreeNode.MoveNext())
            yield return enumeratorForTreeNode.Current;
        yield return tree.Value;
        enumeratorForTreeNode = TreeValueCompareAndReturn(tree.Right);
        while (enumeratorForTreeNode.MoveNext())
            yield return enumeratorForTreeNode.Current;
    }

    public IEnumerator<T> GetEnumerator() => TreeValueCompareAndReturn(Tree);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public T this[int index]
    {
        get
        {
            var node = Tree;
            int parrentNumber = 0;
            while (true)
            {
                int currentIndex = (node.Left == null ? 0 : node.Left.Weight) + parrentNumber;
                if (currentIndex == index) return node.Value;
                else if (currentIndex > index)
                    node = node.Left;
                else
                {
                    node = node.Right;
                    parrentNumber = currentIndex + 1;
                }
            }
        }
    }
}

