using System;
using System.Collections.Generic;
using System.Linq;

using Bery0za.Core.Extensions;

namespace Bery0za.Core.Collections
{
    [Serializable]
    public class LinkedList<T> : IList<T>
    {
        public int Count { get; protected set; }
        public bool IsReadOnly => false;

        public T First => _first.Value;
        public T Last => _last.Value;

        public LinkedListNode<T> FirstNode => _first;
        public LinkedListNode<T> LastNode => _last;

        public T this[int index]
        {
            get => NodeAt(index).Value;
            set => NodeAt(index).Value = value;
        }

        protected LinkedListNode<T> _first;
        protected LinkedListNode<T> _last;
        protected Dictionary<T, LinkedListNode<T>> _nodes;

        private IEqualityComparer<T> _comparer;

        public LinkedList()
            : this(EqualityComparer<T>.Default) { }

        public LinkedList(IEqualityComparer<T> comparer)
        {
            _comparer = comparer;
            _nodes = new Dictionary<T, LinkedListNode<T>>(_comparer);
        }

        public void Add(T item)
        {
            AddLast(item);
        }

        public void Add(IEnumerable<T> range)
        {
            range.ForEach(Add);
        }

        public LinkedListNode<T> AddAfter(T element, T item)
        {
            LinkedListNode<T> node = FindNode(element);
            if (node == null) throw new Exception("Exsiting element is not in list.");

            return AddAfter(node, item);
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T item)
        {
            var newNode = new LinkedListNode<T>(item);

            if (node == _last) AddLast(newNode);
            else AddAfter(node, newNode);

            return newNode;
        }

        protected void AddAfter(LinkedListNode<T> node, LinkedListNode<T> itemNode)
        {
            if (node.List != this) throw new Exception("Node is not in list.");
            if (itemNode.List != null) throw new Exception("Nodes is already in list.");

            itemNode.List = this;
            itemNode.Previous = node;
            itemNode.Next = node.Next;
            if (node.Next != null) node.Next.Previous = itemNode;
            node.Next = itemNode;
            _nodes.Add(itemNode.Value, itemNode);
            Count++;
        }

        public LinkedListNode<T> AddBefore(T element, T item)
        {
            LinkedListNode<T> node = FindNode(element);
            if (node == null) throw new Exception("Exsiting element is not in list.");

            return AddBefore(node, item);
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T item)
        {
            var newNode = new LinkedListNode<T>(item);

            if (node == _first) AddFirst(newNode);
            else AddBefore(node, newNode);

            return newNode;
        }

        protected void AddBefore(LinkedListNode<T> node, LinkedListNode<T> itemNode)
        {
            if (node.List != this) throw new Exception("Node is not in list.");
            if (itemNode.List != null) throw new Exception("Nodes is already in list.");

            itemNode.List = this;
            itemNode.Previous = node.Previous;
            itemNode.Next = node;
            if (node.Previous != null) node.Previous.Next = itemNode;
            node.Previous = itemNode;
            _nodes.Add(itemNode.Value, itemNode);
            Count++;
        }

        public LinkedListNode<T> AddFirst(T item)
        {
            LinkedListNode<T> node = FindNode(item);
            LinkedListNode<T> newNode = new LinkedListNode<T>(item);
            AddFirst(newNode);
            return newNode;
        }

        protected virtual void AddFirst(LinkedListNode<T> node)
        {
            if (node.List != null) throw new Exception("Nodes is already in list.");

            if (_first == null)
            {
                node.List = this;
                _first = node;
                _last = node;
                _nodes.Add(node.Value, node);
                Count++;
            }
            else
            {
                AddBefore(_first, node);
                _first = node;
            }
        }

        public LinkedListNode<T> AddLast(T item)
        {
            LinkedListNode<T> node = FindNode(item);
            LinkedListNode<T> newNode = new LinkedListNode<T>(item);
            AddLast(newNode);
            return newNode;
        }

