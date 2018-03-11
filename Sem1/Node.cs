﻿namespace Sem1
{
    public class Node<T>
    {
        public Node<T> Previous { get; set; }
        public Node<T> Next { get; set; }
        public T Item { get; set; }
        public Node(T item) => Item = item;
    }
}