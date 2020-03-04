/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;

namespace Experilous.Containers
{
	/// <summary>
	/// A priority queue container that uses a custom delegate to compare elements
	/// and ensure that the correct one is always at the front of the queue.
	/// </summary>
	/// <typeparam name="T">The element type stored in the container.</typeparam>
	public class DelegateOrderedPriorityQueue<T> : PriorityQueue<T> where T : IEquatable<T>
	{
		/// <summary>
		/// The custom delegate signature for comparing two elements to determine their appropriate order.
		/// </summary>
		/// <param name="lhs">The first item to compare.</param>
		/// <param name="rhs">The second item to compare.</param>
		/// <returns>True if the first item ought to come before the second item, and false if the second item should come first instead.</returns>
		public delegate bool AreOrderedDelegate(T lhs, T rhs);

		private AreOrderedDelegate _areOrdered;

		/// <summary>
		/// Initializes the priority queue with the specified ordering delegate and zero initial capacity.
		/// </summary>
		/// <param name="areOrdered">The delegate that will indicate if two items are ordered properly.</param>
		public DelegateOrderedPriorityQueue(AreOrderedDelegate areOrdered)
		{
			if (areOrdered == null) throw new ArgumentNullException("areOrdered");
			_areOrdered = areOrdered;
		}

		/// <summary>
		/// Initializes the priority queue with the specified ordering delegate and a specified initial capacity.
		/// </summary>
		/// <param name="areOrdered">The delegate that will indicate if two items are ordered properly.</param>
		/// <param name="initialCapacity">The number of items for which underlying storage capacity will be allocated immediately.</param>
		public DelegateOrderedPriorityQueue(AreOrderedDelegate areOrdered, int initialCapacity)
			: base(initialCapacity)
		{
			if (areOrdered == null) throw new ArgumentNullException("areOrdered");
			_areOrdered = areOrdered;
		}

		/// <summary>
		/// Removes all elements from the queue, and sets a new <see cref="AreOrderedDelegate"/> functor which will be applied to any elements pushed later on.
		/// </summary>
		/// <param name="areOrdered">The delegate that will indicate if two items are ordered properly.</param>
		/// <remarks>This does not deallocate any memory used by the internal data structure.
		/// It therefore enables reuse of the priority queue instance to cut down on the cost
		/// of allocation and garbage collection.</remarks>
		public void Reset(AreOrderedDelegate areOrdered)
		{
			if (areOrdered == null) throw new ArgumentNullException("areOrdered");

			Clear();
			_areOrdered = areOrdered;
		}

		/// <summary>
		/// Implementation of the AreOrdered() abstract function which defers its logic to the custom delegate.
		/// </summary>
		/// <param name="lhs">The first item to compare.</param>
		/// <param name="rhs">The second item to compare.</param>
		/// <returns>True if the first item ought to come before the second item, and false if the second item should come first instead.</returns>
		protected override bool AreOrdered(T lhs, T rhs)
		{
			return _areOrdered(lhs, rhs);
		}
	}
}
