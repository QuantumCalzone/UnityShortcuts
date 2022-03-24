#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public static class ConvertSelectedImageAssetsRedMenuItem
{
    private const string itemName = "Tools/Convert Image Asset Red";
    private const int priority = 9999;

    [MenuItem(itemName: itemName, isValidateFunction: false, priority: priority)]
    private static void ConvertSelectedImageAssetsRed()
    {
        var selectedObjects = Selection.objects;

        for (var i = 0; i < selectedObjects.Length; i++)
        {
            var selectedObject = selectedObjects[i];
            var selectedObjectPath = AssetDatabase.GetAssetPath(selectedObject);

            if (AssetDatabase.IsMainAsset(selectedObject))
            {
                if (selectedObjectPath.EndsWith(".png"))
                {
                    ConvertImageAssetRed(selectedObjectPath);
                }
                else if (AssetDatabase.IsValidFolder(selectedObjectPath))
                {
                    foreach (string filePath in Directory.EnumerateFiles(selectedObjectPath, "*.png", SearchOption.AllDirectories))
                    {
                        ConvertImageAssetRed(filePath);
                    }
                }
            }
        }

        AssetDatabase.Refresh();
    }

    private static void ConvertImageAssetRed(string atPath)
    {
        var targetTexture2D = AssetDatabase.LoadAssetAtPath<Texture2D>(atPath);

        var newTexture2D = new Texture2D(targetTexture2D.width, targetTexture2D.height, TextureFormat.RGB24, false);

        var newTexture2DPixels = newTexture2D.GetPixels();
        for (var i = 0; i < newTexture2DPixels.Length; ++i)
        {
            newTexture2DPixels[i] = Color.red;
        }
        newTexture2D.SetPixels(newTexture2DPixels);
        newTexture2D.Apply();

        var newTexture2DBytes = newTexture2D.EncodeToPNG();
        Object.DestroyImmediate(newTexture2D);

        File.WriteAllBytes(atPath, newTexture2DBytes);
    }
}
#endif
