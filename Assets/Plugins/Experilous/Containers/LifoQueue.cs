/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;
using System.Collections.Generic;

namespace Experilous.Containers
{
	/// <summary>
	/// A last-in first-out queue-interface container, also commonly known as a stack.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the queue.</typeparam>
	[Serializable]
	public class LifoQueue<T> : IPeekableQueue<T>
	{
		[SerializeField] private List<T> _items;

		/// <summary>
		/// Construct an empty queue.
		/// </summary>
		public LifoQueue()
		{
			_items = new List<T>();
		}

		/// <summary>
		/// The item at the front of the queue.
		/// </summary>
		public T front
		{
			get
			{
				return _items[_items.Count - 1];
			}
		}

		/// <summary>
		/// Indicates if there are zero items in the queue.
		/// </summary>
		public bool isEmpty
		{
			get
			{
				return _items.Count == 0;
			}
		}

		/// <summary>
		/// Returns the number of items in the queue.
		/// </summary>
		public int Count
		{
			get
			{
				return _items.Count;
			}
		}

		/// <summary>
		/// Pushes an item into the queue.
		/// </summary>
		/// <param name="item">The item to put in the queue.</param>
		public void Push(T item)
		{
			_items.Add(item);
		}

		/// <summary>
		/// Removes the front item from the queue.
		/// </summary>
		/// <returns>The item that was just removed from the front of the queue.</returns>
		public T Pop()
		{
			var item = front;
			RemoveFront();
			return item;
		}

		/// <summary>
		/// Returns the item at the front of the queue without removing it like <see cref="IQueue{T}.Pop()"/>.
		/// </summary>
		/// <returns>The item at the front of the queue.</returns>
		public T Peek()
		{
			return front;
		}

		/// <summary>
		/// Removes the item at the front of the queue without returning it like <see cref="IQueue{T}.Pop()"/>.
		/// </summary>
		public void RemoveFront()
		{
			_items.RemoveAt(_items.Count - 1);
		}

		/// <summary>
		/// Removes all items from the queue.
		/// </summary>
		public void Clear()
		{
			_items.Clear();
		}
	}
}
