using UnityEngine;
using UnityEditor;
using System.IO;

public class SavePrefabFileNames : MonoBehaviour
{
    [MenuItem("Custom/Save Prefab File Names")]
    private static void SaveFileNames()
    {
        string folderPath = "Assets/Prefabs/Model"; // 替换为您想要保存文件名的文件夹路径
        string writeText = "";
        string filePath = folderPath + "/name.txt";
        //string filePath = Path.Combine(folderPath, fileName + ".txt");

        string[] prefabPaths = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);

        foreach (string prefabPath in prefabPaths)
        {
            // 获取文件名（不包含扩展名）
            writeText += Path.GetFileNameWithoutExtension(prefabPath);
            writeText += ",\n";

            // 创建.txt文件
            
            File.WriteAllText(filePath, writeText);
        }

        Debug.Log("Prefab文件名已保存在"+filePath);
    }
}
