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

using UnityEngine;

using System.Threading;

namespace MakeIt.Core
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
