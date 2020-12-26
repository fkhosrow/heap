using System;

namespace BinaryHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Min Heap:\n");

            var heap = new BinaryHeap(5, BinaryHeapType.MIN);
            heap.Insert(10);
            heap.Insert(5);
            heap.Insert(6);
            heap.Insert(2);
            heap.Insert(1);
            var min = heap.GetRoot();
            Console.WriteLine("Minimum is " + min.Value);

            Console.WriteLine("Sorted:");
            var sortedArray = heap.Sort();
            foreach (var v in sortedArray)
            {
                Console.Write(v);
                Console.Write(",");
            }

            Console.WriteLine("\nMax Heap:\n");

            heap.Clear();

            heap = new BinaryHeap(5, BinaryHeapType.MAX);
            heap.Insert(1);
            heap.Insert(5);
            heap.Insert(2);
            heap.Insert(6);
            heap.Insert(10);
            var max = heap.GetRoot();
            Console.WriteLine("Maximum is " + max.Value);

            Console.WriteLine("Sorted:");
            sortedArray = heap.Sort();
            foreach (var v in sortedArray)
            {
                Console.Write(v);
                Console.Write(",");
            }
        }
    }
}
