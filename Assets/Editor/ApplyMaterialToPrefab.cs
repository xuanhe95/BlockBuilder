using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class ApplyMaterialToPrefab : EditorWindow
{
    private string _path = "";
    private Material _material = null;

    [MenuItem("Custom/Apply Material to Prefab")]
    public static void ShowWindow()
    {
        GetWindow<ApplyMaterialToPrefab>("Apply Material to Prefab");
    }

    void OnGUI()
    {
        GUILayout.Label("Set the path and material to apply", EditorStyles.boldLabel);

        _path = EditorGUILayout.TextField("Path", _path);
        _material = EditorGUILayout.ObjectField("Material", _material, typeof(Material), false) as Material;

        if (GUILayout.Button("Apply Material"))
        {
            ApplyMaterialToAllPrefabs();
        }
    }

    private void ApplyMaterialToAllPrefabs()
    {
        string[] filePaths = Directory.GetFiles(_path, "*.prefab", SearchOption.AllDirectories);
        Debug.Log(filePaths.Length);
        
        foreach (string filePath in filePaths)
        {
            string assetPath = filePath;
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

            if (prefab == null)
            {
                Debug.LogError($"Couldn't load prefab at path: {assetPath}");
                continue;
            }

            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            
            foreach (Renderer renderer in renderers)
            {
                Material[] mats = renderer.sharedMaterials;
                mats = mats.Select(mat => _material).ToArray();
                renderer.sharedMaterials = mats;
            }
            
            EditorUtility.SetDirty(prefab);
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}