using System;

namespace Bery0za.Core.Collections
{
    public class CircularLinkedList<T> : LinkedList<T>
    {
        protected override void AddFirst(LinkedListNode<T> node)
        {
            if (node.List != null) throw new Exception("Nodes is already in list.");

            if (_first == null)
            {
                node.List = this;
                node.Previous = node;
                node.Next = node;
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

        protected override void AddLast(LinkedListNode<T> node)
        {
            if (node.List != null) throw new Exception("Nodes is already in list.");

            if (_first == null)
            {
                node.List = this;
                node.Previous = node;
                node.Next = node;
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
        
        public override bool Remove(LinkedListNode<T> node, bool throwIfAnotherList = true)
        {
            if (node.List != this) return throwIfAnotherList ? throw new Exception("Node is not part of list.") : false;
            
            if (Count == 1)
            {
                _first = null;
            }
            else 
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
                if (node == _first) _first = _first.Next;
                if (node == _last) _last = _last.Previous;
            }
            
            _nodes.Remove(node.Value);
            ResetNode(node);
            Count--;
            return true;
        }
        
        public override T ElementAfter(T element)
        {
            LinkedListNode<T> node = FindNode(element);
            if (node == null) throw new Exception("Exsiting element is not in list.");
            if (node.Next == null) throw new Exception("Element has no next node but should. Is changed outside?");
            return node.Next.Value;
        }
        
        public override T ElementBefore(T element)
        {
            LinkedListNode<T> node = FindNode(element);
            if (node == null) throw new Exception("Exsiting element is not in list.");
            if (node.Previous == null) throw new Exception("Element has no previous node but should. Is changed outside?");
            return node.Previous.Value;
        }
    }
}