using UnityEngine;
using UnityEditor;
using System.IO;

public class BatchFBXToPrefabConverter : EditorWindow
{
    private string folderPath; // 文件夹路径

    [MenuItem("Custom Tools/Batch FBX to Prefab Converter")]
    private static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BatchFBXToPrefabConverter));
    }

    private void OnGUI()
    {
        GUILayout.Label("Batch FBX to Prefab Converter", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        folderPath = EditorGUILayout.TextField("Folder Path:", folderPath);

        EditorGUILayout.Space();

        if (GUILayout.Button("Convert"))
        {
            ConvertFBXToPrefab();
        }
    }

    private void ConvertFBXToPrefab()
    {
        if (string.IsNullOrEmpty(folderPath))
        {
            Debug.LogError("Folder path is empty!");
            return;
        }

        string[] fbxFiles = Directory.GetFiles(folderPath, "*.fbx");

        foreach (string fbxFile in fbxFiles)
        {
            string prefabPath = fbxFile.Replace(".fbx", ".prefab");
            string renamePath = prefabPath.Replace("Dir", "");
            renamePath = renamePath.Replace("FBX/", "");
            //renamePath = renamePath.Replace("/T", "/P");
            //renamePath = renamePath.Replace("", "Assets/Dir");
            //prefabPath = prefabPath.Replace(folderPath, "Assets");
            
            GameObject fbxObject = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            Debug.Log(fbxObject);
            if (fbxObject == null)
            {
                var SceneObject = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(fbxFile)); 
                PrefabUtility.SaveAsPrefabAsset (SceneObject, renamePath); 
                DestroyImmediate (SceneObject);
                //GameObject prefab = PrefabUtility.SaveAsPrefabAsset(AssetDatabase.LoadAssetAtPath<GameObject>(fbxFile), prefabPath);
                Debug.Log("FBX file converted to prefab: " + prefabPath);
            }
            else
            {
                Debug.LogWarning("Prefab already exists for FBX file: " + prefabPath);
            }
        }

        AssetDatabase.Refresh();
    }
}

