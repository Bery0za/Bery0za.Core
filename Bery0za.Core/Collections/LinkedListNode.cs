using System;

namespace Bery0za.Core.Collections
{
    [Serializable]
    public sealed class LinkedListNode<T> : ILinked<LinkedListNode<T>>
    {
        public LinkedList<T> List
        {
            get => _list;
            internal set => _list = _list == null || value == null
                ? value
                : throw new AccessViolationException("Node is already assigned to another list.");
        }

        public LinkedListNode<T> Next { get; set; }
        public LinkedListNode<T> Previous { get; set; }
        public T Value { get; set; }

        private LinkedList<T> _list;

        internal LinkedListNode(T value = default)
        {
            Value = value;
        }
    }
}