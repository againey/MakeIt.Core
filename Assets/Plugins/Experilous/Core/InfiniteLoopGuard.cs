/******************************************************************************\
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

		public InfiniteLoopGuard(int maxIterationCount)
		{
			_curIterationCount = 0;
			_maxIterationCount = maxIterationCount;
		}

		public void Reset()
		{
			_curIterationCount = 0;
		}

		public void Reset(int maxIterationCount)
		{
			_curIterationCount = 0;
			_maxIterationCount = maxIterationCount;
		}

		public void Iterate()
		{
			if (_curIterationCount == _maxIterationCount) throw new InfiniteLoopException();
			++_curIterationCount;
		}
#else
		public InfiniteLoopGuard(int maxIterationCount)
		{
		}

		public void Reset()
		{
		}

		public void Reset(int maxIterationCount)
		{
		}

		public void Iterate()
		{
		}
#endif
	}

	public class InfiniteLoopException : System.ApplicationException
	{
	}
}
