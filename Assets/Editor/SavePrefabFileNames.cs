using UnityEngine;
using UnityEditor;
using System.IO;

public class SavePrefabFileNames : EditorWindow
{

    private string folderPath; // 替换为您想要保存文件名的文件夹路径
    //[MenuItem("Custom Tools/Save Prefab File Names")]
    [MenuItem("Custom Tools/Save Prefab File Names")]
    

    private void OnGUI()
    {
        GUILayout.Label("Save File Names to TXT", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        folderPath = EditorGUILayout.TextField("Folder Path:", folderPath);

        EditorGUILayout.Space();

        if (GUILayout.Button("Save"))
        {
            SaveFileNames();
        }
    }

    private void SaveFileNames()
    {
        

        string[] prefabPaths = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);

        foreach (string prefabPath in prefabPaths)
        {
            // 获取文件名（不包含扩展名）
            string fileName = Path.GetFileNameWithoutExtension(prefabPath);

            // 创建.txt文件
            string filePath = Path.Combine(Application.dataPath, fileName + ".txt");
            File.WriteAllText(filePath, fileName);
        }

        Debug.Log("Prefab文件名已保存为.txt文件");
    }
}
