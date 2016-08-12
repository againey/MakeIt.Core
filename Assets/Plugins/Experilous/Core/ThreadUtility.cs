/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Threading;

namespace Experilous.Core
{
	public static class ThreadUtility
	{
		private static bool _mainThreadIdentified = false;
		private static int _mainThreadId;

#if UNITY_EDITOR
		[InitializeOnLoadMethod]
#endif
		[RuntimeInitializeOnLoadMethod]
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
		/// the [<see cref="InitializeOnLoadMethod"/>] attribute or
		/// the [<see cref="RuntimeInitializeOnLoadMethod"/>] attribute.</para>
		/// 
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