        protected virtual void AddLast(LinkedListNode<T> node)
        {
            if (node.List != null) throw new Exception("Nodes is already in list.");

            if (_first == null)
            {
                node.List = this;
                _first = node;
                _last = node;
                _nodes.Add(node.Value, node);
                Count++;
            }
            else
            {
                AddAfter(_last, node);
                _last = node;
            }
        }

        private bool CheckIndex(int index)
        {
            Guard.GreaterThanOrEqual(index, 0, "Index must be non-negative.");
            Guard.LessThan(index, Count, "Index was out of range.");
            return true;
        }

        public void Clear()
        {
            _nodes.ForEach(kvp => ResetNode(kvp.Value));
            _nodes.Clear();
            _first = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return _nodes.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null) throw new NullReferenceException();
            if (arrayIndex < 0 || arrayIndex >= Count) throw new ArgumentOutOfRangeException();
            if (array.Length < Count - arrayIndex) throw new ArgumentException();

            LinkedListNode<T> cur = NodeAt(arrayIndex);

            for (int i = arrayIndex; i < Count; i++)
            {
                array[i - arrayIndex] = cur.Value;
                cur = cur.Next;
            }
        }

        public virtual T ElementAfter(T element)
        {
            LinkedListNode<T> node = FindNode(element);
            if (node == null) throw new Exception("Exsiting element is not in list.");
            if (node.Next == null) throw new Exception("Element is last.");

            return node.Next.Value;
        }

        public virtual T ElementBefore(T element)
        {
            LinkedListNode<T> node = FindNode(element);
            if (node == null) throw new Exception("Exsiting element is not in list.");
            if (node.Previous == null) throw new Exception("Element is first.");

            return node.Previous.Value;
        }

        public LinkedListNode<T> FindNode(T element)
        {
            if (element == null) throw new NullReferenceException("Element can't be null.");

            return _nodes.TryGetValue(element, out LinkedListNode<T> node) ? node : null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListIterator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new LinkedListIterator(this);
        }

        public int IndexOf(T item)
        {
            LinkedListNode<T> cur = _first;

            for (int i = 0; i < Count; i++)
            {
                if (_comparer.Equals(cur.Value, item)) return i;

                cur = cur.Next;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            CheckIndex(index);
            LinkedListNode<T> node = NodeAt(index);
            AddBefore(node, item);
        }

        public LinkedListNode<T> NodeAt(int index)
        {
            CheckIndex(index);

            if (index == 0) return _first;

            LinkedListNode<T> res = _first;

            while (index > 0)
            {
                res = res.Next;
                index--;
            }

            return res;
        }

        public bool Remove(T element)
        {
            LinkedListNode<T> node = FindNode(element);
            if (node == null) return false;

            return Remove(node, false);
        }

        public virtual bool Remove(LinkedListNode<T> node, bool throwIfAnotherList = true)
        {
            if (node.List != this)
                return throwIfAnotherList ? throw new Exception("Node is not a part of the list.") : false;

            if (Count == 1)
            {
                _first = null;
                _last = null;
            }
            else if (node == _first)
            {
                _first = _first.Next;
                _first.Previous = null;
            }
            else if (node == _last)
            {
                _last = _last.Previous;
                _last.Next = null;
            }
            else
            {
                node.Previous.Next = node.Next;
                if (node.Next != null) node.Next.Previous = node.Previous;
            }

            _nodes.Remove(node.Value);
            ResetNode(node);
            Count--;
            return true;
        }

        public void RemoveAt(int index)
        {
            Remove(NodeAt(index));
        }

        protected void ResetNode(LinkedListNode<T> node)
        {
            node.List = null;
            node.Previous = null;
            node.Next = null;
        }

        private class LinkedListIterator : IEnumerator<T>
        {
            private int _i = -1;
            private LinkedListNode<T> _current;
            private LinkedList<T> _list;

            public LinkedListIterator(LinkedList<T> list)
            {
                _list = list;
                _current = new LinkedListNode<T> { Next = _list._first };
            }

            public T Current => _current.Value;

            public void Dispose() { }

            object System.Collections.IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (++_i < _list.Count)
                {
                    _current = _current.Next;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                _i = -1;
                _current = new LinkedListNode<T> { Next = _list._first };
            }
        }
    }
}