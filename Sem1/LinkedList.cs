using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem1
{
    class LinkedList<T> : IEnumerable<T>
    {
        public int Count { get; private set; }
        public Node<T> First { get; set; }
        public Node<T> Last { get; set; }
        public Node<T> Node { get; set; }

        public LinkedList()
        {
            First = Last = null;
        }

        public void AddFirst(T item)
        {
            if (First == null)
            {
                First = Last = new Node<T>(item);
            }
            else
            {
                var newNode = new Node<T>(item);
                newNode.Next = First;
                First.Previous = newNode;
                First = newNode;
            }
            Count++;
        }
        
        public void AddLast(T item)
        {
            if (Last == null)
            {
                First = Last = new Node<T>(item);
            }
            else
            {
                var newNode = new Node<T>(item);
                Last.Next = newNode;
                newNode.Previous = Last;
                Last = newNode;
            }
            Count++;
        }

        public void AddAfter(Node<T> prevNode, T item)
        {
            if (prevNode == Last)
            {
                AddLast(item);
            }
            var newNode = new Node<T>(item)
            {
                Next = prevNode.Next,
                Previous = prevNode
            };
            prevNode.Next = newNode;
            if (newNode.Next != null) 
            {
                newNode.Next.Previous = newNode;
            }
            Count++;
        }

        public void AddBefore(Node<T> nextNode, T item)
        {
            if (nextNode == First)
            {
                AddFirst(item);
            }
            var newNode = new Node<T>(item)
            {
                Next = nextNode,
                Previous = nextNode.Previous
            };
            nextNode.Previous = newNode;
            if (newNode.Previous != null)
            {
                newNode.Previous.Next = newNode;
            }
            Count++;
        }
        
        public void Remove(Node<T> node)
        {
            if (node == First)
            {
                First = First.Next;
            }
            if (node.Next != null)
            {
                node.Next.Previous = node.Previous;
            }
            if (node.Previous != null)
            {
                node.Previous.Next = node.Next;
            }
            if (node == null)
            {
                return;
            }
            Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = First;
            while (current != null) 
            {
                yield return current.Item;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}