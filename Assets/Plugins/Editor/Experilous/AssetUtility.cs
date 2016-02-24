﻿/******************************************************************************\
 *  Copyright (C) 2016 Experilous <againey@experilous.com>
 *  
 *  This file is subject to the terms and conditions defined in the file
 *  'Assets/Plugins/Experilous/License.txt', which is a part of this package.
 *
\******************************************************************************/

using UnityEngine;
using UnityEditor;
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

		public static string GetDataRelativeAssetFolder(Object asset)
		{
			return TrimDataPath(GetFullCanonicalAssetFolder(asset));
		}

		public static string GetProjectRelativeAssetFolder(Object asset)
		{
			return TrimProjectPath(GetFullCanonicalAssetFolder(asset));
		}

		public static string GetFullCanonicalAssetFolder(Object asset)
		{
			if (!AssetDatabase.Contains(asset))
			{
				throw new System.InvalidOperationException();
			}

			return GetCanonicalPath(Path.GetDirectoryName(Path.Combine(canonicalProjectPath, AssetDatabase.GetAssetPath(asset))));
		}

		public static string GetCanonicalPath(string path)
		{
			// Some Unity APIs require Unix-style path separators, so use them in the canonical representation of paths.
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

		public static bool RecursivelyDeleteEmptyFolders(string path, bool deleteRootFolderIfEmpty = true)
		{
			if (!Directory.Exists(path))
			{
				return true;
			}

			var isEmpty = true;

			foreach (var folderPath in Directory.GetDirectories(Path.Combine(canonicalProjectPath, path)))
			{
				if (!RecursivelyDeleteEmptyFolders(TrimProjectPath(folderPath), true))
				{
					isEmpty = false;
				}
			}

			if (isEmpty && Directory.GetFiles(path).Length > 0)
			{
				isEmpty = false;
			}

			if (isEmpty && deleteRootFolderIfEmpty)
			{
				FileUtil.DeleteFileOrDirectory(path);
				var folderMetadataPath = path + ".meta";
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
