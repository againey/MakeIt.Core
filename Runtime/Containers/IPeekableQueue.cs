/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace MakeIt.Containers
{
	/// <summary>
	/// A container supporting abstract push and pop actions for inserting
	/// and removing elements in an implementation-defined order, and allowing
	/// the front item to be accessed without popping it.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the container.</typeparam>
	public interface IPeekableQueue<T> : IQueue<T>
	{
		/// <summary>
		/// The item at the front of the queue.
		/// </summary>
		/// <remarks><para>If the queue is empty, it is implementation defined what will happen, but
		/// a likely behavior is that an exceptio will be thrown.  If this is undesirable, make sure
		/// to chck <see cref="IQueue{T}.isEmpty"/> before acessing the front item.</para></remarks>
		T front { get; }

		/// <summary>
		/// Returns the item at the front of the queue without removing it like <see cref="IQueue{T}.Pop()"/>.
		/// </summary>
		/// <returns>The item at the front of the queue.</returns>
		/// <remarks><para>If the queue is empty, it is implementation defined what will happen, but
		/// a likely behavior is that an exceptio will be thrown.  If this is undesirable, make sure
		/// to chck <see cref="IQueue{T}.isEmpty"/> before acessing the front item.</para></remarks>
		T Peek();

		/// <summary>
		/// Removes the item at the front of the queue without returning it like <see cref="IQueue{T}.Pop()"/>.
		/// </summary>
		/// <remarks><para>If the queue is empty, it is implementation defined what will happen, but
		/// a likely behavior is that an exceptio will be thrown.  If this is undesirable, make sure
		/// to chck <see cref="IQueue{T}.isEmpty"/> before acessing the front item.</para></remarks>
		void RemoveFront();
	}
}
