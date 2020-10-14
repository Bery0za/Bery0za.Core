using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bery0za.Core.Collections
{
    public class LinkedHashSet<T> : ICollection<T>
    {
        public int Count => _nodes.Count;
        public virtual bool IsReadOnly => _nodes.IsReadOnly;

        private readonly IDictionary<T, LinkedListNode<T>> _nodes;
        private readonly LinkedList<T> _order;

        public LinkedHashSet()
            : this(EqualityComparer<T>.Default) { }

        public LinkedHashSet(IEqualityComparer<T> comparer)
        {
            _nodes = new Dictionary<T, LinkedListNode<T>>(comparer);
            _order = new LinkedList<T>();
        }

        public bool Add(T item)
        {
            if (_nodes.ContainsKey(item)) return false;

            LinkedListNode<T> node = _order.AddLast(item);
            _nodes.Add(item, node);
            return true;
        }

        public bool Add(IEnumerable<T> range)
        {
            return range.Aggregate(true, (next, el) => next && Add(el));
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public void Clear()
        {
            _nodes.Clear();
            _order.Clear();
        }

        public bool Contains(T item)
        {
            return _nodes.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _order.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _order.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(T item)
        {
            if (!_nodes.TryGetValue(item, out LinkedListNode<T> node)) return false;

            _nodes.Remove(item);
            _order.Remove(node);
            return true;
        }
    }
}