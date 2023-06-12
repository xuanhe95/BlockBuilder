using UnityEngine;
using UnityEditor;
using System.IO;

public class AddBuildingsToWater : MonoBehaviour
{
    public static string addPrefabPath = "Assets/Prefabs/Model/Water.prefab";
    public static string folderPath = "Assets/Prefabs/Model/Roof"; // 文件夹路径

    [MenuItem("Custom/Add Buildings to Water")]
    private static void GetScriptsFromPrefabs()
    {
        Debug.Log("Running");
        string[] prefabPaths = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);

        // 加载要添加的 Prefab
        GameObject addPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(addPrefabPath);
        Debug.Log(addPrefab.name);
        // 遍历每个 Prefab
        foreach (string prefabPath in prefabPaths)
        {
            // 加载 Prefab
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab != null)
            {
                // 获取 Prefab 上的所有脚本组件
                MonoBehaviour[] scripts = prefab.GetComponents<RuleCreator>();

                // 输出脚本名称
                foreach (MonoBehaviour script in scripts)
                {
                    addPrefab.GetComponent<RuleCreator>().Up.Add(prefab);
                    Debug.Log("Prefab: " + prefab.name + ", Script: " + script.GetType().Name);
                }
            }
        }
    }
}
