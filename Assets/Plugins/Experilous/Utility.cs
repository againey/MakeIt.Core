﻿/******************************************************************************\
 *  Copyright (C) 2016 Experilous <againey@experilous.com>
 *  
 *  This file is subject to the terms and conditions defined in the file
 *  'Assets/Plugins/Experilous/License.txt', which is a part of this package.
 *
\******************************************************************************/

using UnityEngine;
using System.Text.RegularExpressions;

namespace Experilous
{
	public static class Utility
	{
		public static void Swap<T>(ref T lhs, ref T rhs)
		{
			T intermediate = lhs;
			lhs = rhs;
			rhs = intermediate;
		}

		public static string GetPrettyName(this System.Type type, bool includeNamespaces = false, bool useBuiltInNames = true)
		{
			if (useBuiltInNames)
			{
				if (type == typeof(bool)) return "bool";
				else if (type == typeof(byte)) return "byte";
				else if (type == typeof(sbyte)) return "sbyte";
				else if (type == typeof(char)) return "char";
				else if (type == typeof(decimal)) return "decimal";
				else if (type == typeof(double)) return "double";
				else if (type == typeof(float)) return "float";
				else if (type == typeof(int)) return "int";
				else if (type == typeof(uint)) return "uint";
				else if (type == typeof(long)) return "long";
				else if (type == typeof(ulong)) return "ulong";
				else if (type == typeof(object)) return "object";
				else if (type == typeof(short)) return "short";
				else if (type == typeof(ushort)) return "ushort";
				else if (type == typeof(string)) return "string";
			}

			var name = Regex.Unescape(type.Name);
			var backtickIndex = name.IndexOf('`');
			if (backtickIndex >= 0)
			{
				name = name.Substring(0, backtickIndex);
				var genericArguments = type.GetGenericArguments();
				if (genericArguments.Length == 1)
				{
					name += '<' + genericArguments[0].GetPrettyName(includeNamespaces, useBuiltInNames) + '>';
				}
				else
				{
					var genericArgumentNames = new string[genericArguments.Length];
					for (int i = 0; i < genericArguments.Length; ++i)
					{
						genericArgumentNames[i] = genericArguments[i].GetPrettyName(includeNamespaces, useBuiltInNames);
					}
					name += '<' + string.Join(", ", genericArgumentNames) + '>';
				}
			}

			if (includeNamespaces)
			{
				name = Regex.Unescape(type.Namespace) + '.' + name;
			}

			return name;
		}

		public static T SetName<T>(this T obj, string name) where T : UnityEngine.Object
		{
			obj.name = name;
			return obj;
		}

		private static TAttribute GetAttribute<TAttribute>(object[] attributes) where TAttribute : System.Attribute
		{
			foreach (var attribute in attributes)
			{
				if (attribute is TAttribute)
				{
					return (TAttribute)attribute;
				}
			}
			return null;
		}

		public static TAttribute GetAttribute<TAttribute>(System.Type type, bool inherit = true) where TAttribute : System.Attribute
		{
			return GetAttribute<TAttribute>(type.GetCustomAttributes(inherit));
		}

		public static TAttribute GetAttribute<TAttribute>(System.Reflection.FieldInfo field) where TAttribute : System.Attribute
		{
			return GetAttribute<TAttribute>(field.GetCustomAttributes(true));
		}

		public static void DisableAndThrowOnMissingClassInstance<TField>(this MonoBehaviour component, TField field, string message) where TField : class
		{
#if UNITY_EDITOR
			if (field == null && UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
#else
			if (field == null)
#endif
			{
				component.enabled = false;
				throw new MissingReferenceException(message);
			}
		}

		public static void DisableAndThrowOnMissingReference<TField>(this MonoBehaviour component, TField field, string message) where TField : Object
		{
#if UNITY_EDITOR
			if (field == null && UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
#else
			if (field == null)
#endif
			{
				component.enabled = false;
				throw new MissingReferenceException(message);
			}
		}

		public static void DisableAndThrowOnMissingComponent<TField>(this MonoBehaviour component, TField field, string message) where TField : Component
		{
#if UNITY_EDITOR
			if (field == null && UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
#else
			if (field == null)
#endif
			{
				component.enabled = false;
				throw new MissingComponentException(message);
			}
		}
	}
}
