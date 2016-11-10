﻿/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System.Collections.Generic;

namespace Experilous.Core
{
	/// <summary>
	/// Static class of extensions for working with the GUI system.
	/// </summary>
	public static class GUIExtensions
	{
		private enum EnableDirectives
		{
			Disable,
			Enable,
			ForceEnable,
		}

		private static List<EnableDirectives> _enableStack = new List<EnableDirectives>();
		private static bool _enableState = true;

		/// <summary>
		/// Set the GUI's enabled state to the specified value, and push this state onto a stack to be popped later.
		/// </summary>
		/// <param name="enable">The value to assign to the GUI's enabled state.</param>
		/// <param name="force">If true, the GUI's enabled state is forced to the specified value.  Otherwise, it is enabled only if there are no disabled states anywhere deeper in the stack before reaching the next forced enable or the bottom of the stack.</param>
		/// <returns>The GUI's current enabled state after the function completes.</returns>
		public static bool PushEnable(bool enable, bool force = false)
		{
			if (enable == false)
			{
				_enableStack.Add(EnableDirectives.Disable);
				_enableState = false;
			}
			else if (force == false)
			{
				_enableStack.Add(EnableDirectives.Enable);
			}
			else
			{
				_enableStack.Add(EnableDirectives.ForceEnable);
				_enableState = true;
			}

			return GUI.enabled = _enableState;
		}

		/// <summary>
		/// Removes the most recent push to the GUI's enabled state, returning it to its previous state in the process.
		/// </summary>
		/// <returns>The GUI's current enabled state after the function completes.</returns>
		public static bool PopEnable()
		{
			var index = _enableStack.Count - 1;
			if (index >= 0)
			{
				var top = _enableStack[index];
				_enableStack.RemoveAt(index);
				if (top != EnableDirectives.Enable)
				{
					_enableState = true;
					while (--index >= 0)
					{
						if (_enableStack[index] == EnableDirectives.Disable)
						{
							_enableState = false;
							break;
						}
						else if (_enableStack[index] == EnableDirectives.ForceEnable)
						{
							break;
						}
					}
				}
			}

			return GUI.enabled = _enableState;
		}

		/// <summary>
		/// Clears the stack of modifications to the GUI's enabled state, and setting that state to true.
		/// </summary>
		public static void ResetEnable()
		{
			_enableStack.Clear();
			GUI.enabled = _enableState = true;
		}
	}
}
