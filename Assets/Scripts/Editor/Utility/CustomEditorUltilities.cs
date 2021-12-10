using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UnityEditor
{
    public partial class CustomEditorUltilities
    {
        public static ScriptableObject CreateScriptableObject<T>(string path)
        {
            if(!typeof(T).IsSubclassOf(typeof(ScriptableObject)))
                return null;
            var asset = ScriptableObject.CreateInstance(typeof(T));
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
            return asset;
        }
        public static bool DeleteScriptableObject(Object assetObject)
        {
            var assetPath = AssetDatabase.GetAssetPath(assetObject);
            bool isSuccess = AssetDatabase.DeleteAsset(assetPath);
            AssetDatabase.SaveAssets();
            return isSuccess;
        }
        public static string GenerateAssetPathFromAsset(Object assetObject, string postfix)
        {
            var parentFolder = GetFolderAssetPath(assetObject);
            if (parentFolder == null)
                return null;
            var assetPath = parentFolder + assetObject.GetInstanceID() + postfix;
            return assetPath;
        }
        public static string GenerateAssetPathFromAsset<T>(Object assetObject)
        {
            var parentFolder = GetFolderAssetPath(assetObject);
            if (parentFolder == null)
                return null;
            var assetPath = parentFolder + "/" + assetObject.name + "/" + typeof(T).ToString() + ".asset";
            return assetPath;
        }
        public static string GetFolderAssetPath(Object assetObject)
        {
            var assetPath = AssetDatabase.GetAssetPath(assetObject);
            if (string.IsNullOrEmpty(assetPath))
                return null;
            return assetPath.Remove(assetPath.LastIndexOf('/'));
        }
        public static bool CreateFolder(string parent, string path)
        {
            if (AssetDatabase.IsValidFolder(parent+"/"+path))
                return false;
            AssetDatabase.CreateFolder(parent, path);
            return true;
        }
    }
}
