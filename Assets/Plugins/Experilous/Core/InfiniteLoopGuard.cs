﻿/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Core
{
	/// <summary>
	/// A debug utility class for guarding against infinite loops that will hang Unity.
	/// </summary>
	public struct InfiniteLoopGuard
	{
#if UNITY_EDITOR
		private int _curIterationCount;
		private int _maxIterationCount;

		/// <summary>
		/// Constructs an infinite loop guard configured to throw an exception if it ever iterates more than the specified maximum number of times.
		/// </summary>
		/// <param name="maxIterationCount">The maximum number of times the loop guard is allowed to be iterated before it throws an exception.</param>
		public InfiniteLoopGuard(int maxIterationCount)
		{
			_curIterationCount = 0;
			_maxIterationCount = maxIterationCount;
		}

		/// <summary>
		/// Resets the current iteration count back to zero.  The maximum iteration count remains unchanged.
		/// </summary>
		public void Reset()
		{
			_curIterationCount = 0;
		}

		/// <summary>
		/// Resets the current iteration count back to zero, and changes the maximum iteration count to the number specified.
		/// </summary>
		/// <param name="maxIterationCount">The maximum number of times the loop guard is allowed to be iterated before it throws an exception.</param>
		public void Reset(int maxIterationCount)
		{
			_curIterationCount = 0;
			_maxIterationCount = maxIterationCount;
		}

		/// <summary>
		/// Increments the iteration count by one, and throws an exception if it exceeds the previously specified maximum iteration count.
		/// </summary>
		public void Iterate()
		{
			if (_curIterationCount == _maxIterationCount) throw new InfiniteLoopException();
			++_curIterationCount;
		}
#else
		/// <summary>
		/// Constructs an infinite loop guard configured to throw an exception if it ever iterates more than the specified maximum number of times.
		/// </summary>
		/// <param name="maxIterationCount">The maximum number of times the loop guard is allowed to be iterated before it throws an exception.</param>
		public InfiniteLoopGuard(int maxIterationCount)
		{
		}

		/// <summary>
		/// Resets the current iteration count back to zero.  The maximum iteration count remains unchanged.
		/// </summary>
		public void Reset()
		{
		}

		/// <summary>
		/// Resets the current iteration count back to zero, and changes the maximum iteration count to the number specified.
		/// </summary>
		/// <param name="maxIterationCount">The maximum number of times the loop guard is allowed to be iterated before it throws an exception.</param>
		public void Reset(int maxIterationCount)
		{
		}

		/// <summary>
		/// Increments the iteration count by one, and throws an exception if it exceeds the previously specified maximum iteration count.
		/// </summary>
		public void Iterate()
		{
		}
#endif
	}

	/// <summary>
	/// An exception which indicates that an <see cref="InfiniteLoopGuard"/> has exceeded its maximum iteration count.
	/// </summary>
	public class InfiniteLoopException : System.ApplicationException
	{
	}
}
