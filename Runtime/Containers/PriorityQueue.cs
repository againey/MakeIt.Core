/******************************************************************************\
* Copyright Andy Gainey                                                        *
*                                                                              *
* Licensed under the Apache License, Version 2.0 (the "License");              *
* you may not use this file except in compliance with the License.             *
* You may obtain a copy of the License at                                      *
*                                                                              *
*     http://www.apache.org/licenses/LICENSE-2.0                               *
*                                                                              *
* Unless required by applicable law or agreed to in writing, software          *
* distributed under the License is distributed on an "AS IS" BASIS,            *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.     *
* See the License for the specific language governing permissions and          *
* limitations under the License.                                               *
\******************************************************************************/

using System;
using GeneralUtility = MakeIt.Core.GeneralUtility;

namespace MakeIt.Containers
{
	/// <summary>
	/// A priority queue based on the heap data structure, providing constant-time access and logarithmic-time
	/// removal of the highest priority item, and logarithmic insertion of items.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the priority queue.</typeparam>
	public abstract class PriorityQueue<T> : IPeekableQueue<T> where T : IEquatable<T>
	{
		private T[] _heap;
		private int _size;

		/// <summary>
		/// Constructs an empty priority queue.
		/// </summary>
		protected PriorityQueue()
		{
		}

		/// <summary>
		/// Constructs an empty priority queue with space reserved to store at least <paramref name="initialCapacity"/> elements.
		/// </summary>
		/// <param name="initialCapacity">The minimum number of elements for which storage should be reserved immediately.</param>
		protected PriorityQueue(int initialCapacity)
		{
			if (initialCapacity < 0) throw new ArgumentOutOfRangeException("initialCapacity", initialCapacity, "The initial capacity of the priority queue heap cannot be negative.");
			if (initialCapacity > 0) _heap = new T[initialCapacity];
		}

		/// <summary>
		/// The element in the queue with the highest priority, determined by a derived class's implementation of the <see cref="AreOrdered"/> method.
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
		/// Indicates if there are no items queued.
		/// </summary>
		public bool isEmpty
		{
			get
			{
				return _size == 0;
			}
		}

		/// <summary>
		/// The number of elements in the queue.
		/// </summary>
		public int Count { get { return _size; } }

		/// <summary>
		/// Adds an element to the queue.  It's position in the queue will be determined by a derived class's implementation of the <see cref="AreOrdered"/> method.
		/// </summary>
		/// <param name="item">The item to be added to the queue.</param>
		public void Push(T item)
		{
			var index = _size;
			if (_heap == null)
			{
				CreateHeap();
			}
			else if (index >= _heap.Length)
			{
				ExtendHeap();
			}
			_heap[index] = item;
			++_size;
			BubbleUp(index);
		}

		/// <summary>
		/// Removes and returns the highest priority element from the queue, determined by a derived class's implementation of the <see cref="AreOrdered"/> method.
		/// </summary>
		/// <returns>The highest priority element in the queue.</returns>
		public T Pop()
		{
			var item = front;
			RemoveFront();
			return item;
		}

		/// <summary>
		/// Returns the highest priority element from the queue without removing it, determined by a derived class's implementation of the <see cref="AreOrdered"/> method.
		/// </summary>
		/// <returns>The highest priority element in the queue.</returns>
		public T Peek()
		{
			return front;
		}

		/// <summary>
		/// Removes the highest priority element from the queue, determined by a derived class's implementation of the <see cref="AreOrdered"/> method.
		/// </summary>
		public void RemoveFront()
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
				throw new InvalidOperationException("The front element cannot be removed when the priority queue is empty.");
			}
		}

		/// <summary>
		/// Recomputes the priority of the indicated item using an overriden <see cref="AreOrdered"/> method provided by the derived class, and repositions the item accordingly within the internal data structure.
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
						if (AreOrdered(_heap[parentIndex], _heap[index]))
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
		/// Abstract function for comparing two elements to determine their appropriate order.
		/// </summary>
		/// <param name="lhs">The first item to compare.</param>
		/// <param name="rhs">The second item to compare.</param>
		/// <returns>True if the first item ought to come before the second item, and false if the second item should come first instead.</returns>
		protected abstract bool AreOrdered(T lhs, T rhs);

		private void BubbleUp(int index)
		{
			while (index > 0)
			{
				var parentIndex = (index - 1) / 2;
				if (AreOrdered(_heap[parentIndex], _heap[index])) break;
				GeneralUtility.Swap(ref _heap[index], ref _heap[parentIndex]);
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
					if (AreOrdered(_heap[index], _heap[leftChildIndex]) && AreOrdered(_heap[index], _heap[rightChildIndex])) break;
					if (AreOrdered(_heap[leftChildIndex], _heap[rightChildIndex]))
					{
						GeneralUtility.Swap(ref _heap[index], ref _heap[leftChildIndex]);
						index = leftChildIndex;
					}
					else
					{
						GeneralUtility.Swap(ref _heap[index], ref _heap[rightChildIndex]);
						index = rightChildIndex;
					}
					leftChildIndex = index * 2 + 1;
					rightChildIndex = leftChildIndex + 1;
				}
				else
				{
					if (AreOrdered(_heap[index], _heap[leftChildIndex])) break;
					GeneralUtility.Swap(ref _heap[index], ref _heap[leftChildIndex]);
					index = leftChildIndex;
					break;
				}
			}
		}

		private void CreateHeap()
		{
			_heap = new T[4];
		}

		private void ExtendHeap()
		{
			var extendedHeap = new T[_heap.Length * 2];
			Array.Copy(_heap, extendedHeap, _size);
			_heap = extendedHeap;
		}
	}
}
