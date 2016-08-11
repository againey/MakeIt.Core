/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Experilous
{
	public static class MiscTools
	{
		/// <summary>
		/// Swap the values of two variables/fields.
		/// </summary>
		/// <typeparam name="T">The shared type of the two elements.  This can be a reference type, in which case the references will be swapped, or a value type, in which case the values will be swapped.</typeparam>
		/// <param name="lhs">The first item to be swapped.</param>
		/// <param name="rhs">The second item to be swapped.</param>
		public static void Swap<T>(ref T lhs, ref T rhs)
		{
			T intermediate = lhs;
			lhs = rhs;
			rhs = intermediate;
		}

		/// <summary>
		/// Generates a C#-style type name for a specified type, supporting fully specified generic types.
		/// </summary>
		/// <param name="type">The type for which a pretty name should be generated.</param>
		/// <param name="includeNamespaces">Whether the full namespace of the type should be included, or just the immdiate typename itself.</param>
		/// <param name="useBuiltInNames">Whether to use built-in C# names for primitive types, or use the full type name found in the .NET library.</param>
		/// <returns></returns>
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

		/// <summary>
		/// A chainable extension method to set the name of a Unity object.
		/// </summary>
		/// <typeparam name="T">The type of the Unity object; any type deriving from UnityEngine.Object.</typeparam>
		/// <param name="obj">The object whose name is to be set.</param>
		/// <param name="name">The name to assign to the specified object.</param>
		/// <returns>The object whose name was just set.</returns>
		public static T SetName<T>(this T obj, string name) where T : UnityEngine.Object
		{
			obj.name = name;
			return obj;
		}

		/// <summary>
		/// Search for and return the first attribute of the specified type within the array of attributes provided.
		/// </summary>
		/// <typeparam name="TAttribute">The attribute type to be searched for.</typeparam>
		/// <param name="attributes">The array of potential attributes to be searched.</param>
		/// <returns>The first instance of an attribute of the specified type within the array, or null if no matching attribute was found.</returns>
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

		/// <summary>
		/// Search for and return the all attributes of the specified type within the array of attributes provided.
		/// </summary>
		/// <typeparam name="TAttribute">The attribute type to be searched for.</typeparam>
		/// <param name="attributes">The array of potential attributes to be searched.</param>
		/// <returns>An array of all instances of attributes of the specified type within the array, or an empty array if no matching attribute was found.</returns>
		private static TAttribute[] GetAttributes<TAttribute>(object[] attributes) where TAttribute : System.Attribute
		{
			var matchingAttributes = new List<TAttribute>();
			foreach (var attribute in attributes)
			{
				if (attribute is TAttribute)
				{
					matchingAttributes.Add((TAttribute)attribute);
				}
			}
			return matchingAttributes.ToArray();
		}

		/// <summary>
		/// Search for and return the first attribute of the specified attribute type attached to the provided object type.
		/// </summary>
		/// <typeparam name="TAttribute">The attribute type to be searched for.</typeparam>
		/// <param name="type">The object type which may have attributes attached to it.</param>
		/// <param name="inherit">Whether the search should include inherited attributes.</param>
		/// <returns>The first instance of an attribute of the specified type attached to the object type, or null if no matching attribute was found.</returns>
		public static TAttribute GetAttribute<TAttribute>(System.Type type, bool inherit = true) where TAttribute : System.Attribute
		{
			return GetAttribute<TAttribute>(type.GetCustomAttributes(inherit));
		}

		/// <summary>
		/// Search for and return the all attributes of the specified attribute type attached to the provided object type.
		/// </summary>
		/// <typeparam name="TAttribute">The attribute type to be searched for.</typeparam>
		/// <param name="type">The object type which may have attributes attached to it.</param>
		/// <param name="inherit">Whether the search should include inherited attributes.</param>
		/// <returns>An array of all instances of attributes of the specified type attached to the object type, or an empty array if no matching attribute was found.</returns>
		public static TAttribute[] GetAttributes<TAttribute>(System.Type type, bool inherit = true) where TAttribute : System.Attribute
		{
			return GetAttributes<TAttribute>(type.GetCustomAttributes(inherit));
		}

		/// <summary>
		/// Search for and return the first attribute of the specified attribute type attached to the provided type field.
		/// </summary>
		/// <typeparam name="TAttribute">The attribute type to be searched for.</typeparam>
		/// <param name="field">The type field which may have attributes attached to it.</param>
		/// <returns>The first instance of an attribute of the specified type attached to the type field, or null if no matching attribute was found.</returns>
		public static TAttribute GetAttribute<TAttribute>(System.Reflection.FieldInfo field) where TAttribute : System.Attribute
		{
			return GetAttribute<TAttribute>(field.GetCustomAttributes(true));
		}

		/// <summary>
		/// Search for and return the all attributes of the specified attribute type attached to the provided type field.
		/// </summary>
		/// <typeparam name="TAttribute">The attribute type to be searched for.</typeparam>
		/// <param name="field">The type field which may have attributes attached to it.</param>
		/// <returns>An array of all instances of attributes of the specified type attached to the type field, or an empty array if no matching attribute was found.</returns>
		public static TAttribute[] GetAttributes<TAttribute>(System.Reflection.FieldInfo field) where TAttribute : System.Attribute
		{
			return GetAttributes<TAttribute>(field.GetCustomAttributes(true));
		}

		/// <summary>
		/// Verify that a field of a <see cref="MonoBehaviour"/> is not null, or disable the component and throw an exception if it is null.
		/// </summary>
		/// <typeparam name="TField">The type of the field; can be any reference type.</typeparam>
		/// <param name="component">The component to which the field belongs.</param>
		/// <param name="field">The field which should not be null.</param>
		/// <param name="message">The message to include in the exception if the field does happen to be null.</param>
		public static void DisableAndThrowOnUnassignedClassInstance<TField>(this MonoBehaviour component, TField field, string message) where TField : class
		{
#if UNITY_EDITOR
			if (field == null && UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
#else
			if (field == null)
#endif
			{
				component.enabled = false;
				throw new UnassignedReferenceException(message);
			}
		}

		/// <summary>
		/// Verify that a field of a <see cref="MonoBehaviour"/> is not null, or disable the component and throw an exception if it is null.
		/// </summary>
		/// <typeparam name="TField">The type of the field; can be any type derived from UnityEngine.Object.</typeparam>
		/// <param name="component">The component to which the field belongs.</param>
		/// <param name="field">The field which should not be null.</param>
		/// <param name="message">The message to include in the exception if the field does happen to be null.</param>
		/// <remarks>This will use Unity's overridden null check to also catch a reference to an uninitialized or already-destroyed instance.</remarks>
		public static void DisableAndThrowOnUnassignedReference<TField>(this MonoBehaviour component, TField field, string message) where TField : UnityEngine.Object
		{
#if UNITY_EDITOR
			if (field == null && UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
#else
			if (field == null)
#endif
			{
				component.enabled = false;
				throw new UnassignedReferenceException(message);
			}
		}

		/// <summary>
		/// Verify that a field of a <see cref="MonoBehaviour"/> is not null, or disable the component and throw an exception if it is null.
		/// </summary>
		/// <typeparam name="TField">The type of the field; can be any type derived from UnityEngine.Component.</typeparam>
		/// <param name="component">The component to which the field belongs.</param>
		/// <param name="field">The field which should not be null.</param>
		/// <param name="message">The message to include in the exception if the field does happen to be null.</param>
		/// <remarks>This will use Unity's overridden null check to also catch a reference to an uninitialized or already-destroyed instance.</remarks>
		public static void DisableAndThrowOnMissingComponent<TField>(this MonoBehaviour component, TField field, string message) where TField : UnityEngine.Component
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
