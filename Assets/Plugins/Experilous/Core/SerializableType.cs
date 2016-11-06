/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System.Collections.Generic;
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

		public SerializableType(Type type)
		{
			_assemblyQualifiedTypeName = "";
			this.type = type;
		}

		public SerializableType(string assemblyQualifiedTypeName)
		{
			_assemblyQualifiedTypeName = "";
			type = Type.GetType(_assemblyQualifiedTypeName);
		}

		public static implicit operator Type(SerializableType serializableType)
		{
			return serializableType.type;
		}

		public static implicit operator SerializableType(Type type)
		{
			return new SerializableType(type);
		}

		public override bool Equals(object other) { return other is SerializableType && type == ((SerializableType)other).type || other is Type && type == (Type)other; }
		public override int GetHashCode() { return type.GetHashCode(); }
		public bool Equals(Type other) { return type == other; }
		public bool Equals(SerializableType other) { return type == other.type; }
		public static bool operator ==(SerializableType lhs, SerializableType rhs) { return lhs.type == rhs.type; }
		public static bool operator !=(SerializableType lhs, SerializableType rhs) { return lhs.type != rhs.type; }
		public static bool operator ==(SerializableType lhs, Type rhs) { return lhs.type == rhs; }
		public static bool operator !=(SerializableType lhs, Type rhs) { return lhs.type != rhs; }
		public static bool operator ==(Type lhs, SerializableType rhs) { return lhs == rhs.type; }
		public static bool operator !=(Type lhs, SerializableType rhs) { return lhs != rhs.type; }

		public void OnBeforeSerialize()
		{
			_assemblyQualifiedTypeName = (type != null ? type.AssemblyQualifiedName : "");
		}

		public void OnAfterDeserialize()
		{
			type = !string.IsNullOrEmpty(_assemblyQualifiedTypeName) ? Type.GetType(_assemblyQualifiedTypeName) : null;
		}

		public override string ToString()
		{
			return type.ToString();
		}
	}
}
