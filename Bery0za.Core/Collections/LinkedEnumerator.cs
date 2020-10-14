using System.Collections;
using System.Collections.Generic;

namespace Bery0za.Core.Collections
{
    /// <summary>
    /// Use this iterator if you want to iterate over sequences of elements
    /// that contain links to next and previous items.
    /// </summary>
    /// <typeparam name="T">Type of element, must support <see cref="ILinked{T}"/>.</typeparam>
    internal class LinkedEnumerator<T> : IEnumerator<T>
        where T : ILinked<T>
    {
        private T _start;
        private T _current;
        private bool _justStarted = true;

        public LinkedEnumerator(T start)
        {
            _start = start;
        }

        public bool MoveNext()
        {
            if (_justStarted)
            {
                _current = _start;
                _justStarted = false;
            }
            else
            {
                _current = _current.Next;
            }

            return _current != null && _current.Next != null;
        }

        public void Reset()
        {
            _current = default;
            _justStarted = true;
        }

        public T Current => _current;

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}