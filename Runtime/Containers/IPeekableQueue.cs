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
