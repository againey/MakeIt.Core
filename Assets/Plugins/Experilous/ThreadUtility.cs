/******************************************************************************\
 *  Copyright (C) 2016 Experilous <againey@experilous.com>
 *  
 *  This file is subject to the terms and conditions defined in the file
 *  'Assets/Plugins/Experilous/License.txt', which is a part of this package.
 *
\******************************************************************************/

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Threading;

namespace Experilous
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

		public static bool isMainThread
		{
			get
			{
				return _mainThreadIdentified && Thread.CurrentThread.ManagedThreadId == _mainThreadId;
			}
		}
	}
}
