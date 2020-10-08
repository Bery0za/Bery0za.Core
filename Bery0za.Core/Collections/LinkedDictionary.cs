using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bery0za.Core.Collections
{
    [Serializable]
    public class LinkedDictionary<T, U> : IDictionary<T, U>
    {
        public int Count => _order.Count;
        public virtual bool IsReadOnly => _dictionary.IsReadOnly;
        
        public ICollection<T> Keys => _order;
        public ICollection<U> Values => _order.Select(key => _dictionary[key]).ToList();

        public T First => _order.First;
        public T Last => _order.Last;

        public LinkedListNode<T> FirstNode => _order.FirstNode;
        public LinkedListNode<T> LastNode => _order.LastNode;
        
        public U this[T key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }
        
        private readonly IDictionary<T, U> _dictionary;
        private readonly IDictionary<T, LinkedListNode<T>> _nodes;
        private readonly LinkedList<T> _order;
        
        public LinkedDictionary()
            : this(EqualityComparer<T>.Default)
        {
            
        }

        public LinkedDictionary(IEqualityComparer<T> comparer)
        {
            _dictionary = new Dictionary<T, U>(comparer);
            _nodes = new Dictionary<T, LinkedListNode<T>>();
            _order = new LinkedList<T>();
        }
        
        public void Add(KeyValuePair<T, U> item)
        {
            Add(item.Key, item.Value);
        }
        
        public void Add(T key, U value)
        {
            if (key == null) throw new ArgumentNullException();
            if (_dictionary.ContainsKey(key)) throw new ArgumentException();
            if (_dictionary.IsReadOnly) throw new NotSupportedException();
            
            _nodes.Add(key, _order.AddLast(key));
            _dictionary.Add(key, value);
        }

        public LinkedListNode<T> AddAfter(T element, T key, U value)
        {
            if (!_nodes.ContainsKey(element)) throw new Exception("Exciting element is not in list.");
            LinkedListNode<T> node = _nodes[element];

            return AddAfter(node, key, value);
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T key, U value)
        {
            var newNode = _order.AddAfter(node, key);
            _nodes.Add(key, newNode);
            _dictionary.Add(key, value);

            return newNode;
        }

        public LinkedListNode<T> AddBefore(T element, T key, U value)
        {
            if (!_nodes.ContainsKey(element)) throw new Exception("Exciting element is not in list.");
            LinkedListNode<T> node = _nodes[element];

            return AddBefore(node, key, value);
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T key, U value)
        {
            var newNode = _order.AddBefore(node, key);
            _nodes.Add(key, newNode);
            _dictionary.Add(key, value);

            return newNode;
        }

        public void AddFirst(T key, U value)
        {
            if (key == null) throw new ArgumentNullException();
            if (_dictionary.ContainsKey(key)) throw new ArgumentException();
            if (_dictionary.IsReadOnly) throw new NotSupportedException();

            _nodes.Add(key, _order.AddFirst(key));
            _dictionary.Add(key, value);
        }

        public void Clear()
        {
            _dictionary.Clear();
            _nodes.Clear();
            _order.Clear();
        }

        public bool Contains(KeyValuePair<T, U> item)
        {
            return _dictionary.Contains(item);
        }
        
        public bool ContainsKey(T key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<T, U>[] array, int arrayIndex)
        {
            _order.Select(key => new KeyValuePair<T, U>(key, _dictionary[key]))
                  .ToList()
                  .CopyTo(array, arrayIndex);
        }

        public T ElementAfter(T element)
        {
            if (!_nodes.ContainsKey(element)) throw new Exception("Exciting element is not in list.");
            LinkedListNode<T> node = _nodes[element];
            if (node.Next == null) throw new Exception("Element is last.");

            return node.Next.Value;
        }

        public T ElementBefore(T element)
        {
            if (!_nodes.ContainsKey(element)) throw new Exception("Exciting element is not in list.");
            LinkedListNode<T> node = _nodes[element];
            if (node.Previous == null) throw new Exception("Element is first.");

            return node.Previous.Value;
        }

        public LinkedListNode<T> FindNode(T element)
        {
            if (element == null) throw new NullReferenceException("Element can't be null.");
            return _nodes.TryGetValue(element, out LinkedListNode<T> node) ? node : null;
        }

        public IEnumerator<KeyValuePair<T, U>> GetEnumerator()
        {
            return _order.Select(key => new KeyValuePair<T, U>(key, _dictionary[key]))
                         .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(KeyValuePair<T, U> item)
        {
            if (!_dictionary.Contains(item)) return false;
            _dictionary.Remove(item);
            _order.Remove(_nodes[item.Key]);
            _nodes.Remove(item.Key);

            return true;
        }
        
        public bool Remove(T key)
        {
            if (!_nodes.TryGetValue(key, out LinkedListNode<T> node)) return false;
            _dictionary.Remove(key);
            _order.Remove(node);
            _nodes.Remove(key);

            return true;
        }
        
        public bool TryGetValue(T key, out U value)
        {
            return _dictionary.TryGetValue(key, out value);
        }
    }
}