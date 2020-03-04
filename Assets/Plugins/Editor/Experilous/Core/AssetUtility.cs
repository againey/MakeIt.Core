/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEditor;
using System.IO;

namespace Experilous.Core
{
	/// <summary>
	/// A class of static functions for working with Unity assets.
	/// </summary>
	public static class AssetUtility
	{
		private static string _canonicalDataPath;
		private static string _canonicalProjectPath;
		private static string _projectRelativeDataPath;

		/// <summary>
		/// The standard representation of the full path to the current project's data folder (typically &lt;project-path&gt;/Assets).
		/// </summary>
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

		/// <summary>
		/// The standard representation of the full path to the current project's root folder.
		/// </summary>
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

		/// <summary>
		/// The standard representation of the relative path to the projects data folder, starting from the project folder (typically Assets).
		/// </summary>
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

		/// <summary>
		/// The project relative path to the select asset(s), or the data path if no asset is selected.
		/// </summary>
		/// <remarks><para>If multiple assets are selected and are in different folders, then the path
		/// to the deepest folder shared by all of them will be returned.</para></remarks>
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

		/// <summary>
		/// Gets the portion of the two paths that are common between them.
		/// </summary>
		/// <param name="firstPath">The first path.</param>
		/// <param name="secondPath">The second path.</param>
		/// <returns>The longest portion at the beginning of both paths that are shared between them.</returns>
		public static string GetSharedPath(string firstPath, string secondPath)
		{
			if (firstPath == null || secondPath == null) return null;

			// Guarantee that the first path is not longer than the second, for convenience below.
			if (firstPath.Length > secondPath.Length) GeneralUtility.Swap(ref firstPath, ref secondPath);

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

		/// <summary>
		/// Removes the data path portion from the front of a full path, turning it into a path relative to the data folder.
		/// </summary>
		/// <param name="fullPath">The full path to a file or folder in the data folder.</param>
		/// <returns>The same path, but relative to the data folder.</returns>
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

		/// <summary>
		/// Removes the project path portion from the front of a full path, turning it into a path relative to the project's root folder.
		/// </summary>
		/// <param name="fullPath">The full path to a file or folder in the project's root folder.</param>
		/// <returns>The same path, but relative to the project's root folder.</returns>
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

		/// <summary>
		/// Gets the path to the specified asset, relative to the data folder.
		/// </summary>
		/// <param name="asset">The asset whose path is to be returned.</param>
		/// <returns>The path to the specified asset, relative to the data folder.</returns>
		public static string GetDataRelativeAssetPath(Object asset)
		{
			return TrimDataPath(GetFullCanonicalAssetPath(asset));
		}

		/// <summary>
		/// Gets the path to the specified asset, relative to the project's root folder.
		/// </summary>
		/// <param name="asset">The asset whose path is to be returned.</param>
		/// <returns>The path to the specified asset, relative to the project's root folder.</returns>
		public static string GetProjectRelativeAssetPath(Object asset)
		{
			return TrimProjectPath(GetFullCanonicalAssetPath(asset));
		}

		/// <summary>
		/// Gets the standard representation of the full path to the specified asset.
		/// </summary>
		/// <param name="asset">The asset whose path is to be returned.</param>
		/// <returns>The full path to the specified asset.</returns>
		public static string GetFullCanonicalAssetPath(Object asset)
		{
			if (!AssetDatabase.Contains(asset))
			{
				throw new System.InvalidOperationException();
			}

			return GetCanonicalPath(Path.Combine(canonicalProjectPath, AssetDatabase.GetAssetPath(asset)));
		}

		/// <summary>
		/// Gets the path of the folder containing the specified asset, relative to the data folder.
		/// </summary>
		/// <param name="asset">The asset whose folder path is to be returned.</param>
		/// <returns>The path to the folder containing the specified asset, relative to the data folder.</returns>
		public static string GetDataRelativeAssetFolder(Object asset)
		{
			return TrimDataPath(GetFullCanonicalAssetFolder(asset));
		}

		/// <summary>
		/// Gets the path of the folder containing the specified asset, relative to the project's root folder.
		/// </summary>
		/// <param name="asset">The asset whose folder path is to be returned.</param>
		/// <returns>The path to the folder containing the specified asset, relative to the project's root folder.</returns>
		public static string GetProjectRelativeAssetFolder(Object asset)
		{
			return TrimProjectPath(GetFullCanonicalAssetFolder(asset));
		}

		/// <summary>
		/// Gets the standard representation of the full path of the folder containing the specified asset.
		/// </summary>
		/// <param name="asset">The asset whose folder path is to be returned.</param>
		/// <returns>The full path of the folder containing the specified asset.</returns>
		public static string GetFullCanonicalAssetFolder(Object asset)
		{
			if (!AssetDatabase.Contains(asset))
			{
				throw new System.InvalidOperationException();
			}

			return GetCanonicalPath(Path.GetDirectoryName(Path.Combine(canonicalProjectPath, AssetDatabase.GetAssetPath(asset))));
		}

		/// <summary>
		/// Converts a path to a standardized representation, to make comparisons and other operations easier and more consistent.
		/// </summary>
		/// <param name="path">The path to be standardized.</param>
		/// <returns>The standardized path.</returns>
		/// <remarks><para>Standardization of a path involves converting all back slashes to forward slashes,
		/// and the removal of all slashes from the very beginning and end of the path.</para></remarks>
		public static string GetCanonicalPath(string path)
		{
			// Some Unity APIs require Unix-style path separators, so use them in the canonical representation of paths.
			return path
				.Replace('\\', '/')
				.Trim('\\', '/');
		}

		/// <summary>
		/// Gets the the full path to the script file for the given <see cref="MonoBehaviour"/>.
		/// </summary>
		/// <param name="monoBehaviour">The <see cref="MonoBehaviour"/> whose path is to be returned.</param>
		/// <returns>The full path to the script file.</returns>
		public static string GetFullScriptFolder(MonoBehaviour monoBehaviour)
		{
			var scriptAsset = MonoScript.FromMonoBehaviour(monoBehaviour);
			return GetCanonicalPath(Path.GetDirectoryName(Path.Combine(canonicalProjectPath, AssetDatabase.GetAssetPath(scriptAsset))));
		}

		/// <summary>
		/// Gets the the full path to the script file for the given <see cref="ScriptableObject"/>.
		/// </summary>
		/// <param name="scriptableObject">The <see cref="ScriptableObject"/> whose path is to be returned.</param>
		/// <returns>The full path to the script file.</returns>
		public static string GetFullScriptFolder(ScriptableObject scriptableObject)
		{
			var scriptAsset = MonoScript.FromScriptableObject(scriptableObject);
			return GetCanonicalPath(Path.GetDirectoryName(Path.Combine(canonicalProjectPath, AssetDatabase.GetAssetPath(scriptAsset))));
		}

		/// <summary>
		/// Moves an asset file from its current path to a new path, either moving folders or renaming the file, or both.
		/// </summary>
		/// <param name="asset">The asset to be moved or renamed.</param>
		/// <param name="path">The new path to the asset.</param>
		/// <param name="selectOnChange">Indicates whether to select the asset file in the editor project window after the change.</param>
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

		/// <summary>
		/// Deletes the asset, and any other associated assets that share the same path.
		/// </summary>
		/// <param name="asset">The asset to delete.</param>
		public static void DeleteAsset(Object asset)
		{
			if (AssetDatabase.Contains(asset))
			{
				var assetPath = AssetDatabase.GetAssetPath(asset);
				if (asset is GameObject)
				{
					AssetDatabase.DeleteAsset(assetPath);
				}
				else
				{
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
		}

		/// <summary>
		/// Select the specified asset in the editor project window.
		/// </summary>
		/// <param name="asset">The asset to be selected.</param>
		public static void SelectAsset(Object asset)
		{
			Selection.activeObject = asset;
			EditorApplication.delayCall += () => { EditorUtility.FocusProjectWindow(); };
		}

		/// <summary>
		/// Copy new data to an existing asset without breaking any references to that asset.
		/// </summary>
		/// <param name="newAsset">The asset containing the new data to be copied into an existing asset.</param>
		/// <param name="existingAsset">The existing asset to receive the new data.</param>
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

		/// <summary>
		/// Generates an unused path based on the specified path, using the format string and an incrementing index to find the first path that is available.
		/// </summary>
		/// <param name="path">The base path.</param>
		/// <param name="format">The format string containing four format items:  folder, file, index, and extension.</param>
		/// <param name="startingIndex">The initial integer value to use when searching for an available path.</param>
		/// <returns>A full path which is not yet used by any file or folder, based on the specified path.</returns>
		public static string GenerateAvailableAssetPath(string path, string format = "{0}/{1} ({2}){3}", int startingIndex = 1)
		{
			var absolutePath = GetCanonicalPath(Path.Combine(canonicalProjectPath, path));

			if (!File.Exists(absolutePath) && !Directory.Exists(absolutePath))
			{
				return path;
			}

			var folderPath = Path.GetDirectoryName(absolutePath);
			var fileName = Path.GetFileNameWithoutExtension(absolutePath);
			var extension = Path.GetExtension(absolutePath);

			int index = startingIndex;
			do
			{
				var formattedPath = string.Format(format, folderPath, fileName, index, extension);
				if (!File.Exists(formattedPath) && !Directory.Exists(formattedPath))
				{
					return TrimProjectPath(		formattedPath);
				}
			} while (index++ < int.MaxValue);

			throw new System.InvalidOperationException(string.Format("No path could be found with the requested format that is not already in use.  Original:  \"{0}\"", path));
		}

		/// <summary>
		/// Ensures that all folders in the specified path exist, creating any at the end that are missing.
		/// </summary>
		/// <param name="path">The path, relative to the project's root folder, for which all folders should exist.</param>
		/// <param name="pathIncludesFileName">If true, indicates that the last segment of the path is a filename.  If false, all segments including the last are treated as folders and will be created if necessary.</param>
		public static void CreatePathFolders(string path, bool pathIncludesFileName = true)
		{
			var folderNames = GetCanonicalPath(pathIncludesFileName ? Path.GetDirectoryName(path) : path).Split('/');
			var currentPath = canonicalProjectPath;
			foreach (var folderName in folderNames)
			{
				var extendedPath = GetCanonicalPath(Path.Combine(currentPath, folderName));
				if (!Directory.Exists(extendedPath))
				{
					AssetDatabase.CreateFolder(TrimProjectPath(currentPath), folderName);
				}
				currentPath = extendedPath;
			}
		}

		/// <summary>
		/// Deletes all empty folders nested underneath the specified folder, along with their associated .meta files.
		/// </summary>
		/// <param name="path">The path, relative to the project's root folder, to the folder underneath which all empty folders should be deleted.</param>
		/// <param name="deleteRootFolderIfEmpty">If true and the specified folder is empty, that folder itself will also be deleted.</param>
		/// <returns>True if the specified folder does not exist after this call is complete.</returns>
		public static bool RecursivelyDeleteEmptyFolders(string path, bool deleteRootFolderIfEmpty = true)
		{
			var fullPath = Path.Combine(canonicalProjectPath, GetCanonicalPath(path));

			if (!Directory.Exists(fullPath))
			{
				return true;
			}

			var isEmpty = true;

			foreach (var folderPath in Directory.GetDirectories(fullPath))
			{
				if (!RecursivelyDeleteEmptyFolders(TrimProjectPath(folderPath), true))
				{
					isEmpty = false;
				}
			}

			if (isEmpty && Directory.GetFiles(fullPath).Length > 0)
			{
				isEmpty = false;
			}

			if (isEmpty && deleteRootFolderIfEmpty)
			{
				FileUtil.DeleteFileOrDirectory(fullPath);
				var folderMetadataPath = fullPath + ".meta";
				if (File.Exists(folderMetadataPath))
				{
					FileUtil.DeleteFileOrDirectory(folderMetadataPath);
				}
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
