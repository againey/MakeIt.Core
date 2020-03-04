/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Containers
{
	/// <summary>
	/// A container supporting abstract push and pop actions for inserting
	/// and removing elements in an implementation-defined order.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the container.</typeparam>
	public interface IQueue<T>
	{
		/// <summary>
		/// Indicates if there are zero items in the queue.
		/// </summary>
		bool isEmpty { get; }

		/// <summary>
		/// Returns the number of items in the queue.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Pushes an item into the queue.
		/// </summary>
		/// <param name="item">The item to put in the queue.</param>
		void Push(T item);

		/// <summary>
		/// Removes the front item from the queue.
		/// </summary>
		/// <returns>The item that was just removed from the front of the queue.</returns>
		T Pop();

		/// <summary>
		/// Removes all items from the queue.
		/// </summary>
		void Clear();
	}
}
