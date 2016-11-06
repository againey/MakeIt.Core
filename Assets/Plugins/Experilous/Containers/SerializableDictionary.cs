/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Experilous.Containers
{
	/// <summary>
	/// A dictionary class that can be serialized by Unity.
	/// </summary>
	/// <typeparam name="TKey">The key type by which elements are organized and retrieved.</typeparam>
	/// <typeparam name="TValue">The type of the values that are associated with each key.</typeparam>
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[NonSerialized] private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();
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

		public ICollection<TKey> Keys { get { return ((IDictionary<TKey, TValue>)_dictionary).Keys; } }
		public ICollection<TValue> Values { get { return ((IDictionary<TKey, TValue>)_dictionary).Values; } }
		public int Count { get { return ((IDictionary<TKey, TValue>)_dictionary).Count; } }
		public bool IsReadOnly { get { return ((IDictionary<TKey, TValue>)_dictionary).IsReadOnly; } }

		public TValue this[TKey key]
		{
			get { return ((IDictionary<TKey, TValue>)_dictionary)[key]; }
			set { ((IDictionary<TKey, TValue>)_dictionary)[key] = value; }
		}

		public void Add(TKey key, TValue value)
		{
			((IDictionary<TKey, TValue>)_dictionary).Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).ContainsKey(key);
		}

		public bool Remove(TKey key)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).TryGetValue(key, out value);
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			((IDictionary<TKey, TValue>)_dictionary).Add(item);
		}

		public void Clear()
		{
			((IDictionary<TKey, TValue>)_dictionary).Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((IDictionary<TKey, TValue>)_dictionary).CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).Remove(item);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return ((IDictionary<TKey, TValue>)_dictionary).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IDictionary<TKey, TValue>)_dictionary).GetEnumerator();
		}
	}
}
