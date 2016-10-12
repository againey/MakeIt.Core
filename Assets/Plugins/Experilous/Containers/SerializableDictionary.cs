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

		/// <summary>
		/// Called before Unity serializes the data within the dictionary.
		/// </summary>
		/// <remarks><para>This function copies the dictionary data into lists that Unity knows how to serialize.</para></remarks>
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

		/// <summary>
		/// Called after Unity serializes the data within the dictionary.
		/// </summary>
		/// <remarks><para>This function rebuilds the dictionary from the lists that Unity knows how to deserialize.</para></remarks>
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

		/// <summary>
		/// A collection of just the keys in this dictionary.
		/// </summary>
		public ICollection<TKey> Keys { get { return ((IDictionary<TKey, TValue>)_dictionary).Keys; } }

		/// <summary>
		/// A collection of just the values in the dictionary.
		/// </summary>
		public ICollection<TValue> Values { get { return ((IDictionary<TKey, TValue>)_dictionary).Values; } }

		/// <summary>
		/// The number of key-value pairs in this dictionary.
		/// </summary>
		public int Count { get { return ((IDictionary<TKey, TValue>)_dictionary).Count; } }

		/// <summary>
		/// Whether this dictionary is read-only and thus cannot have items interted into or removed from it.
		/// </summary>
		public bool IsReadOnly { get { return ((IDictionary<TKey, TValue>)_dictionary).IsReadOnly; } }

		/// <summary>
		/// Accesses a value indexed by the specified key.
		/// </summary>
		/// <param name="key">The key which maps to a value.</param>
		/// <returns>The value to which the provided kep maps.</returns>
		public TValue this[TKey key]
		{
			get { return ((IDictionary<TKey, TValue>)_dictionary)[key]; }
			set { ((IDictionary<TKey, TValue>)_dictionary)[key] = value; }
		}

		/// <summary>
		/// Inserts the given value into the dictionary, mapped by the given key.
		/// </summary>
		/// <param name="key">The key which maps to the value.</param>
		/// <param name="value">The value that is mapped by the key.</param>
		public void Add(TKey key, TValue value)
		{
			((IDictionary<TKey, TValue>)_dictionary).Add(key, value);
		}

		/// <summary>
		/// Checks if there is a value in the dictionary that is mapped by the given key.
		/// </summary>
		/// <param name="key">The key which might map to a value in the dictionary.</param>
		/// <returns>True if a value exists in the dictionary mapped by the given key, and false otherwise.</returns>
		public bool ContainsKey(TKey key)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).ContainsKey(key);
		}

		/// <summary>
		/// Removes a value from the dictionary that is mapped by the given key, if such an item exists.
		/// </summary>
		/// <param name="key">The key which might map to a value in the dictionary.</param>
		/// <returns>True if a value mapped by the key was found and removed, and false if no such value was found.</returns>
		public bool Remove(TKey key)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).Remove(key);
		}

		/// <summary>
		/// Attempts to get the value mapped by the given key, if such a value is in the dictionary.
		/// </summary>
		/// <param name="key">The key which might map to a value in the dictionary.</param>
		/// <param name="value">The value which is mapped by the given key.</param>
		/// <returns>True if the key does map to a value in the dictionary, and false if it does not.</returns>
		public bool TryGetValue(TKey key, out TValue value)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).TryGetValue(key, out value);
		}

		/// <summary>
		/// Inserts the given value into the dictionary, mapped by the given key.
		/// </summary>
		/// <param name="item">The key-value pair to be inserted into the dictionary.</param>
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			((IDictionary<TKey, TValue>)_dictionary).Add(item);
		}

		/// <summary>
		/// Removes all key-value pairs from the dictionary.
		/// </summary>
		public void Clear()
		{
			((IDictionary<TKey, TValue>)_dictionary).Clear();
		}

		/// <summary>
		/// Checks if there is a key-value pair that matches the given pair.
		/// </summary>
		/// <param name="item">The key-value pair to be looked for in the dictionary.</param>
		/// <returns>True if the given key-value pair exists in the dictionary, and false otherwise.</returns>
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).Contains(item);
		}

		/// <summary>
		/// Copies the contents of the dictionary to a key-value pair array.
		/// </summary>
		/// <param name="array">The target array to copy the dictionary contents into.</param>
		/// <param name="arrayIndex">The starting index of the array ti where the dictionary contents will be copied.</param>
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((IDictionary<TKey, TValue>)_dictionary).CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Removes a key-value pair from the dictionary that matches the given pair, if such a pair exists in the dictionary.
		/// </summary>
		/// <param name="item">The key-value pair to look for and remove from the dictionary.</param>
		/// <returns>True if a matching key-value pair was found and removed from the dictionary, and false if no match was found.</returns>
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>)_dictionary).Remove(item);
		}

		/// <summary>
		/// Returns an enumerator which will enumerate over all the key-value pairs in the dictionary in an unspecified order.
		/// </summary>
		/// <returns>An enumerator over all the key-value pairs in the dictionary.</returns>
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return ((IDictionary<TKey, TValue>)_dictionary).GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator which will enumerate over all the key-value pairs in the dictionary in an unspecified order.
		/// </summary>
		/// <returns>An enumerator over all the key-value pairs in the dictionary.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IDictionary<TKey, TValue>)_dictionary).GetEnumerator();
		}
	}
}
