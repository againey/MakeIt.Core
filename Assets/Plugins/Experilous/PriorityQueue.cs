﻿using System;

namespace Experilous
{
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

		public T front
		{
			get
			{
				if (_size > 0) return _heap[0];
				throw new InvalidOperationException("The front element cannot be accessed when the priority queue is empty.");
			}
		}

		public int Count { get { return _size; } }

		public void Push(T item)
		{
			var index = _size;
			if (index >= _heap.Length) ExtendHeap();
			_heap[index] = item;
			++_size;
			BubbleUp(index);
		}

		public void Pop()
		{
			if (_size > 1)
			{
				--_size;
				_heap[0] = _heap[_size];
				System.Array.Clear(_heap, _size, 1);
				BubbleDown(0);
			}
			else if (_size == 1)
			{
				System.Array.Clear(_heap, 0, 1);
				_size = 0;
			}
			else
			{
				throw new InvalidOperationException("The front element cannot be popped when the priority queue is empty.");
			}
		}

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
			System.Array.Copy(_heap, extendedHeap, _size);
			_heap = extendedHeap;
		}
	}
}
