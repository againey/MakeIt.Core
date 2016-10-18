/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous.Core
{
	/// <summary>
	/// A representation of <see cref="System.Type"/> that can be serialized by Unity.
	/// </summary>
	[Serializable]
	public struct SerializableType : ISerializationCallbackReceiver, IEquatable<SerializableType>, IEquatable<Type>
	{
		[SerializeField] private string _assemblyQualifiedTypeName;
		[NonSerialized] public Type type;

		/// <summary>
		/// Constructs an instance using the given type.
		/// </summary>
		/// <param name="type">The type to be serialized.</param>
		public SerializableType(Type type)
		{
			_assemblyQualifiedTypeName = "";
			this.type = type;
		}

		/// <summary>
		/// Constructs an instance using the type described by the given assembly qualified name.
		/// </summary>
		/// <param name="assemblyQualifiedTypeName">The name of the type, including the full namespace and, if necessary, the name of the assembly.</param>
		/// <seealso cref="System.Type.GetType(System.String)"/>
		public SerializableType(string assemblyQualifiedTypeName)
		{
			_assemblyQualifiedTypeName = "";
			type = Type.GetType(_assemblyQualifiedTypeName);
		}

		/// <summary>
		/// Implicitly convert a serializable type to the <see cref="System.Type"/> that it represents.
		/// </summary>
		/// <param name="serializableType">The serializable type to be converted.</param>
		public static implicit operator Type(SerializableType serializableType)
		{
			return serializableType.type;
		}

		/// <summary>
		/// Implicitly convert a <see cref="System.Type"/> to a serializable type.
		/// </summary>
		/// <param name="type">The type to be convert.</param>
		public static implicit operator SerializableType(Type type)
		{
			return new SerializableType(type);
		}

		/// <summary>
		/// Checks if the current type is equal to the given type.
		/// </summary>
		/// <param name="other">The other type to compare to.</param>
		/// <returns>True if the given object is an instance of either <see cref="System.Type"/> or <see cref="SerializableType"/> and is equal to the type that the current serializable type represents.</returns>
		public override bool Equals(object other) { return other is SerializableType && type == ((SerializableType)other).type || other is Type && type == (Type)other; }

		/// <summary>
		/// Gets the hash code of the current type.
		/// </summary>
		/// <returns>Hash code of the current type.</returns>
		/// <seealso cref="System.Type.GetHashCode"/>
		public override int GetHashCode() { return type.GetHashCode(); }

		/// <summary>
		/// Checks if the current type is equal to the given type.
		/// </summary>
		/// <param name="other">The other type to compare to.</param>
		/// <returns>True if the given type is equal to the type that the current serializable type represents.</returns>
		public bool Equals(Type other) { return type == other; }

		/// <summary>
		/// Checks if the current type is equal to the given type.
		/// </summary>
		/// <param name="other">The other type to compare to.</param>
		/// <returns>True if the given type is equal to the current type.</returns>
		public bool Equals(SerializableType other) { return type == other.type; }

		/// <summary>
		/// Checks if the two types are equal.
		/// </summary>
		/// <param name="lhs">The first type to compare.</param>
		/// <param name="rhs">The second type to compare.</param>
		/// <returns>True if the two types are equal, false otherwise.</returns>
		public static bool operator ==(SerializableType lhs, SerializableType rhs) { return lhs.type == rhs.type; }

		/// <summary>
		/// Checks if the two types are not equal.
		/// </summary>
		/// <param name="lhs">The first type to compare.</param>
		/// <param name="rhs">The second type to compare.</param>
		/// <returns>True if the two types are not equal, false if they are.</returns>
		public static bool operator !=(SerializableType lhs, SerializableType rhs) { return lhs.type != rhs.type; }

		/// <summary>
		/// Checks if the two types are equal.
		/// </summary>
		/// <param name="lhs">The first type to compare.</param>
		/// <param name="rhs">The second type to compare.</param>
		/// <returns>True if the two types are equal, false otherwise.</returns>
		public static bool operator ==(SerializableType lhs, Type rhs) { return lhs.type == rhs; }

		/// <summary>
		/// Checks if the two types are not equal.
		/// </summary>
		/// <param name="lhs">The first type to compare.</param>
		/// <param name="rhs">The second type to compare.</param>
		/// <returns>True if the two types are not equal, false if they are.</returns>
		public static bool operator !=(SerializableType lhs, Type rhs) { return lhs.type != rhs; }

		/// <summary>
		/// Checks if the two types are equal.
		/// </summary>
		/// <param name="lhs">The first type to compare.</param>
		/// <param name="rhs">The second type to compare.</param>
		/// <returns>True if the two types are equal, false otherwise.</returns>
		public static bool operator ==(Type lhs, SerializableType rhs) { return lhs == rhs.type; }

		/// <summary>
		/// Checks if the two types are not equal.
		/// </summary>
		/// <param name="lhs">The first type to compare.</param>
		/// <param name="rhs">The second type to compare.</param>
		/// <returns>True if the two types are not equal, false if they are.</returns>
		public static bool operator !=(Type lhs, SerializableType rhs) { return lhs != rhs.type; }

		/// <summary>
		/// Called before Unity serializes the type.
		/// </summary>
		/// <remarks><para>This function copies the type's assembly qualified name into a private string to be serialized.</para></remarks>
		public void OnBeforeSerialize()
		{
			_assemblyQualifiedTypeName = (type != null ? type.AssemblyQualifiedName : "");
		}

		/// <summary>
		/// Called after Unity deserializes the assembly qualified type name.
		/// </summary>
		/// <remarks><para>This function gets the appropriate <see cref="System.Type"/> based on the deserialized assembly qualified name.</para></remarks>
		public void OnAfterDeserialize()
		{
			type = !string.IsNullOrEmpty(_assemblyQualifiedTypeName) ? Type.GetType(_assemblyQualifiedTypeName) : null;
		}

		/// <summary>
		/// Converts the current type to a string.
		/// </summary>
		/// <returns>A string representation of the current type.</returns>
		public override string ToString()
		{
			return type.ToString();
		}
	}
}
