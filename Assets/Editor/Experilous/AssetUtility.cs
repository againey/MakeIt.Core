using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace Experilous
{
	public static class AssetUtility
	{
		private static string _canonicalDataPath;
		private static string _canonicalProjectPath;
		private static string _projectRelativeDataPath;

		public static string canonicalDataPath
		{
			get
			{
				if (_canonicalDataPath == null)
				{
					_canonicalDataPath = GetCanonicalPath(Application.dataPath);
				}

				return _canonicalDataPath;
			}
		}

		public static string canonicalProjectPath
		{
			get
			{
				if (_canonicalProjectPath == null)
				{
					_canonicalProjectPath = GetCanonicalPath(Directory.GetCurrentDirectory());
				}

				return _canonicalProjectPath;
			}
		}

		public static string projectRelativeDataPath
		{
			get
			{
				if (_projectRelativeDataPath == null)
				{
					_projectRelativeDataPath = TrimProjectPath(canonicalDataPath);
				}

				return _projectRelativeDataPath;
			}
		}

		public static string selectedFolderOrDefault
		{
			get
			{
				if (Selection.objects != null && Selection.objects.Length > 0)
				{
					string selectedPath = null;
					foreach (var selectedObject in Selection.objects)
					{
						if (AssetDatabase.Contains(selectedObject))
						{
							var assetPath = GetProjectRelativeAssetPath(selectedObject);
							if (selectedObject is DefaultAsset && string.IsNullOrEmpty(Path.GetExtension(assetPath)))
							{
								if (selectedPath == null)
								{
									selectedPath = assetPath;
								}
								else
								{
									selectedPath = GetSharedPath(selectedPath, assetPath);
								}
							}
							else
							{
								if (selectedPath == null)
								{
									selectedPath = Path.GetDirectoryName(assetPath);
								}
								else
								{
									selectedPath = GetSharedPath(selectedPath, Path.GetDirectoryName(assetPath));
								}
							}
						}
					}

					return selectedPath != null ? selectedPath : projectRelativeDataPath;
				}
				else
				{
					return projectRelativeDataPath;
				}
			}
		}

		public static string GetSharedPath(string firstPath, string secondPath)
		{
			if (firstPath == null || secondPath == null) return null;

			// Guarantee that the first path is not longer than the second, for convenience below.
			if (firstPath.Length > secondPath.Length) Utility.Swap(ref firstPath, ref secondPath);

			var lastConfirmedIndex = 0;
			for (int i = 0; i < firstPath.Length; ++i)
			{
				if (firstPath[i] != secondPath[i])
				{
					return GetCanonicalPath(firstPath.Substring(0, lastConfirmedIndex));
				}
				else if (firstPath[i] == '\\' || firstPath[i] == '/')
				{
					lastConfirmedIndex = i;
				}
			}

			return GetCanonicalPath(firstPath);
		}

		public static string TrimDataPath(string fullPath)
		{
			fullPath = GetCanonicalPath(fullPath);
			var dataPath = canonicalDataPath;
			if (fullPath == dataPath) return "";
			if (fullPath.Length <= dataPath.Length || !fullPath.StartsWith(dataPath + '/'))
			{
				throw new System.InvalidOperationException(string.Format("The full path \"{0}\" does not start with the data path \"{1}\".", fullPath, dataPath));
			}
			return fullPath.Substring(dataPath.Length + 1);
		}

		public static string TrimProjectPath(string fullPath)
		{
			fullPath = GetCanonicalPath(fullPath);
			var projectPath = canonicalProjectPath;
			if (fullPath == projectPath) return "";
			if (fullPath.Length <= projectPath.Length || !fullPath.StartsWith(projectPath + '/'))
			{
				throw new System.InvalidOperationException(string.Format("The full path \"{0}\" does not start with the project path \"{1}\".", fullPath, projectPath));
			}
			return fullPath.Substring(projectPath.Length + 1);
		}

		public static string GetDataRelativeAssetPath(Object asset)
		{
			return TrimDataPath(GetFullCanonicalAssetPath(asset));
		}

		public static string GetProjectRelativeAssetPath(Object asset)
		{
			return TrimProjectPath(GetFullCanonicalAssetPath(asset));
		}

		public static string GetFullCanonicalAssetPath(Object asset)
		{
			if (!AssetDatabase.Contains(asset))
			{
				throw new System.InvalidOperationException();
			}

			return GetCanonicalPath(Path.Combine(canonicalProjectPath, AssetDatabase.GetAssetPath(asset)));
		}

		public static string GetCanonicalPath(string path)
		{
			return path
				.Replace('\\', '/')
				.Trim('\\', '/');
		}

		public static void MoveOrRenameAsset(Object asset, string path, bool selectOnChange)
		{
			var currentAssetPath = GetProjectRelativeAssetPath(asset);

			if (currentAssetPath != path)
			{
				var errorMessage = AssetDatabase.MoveAsset(currentAssetPath, path);
				if (!string.IsNullOrEmpty(errorMessage))
				{
					throw new System.InvalidOperationException(errorMessage);
				}
				else if (selectOnChange)
				{
					SelectAsset(asset);
				}
			}
		}

		public static void DeleteAsset(Object asset)
		{
			if (AssetDatabase.Contains(asset))
			{
				var assetPath = AssetDatabase.GetAssetPath(asset);
				var assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
				if (assets.Length == 1)
				{
					AssetDatabase.DeleteAsset(assetPath);
				}
				else if (assets.Length > 1)
				{
					Object.DestroyImmediate(asset, true);
				}
			}
		}

		public static void SelectAsset(Object asset)
		{
			Selection.activeObject = asset;
			EditorApplication.delayCall += () => { EditorUtility.FocusProjectWindow(); };
		}

		public static void UpdateAssetInPlace(Object newAsset, Object existingAsset)
		{
			if (newAsset is Mesh)
			{
				var existingMesh = (Mesh)existingAsset;
				existingMesh.Clear();
				CombineInstance[] combine = new CombineInstance[1];
				combine[0].mesh = (Mesh)newAsset;
				combine[0].transform = Matrix4x4.identity;
				existingMesh.CombineMeshes(combine);
			}
			else
			{
				EditorUtility.CopySerialized(newAsset, existingAsset);
			}
		}

		public static void CreateCompositeAsset(IEnumerable<Object> assets, string path)
		{
			if (assets == null) throw new System.ArgumentNullException("assets");

			var assetEnumerator = assets.GetEnumerator();
			if (!assetEnumerator.MoveNext()) throw new System.ArgumentException("The assets collection cannot be empty.", "assets");

			var root = assetEnumerator.Current;
			var rootName = root.name;
			AssetDatabase.CreateAsset(assetEnumerator.Current, path);

			while (assetEnumerator.MoveNext())
			{
				AssetDatabase.AddObjectToAsset(assetEnumerator.Current, root);
			}

			root.name = rootName;
			EditorUtility.SetDirty(root);
			AssetDatabase.SaveAssets();
		}

		public static void CreateCompositeAsset(IEnumerable<TypedAsset> assets, string path)
		{
			if (assets == null) throw new System.ArgumentNullException("assets");

			var assetEnumerator = assets.GetEnumerator();
			if (!assetEnumerator.MoveNext()) throw new System.ArgumentException("The assets collection cannot be empty.", "assets");

			var root = assetEnumerator.Current.asset;
			var rootName = root.name;
			AssetDatabase.CreateAsset(assetEnumerator.Current.asset, path);

			while (assetEnumerator.MoveNext())
			{
				assetEnumerator.Current.asset.hideFlags = HideFlags.HideInHierarchy;
				AssetDatabase.AddObjectToAsset(assetEnumerator.Current.asset, root);
			}

			root.name = rootName;
			EditorUtility.SetDirty(root);
			AssetDatabase.SaveAssets();
		}

		public static void UpdateCompositeAsset(ICollection<TypedAsset> assets, string path)
		{
			if (assets == null) throw new System.ArgumentNullException("assets");

			var existingAssets = AssetDatabase.LoadAllAssetsAtPath(path);
			if (existingAssets == null || existingAssets.Length == 0) throw new System.InvalidOperationException(string.Format("No existing assets found at \"{0}\".", path));

			UpdateCompositeAsset(assets, path, existingAssets);
		}

		private static void UpdateCompositeAsset(ICollection<TypedAsset> assets, string path, Object[] existingAssets)
		{
			if (assets == null) throw new System.ArgumentNullException("assets");

			Object root = null;
			string rootName = null;

			List<Object> persistedAssets = new List<Object>();

			foreach (var existingAsset in existingAssets)
			{
				if (existingAsset != null)
				{
					var matchFound = false;
					foreach (var typedAsset in assets)
					{
						if (typedAsset.asset.name == existingAsset.name && existingAsset.GetType().IsInstanceOfType(typedAsset.type))
						{
							matchFound = true;
							if (root == null) root = existingAsset;
							EditorUtility.CopySerialized(typedAsset.asset, existingAsset);
							EditorUtility.SetDirty(existingAsset);
							persistedAssets.Add(typedAsset.asset);
							break;
						}
					}

					if (!matchFound)
					{
						Object.DestroyImmediate(existingAsset, true);
					}
				}
			}

			foreach (var typedAsset in assets)
			{
				if (!persistedAssets.Contains(typedAsset.asset))
				{
					if (root == null)
					{
						root = typedAsset.asset;
						rootName = root.name;
						AssetDatabase.CreateAsset(root, path);
					}
					else
					{
						typedAsset.asset.hideFlags = HideFlags.HideInHierarchy;
						AssetDatabase.AddObjectToAsset(typedAsset.asset, root);
						EditorUtility.SetDirty(root);
					}
				}
			}

			if (rootName != null)
			{
				root.name = rootName;
				EditorUtility.SetDirty(root);
			}

			AssetDatabase.SaveAssets();
		}

		public static void UpdateOrCreateCompositeAsset(ICollection<TypedAsset> assets, string path)
		{
			var existingAssets = AssetDatabase.LoadAllAssetsAtPath(path);
			if (existingAssets == null || existingAssets.Length == 0)
			{
				CreateCompositeAsset(assets, path);
			}
			else
			{
				UpdateCompositeAsset(assets, path, existingAssets);
			}
		}
	}
}
