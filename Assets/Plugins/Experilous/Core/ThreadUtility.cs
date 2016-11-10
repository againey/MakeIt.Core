/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

using System.Threading;

namespace Experilous.Core
{
	/// <summary>
	/// A class of static helper functions for working with threads.
	/// </summary>
	public static class ThreadUtility
	{
		private static bool _mainThreadIdentified = false;
		private static int _mainThreadId;

#if UNITY_EDITOR
		[UnityEditor.InitializeOnLoadMethod]
#endif
#if UNITY_5_2 || UNITY_5_3_OR_NEWER
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#else
		[RuntimeInitializeOnLoadMethod]
#endif
		private static void OnLoad()
		{
			_mainThreadId = Thread.CurrentThread.ManagedThreadId;
			_mainThreadIdentified = true;
		}

		/// <summary>
		/// Indicates if the currently executing thread is Unity's main thread.
		/// </summary>
		/// <remarks>
		/// <para>The value of this property is determined by comparing the current thread
		/// id to the thread id acquired during Unity's call of methods marked with either
		/// the [<see cref="UnityEditor.InitializeOnLoadMethodAttribute"/>] or
		/// the [<see cref="RuntimeInitializeOnLoadMethodAttribute"/>].</para>
		/// <para>If this property returns false, you should not attempt to access the vast
		/// majority of the Unity API.</para>
		/// </remarks>
		public static bool isMainThread
		{
			get
			{
				return _mainThreadIdentified && Thread.CurrentThread.ManagedThreadId == _mainThreadId;
			}
		}
	}
}
