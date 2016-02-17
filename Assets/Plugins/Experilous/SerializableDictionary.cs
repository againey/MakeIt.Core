using UnityEngine;
using System.Collections.Generic;
using System;

namespace Experilous
{
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField] private List<TKey> _keys = new List<TKey>();
		[SerializeField] private List<TValue> _values = new List<TValue>();

		public void OnBeforeSerialize()
		{
			_keys.Clear();
			_values.Clear();
			foreach (var keyValuePair in this)
			{
				if (keyValuePair.Key != null)
				{
					_keys.Add(keyValuePair.Key);
					_values.Add(keyValuePair.Value);
				}
			}
		}

		public void OnAfterDeserialize()
		{
			if ((_keys == null) != (_values == null) || _keys.Count != _values.Count)
			{
				throw new InvalidOperationException("Dictionary keys and values were in an invalid state immediately after deserialization.");
			}

			Clear();

			if (_keys != null && _keys.Count > 0)
			{
				for (int i = 0; i < _keys.Count; ++i)
				{
					if (_keys[i] != null)
					{
						Add(_keys[i], _values[i]);
					}
				}
			}
		}
	}
}
