// Array implementation of a heap aka binary heap
// Heap is used in implementation of heap sort and priority queue
// Useful visualization tool: // http://btv.melezinek.cz/binary-heap.html
// This implementation uses a fixed size array because:
// 1. It's easy to compute the array index of a node's children.
// 2. It's massively more efficient to find the Kth element of an array than the Kth element of a linked list. \Advantages of storing a heap as an array rather than a pointer-based binary tree include the following.
// 3. Lower memory usage (no need to store three pointers for every element of the heap).
// 4. Easier memory management (just one object allocated, rather than N).
// 5. Better locality of reference(the items in the heap are relatively close together in memory).

using System;

namespace BinaryHeap
{
    public enum BinaryHeapType
    {
        MIN,
        MAX
    }

    public class BinaryHeap
    {
        int?[] _heap;
        int _numHeapValues;
        BinaryHeapType _heapType;

        /// <summary>
        /// Construct a binary heap of <paramref name="heapSize"/> and <paramref name="type"/>
        /// </summary>
        /// <param name="heapSize"></param>
        /// <param name="type"></param>
        public BinaryHeap(int heapSize, BinaryHeapType type)
        {
            if (heapSize == 0)
            {
                throw new ArgumentException(nameof(heapSize));
            }

            _heap = new int?[heapSize + 1]; // Keep the first node blank
            _numHeapValues = 0;
            _heapType = type;
        }

        /// <summary>
        /// Get the root of the element.
        /// For max heap, this is the max value.
        /// For min heap, this is the min value.
        /// O(1)
        /// </summary>
        /// <returns></returns>
        public int? GetRoot()
        {
            if (_numHeapValues == 0)
            {
                return null;
            }
            else
            {
                return _heap[1]; // First non-blank node is the root
            }
        }

        /// <summary>
        /// Removes the root of the heap and ensures heap property is preserved
        /// Worst case: O(log(n)) i.e. height of the tree
        /// </summary>
        public int? ExtractRoot()
        {
            if (_numHeapValues == 0)
            {
                // Nothing to remove
                return null;
            }

            var root = _heap[1];
            // Swap the last heap value with root value
            _heap[1] = _heap[_numHeapValues];
            // Remove the last element
            _heap[_numHeapValues] = null;
            _numHeapValues--;
            _HeapifyTopToBottom(1);
            
            return root;
        }

        private void _HeapifyTopToBottom(int parentIndex)
        {
            // Compare to child nodes
            int leftChildIndex = parentIndex * 2;
            int rightChildIndex = parentIndex * 2 + 1;
            if (leftChildIndex > _numHeapValues)
            {
                // Reached the end of the heap
                return;
            }

            switch(_heapType)
            {
                case BinaryHeapType.MIN:
                    // There is only a left child node
                    int smallestChildIndex;
                    if (leftChildIndex == _numHeapValues)
                    {
                        smallestChildIndex = leftChildIndex;
                    }
                    else
                    {
                        smallestChildIndex = _heap[leftChildIndex] < _heap[rightChildIndex] ? leftChildIndex : rightChildIndex;
                    }

                    if (_heap[smallestChildIndex] < _heap[parentIndex])
                    {
                        // swap
                        int? tmp = _heap[parentIndex];
                        _heap[parentIndex] = _heap[smallestChildIndex];
                        _heap[smallestChildIndex] = tmp;
                        _HeapifyTopToBottom(smallestChildIndex);
                    }

                    break;
                case BinaryHeapType.MAX:
                    // There is only a left child node
                    int largestChildIndex;
                    if (leftChildIndex == _numHeapValues)
                    {
                        largestChildIndex = leftChildIndex;
                    }
                    else
                    {
                        largestChildIndex = _heap[leftChildIndex] > _heap[rightChildIndex] ? leftChildIndex : rightChildIndex;
                    }

                    if (_heap[largestChildIndex] > _heap[parentIndex])
                    {
                        // swap
                        int? tmp = _heap[parentIndex];
                        _heap[parentIndex] = _heap[largestChildIndex];
                        _heap[largestChildIndex] = tmp;
                        _HeapifyTopToBottom(largestChildIndex);
                    }
                    break;
                default:
                    throw new NotSupportedException(nameof(_heapType));
            }
        }

        /// <summary>
        /// Clears the heap
        /// O(n)
        /// </summary>
        public void Clear()
        {
            for (int i = 1; i < _heap.Length; i++)
            {
                _heap[i] = null;
            }
        }

        /// <summary>
        /// Insert a value into the heap 
        /// Worst case: O(log(n)) i.e. height of the tree
        /// </summary>
        public void Insert(int value)
        {
            if (_numHeapValues >= _heap.Length-1)
            {
                throw new ArgumentException("There is no more space to insert");
            }

            // Insert at the end of the array
            _heap[++_numHeapValues] = value;
            // Maintain the heap property
            _HeapifyBottomToTop(_numHeapValues);
        }

        private void _HeapifyBottomToTop(int index)
        {
            int parentIndex = index / 2;
            if (parentIndex < 1)
            {
                // We reached the root node
                return;
            }

            switch (_heapType)
            {
                case BinaryHeapType.MIN:
                    // Swap child node with its parent if the child node value < parent node value.
                    if (!_heap[parentIndex].HasValue || _heap[index] < _heap[parentIndex])
                    {
                        int tmp = _heap[index].Value;
                        _heap[index] = _heap[parentIndex];
                        _heap[parentIndex] = tmp;
                        _HeapifyBottomToTop(parentIndex);
                    }
                    break;
                case BinaryHeapType.MAX:
                    // Swap child node with its parent if the child node value < parent node value.
                    if (!_heap[parentIndex].HasValue || _heap[index] > _heap[parentIndex])
                    {
                        int tmp = _heap[index].Value;
                        _heap[index] = _heap[parentIndex];
                        _heap[parentIndex] = tmp;
                        _HeapifyBottomToTop(parentIndex);
                    }
                    break;
                default:
                    throw new NotSupportedException(nameof(_heapType));
            }
        }

        /// <summary>
        /// Heap Sort
        /// Outputs a sorted array in desc order.
        /// This extracts elements from the original heap and puts in an array
        /// Theoretically this should copy the original heap to extract from but 
        /// I am too lazy.
        /// O(nlog(n))
        /// </summary>
        /// <returns></returns>
        public int[] Sort()
        {
            if (_numHeapValues == 0)
            {
                // Nothing to sort.
                return null;
            }
            
            // Remember that the first element is blank
            var sortedArray = new int[_heap.Length-1];
            switch(_heapType)
            {
                case BinaryHeapType.MIN:
                    // Remember that the first element is blank
                    for (int i = 0; i < _heap.Length-1; i++)
                    {
                        var min = ExtractRoot();
                        sortedArray[i] = min.Value;
                    }
                    break;
                case BinaryHeapType.MAX:
                    for (int i = _heap.Length-2; i >= 0; i--)
                    {
                        var max = ExtractRoot();
                        sortedArray[i] = max.Value;
                    }
                    break;
                default:
                    throw new NotSupportedException(nameof(_heapType));
            }

            return sortedArray;
        }

        /// <summary>
        /// Print elements in the heap in the correct order
        /// O(n)
        /// </summary>
        public void Print()
        {
            for (int i = 1; i < _heap.Length; i++)
            {
                Console.WriteLine(_heap[i]);
                Console.Write(i == _heap.Length-1 ? "\n" : ",");
            }
        }
    }

}