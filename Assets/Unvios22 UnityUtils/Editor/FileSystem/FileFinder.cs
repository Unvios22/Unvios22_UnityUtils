using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Unvios22_UnityUtils.Editor.FileSystem {
	public class FileFinder {
		public static T FindUniqueAssetByFileName<T>(string assetFileName) where T : class {
			
			//using System.IO instead of AssetDatabase because the database won't find files with exact extension given.
			var foundFiles = Directory.GetFiles(Application.dataPath + @"\", $"{assetFileName}",
				SearchOption.AllDirectories);
			
			if (foundFiles.Length != 1) {
				throw new ArgumentException($"Found:{foundFiles.Length} '{assetFileName}' files!");
			}
			var foundAssetPath = foundFiles[0];
			var foundAssetPathRelativeToProjectFolder = foundAssetPath.Replace(Application.dataPath,
				@"Assets\");
			var foundAsset = AssetDatabase.LoadAssetAtPath(foundAssetPathRelativeToProjectFolder, typeof(T)) as T;
			return foundAsset;
		}
	}
}