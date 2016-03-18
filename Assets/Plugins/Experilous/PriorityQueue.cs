/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;

namespace Experilous
{
	/// <summary>
	/// A priority queue based on the heap data structure, providing constant-time access and logarithmic-time
	/// removal of the highest priority item, and logarithmic insertion of items.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the priority queue.</typeparam>
	public class PriorityQueue<T> where T : IEquatable<T>
	{
		public delegate bool AreOrderedDelegate(T lhs, T rhs);

		private T[] _heap;
		private int _size;

		private AreOrderedDelegate _areOrdered;

		public PriorityQueue(AreOrderedDelegate areOrdered, int initialCapacity)
		{
			if (initialCapacity < 0) throw new ArgumentOutOfRangeException("initialCapacity", initialCapacity, "The initial capacity of the priority queue heap cannot be negative.");
			if (initialCapacity > 0) _heap = new T[initialCapacity];
			_areOrdered = areOrdered;
		}

		/// <summary>
		/// The element in the queue with the highest priority.
		/// </summary>
		public T front
		{
			get
			{
				if (_size > 0) return _heap[0];
				throw new InvalidOperationException("The front element cannot be accessed when the priority queue is empty.");
			}
		}

		/// <summary>
		/// The number of elements in the queue.
		/// </summary>
		public int Count { get { return _size; } }

		/// <summary>
		/// Adds an element to the queue.  It's position in the queue will be determined by the <see cref="AreOrderedDelegate"/> functor provided during construction of the queue.
		/// </summary>
		/// <param name="item">The item to be added to the queue.</param>
		public void Push(T item)
		{
			var index = _size;
			if (index >= _heap.Length) ExtendHeap();
			_heap[index] = item;
			++_size;
			BubbleUp(index);
		}

		/// <summary>
		/// Removes the highest priority element from the queue.
		/// </summary>
		public void Pop()
		{
			if (_size > 1)
			{
				--_size;
				_heap[0] = _heap[_size];
				Array.Clear(_heap, _size, 1);
				BubbleDown(0);
			}
			else if (_size == 1)
			{
				Array.Clear(_heap, 0, 1);
				_size = 0;
			}
			else
			{
				throw new InvalidOperationException("The front element cannot be popped when the priority queue is empty.");
			}
		}

		/// <summary>
		/// Recomputes the priority of the indicated item using the <see cref="AreOrderedDelegate"/> functor provided during construction of the queue, and repositions the item accordingly within the internal data structure.
		/// </summary>
		/// <param name="item">The item to be reprioritized.</param>
		public void Reprioritize(T item)
		{
			for (int index = 0; index < _size; ++index)
			{
				if (_heap[index].Equals(item))
				{
					_heap[index] = item;
					if (index == 0)
					{
						BubbleDown(0);
					}
					else if (index * 2 + 1 >= _size)
					{
						BubbleUp(index);
					}
					else
					{
						var parentIndex = (index - 1) / 2;
						if (_areOrdered(_heap[parentIndex], _heap[index]))
						{
							BubbleDown(index);
						}
						else
						{
							BubbleUp(index);
						}
					}
					return;
				}
			}
			throw new InvalidOperationException("The item provided cannot be reprioritized, as it was not found within the priority queue.");
		}

		/// <summary>
		/// Removes all elements from the queue.
		/// </summary>
		/// <remarks>This does not deallocate any memory used by the internal data structure.
		/// It therefore enables reuse of the priority queue instance to cut down on the cost
		/// of allocation and garbage collection.</remarks>
		public void Clear()
		{
			_size = 0;
		}

		/// <summary>
		/// Removes all elements from the queue, and sets a new <see cref="AreOrderedDelegate"/> functor which will be applied to any elements pushed later on.
		/// </summary>
		/// <remarks>This does not deallocate any memory used by the internal data structure.
		/// It therefore enables reuse of the priority queue instance to cut down on the cost
		/// of allocation and garbage collection.</remarks>
		public void Reset(AreOrderedDelegate areOrdered)
		{
			Clear();
			_areOrdered = areOrdered;
		}

		private void BubbleUp(int index)
		{
			while (index > 0)
			{
				var parentIndex = (index - 1) / 2;
				if (_areOrdered(_heap[parentIndex], _heap[index])) break;
				Utility.Swap(ref _heap[index], ref _heap[parentIndex]);
				index = parentIndex;
			}
		}

		private void BubbleDown(int index)
		{
			var leftChildIndex = index * 2 + 1;
			var rightChildIndex = leftChildIndex + 1;
			while (leftChildIndex < _size)
			{
				if (rightChildIndex < _size)
				{
					if (_areOrdered(_heap[index], _heap[leftChildIndex]) && _areOrdered(_heap[index], _heap[rightChildIndex])) break;
					if (_areOrdered(_heap[leftChildIndex], _heap[rightChildIndex]))
					{
						Utility.Swap(ref _heap[index], ref _heap[leftChildIndex]);
						index = leftChildIndex;
					}
					else
					{
						Utility.Swap(ref _heap[index], ref _heap[rightChildIndex]);
						index = rightChildIndex;
					}
					leftChildIndex = index * 2 + 1;
					rightChildIndex = leftChildIndex + 1;
				}
				else
				{
					if (_areOrdered(_heap[index], _heap[leftChildIndex])) break;
					Utility.Swap(ref _heap[index], ref _heap[leftChildIndex]);
					index = leftChildIndex;
					break;
				}
			}
		}

		private void ExtendHeap()
		{
			var extendedHeap = new T[_heap.Length * 2];
			Array.Copy(_heap, extendedHeap, _size);
			_heap = extendedHeap;
		}
	}
}
